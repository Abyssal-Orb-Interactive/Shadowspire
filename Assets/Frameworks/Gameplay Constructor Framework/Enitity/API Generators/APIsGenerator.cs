using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameplayConstructorFramework.APIs
{
    public class APIsGenerator : IDisposable
    {
        private const string DATA_KEYS_API_NAME = "GlobalDataAPI";

        private readonly BehaviourKeyRegistrar _behaviourKeyRegistrar = null;
        private readonly KeyTypeDeclarator _dataKeyTypeDeclarator = null;
        private readonly EntityAPIUpdater _entityAPIUpdater = null;

        public IEnumerable<DeclaratoryPair> DeclaredPairs => _dataKeyTypeDeclarator.DeclaredPairs;

        private APIsGenerator(){}

        public APIsGenerator(IAPIGeneratorConfig config)
        {
            _dataKeyTypeDeclarator = new KeyTypeDeclarator(DATA_KEYS_API_NAME, config);
            _behaviourKeyRegistrar = new BehaviourKeyRegistrar(config);
            _entityAPIUpdater = new EntityAPIUpdater(_dataKeyTypeDeclarator, _behaviourKeyRegistrar.KeyTypeDeclarator, config); 
            _entityAPIUpdater.SubscribeOnKeyAPIsUpdating();
        }
        
        public APIsGenerator(IAPIGeneratorConfig config, IEnumerable<DeclaratoryPair> declaredPairs)
        {
            _dataKeyTypeDeclarator = new KeyTypeDeclarator(DATA_KEYS_API_NAME, declaredPairs, config);
            _behaviourKeyRegistrar = new BehaviourKeyRegistrar(config);
            _entityAPIUpdater = new EntityAPIUpdater(_dataKeyTypeDeclarator, _behaviourKeyRegistrar.KeyTypeDeclarator, config);
            _entityAPIUpdater.SubscribeOnKeyAPIsUpdating();
        }

        public bool TryDeclareDataKeyTypePair(in DeclaratoryPair pair)
        {
            return _dataKeyTypeDeclarator.TryDeclare(in pair);
        }

        public bool TryDeleteDataKeyTypePair(DeclaratoryPair pair)
        {
            return _dataKeyTypeDeclarator.TryRemove(pair);
        }

        public void SaveChanges()
        {
            _dataKeyTypeDeclarator.ConfirmChanges();
            _behaviourKeyRegistrar.UpdateBehavioursRegister();
        }
        
        public void UpdateBehavioursRegister()
        {
            _behaviourKeyRegistrar.UpdateBehavioursRegister();
        }

        public void Dispose()
        {
            _entityAPIUpdater?.Dispose();
        }
    }
}