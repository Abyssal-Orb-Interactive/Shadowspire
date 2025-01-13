using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameplayConstructorFramework.APIs
{
    public static class KeyAPIGenerator
    {
        private const string C_SHARP_FILE_EXTENSION = ".cs";
        
        public static bool TryGenerateAPI(string apiName, IEnumerable<string> keys, IAPIGeneratorConfig config)
        {
            var keysArray = ToArray(keys);
            
            if (ArgumentsInvalid(apiName, config, keysArray)) return false;

            GenerateAPI(apiName, keysArray, config.APIsFolderPath, config.APIsNamespace);
            
            return true;
        }

        private static string[] ToArray(IEnumerable<string> keys)
        {
            if (keys == null) return null;
            
            return keys as string[] ?? keys.ToArray();
        }

        private static bool ArgumentsInvalid(string apiName, IAPIGeneratorConfig config, IEnumerable<string> keysArray)
        {
            return APINameInvalid(apiName) || KeysInvalid(keysArray) || ConfigInvalid(config);
        }

        private static bool APINameInvalid(string apiName)
        {
            return string.IsNullOrWhiteSpace(apiName);
        }

        private static bool KeysInvalid(IEnumerable<string> keys)
        {
            return keys == null;
        }
        
        private static bool ConfigInvalid(IAPIGeneratorConfig config)
        {
            return config == null || string.IsNullOrWhiteSpace(config.APIsFolderPath) || string.IsNullOrWhiteSpace(config.APIsNamespace);
        }

        private static void GenerateAPI(string apiName, IEnumerable<string> keys, string outputFolderPath, string apiNamespace)
        {
            var apiContentBuilder = new StringBuilder();
            apiContentBuilder.AppendLine($"namespace {apiNamespace}");
            apiContentBuilder.AppendLine("{");
            apiContentBuilder.AppendLine($"    public enum {apiName}");
            apiContentBuilder.AppendLine("     {");

            var keyIndex = 0;
            foreach (var key in keys.Distinct())
            {
                apiContentBuilder.AppendLine($"        {key} = {keyIndex},");
                keyIndex++;
            }

            apiContentBuilder.AppendLine("     }");
            apiContentBuilder.AppendLine("}");

            var apiContent = apiContentBuilder.ToString();
            var filePat = Path.Combine(outputFolderPath, $"{apiName}{C_SHARP_FILE_EXTENSION}");
            File.WriteAllText(filePat, apiContent);
        }

        public static bool TryUpdateAPIWith(string apiName, IEnumerable<string> keys, IAPIGeneratorConfig config)
        {
            var keysArray = ToArray(keys);
            
            if (ArgumentsInvalid(apiName, config, keysArray)) return false;

            var filePath = Path.Combine(config.APIsFolderPath, $"{apiName}{C_SHARP_FILE_EXTENSION}");

            if (!File.Exists(filePath))
            {
                return TryGenerateAPI(apiName, keysArray, config);
            }
            UpdateAPIWith(apiName, keysArray, config.APIsFolderPath);

            return true;
        }

        private static void UpdateAPIWith(string apiName, IEnumerable<string> keys, string apiFolderPath)
        {
            var keysArray = ToArray(keys);

            var filePath = Path.Combine(apiFolderPath, $"{apiName}{C_SHARP_FILE_EXTENSION}");
            
            var apiContent = File.ReadAllText(filePath);
            var existingKeys = new HashSet<string>();
            
            var lines = apiContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (!line.Contains("=")) continue;
                
                var key = line.Split("=")[0].Trim();
                existingKeys.Add(key);
            }


            var newUniqueKeys = keysArray.Except(existingKeys).Distinct().ToList();
            var keysToRemove = existingKeys.Except(keysArray).ToList();

            if (newUniqueKeys.Count == 0 && keysToRemove.Count == 0)
            {
                return;
            }
            
            var updatedKeys = existingKeys.Union(newUniqueKeys).Except(keysToRemove).ToList();
            
            var apiContentBuilder = new StringBuilder();
            apiContentBuilder.AppendLine(lines.First());
            apiContentBuilder.AppendLine(lines[1]);
            apiContentBuilder.AppendLine(lines[2]);
            apiContentBuilder.AppendLine(lines[3]);

            var keyIndex = 0;
            foreach (var key in updatedKeys)
            {
                apiContentBuilder.AppendLine($"    {key} = {keyIndex},");
                keyIndex++;
            }

            apiContentBuilder.AppendLine(lines[^2]);
            apiContentBuilder.AppendLine(lines.Last());

            var newAPIContent = apiContentBuilder.ToString();
            
            File.WriteAllText(filePath, newAPIContent);
        }
    }
}