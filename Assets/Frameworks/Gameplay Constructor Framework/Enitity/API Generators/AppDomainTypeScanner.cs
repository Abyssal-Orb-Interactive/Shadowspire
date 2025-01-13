using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameplayConstructorFramework.APIs
{
    public static class AppDomainTypeScanner
    {
        private static readonly Dictionary<string, Type> CachedRequests = new();
        
        private static readonly Dictionary<string, string> TypeAliasMap = new()
        {
            { "int", "Int32" },
            { "float", "Single" },
            { "double", "Double" },
            { "bool", "Boolean" },
            { "string", "String" },
            { "object", "Object" },
            { "byte", "Byte" },
            { "short", "Int16" },
            { "long", "Int64" },
            { "uint", "UInt32" },
            { "ulong", "UInt64" },
            { "ushort", "UInt16" },
            { "sbyte", "SByte" },
            { "char", "Char" },
            { "decimal", "Decimal" }
        };

        public static bool TryGet(string typeName, out Type type)
        {
            if (CachedRequests.ContainsKey(typeName))
            {
                type = CachedRequests[typeName];
                return true;
            }
            
            type = Get(typeName);

            if (type == null) return false;
            
            CachedRequests.Add(typeName, type);
            
            return true;
        }

        private static Type Get(string typeName)
        {
            typeName = ResolveTypeAlias(typeName);

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetTypes());

            Type foundType = types.FirstOrDefault(t => t.Name == typeName);

            if (foundType == null && IsGenericTypeName(typeName))
            {
                foundType = GetOpenGenericType(typeName);
            }
            
            return foundType;
        }
        
        private static bool IsGenericTypeName(string typeName)
        {
            return typeName.Contains("<") && typeName.Contains(">");
        }
        
        private static string ResolveTypeAlias(string typeName)
        {
            return TypeAliasMap.TryGetValue(typeName, out var resolvedType) ? resolvedType : typeName;
        }
        
        private static Type GetOpenGenericType(string typeName)
        {
            try
            {
                // Extract base type name and generic arguments
                var baseTypeName = typeName[..typeName.IndexOf("<", StringComparison.Ordinal)];
                var typeArgumentsString = typeName[(typeName.IndexOf("<", StringComparison.Ordinal) + 1)..typeName.LastIndexOf(">", StringComparison.Ordinal)];
                var typeArguments = typeArgumentsString.Split(',').Select(arg => arg.Trim()).ToArray();
                
                // Resolve base type
                var baseType = Get($"{baseTypeName}`{typeArguments.Length}");

                if (baseType is { IsGenericTypeDefinition: false })
                {
                    return null;
                }
                
                // Resolve each generic argument type
                var resolvedArguments = typeArguments
                    .Select(Get)
                    .ToArray();

                var test = resolvedArguments.Any(arg => arg == null) ? null : baseType.MakeGenericType(resolvedArguments);
                
                return test;
            }
            catch
            {
                return null;
            }
        }
    }
}