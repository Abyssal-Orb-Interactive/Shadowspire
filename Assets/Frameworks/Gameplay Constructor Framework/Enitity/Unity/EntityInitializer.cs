using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace GameplayConstructorFramework.Entity.Unity
{ 
    public class EntityInitializer : SerializedMonoBehaviour
    {
        [OdinSerialize, Searchable] private List<IEntityAtomicElementInstaller> _dataInstallers = new();
        [OdinSerialize, Searchable] private List<IEntityAtomicElementInstaller> _behaviourInstallers = new();

        public void InitializeData(IEntity entity)
        {
            foreach (var installer in _dataInstallers)
            {
                installer.InstallTo(entity);
            }
        }
        
        public void InitializeBehaviours(IEntity entity)
        {
            foreach (var installer in _behaviourInstallers)
            {
                installer.InstallTo(entity);
            }
        }
    }
}