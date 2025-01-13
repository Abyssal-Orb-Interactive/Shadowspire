using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GameplayConstructorFramework.APIs.DotNetExtensions;

namespace GameplayConstructorFramework.APIs
{
    public static class EntityAPIGenerator
    {
        private const string C_SHARP_FILE_EXTENSION = ".cs";
        private const string EXTENDED_TYPE = "IEntity";
        private const string API_NAME = "EntityAPI";

        public static bool TryGenerateAPI(IEnumerable<DeclaratoryPair> dataPairs,
            IEnumerable<DeclaratoryPair> behaviourPairs, string apiNamespace, string apisFolderPath)
        {
            if (dataPairs == null || behaviourPairs == null) return false;

            GenerateAPI(API_NAME, apiNamespace, dataPairs, behaviourPairs, apisFolderPath);
            
            return true;
        }

        private static void GenerateAPI(string apiName, string apiNamespace, IEnumerable<DeclaratoryPair> dataPairs,
            IEnumerable<DeclaratoryPair> behaviourPairs, string apiPath)
        {
            var dataPairsArray = dataPairs as DeclaratoryPair[] ?? dataPairs.ToArray();
            var behaviourPairsArray = behaviourPairs as DeclaratoryPair[] ?? behaviourPairs.ToArray();

            var allTypes = new HashSet<string>(
                dataPairsArray.Select(dp => dp.Type).Concat(behaviourPairsArray.Select(bp => bp.Type))
            );

            var apiContentBuilder = new StringBuilder();
            var namespaces = new HashSet<string>();

            foreach (var namespacesForType in allTypes.Select(t => GetNamespaceForType(t).Split(Environment.NewLine)))
            {
                if(namespacesForType == null) continue;
                foreach (var ns in namespacesForType)
                {
                    if (namespaces.Contains(ns)) continue;
                    apiContentBuilder.AppendLine($"using {ns};");
                    namespaces.Add(ns);
                }
            }
            

            var entityNamespace = GetNamespaceForType(EXTENDED_TYPE).Split(Environment.NewLine);
            foreach (var ns in entityNamespace.Where(ns => !namespaces.Contains(ns)))
            {
                apiContentBuilder.AppendLine($"using {ns};");
                namespaces.Add(ns);
            }

            
            apiContentBuilder
                .AppendLine()
                .AppendLine($"namespace {apiNamespace}")
                .AppendLine("{")
                .AppendLine($"    public static class {apiName}")
                .AppendLine("     {")
                .AppendLine()
                .AppendLine("         #region dataAPI")
                .AppendLine();

            foreach (var dataPair in dataPairsArray)
            {
                apiContentBuilder
                    .AppendLine(
                        $"        public static bool TryGet{dataPair.Key}Data(this {EXTENDED_TYPE} entity, out {dataPair.Type} {dataPair.Key.FromPascalToCamelCase()})")
                    .AppendLine("         {")
                    .AppendLine(
                        $"            return entity.TryGetData((int)GlobalDataAPI.{dataPair.Key}, out {dataPair.Key.FromPascalToCamelCase()});")
                    .AppendLine("         }")
                    .AppendLine()
                    .AppendLine(
                        $"         public static bool TryAdd{dataPair.Key}Data(this {EXTENDED_TYPE} entity, {dataPair.Type} {dataPair.Key.FromPascalToCamelCase()})")
                    .AppendLine("          {")
                    .AppendLine(
                        $"            return entity.TryAddData((int)GlobalDataAPI.{dataPair.Key}, {dataPair.Key.FromPascalToCamelCase()});")
                    .AppendLine("          }")
                    .AppendLine()
                    .AppendLine($"         public static bool TryRemove{dataPair.Key}Data(this {EXTENDED_TYPE} entity)")
                    .AppendLine("          {")
                    .AppendLine($"            return entity.TryRemoveData((int)GlobalDataAPI.{dataPair.Key});")
                    .AppendLine("          }")
                    .AppendLine();

            }

            apiContentBuilder
                .AppendLine("         #endregion")
                .AppendLine()
                .AppendLine("         #region behavioursAPI")
                .AppendLine();

            foreach (var behaviourPair in behaviourPairsArray)
            {
                apiContentBuilder
                    .AppendLine(
                        $"        public static bool TryGet{behaviourPair.Key}Behaviour(this {EXTENDED_TYPE} entity, out {behaviourPair.Type} {behaviourPair.Key.FromPascalToCamelCase()})")
                    .AppendLine("         {")
                    .AppendLine(
                        $"            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.{behaviourPair.Key}, out {behaviourPair.Key.FromPascalToCamelCase()});")
                    .AppendLine("         }")
                    .AppendLine()
                    .AppendLine(
                        $"         public static bool TryAdd{behaviourPair.Key}Behaviour(this {EXTENDED_TYPE} entity, {behaviourPair.Type} {behaviourPair.Key.FromPascalToCamelCase()})")
                    .AppendLine("          {")
                    .AppendLine(
                        $"            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.{behaviourPair.Key}, {behaviourPair.Key.FromPascalToCamelCase()});")
                    .AppendLine("          }")
                    .AppendLine()
                    .AppendLine(
                        $"         public static bool TryRemove{behaviourPair.Key}Behaviour(this {EXTENDED_TYPE} entity)")
                    .AppendLine("          {")
                    .AppendLine($"            return entity.TryRemoveBehaviour<{behaviourPair.Key}>((int)GlobalBehavioursAPI.{behaviourPair.Key});")
                    .AppendLine("          }")
                    .AppendLine();
            }

            apiContentBuilder
                .AppendLine("         #endregion")
                .AppendLine("     }")
                .AppendLine("}");

            var apiContent = apiContentBuilder.ToString();

            var filePat = Path.Combine(apiPath, $"{apiName}{C_SHARP_FILE_EXTENSION}");
            File.WriteAllText(filePat, apiContent);
        }

        private static string GetNamespaceForType(string typeName)
        {
            if (typeName.Contains("<") && typeName.Contains(">"))
            {
                return GetNamespaceForGenericType(typeName);
            }

            
            var type = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == typeName);

            return type?.Namespace ?? string.Empty;
        }
        
        private static string GetNamespaceForGenericType(string typeName)
        {
            var baseTypeName = typeName[..typeName.IndexOf("<", StringComparison.Ordinal)];
            var typeArgumentsString = typeName.Substring(typeName.IndexOf("<", StringComparison.Ordinal) + 1,
                typeName.LastIndexOf(">", StringComparison.Ordinal) - typeName.IndexOf("<", StringComparison.Ordinal) - 1);

            var typeArguments = typeArgumentsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(arg => arg.Trim())
                .ToList();

            var baseType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == $"{baseTypeName}`{typeArguments.Count}" || t.FullName == $"{baseTypeName}`{typeArguments.Count}");

            if (baseType == null)
            {
                return string.Empty;
            }

            var namespaces = new HashSet<string> { baseType.Namespace };

            foreach (var argumentNamespace in typeArguments.Select(GetNamespaceForType).Where(argumentNamespace => !string.IsNullOrEmpty(argumentNamespace)))
            {
                namespaces.Add(argumentNamespace);
            }

            return string.Join(Environment.NewLine, namespaces);
        }
    }
}