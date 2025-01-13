using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using UnityEngine;

namespace Tests.EntityExtensionsTests.TestsScriptLanguage
{
    public  static class SetUp
    {
        public static IEntity EntityWithHealth(in float health = 0f)
        {
            var entity = Create.Entity();
            entity.TryAddHealthData(new AtomicReactiveProperty<float>(health));
            return entity;
        }

        public static IEntity EntityWithDamage(in float damage = 0f)
        {
            var damager = Create.Entity();
            damager.TryAddDamageData(new AtomicReactiveProperty<float>(damage));
            return damager;
        }

        public static GameObject DamagerGameObjectWithEntity()
        {
            var damagerGameObject = Create.GameObject();
            damagerGameObject.AddComponent<EntityComponent>();
            return damagerGameObject;
        }

        public static GameObject DamagerGameObjectWithEntityWithDamage(in float damage = 0f)
        {
            var damagerGameObject = DamagerGameObjectWithEntity();
            damagerGameObject.TryGetComponent<IEntity>(out var damager);
            damager.TryAddDamageData(new AtomicReactiveProperty<float>(damage));
            return damagerGameObject;
        }
        
        public static Component DamagerComponentWithEntity()
        {
            var damagerComponent = Create.DamgerComponent();
            damagerComponent.gameObject.AddComponent<EntityComponent>();
            return damagerComponent;
        }
        
        public static Component DamagerComponentWithEntityWithDamage(in float damage = 0f)
        {
            var damagerComponent = DamagerComponentWithEntity();
            damagerComponent.TryGetComponent<IEntity>(out var damager);
            damager.TryAddDamageData(new AtomicReactiveProperty<float>(damage));
            return damagerComponent;
        }

        public static Collider2D EntityCollider()
        {
            var entityGameObject = Create.GameObject();
            return entityGameObject.AddComponent<BoxCollider2D>();
        }

        public static Collider2D EntityColliderWithEntity()
        {
            var entityCollider = EntityCollider();
            entityCollider.gameObject.AddComponent<EntityComponent>();
            return entityCollider;
        }
        
        public static Collider2D EntityColliderWithEntityWithHealth(in float health = 0f)
        {
            var entityCollider = EntityColliderWithEntity();
            entityCollider.gameObject.TryGetComponent<IEntity>(out var entity);
            entity.TryAddHealthData(new AtomicReactiveProperty<float>(health));
            return entityCollider;
        }
    }
}