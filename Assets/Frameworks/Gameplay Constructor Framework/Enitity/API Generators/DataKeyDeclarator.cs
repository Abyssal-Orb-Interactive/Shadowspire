using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameplayConstructorFramework.APIs
{
    public sealed class KeyTypeDeclarator
    {
        private readonly HashSet<string> _declaredKeys = null;
        private readonly HashSet<string> _keysToDeclare = null;
        private readonly HashSet<DeclaratoryPair> _declaredPairs = null;
        private readonly HashSet<DeclaratoryPair> _pairsToDeclare = null;
        private readonly IAPIGeneratorConfig _apiGeneratorConfig = null;
        private readonly string _apiName = null;
        public IEnumerable<DeclaratoryPair> DeclaredPairs => _declaredPairs;

        public Action APIUpdated = null;

        private KeyTypeDeclarator()
        {
        }

        public KeyTypeDeclarator(string apiName, IAPIGeneratorConfig apiGeneratorConfig)
        {
            _declaredKeys = new HashSet<string>();
            _keysToDeclare = new HashSet<string>();
            _declaredPairs = new HashSet<DeclaratoryPair>();
            _pairsToDeclare = new HashSet<DeclaratoryPair>();
            _apiGeneratorConfig = apiGeneratorConfig;
            _apiName = apiName;
        }

        public KeyTypeDeclarator(string apiName, IEnumerable<DeclaratoryPair> pairs,
            IAPIGeneratorConfig apiGeneratorConfig)
        {
            _apiGeneratorConfig = apiGeneratorConfig;
            _declaredKeys = new HashSet<string>();
            _keysToDeclare = new HashSet<string>();
            _declaredPairs = new HashSet<DeclaratoryPair>();
            _pairsToDeclare = new HashSet<DeclaratoryPair>();
            _apiName = apiName;

            foreach (var pair in pairs)
            {
                if (string.IsNullOrWhiteSpace(pair.Key) || string.IsNullOrWhiteSpace(pair.Type)) continue;
                _declaredKeys.Add(pair.Key);
                _declaredPairs.Add(pair);
            }
        }

        public bool TryDeclare(in DeclaratoryPair pair)
        {
            if (DeclaratoryPairInvalid(in pair) || ApiNameInvalid(_apiName)) return false;
            

            Declare(in pair);

            return true;
        }

        private bool DeclaratoryPairInvalid(in DeclaratoryPair pair)
        {
            return TypeWithSameKeyAlreadyDeclared(pair.Key) || !TryFindReferenceTypeInAppDomain(pair.Type);
        }

        private bool TypeWithSameKeyAlreadyDeclared(string key)
        {
            return _declaredKeys.Contains(key) || _keysToDeclare.Contains(key);
        }

        private bool TryFindReferenceTypeInAppDomain(string typeName)
        {
            return AppDomainTypeScanner.TryGet(typeName, out var type) && IsValidType(type);
        }

        private bool IsValidType(Type type)
        {
            if (!type.IsPublic) return false;

            if (type.IsValueType) return false;

            if (type.IsAbstract && type.IsSealed) return false;
            
            return type.IsClass || type.IsInterface || type.IsAbstract || type.IsGenericType;
        }

        private static bool ApiNameInvalid(string apiName)
        {
            return string.IsNullOrWhiteSpace(apiName);
        }

        private void Declare(in DeclaratoryPair pair)
        {
            _keysToDeclare.Add(pair.Key);
            _pairsToDeclare.Add(pair);
        }

        public void ConfirmChanges()
        {
            foreach (var pair in _pairsToDeclare)
            {
                _declaredKeys.Add(pair.Key);
                _declaredPairs.Add(pair);
            }

            _pairsToDeclare.Clear();
            _keysToDeclare.Clear();
            
            KeyAPIGenerator.TryUpdateAPIWith(_apiName, _declaredKeys, _apiGeneratorConfig);
            APIUpdated?.Invoke();
        }

        public bool TryRemove(DeclaratoryPair pair)
        {
            if(!_declaredKeys.Contains(pair.Key) || !_declaredPairs.Contains(pair)) return false;

            _declaredKeys.Remove(pair.Key);
            _declaredPairs.Remove(pair);
            if(KeyAPIGenerator.TryUpdateAPIWith(_apiName, _declaredKeys, _apiGeneratorConfig)) APIUpdated?.Invoke();
            return true;
        }
    }
}