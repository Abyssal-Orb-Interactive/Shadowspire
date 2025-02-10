using System;
using GameplayConstructorElements.Behaviours.TorchModel;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;

namespace GameplayConstructorElements.Installers.Models.Behaviour
{
    [Serializable]
    public sealed class TorchModeBehavioursInstaller : IEntityAtomicElementInstaller
    {
        public void InstallTo(IEntity entity)
        {
            entity.TryAddTorchFadeBehaviourBehaviour(new TorchFadeBehaviour(entity));
        }
    }
}