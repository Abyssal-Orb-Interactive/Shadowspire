using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameplayConstructorFramework.Unity
{
    public static class AssemblyDefinitionUpdater
    {
        private const string ASSEMBLY_DEFINITION_FILE_NAME = "GameplayConstructorElements.asmdef";

        private static readonly Dictionary<string, string> TypeToAssembly = new();
        private static readonly Dictionary<string, int> AssemblyReferenceCount = new();
        private static readonly string DictionarySavePath = Path.Combine(Application.persistentDataPath, "AsmdefTracking.json");

        static AssemblyDefinitionUpdater()
        {
            //LoadDictionaries();
        }
        
        public static void UpdateForAdding(string folderPath, string type)
        {
            var asmdefPath = Path.Combine(folderPath, ASSEMBLY_DEFINITION_FILE_NAME);

            string asmdefContent = File.Exists(asmdefPath) ? File.ReadAllText(asmdefPath) : CreateDefaultAsmdefContent();

            var existingReferences = GetReferencesFromAsmdef(asmdefContent);

            if (IsGenericType(type))
            {
                var (baseType, genericArguments) = ParseGenericType(type);

                var baseTypeGUID = FindAssemblyGUIDForType(baseType);
                if (!string.IsNullOrEmpty(baseTypeGUID))
                    AddAssemblyReference(baseType, baseTypeGUID, existingReferences);

                foreach (var argType in genericArguments)
                {
                    var argumentGUID = FindAssemblyGUIDForType(argType);
                    
                    if (string.IsNullOrEmpty(argumentGUID)) continue;
                    
                    AddAssemblyReference(argType, argumentGUID, existingReferences);
                }
            }
            else
            {
                var assemblyGUID = FindAssemblyGUIDForType(type);
                
                if (!string.IsNullOrEmpty(assemblyGUID)) 
                    AddAssemblyReference(type, assemblyGUID, existingReferences);
            }
            
            SaveDictionaries();

            var updatedAsmdefContent = UpdateReferencesInAsmdef(asmdefContent, existingReferences);
            File.WriteAllText(asmdefPath, updatedAsmdefContent);
            
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }
        
        private static bool IsGenericType(string typeName)
        {
            return typeName.Contains("<") && typeName.Contains(">");
        }
        
        private static (string BaseType, List<string> GenericArguments) ParseGenericType(string typeName)
        {
            var baseTypeName = typeName[..typeName.IndexOf("<", StringComparison.Ordinal)];

            var typeArgumentsString = typeName.Substring(typeName.IndexOf("<", StringComparison.Ordinal) + 1,
                typeName.LastIndexOf(">", StringComparison.Ordinal) - typeName.IndexOf("<", StringComparison.Ordinal) - 1);

            var typeArguments = typeArgumentsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(arg => arg.Trim())
                .ToList();

            return ($"{baseTypeName}`{typeArguments.Count}", typeArguments);
        }
        
        private static void AddAssemblyReference(string type, string assemblyGUID, ISet<string> existingReferences)
        {
            if (!TypeToAssembly.ContainsKey(type)) TypeToAssembly[type] = assemblyGUID;
            if (!AssemblyReferenceCount.ContainsKey(assemblyGUID)) AssemblyReferenceCount[assemblyGUID] = 0;

            AssemblyReferenceCount[assemblyGUID]++;
            var referenceWithGUIDPrefix = $"GUID:{assemblyGUID}";
            existingReferences.Add(referenceWithGUIDPrefix);
        }
        
        public static void UpdateForRemoving(string folderPath, string type)
        {
            var asmdefPath = Path.Combine(folderPath, ASSEMBLY_DEFINITION_FILE_NAME);

            if (!File.Exists(asmdefPath)) return; 
            
            string asmdefContent = File.ReadAllText(asmdefPath);

            var existingReferences = GetReferencesFromAsmdef(asmdefContent);
            
            if (IsGenericType(type))
            {
                var (baseType, genericArguments) = ParseGenericType(type);

                // Remove base type
                RemoveAssemblyReference(baseType, existingReferences);

                // Remove each argument type
                foreach (var argType in genericArguments)
                {
                    RemoveAssemblyReference(argType, existingReferences);
                }
            } 
            else
            {
                RemoveAssemblyReference(type, existingReferences);
            }
            
            var updatedAsmdefContent = UpdateReferencesInAsmdef(asmdefContent, existingReferences);
            File.WriteAllText(asmdefPath, updatedAsmdefContent);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }
        
        private static void RemoveAssemblyReference(string type, ICollection<string> existingReferences)
        {
            if (!TypeToAssembly.TryGetValue(type, out var assemblyGUID)) return;
            if (!AssemblyReferenceCount.ContainsKey(assemblyGUID)) return;

            AssemblyReferenceCount[assemblyGUID]--;
            if (AssemblyReferenceCount[assemblyGUID] > 0)
            {
                SaveDictionaries();
                return;
            }

            TypeToAssembly.Remove(type);
            AssemblyReferenceCount.Remove(assemblyGUID);
            SaveDictionaries();

            var referenceWithGUIDPrefix = $"GUID:{assemblyGUID}";
            existingReferences.Remove(referenceWithGUIDPrefix);
        }
        
        private static HashSet<string> GetReferencesFromAsmdef(string asmdefContent)
        {
            var references = new HashSet<string>();
            var startIndex = asmdefContent.IndexOf("\"references\": [", StringComparison.Ordinal);

            if (startIndex < 0) return references;

            var endIndex = asmdefContent.IndexOf("]", startIndex, StringComparison.Ordinal);
            if (endIndex < 0) return references;

            var referencesBlock = asmdefContent.Substring(startIndex, endIndex - startIndex);
            var referenceLines = referencesBlock.Split(new[] { ',', '"' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var reference in referenceLines)
            {
                if (reference.Trim().StartsWith("GUID:"))
                {
                    references.Add(reference.Trim());
                }
            }

            return references;
        }
        
        private static string UpdateReferencesInAsmdef(string asmdefContent, IEnumerable<string> newReferences)
        {
            var newReferencesString = string.Join(", ", newReferences.Select(r => $"\"{r}\""));

            var startIndex = asmdefContent.IndexOf("\"references\": [", StringComparison.Ordinal);
            var endIndex = asmdefContent.IndexOf("]", startIndex, StringComparison.Ordinal) + 1;

            var updatedAsmdefContent = asmdefContent.Substring(0, startIndex) +
                                       $"\"references\": [{newReferencesString}]" +
                                       asmdefContent.Substring(endIndex);

            return updatedAsmdefContent;
        }

        private static string FindAssemblyGUIDForType(string typeName)
        {
            var type = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == typeName);

            if (type == null)
            {
                Debug.LogWarning($"Type {typeName} not found.");
                return string.Empty;
            }

            var assemblyName = type.Assembly.GetName().Name;

            var asmdefGUIDs = AssetDatabase.FindAssets($"t:asmdef {assemblyName}");

            foreach (var guid in asmdefGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                var asmdefContent = File.ReadAllText(path);
                if (asmdefContent.Contains($"\"name\": \"{assemblyName}\""))
                {
                    return guid; // Return the GUID of the correct assembly definition
                }
            }

            Debug.LogWarning($"Assembly Definition for {assemblyName} not found.");
            return string.Empty;
        }

        private static void SaveDictionaries()
        {
            var data = new PersistentData
            {
                TypeToAssembly = TypeToAssembly.Select(kvp => new KeyValuePairSerializable(kvp.Key, kvp.Value)).ToList(),
                AssemblyReferenceCount = AssemblyReferenceCount.Select(kvp => new KeyValuePairSerializable(kvp.Key, kvp.Value.ToString())).ToList()
            };
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(DictionarySavePath, json);
        }

        private static void LoadDictionaries()
        {
            if (!File.Exists(DictionarySavePath)) return;
            var json = File.ReadAllText(DictionarySavePath);
            var data = JsonUtility.FromJson<PersistentData>(json);

            TypeToAssembly.Clear();
            AssemblyReferenceCount.Clear();

            foreach (var kvp in data.TypeToAssembly)
            {
                TypeToAssembly[kvp.Key] = kvp.Value;
            }

            foreach (var kvp in data.AssemblyReferenceCount)
            {
                AssemblyReferenceCount[kvp.Key] = int.Parse(kvp.Value);
            }
        }
        
        private static string CreateDefaultAsmdefContent()
        {
            return @"
            {
                ""name"": ""Generated"",
                ""rootNamespace"": """",
                ""references"": [],
                ""includePlatforms"": [],
                ""excludePlatforms"": [],
                ""allowUnsafeCode"": false,
                ""overrideReferences"": false,
                ""precompiledReferences"": [],
                ""autoReferenced"": true,
                ""defineConstraints"": [],
                ""versionDefines"": [],
                ""noEngineReferences"": false
            }";
        }
        
        [Serializable]
        private class PersistentData
        {
            public List<KeyValuePairSerializable> TypeToAssembly;
            public List<KeyValuePairSerializable> AssemblyReferenceCount;
        }

        [Serializable]
        private struct KeyValuePairSerializable
        {
            public string Key;
            public string Value;

            public KeyValuePairSerializable(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}