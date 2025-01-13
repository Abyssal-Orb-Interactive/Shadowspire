using System;
using System.Collections.Generic;
using System.Linq;

namespace GameplayConstructorFramework.APIs
{
    public class BehaviourKeyRegistrar
    {
        private const string BEHAVIOURS_API_NAME = "GlobalBehavioursAPI";
        
        private readonly KeyTypeDeclarator _keyTypeDeclarator = null;

        public KeyTypeDeclarator KeyTypeDeclarator => _keyTypeDeclarator;

        public BehaviourKeyRegistrar(IAPIGeneratorConfig config)
        {
            _keyTypeDeclarator = new KeyTypeDeclarator(BEHAVIOURS_API_NAME, config);
        }

        public void UpdateBehavioursRegister()
        {
           IEnumerable<Type> behaviours = BehavioursScanner.GetAllBehaviours();
           var behavioursNames = behaviours.Select(behaviour => behaviour.Name).ToList();

           foreach (var behaviourName in behavioursNames)
           {
               _keyTypeDeclarator.TryDeclare(new DeclaratoryPair(behaviourName, behaviourName));
           }
           
           _keyTypeDeclarator.ConfirmChanges();
        }
    }
}