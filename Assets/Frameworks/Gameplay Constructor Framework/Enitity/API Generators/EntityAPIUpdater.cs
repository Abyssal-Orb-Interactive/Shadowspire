using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayConstructorFramework.APIs
{
    public class EntityAPIUpdater : IDisposable
    {
        private readonly KeyTypeDeclarator _dataKeyTypeDeclarator = null;
        private readonly KeyTypeDeclarator _behavioursKeyTypeDeclarator = null;
        private readonly IAPIGeneratorConfig _apiGeneratorConfig = null;

        private EntityAPIUpdater() {}

        public EntityAPIUpdater(KeyTypeDeclarator dataKeyTypeDeclarator, KeyTypeDeclarator behavioursKeyTypeDeclarator, IAPIGeneratorConfig config)
        {
            _apiGeneratorConfig = config;
            _dataKeyTypeDeclarator = dataKeyTypeDeclarator;
            _behavioursKeyTypeDeclarator = behavioursKeyTypeDeclarator;
        }

        public void SubscribeOnKeyAPIsUpdating()
        {
            _dataKeyTypeDeclarator.APIUpdated += UpdateEntityAPI;
            _behavioursKeyTypeDeclarator.APIUpdated += UpdateEntityAPI;
        }

        private void UpdateEntityAPI()
        {
            EntityAPIGenerator.TryGenerateAPI(_dataKeyTypeDeclarator.DeclaredPairs,
                _behavioursKeyTypeDeclarator.DeclaredPairs, _apiGeneratorConfig.APIsNamespace, _apiGeneratorConfig.APIsFolderPath);
        }

        public void Dispose()
        {
            if(_dataKeyTypeDeclarator == null) return;
            _dataKeyTypeDeclarator.APIUpdated -= UpdateEntityAPI;
      
            if(_behavioursKeyTypeDeclarator == null) return;
            _behavioursKeyTypeDeclarator.APIUpdated -= UpdateEntityAPI;
        }
    }
}