using System;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructor.Enitity.Behaviours
{
    [Serializable]
    public abstract class BehaviourBase
    {
        protected readonly IEntity _entity = null;

        protected BehaviourBase()
        {
            _entity = new Entity();
        }

        protected BehaviourBase(IEntity entity)
        {
            _entity = entity;
        }
    }
}