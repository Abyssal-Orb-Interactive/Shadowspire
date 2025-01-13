using System;
using System.Collections.Generic;
using GameplayConstructorFramework.Enitity.World;
using UnityEngine;

namespace GameplayConstructorFramework.Entity.Unity
{
    [Serializable]
    public sealed class EntitiesInitializer : MonoBehaviour
    {
        [SerializeField] private List<EntityComponent> _entityComponents = new();

        public void InitializeEntities(IWorld world)
        {
            var count = _entityComponents.Count;
            for (var i = 0; i < count; i++)
            {
               world.AddEntity(_entityComponents[i]);
            }
        }
    }
}