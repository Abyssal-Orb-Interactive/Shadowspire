using AtomicFramework.AtomicStructures;
using UnityEngine;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorFramework.Entity;

namespace GameplayConstructorFrameworkAPIs
{
    public static class EntityAPI
     {

         #region dataAPI

        public static bool TryGetNameData(this IEntity entity, out AtomicReactiveProperty<string> name)
         {
            return entity.TryGetData((int)GlobalDataAPI.Name, out name);
         }

         public static bool TryAddNameData(this IEntity entity, AtomicReactiveProperty<string> name)
          {
            return entity.TryAddData((int)GlobalDataAPI.Name, name);
          }

         public static bool TryRemoveNameData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Name);
          }

        public static bool TryGetSpriteData(this IEntity entity, out AtomicReactiveProperty<Sprite> sprite)
         {
            return entity.TryGetData((int)GlobalDataAPI.Sprite, out sprite);
         }

         public static bool TryAddSpriteData(this IEntity entity, AtomicReactiveProperty<Sprite> sprite)
          {
            return entity.TryAddData((int)GlobalDataAPI.Sprite, sprite);
          }

         public static bool TryRemoveSpriteData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Sprite);
          }

        public static bool TryGetSpriteRendererData(this IEntity entity, out AtomicReactiveProperty<SpriteRenderer> spriteRenderer)
         {
            return entity.TryGetData((int)GlobalDataAPI.SpriteRenderer, out spriteRenderer);
         }

         public static bool TryAddSpriteRendererData(this IEntity entity, AtomicReactiveProperty<SpriteRenderer> spriteRenderer)
          {
            return entity.TryAddData((int)GlobalDataAPI.SpriteRenderer, spriteRenderer);
          }

         public static bool TryRemoveSpriteRendererData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.SpriteRenderer);
          }

        public static bool TryGetMaxHealthData(this IEntity entity, out AtomicReactiveProperty<float> maxHealth)
         {
            return entity.TryGetData((int)GlobalDataAPI.MaxHealth, out maxHealth);
         }

         public static bool TryAddMaxHealthData(this IEntity entity, AtomicReactiveProperty<float> maxHealth)
          {
            return entity.TryAddData((int)GlobalDataAPI.MaxHealth, maxHealth);
          }

         public static bool TryRemoveMaxHealthData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.MaxHealth);
          }

        public static bool TryGetHealthData(this IEntity entity, out AtomicReactiveProperty<float> health)
         {
            return entity.TryGetData((int)GlobalDataAPI.Health, out health);
         }

         public static bool TryAddHealthData(this IEntity entity, AtomicReactiveProperty<float> health)
          {
            return entity.TryAddData((int)GlobalDataAPI.Health, health);
          }

         public static bool TryRemoveHealthData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Health);
          }

        public static bool TryGetDamageData(this IEntity entity, out AtomicReactiveProperty<float> damage)
         {
            return entity.TryGetData((int)GlobalDataAPI.Damage, out damage);
         }

         public static bool TryAddDamageData(this IEntity entity, AtomicReactiveProperty<float> damage)
          {
            return entity.TryAddData((int)GlobalDataAPI.Damage, damage);
          }

         public static bool TryRemoveDamageData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Damage);
          }

         #endregion

         #region behavioursAPI

        public static bool TryGetSpriteSetUpBehaviourBehaviour(this IEntity entity, out SpriteSetUpBehaviour spriteSetUpBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.SpriteSetUpBehaviour, out spriteSetUpBehaviour);
         }

         public static bool TryAddSpriteSetUpBehaviourBehaviour(this IEntity entity, SpriteSetUpBehaviour spriteSetUpBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.SpriteSetUpBehaviour, spriteSetUpBehaviour);
          }

         public static bool TryRemoveSpriteSetUpBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<SpriteSetUpBehaviour>((int)GlobalBehavioursAPI.SpriteSetUpBehaviour);
          }

         #endregion
     }
}
