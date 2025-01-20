using AtomicFramework.AtomicStructures;
using UnityEngine;
using Unity.Mathematics;
using InputActions;
using GameplayConstructorFramework.Entity;
using ObservableCollections;
using System.Collections.Generic;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorElements.Behaviours.InventoryModel;
using GameplayConstructorElements.Behaviours.Interaction_Model;
using GameplayConstructorElements.Behaviours.InputHandlerModel;

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

        public static bool TryGetInvincibilitySecondsDurationData(this IEntity entity, out AtomicReactiveProperty<float> invincibilitySecondsDuration)
         {
            return entity.TryGetData((int)GlobalDataAPI.InvincibilitySecondsDuration, out invincibilitySecondsDuration);
         }

         public static bool TryAddInvincibilitySecondsDurationData(this IEntity entity, AtomicReactiveProperty<float> invincibilitySecondsDuration)
          {
            return entity.TryAddData((int)GlobalDataAPI.InvincibilitySecondsDuration, invincibilitySecondsDuration);
          }

         public static bool TryRemoveInvincibilitySecondsDurationData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.InvincibilitySecondsDuration);
          }

        public static bool TryGetCanTakeDamageData(this IEntity entity, out AtomicExpression<bool> canTakeDamage)
         {
            return entity.TryGetData((int)GlobalDataAPI.CanTakeDamage, out canTakeDamage);
         }

         public static bool TryAddCanTakeDamageData(this IEntity entity, AtomicExpression<bool> canTakeDamage)
          {
            return entity.TryAddData((int)GlobalDataAPI.CanTakeDamage, canTakeDamage);
          }

         public static bool TryRemoveCanTakeDamageData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.CanTakeDamage);
          }

        public static bool TryGetInvincibilityData(this IEntity entity, out AtomicReactiveProperty<bool> invincibility)
         {
            return entity.TryGetData((int)GlobalDataAPI.Invincibility, out invincibility);
         }

         public static bool TryAddInvincibilityData(this IEntity entity, AtomicReactiveProperty<bool> invincibility)
          {
            return entity.TryAddData((int)GlobalDataAPI.Invincibility, invincibility);
          }

         public static bool TryRemoveInvincibilityData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Invincibility);
          }

        public static bool TryGetLevelData(this IEntity entity, out AtomicReactiveProperty<int> level)
         {
            return entity.TryGetData((int)GlobalDataAPI.Level, out level);
         }

         public static bool TryAddLevelData(this IEntity entity, AtomicReactiveProperty<int> level)
          {
            return entity.TryAddData((int)GlobalDataAPI.Level, level);
          }

         public static bool TryRemoveLevelData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Level);
          }

        public static bool TryGetXpTargetData(this IEntity entity, out AtomicReactiveProperty<float> xpTarget)
         {
            return entity.TryGetData((int)GlobalDataAPI.XpTarget, out xpTarget);
         }

         public static bool TryAddXpTargetData(this IEntity entity, AtomicReactiveProperty<float> xpTarget)
          {
            return entity.TryAddData((int)GlobalDataAPI.XpTarget, xpTarget);
          }

         public static bool TryRemoveXpTargetData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.XpTarget);
          }

        public static bool TryGetXpData(this IEntity entity, out AtomicReactiveProperty<float> xp)
         {
            return entity.TryGetData((int)GlobalDataAPI.Xp, out xp);
         }

         public static bool TryAddXpData(this IEntity entity, AtomicReactiveProperty<float> xp)
          {
            return entity.TryAddData((int)GlobalDataAPI.Xp, xp);
          }

         public static bool TryRemoveXpData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Xp);
          }

        public static bool TryGetMovementInputActionData(this IEntity entity, out AtomicEvent<float2> movementInputAction)
         {
            return entity.TryGetData((int)GlobalDataAPI.MovementInputAction, out movementInputAction);
         }

         public static bool TryAddMovementInputActionData(this IEntity entity, AtomicEvent<float2> movementInputAction)
          {
            return entity.TryAddData((int)GlobalDataAPI.MovementInputAction, movementInputAction);
          }

         public static bool TryRemoveMovementInputActionData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.MovementInputAction);
          }

        public static bool TryGetSpeedData(this IEntity entity, out AtomicReactiveProperty<float> speed)
         {
            return entity.TryGetData((int)GlobalDataAPI.Speed, out speed);
         }

         public static bool TryAddSpeedData(this IEntity entity, AtomicReactiveProperty<float> speed)
          {
            return entity.TryAddData((int)GlobalDataAPI.Speed, speed);
          }

         public static bool TryRemoveSpeedData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Speed);
          }

        public static bool TryGetRigidbody2DData(this IEntity entity, out AtomicReactiveProperty<Rigidbody2D> rigidbody2D)
         {
            return entity.TryGetData((int)GlobalDataAPI.Rigidbody2D, out rigidbody2D);
         }

         public static bool TryAddRigidbody2DData(this IEntity entity, AtomicReactiveProperty<Rigidbody2D> rigidbody2D)
          {
            return entity.TryAddData((int)GlobalDataAPI.Rigidbody2D, rigidbody2D);
          }

         public static bool TryRemoveRigidbody2DData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Rigidbody2D);
          }

        public static bool TryGetInputActionsData(this IEntity entity, out AtomicReactiveProperty<PlayerActions> inputActions)
         {
            return entity.TryGetData((int)GlobalDataAPI.InputActions, out inputActions);
         }

         public static bool TryAddInputActionsData(this IEntity entity, AtomicReactiveProperty<PlayerActions> inputActions)
          {
            return entity.TryAddData((int)GlobalDataAPI.InputActions, inputActions);
          }

         public static bool TryRemoveInputActionsData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.InputActions);
          }

        public static bool TryGetInputHandlerData(this IEntity entity, out AtomicReactiveProperty<IEntity> inputHandler)
         {
            return entity.TryGetData((int)GlobalDataAPI.InputHandler, out inputHandler);
         }

         public static bool TryAddInputHandlerData(this IEntity entity, AtomicReactiveProperty<IEntity> inputHandler)
          {
            return entity.TryAddData((int)GlobalDataAPI.InputHandler, inputHandler);
          }

         public static bool TryRemoveInputHandlerData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.InputHandler);
          }

        public static bool TryGetCanMoveData(this IEntity entity, out AtomicExpression<bool> canMove)
         {
            return entity.TryGetData((int)GlobalDataAPI.CanMove, out canMove);
         }

         public static bool TryAddCanMoveData(this IEntity entity, AtomicExpression<bool> canMove)
          {
            return entity.TryAddData((int)GlobalDataAPI.CanMove, canMove);
          }

         public static bool TryRemoveCanMoveData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.CanMove);
          }

        public static bool TryGetFlippedData(this IEntity entity, out AtomicReactiveProperty<bool> flipped)
         {
            return entity.TryGetData((int)GlobalDataAPI.Flipped, out flipped);
         }

         public static bool TryAddFlippedData(this IEntity entity, AtomicReactiveProperty<bool> flipped)
          {
            return entity.TryAddData((int)GlobalDataAPI.Flipped, flipped);
          }

         public static bool TryRemoveFlippedData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Flipped);
          }

        public static bool TryGetCoinsData(this IEntity entity, out AtomicReactiveProperty<int> coins)
         {
            return entity.TryGetData((int)GlobalDataAPI.Coins, out coins);
         }

         public static bool TryAddCoinsData(this IEntity entity, AtomicReactiveProperty<int> coins)
          {
            return entity.TryAddData((int)GlobalDataAPI.Coins, coins);
          }

         public static bool TryRemoveCoinsData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Coins);
          }

        public static bool TryGetInteractionInputActionData(this IEntity entity, out AtomicEvent interactionInputAction)
         {
            return entity.TryGetData((int)GlobalDataAPI.InteractionInputAction, out interactionInputAction);
         }

         public static bool TryAddInteractionInputActionData(this IEntity entity, AtomicEvent interactionInputAction)
          {
            return entity.TryAddData((int)GlobalDataAPI.InteractionInputAction, interactionInputAction);
          }

         public static bool TryRemoveInteractionInputActionData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.InteractionInputAction);
          }

        public static bool TryGetInteractionActionEventData(this IEntity entity, out AtomicEvent<IEntity> interactionActionEvent)
         {
            return entity.TryGetData((int)GlobalDataAPI.InteractionActionEvent, out interactionActionEvent);
         }

         public static bool TryAddInteractionActionEventData(this IEntity entity, AtomicEvent<IEntity> interactionActionEvent)
          {
            return entity.TryAddData((int)GlobalDataAPI.InteractionActionEvent, interactionActionEvent);
          }

         public static bool TryRemoveInteractionActionEventData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.InteractionActionEvent);
          }

        public static bool TryGetInventoryData(this IEntity entity, out ObservableDictionary<string, int> inventory)
         {
            return entity.TryGetData((int)GlobalDataAPI.Inventory, out inventory);
         }

         public static bool TryAddInventoryData(this IEntity entity, ObservableDictionary<string, int> inventory)
          {
            return entity.TryAddData((int)GlobalDataAPI.Inventory, inventory);
          }

         public static bool TryRemoveInventoryData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Inventory);
          }

        public static bool TryGetItemsRegisterData(this IEntity entity, out Dictionary<string, IEntity> itemsRegister)
         {
            return entity.TryGetData((int)GlobalDataAPI.ItemsRegister, out itemsRegister);
         }

         public static bool TryAddItemsRegisterData(this IEntity entity, Dictionary<string, IEntity> itemsRegister)
          {
            return entity.TryAddData((int)GlobalDataAPI.ItemsRegister, itemsRegister);
          }

         public static bool TryRemoveItemsRegisterData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.ItemsRegister);
          }

        public static bool TryGetItemsRegisterHolderData(this IEntity entity, out AtomicReactiveProperty<IEntity> itemsRegisterHolder)
         {
            return entity.TryGetData((int)GlobalDataAPI.ItemsRegisterHolder, out itemsRegisterHolder);
         }

         public static bool TryAddItemsRegisterHolderData(this IEntity entity, AtomicReactiveProperty<IEntity> itemsRegisterHolder)
          {
            return entity.TryAddData((int)GlobalDataAPI.ItemsRegisterHolder, itemsRegisterHolder);
          }

         public static bool TryRemoveItemsRegisterHolderData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.ItemsRegisterHolder);
          }

         #endregion

         #region behavioursAPI

        public static bool TryGetInvincibilityBehaviourBehaviour(this IEntity entity, out InvincibilityBehaviour invincibilityBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.InvincibilityBehaviour, out invincibilityBehaviour);
         }

         public static bool TryAddInvincibilityBehaviourBehaviour(this IEntity entity, InvincibilityBehaviour invincibilityBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.InvincibilityBehaviour, invincibilityBehaviour);
          }

         public static bool TryRemoveInvincibilityBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<InvincibilityBehaviour>((int)GlobalBehavioursAPI.InvincibilityBehaviour);
          }

        public static bool TryGetLevelingBehaviourBehaviour(this IEntity entity, out LevelingBehaviour levelingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.LevelingBehaviour, out levelingBehaviour);
         }

         public static bool TryAddLevelingBehaviourBehaviour(this IEntity entity, LevelingBehaviour levelingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.LevelingBehaviour, levelingBehaviour);
          }

         public static bool TryRemoveLevelingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<LevelingBehaviour>((int)GlobalBehavioursAPI.LevelingBehaviour);
          }

        public static bool TryGetMovementBehaviourBehaviour(this IEntity entity, out MovementBehaviour movementBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.MovementBehaviour, out movementBehaviour);
         }

         public static bool TryAddMovementBehaviourBehaviour(this IEntity entity, MovementBehaviour movementBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.MovementBehaviour, movementBehaviour);
          }

         public static bool TryRemoveMovementBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<MovementBehaviour>((int)GlobalBehavioursAPI.MovementBehaviour);
          }

        public static bool TryGetSpriteFlippingBehaviourBehaviour(this IEntity entity, out SpriteFlippingBehaviour spriteFlippingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.SpriteFlippingBehaviour, out spriteFlippingBehaviour);
         }

         public static bool TryAddSpriteFlippingBehaviourBehaviour(this IEntity entity, SpriteFlippingBehaviour spriteFlippingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.SpriteFlippingBehaviour, spriteFlippingBehaviour);
          }

         public static bool TryRemoveSpriteFlippingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<SpriteFlippingBehaviour>((int)GlobalBehavioursAPI.SpriteFlippingBehaviour);
          }

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

        public static bool TryGetItemsDroppingBehaviourBehaviour(this IEntity entity, out ItemsDroppingBehaviour itemsDroppingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.ItemsDroppingBehaviour, out itemsDroppingBehaviour);
         }

         public static bool TryAddItemsDroppingBehaviourBehaviour(this IEntity entity, ItemsDroppingBehaviour itemsDroppingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.ItemsDroppingBehaviour, itemsDroppingBehaviour);
          }

         public static bool TryRemoveItemsDroppingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<ItemsDroppingBehaviour>((int)GlobalBehavioursAPI.ItemsDroppingBehaviour);
          }

        public static bool TryGetInteractionCoinPickUppingBehaviourBehaviour(this IEntity entity, out InteractionCoinPickUppingBehaviour interactionCoinPickUppingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.InteractionCoinPickUppingBehaviour, out interactionCoinPickUppingBehaviour);
         }

         public static bool TryAddInteractionCoinPickUppingBehaviourBehaviour(this IEntity entity, InteractionCoinPickUppingBehaviour interactionCoinPickUppingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.InteractionCoinPickUppingBehaviour, interactionCoinPickUppingBehaviour);
          }

         public static bool TryRemoveInteractionCoinPickUppingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<InteractionCoinPickUppingBehaviour>((int)GlobalBehavioursAPI.InteractionCoinPickUppingBehaviour);
          }

        public static bool TryGetInteractionInputHandlingBehaviourBehaviour(this IEntity entity, out InteractionInputHandlingBehaviour interactionInputHandlingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.InteractionInputHandlingBehaviour, out interactionInputHandlingBehaviour);
         }

         public static bool TryAddInteractionInputHandlingBehaviourBehaviour(this IEntity entity, InteractionInputHandlingBehaviour interactionInputHandlingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.InteractionInputHandlingBehaviour, interactionInputHandlingBehaviour);
          }

         public static bool TryRemoveInteractionInputHandlingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<InteractionInputHandlingBehaviour>((int)GlobalBehavioursAPI.InteractionInputHandlingBehaviour);
          }

        public static bool TryGetMovementInputHandlingBehaviourBehaviour(this IEntity entity, out MovementInputHandlingBehaviour movementInputHandlingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.MovementInputHandlingBehaviour, out movementInputHandlingBehaviour);
         }

         public static bool TryAddMovementInputHandlingBehaviourBehaviour(this IEntity entity, MovementInputHandlingBehaviour movementInputHandlingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.MovementInputHandlingBehaviour, movementInputHandlingBehaviour);
          }

         public static bool TryRemoveMovementInputHandlingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<MovementInputHandlingBehaviour>((int)GlobalBehavioursAPI.MovementInputHandlingBehaviour);
          }

         #endregion
     }
}
