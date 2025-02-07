using AtomicFramework.AtomicStructures;
using UnityEngine;
using GameplayConstructorFramework.Entity;
using Unity.Mathematics;
using InputActions;
using ObservableCollections;
using System.Collections.Generic;
using GameData;
using GameplayConstructorElements.Behaviours;
using GameplayConstructorElements.Behaviours.MovementModel;
using GameplayConstructorElements.Behaviours.InputHandlerModel;
using GameplayConstructorElements.Behaviours.Following_Model;
using GameplayConstructorElements.Behaviours.DeathModel;

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

        public static bool TryGetLeftHandData(this IEntity entity, out AtomicReactiveProperty<IEntity> leftHand)
         {
            return entity.TryGetData((int)GlobalDataAPI.LeftHand, out leftHand);
         }

         public static bool TryAddLeftHandData(this IEntity entity, AtomicReactiveProperty<IEntity> leftHand)
          {
            return entity.TryAddData((int)GlobalDataAPI.LeftHand, leftHand);
          }

         public static bool TryRemoveLeftHandData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.LeftHand);
          }

        public static bool TryGetCanFreeFallingData(this IEntity entity, out AtomicExpression<bool> canFreeFalling)
         {
            return entity.TryGetData((int)GlobalDataAPI.CanFreeFalling, out canFreeFalling);
         }

         public static bool TryAddCanFreeFallingData(this IEntity entity, AtomicExpression<bool> canFreeFalling)
          {
            return entity.TryAddData((int)GlobalDataAPI.CanFreeFalling, canFreeFalling);
          }

         public static bool TryRemoveCanFreeFallingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.CanFreeFalling);
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

        public static bool TryGetVelocityThresholdToJumpHangingData(this IEntity entity, out AtomicReactiveProperty<float> velocityThresholdToJumpHanging)
         {
            return entity.TryGetData((int)GlobalDataAPI.VelocityThresholdToJumpHanging, out velocityThresholdToJumpHanging);
         }

         public static bool TryAddVelocityThresholdToJumpHangingData(this IEntity entity, AtomicReactiveProperty<float> velocityThresholdToJumpHanging)
          {
            return entity.TryAddData((int)GlobalDataAPI.VelocityThresholdToJumpHanging, velocityThresholdToJumpHanging);
          }

         public static bool TryRemoveVelocityThresholdToJumpHangingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.VelocityThresholdToJumpHanging);
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

        public static bool TryGetJumpInputActionData(this IEntity entity, out AtomicEvent<float> jumpInputAction)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpInputAction, out jumpInputAction);
         }

         public static bool TryAddJumpInputActionData(this IEntity entity, AtomicEvent<float> jumpInputAction)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpInputAction, jumpInputAction);
          }

         public static bool TryRemoveJumpInputActionData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpInputAction);
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

        public static bool TryGetRightHandData(this IEntity entity, out AtomicReactiveProperty<IEntity> rightHand)
         {
            return entity.TryGetData((int)GlobalDataAPI.RightHand, out rightHand);
         }

         public static bool TryAddRightHandData(this IEntity entity, AtomicReactiveProperty<IEntity> rightHand)
          {
            return entity.TryAddData((int)GlobalDataAPI.RightHand, rightHand);
          }

         public static bool TryRemoveRightHandData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.RightHand);
          }

        public static bool TryGetUserData(this IEntity entity, out AtomicReactiveProperty<IEntity> user)
         {
            return entity.TryGetData((int)GlobalDataAPI.User, out user);
         }

         public static bool TryAddUserData(this IEntity entity, AtomicReactiveProperty<IEntity> user)
          {
            return entity.TryAddData((int)GlobalDataAPI.User, user);
          }

         public static bool TryRemoveUserData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.User);
          }

        public static bool TryGetLeftHandEquipActionEventData(this IEntity entity, out AtomicEvent<IEntity> leftHandEquipActionEvent)
         {
            return entity.TryGetData((int)GlobalDataAPI.LeftHandEquipActionEvent, out leftHandEquipActionEvent);
         }

         public static bool TryAddLeftHandEquipActionEventData(this IEntity entity, AtomicEvent<IEntity> leftHandEquipActionEvent)
          {
            return entity.TryAddData((int)GlobalDataAPI.LeftHandEquipActionEvent, leftHandEquipActionEvent);
          }

         public static bool TryRemoveLeftHandEquipActionEventData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.LeftHandEquipActionEvent);
          }

        public static bool TryGetRightHandEquipActionEventData(this IEntity entity, out AtomicEvent<IEntity> rightHandEquipActionEvent)
         {
            return entity.TryGetData((int)GlobalDataAPI.RightHandEquipActionEvent, out rightHandEquipActionEvent);
         }

         public static bool TryAddRightHandEquipActionEventData(this IEntity entity, AtomicEvent<IEntity> rightHandEquipActionEvent)
          {
            return entity.TryAddData((int)GlobalDataAPI.RightHandEquipActionEvent, rightHandEquipActionEvent);
          }

         public static bool TryRemoveRightHandEquipActionEventData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.RightHandEquipActionEvent);
          }

        public static bool TryGetTargetsInDamageZoneData(this IEntity entity, out List<IEntity> targetsInDamageZone)
         {
            return entity.TryGetData((int)GlobalDataAPI.TargetsInDamageZone, out targetsInDamageZone);
         }

         public static bool TryAddTargetsInDamageZoneData(this IEntity entity, List<IEntity> targetsInDamageZone)
          {
            return entity.TryAddData((int)GlobalDataAPI.TargetsInDamageZone, targetsInDamageZone);
          }

         public static bool TryRemoveTargetsInDamageZoneData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.TargetsInDamageZone);
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

        public static bool TryGetDamageTypeData(this IEntity entity, out AtomicReactiveProperty<DamageType> damageType)
         {
            return entity.TryGetData((int)GlobalDataAPI.DamageType, out damageType);
         }

         public static bool TryAddDamageTypeData(this IEntity entity, AtomicReactiveProperty<DamageType> damageType)
          {
            return entity.TryAddData((int)GlobalDataAPI.DamageType, damageType);
          }

         public static bool TryRemoveDamageTypeData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.DamageType);
          }

        public static bool TryGetDamageModifiersData(this IEntity entity, out ObservableDictionary<int, float> damageModifiers)
         {
            return entity.TryGetData((int)GlobalDataAPI.DamageModifiers, out damageModifiers);
         }

         public static bool TryAddDamageModifiersData(this IEntity entity, ObservableDictionary<int, float> damageModifiers)
          {
            return entity.TryAddData((int)GlobalDataAPI.DamageModifiers, damageModifiers);
          }

         public static bool TryRemoveDamageModifiersData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.DamageModifiers);
          }

        public static bool TryGetQuantityData(this IEntity entity, out AtomicReactiveProperty<int> quantity)
         {
            return entity.TryGetData((int)GlobalDataAPI.Quantity, out quantity);
         }

         public static bool TryAddQuantityData(this IEntity entity, AtomicReactiveProperty<int> quantity)
          {
            return entity.TryAddData((int)GlobalDataAPI.Quantity, quantity);
          }

         public static bool TryRemoveQuantityData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Quantity);
          }

        public static bool TryGetDeathEventData(this IEntity entity, out AtomicEvent deathEvent)
         {
            return entity.TryGetData((int)GlobalDataAPI.DeathEvent, out deathEvent);
         }

         public static bool TryAddDeathEventData(this IEntity entity, AtomicEvent deathEvent)
          {
            return entity.TryAddData((int)GlobalDataAPI.DeathEvent, deathEvent);
          }

         public static bool TryRemoveDeathEventData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.DeathEvent);
          }

        public static bool TryGetTransformData(this IEntity entity, out AtomicReactiveProperty<Transform> transform)
         {
            return entity.TryGetData((int)GlobalDataAPI.Transform, out transform);
         }

         public static bool TryAddTransformData(this IEntity entity, AtomicReactiveProperty<Transform> transform)
          {
            return entity.TryAddData((int)GlobalDataAPI.Transform, transform);
          }

         public static bool TryRemoveTransformData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.Transform);
          }

        public static bool TryGetTargetForFollowingData(this IEntity entity, out AtomicReactiveProperty<IEntity> targetForFollowing)
         {
            return entity.TryGetData((int)GlobalDataAPI.TargetForFollowing, out targetForFollowing);
         }

         public static bool TryAddTargetForFollowingData(this IEntity entity, AtomicReactiveProperty<IEntity> targetForFollowing)
          {
            return entity.TryAddData((int)GlobalDataAPI.TargetForFollowing, targetForFollowing);
          }

         public static bool TryRemoveTargetForFollowingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.TargetForFollowing);
          }

        public static bool TryGetFollowingDistanceData(this IEntity entity, out AtomicReactiveProperty<float3> followingDistance)
         {
            return entity.TryGetData((int)GlobalDataAPI.FollowingDistance, out followingDistance);
         }

         public static bool TryAddFollowingDistanceData(this IEntity entity, AtomicReactiveProperty<float3> followingDistance)
          {
            return entity.TryAddData((int)GlobalDataAPI.FollowingDistance, followingDistance);
          }

         public static bool TryRemoveFollowingDistanceData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.FollowingDistance);
          }

        public static bool TryGetJumpHeightData(this IEntity entity, out AtomicReactiveProperty<float> jumpHeight)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpHeight, out jumpHeight);
         }

         public static bool TryAddJumpHeightData(this IEntity entity, AtomicReactiveProperty<float> jumpHeight)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpHeight, jumpHeight);
          }

         public static bool TryRemoveJumpHeightData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpHeight);
          }

        public static bool TryGetCanJumpData(this IEntity entity, out AtomicExpression<bool> canJump)
         {
            return entity.TryGetData((int)GlobalDataAPI.CanJump, out canJump);
         }

         public static bool TryAddCanJumpData(this IEntity entity, AtomicExpression<bool> canJump)
          {
            return entity.TryAddData((int)GlobalDataAPI.CanJump, canJump);
          }

         public static bool TryRemoveCanJumpData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.CanJump);
          }

        public static bool TryGetIsGroundedData(this IEntity entity, out AtomicReactiveProperty<bool> isGrounded)
         {
            return entity.TryGetData((int)GlobalDataAPI.IsGrounded, out isGrounded);
         }

         public static bool TryAddIsGroundedData(this IEntity entity, AtomicReactiveProperty<bool> isGrounded)
          {
            return entity.TryAddData((int)GlobalDataAPI.IsGrounded, isGrounded);
          }

         public static bool TryRemoveIsGroundedData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.IsGrounded);
          }

        public static bool TryGetFallingGravityModifierData(this IEntity entity, out AtomicReactiveProperty<float> fallingGravityModifier)
         {
            return entity.TryGetData((int)GlobalDataAPI.FallingGravityModifier, out fallingGravityModifier);
         }

         public static bool TryAddFallingGravityModifierData(this IEntity entity, AtomicReactiveProperty<float> fallingGravityModifier)
          {
            return entity.TryAddData((int)GlobalDataAPI.FallingGravityModifier, fallingGravityModifier);
          }

         public static bool TryRemoveFallingGravityModifierData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.FallingGravityModifier);
          }

        public static bool TryGetMaxFreeFallingSpeedData(this IEntity entity, out AtomicReactiveProperty<float> maxFreeFallingSpeed)
         {
            return entity.TryGetData((int)GlobalDataAPI.MaxFreeFallingSpeed, out maxFreeFallingSpeed);
         }

         public static bool TryAddMaxFreeFallingSpeedData(this IEntity entity, AtomicReactiveProperty<float> maxFreeFallingSpeed)
          {
            return entity.TryAddData((int)GlobalDataAPI.MaxFreeFallingSpeed, maxFreeFallingSpeed);
          }

         public static bool TryRemoveMaxFreeFallingSpeedData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.MaxFreeFallingSpeed);
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

        public static bool TryGetUseInputActionData(this IEntity entity, out AtomicEvent useInputAction)
         {
            return entity.TryGetData((int)GlobalDataAPI.UseInputAction, out useInputAction);
         }

         public static bool TryAddUseInputActionData(this IEntity entity, AtomicEvent useInputAction)
          {
            return entity.TryAddData((int)GlobalDataAPI.UseInputAction, useInputAction);
          }

         public static bool TryRemoveUseInputActionData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.UseInputAction);
          }

        public static bool TryGetFreeFallingAccelerationData(this IEntity entity, out AtomicReactiveProperty<float> freeFallingAcceleration)
         {
            return entity.TryGetData((int)GlobalDataAPI.FreeFallingAcceleration, out freeFallingAcceleration);
         }

         public static bool TryAddFreeFallingAccelerationData(this IEntity entity, AtomicReactiveProperty<float> freeFallingAcceleration)
          {
            return entity.TryAddData((int)GlobalDataAPI.FreeFallingAcceleration, freeFallingAcceleration);
          }

         public static bool TryRemoveFreeFallingAccelerationData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.FreeFallingAcceleration);
          }

        public static bool TryGetCanIncreaseGravityData(this IEntity entity, out AtomicExpression<bool> canIncreaseGravity)
         {
            return entity.TryGetData((int)GlobalDataAPI.CanIncreaseGravity, out canIncreaseGravity);
         }

         public static bool TryAddCanIncreaseGravityData(this IEntity entity, AtomicExpression<bool> canIncreaseGravity)
          {
            return entity.TryAddData((int)GlobalDataAPI.CanIncreaseGravity, canIncreaseGravity);
          }

         public static bool TryRemoveCanIncreaseGravityData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.CanIncreaseGravity);
          }

        public static bool TryGetMeleeAttackActionEventData(this IEntity entity, out AtomicEvent<float, DamageType> meleeAttackActionEvent)
         {
            return entity.TryGetData((int)GlobalDataAPI.MeleeAttackActionEvent, out meleeAttackActionEvent);
         }

         public static bool TryAddMeleeAttackActionEventData(this IEntity entity, AtomicEvent<float, DamageType> meleeAttackActionEvent)
          {
            return entity.TryAddData((int)GlobalDataAPI.MeleeAttackActionEvent, meleeAttackActionEvent);
          }

         public static bool TryRemoveMeleeAttackActionEventData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.MeleeAttackActionEvent);
          }

        public static bool TryGetJumpInputKeyMaxPressingTimeData(this IEntity entity, out AtomicReactiveProperty<float> jumpInputKeyMaxPressingTime)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpInputKeyMaxPressingTime, out jumpInputKeyMaxPressingTime);
         }

         public static bool TryAddJumpInputKeyMaxPressingTimeData(this IEntity entity, AtomicReactiveProperty<float> jumpInputKeyMaxPressingTime)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpInputKeyMaxPressingTime, jumpInputKeyMaxPressingTime);
          }

         public static bool TryRemoveJumpInputKeyMaxPressingTimeData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpInputKeyMaxPressingTime);
          }

        public static bool TryGetIsJumpingData(this IEntity entity, out AtomicReactiveProperty<bool> isJumping)
         {
            return entity.TryGetData((int)GlobalDataAPI.IsJumping, out isJumping);
         }

         public static bool TryAddIsJumpingData(this IEntity entity, AtomicReactiveProperty<bool> isJumping)
          {
            return entity.TryAddData((int)GlobalDataAPI.IsJumping, isJumping);
          }

         public static bool TryRemoveIsJumpingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.IsJumping);
          }

        public static bool TryGetJumpInputKeyMaxPresingTimeData(this IEntity entity, out AtomicReactiveProperty<float> jumpInputKeyMaxPresingTime)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpInputKeyMaxPresingTime, out jumpInputKeyMaxPresingTime);
         }

         public static bool TryAddJumpInputKeyMaxPresingTimeData(this IEntity entity, AtomicReactiveProperty<float> jumpInputKeyMaxPresingTime)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpInputKeyMaxPresingTime, jumpInputKeyMaxPresingTime);
          }

         public static bool TryRemoveJumpInputKeyMaxPresingTimeData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpInputKeyMaxPresingTime);
          }

        public static bool TryGetJumpSpeedupModifierData(this IEntity entity, out AtomicReactiveProperty<float> jumpSpeedupModifier)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpSpeedupModifier, out jumpSpeedupModifier);
         }

         public static bool TryAddJumpSpeedupModifierData(this IEntity entity, AtomicReactiveProperty<float> jumpSpeedupModifier)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpSpeedupModifier, jumpSpeedupModifier);
          }

         public static bool TryRemoveJumpSpeedupModifierData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpSpeedupModifier);
          }

        public static bool TryGetJumpSlowModifierData(this IEntity entity, out AtomicReactiveProperty<float> jumpSlowModifier)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpSlowModifier, out jumpSlowModifier);
         }

         public static bool TryAddJumpSlowModifierData(this IEntity entity, AtomicReactiveProperty<float> jumpSlowModifier)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpSlowModifier, jumpSlowModifier);
          }

         public static bool TryRemoveJumpSlowModifierData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpSlowModifier);
          }

        public static bool TryGetJumpHangingDurationData(this IEntity entity, out AtomicReactiveProperty<float> jumpHangingDuration)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpHangingDuration, out jumpHangingDuration);
         }

         public static bool TryAddJumpHangingDurationData(this IEntity entity, AtomicReactiveProperty<float> jumpHangingDuration)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpHangingDuration, jumpHangingDuration);
          }

         public static bool TryRemoveJumpHangingDurationData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpHangingDuration);
          }

        public static bool TryGetCanJumpHangingData(this IEntity entity, out AtomicExpression<bool> canJumpHanging)
         {
            return entity.TryGetData((int)GlobalDataAPI.CanJumpHanging, out canJumpHanging);
         }

         public static bool TryAddCanJumpHangingData(this IEntity entity, AtomicExpression<bool> canJumpHanging)
          {
            return entity.TryAddData((int)GlobalDataAPI.CanJumpHanging, canJumpHanging);
          }

         public static bool TryRemoveCanJumpHangingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.CanJumpHanging);
          }

        public static bool TryGetIsJumpHangingData(this IEntity entity, out AtomicReactiveProperty<bool> isJumpHanging)
         {
            return entity.TryGetData((int)GlobalDataAPI.IsJumpHanging, out isJumpHanging);
         }

         public static bool TryAddIsJumpHangingData(this IEntity entity, AtomicReactiveProperty<bool> isJumpHanging)
          {
            return entity.TryAddData((int)GlobalDataAPI.IsJumpHanging, isJumpHanging);
          }

         public static bool TryRemoveIsJumpHangingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.IsJumpHanging);
          }

        public static bool TryGetSpeedExpressionData(this IEntity entity, out AtomicExpression<float> speedExpression)
         {
            return entity.TryGetData((int)GlobalDataAPI.SpeedExpression, out speedExpression);
         }

         public static bool TryAddSpeedExpressionData(this IEntity entity, AtomicExpression<float> speedExpression)
          {
            return entity.TryAddData((int)GlobalDataAPI.SpeedExpression, speedExpression);
          }

         public static bool TryRemoveSpeedExpressionData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.SpeedExpression);
          }

        public static bool TryGetJumpHangingMovementSpeedupModifierData(this IEntity entity, out AtomicReactiveProperty<float> jumpHangingMovementSpeedupModifier)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpHangingMovementSpeedupModifier, out jumpHangingMovementSpeedupModifier);
         }

         public static bool TryAddJumpHangingMovementSpeedupModifierData(this IEntity entity, AtomicReactiveProperty<float> jumpHangingMovementSpeedupModifier)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpHangingMovementSpeedupModifier, jumpHangingMovementSpeedupModifier);
          }

         public static bool TryRemoveJumpHangingMovementSpeedupModifierData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpHangingMovementSpeedupModifier);
          }

        public static bool TryGetIsMovingData(this IEntity entity, out AtomicReactiveProperty<bool> isMoving)
         {
            return entity.TryGetData((int)GlobalDataAPI.IsMoving, out isMoving);
         }

         public static bool TryAddIsMovingData(this IEntity entity, AtomicReactiveProperty<bool> isMoving)
          {
            return entity.TryAddData((int)GlobalDataAPI.IsMoving, isMoving);
          }

         public static bool TryRemoveIsMovingData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.IsMoving);
          }

        public static bool TryGetJumpHangingMovementSpeedupBonusDurationData(this IEntity entity, out AtomicReactiveProperty<float> jumpHangingMovementSpeedupBonusDuration)
         {
            return entity.TryGetData((int)GlobalDataAPI.JumpHangingMovementSpeedupBonusDuration, out jumpHangingMovementSpeedupBonusDuration);
         }

         public static bool TryAddJumpHangingMovementSpeedupBonusDurationData(this IEntity entity, AtomicReactiveProperty<float> jumpHangingMovementSpeedupBonusDuration)
          {
            return entity.TryAddData((int)GlobalDataAPI.JumpHangingMovementSpeedupBonusDuration, jumpHangingMovementSpeedupBonusDuration);
          }

         public static bool TryRemoveJumpHangingMovementSpeedupBonusDurationData(this IEntity entity)
          {
            return entity.TryRemoveData((int)GlobalDataAPI.JumpHangingMovementSpeedupBonusDuration);
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

        public static bool TryGetFreeFallingBehaviourBehaviour(this IEntity entity, out FreeFallingBehaviour freeFallingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.FreeFallingBehaviour, out freeFallingBehaviour);
         }

         public static bool TryAddFreeFallingBehaviourBehaviour(this IEntity entity, FreeFallingBehaviour freeFallingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.FreeFallingBehaviour, freeFallingBehaviour);
          }

         public static bool TryRemoveFreeFallingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<FreeFallingBehaviour>((int)GlobalBehavioursAPI.FreeFallingBehaviour);
          }

        public static bool TryGetJumpBehaviourBehaviour(this IEntity entity, out JumpBehaviour jumpBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.JumpBehaviour, out jumpBehaviour);
         }

         public static bool TryAddJumpBehaviourBehaviour(this IEntity entity, JumpBehaviour jumpBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.JumpBehaviour, jumpBehaviour);
          }

         public static bool TryRemoveJumpBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<JumpBehaviour>((int)GlobalBehavioursAPI.JumpBehaviour);
          }

        public static bool TryGetJumpHangingBehaviourBehaviour(this IEntity entity, out JumpHangingBehaviour jumpHangingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.JumpHangingBehaviour, out jumpHangingBehaviour);
         }

         public static bool TryAddJumpHangingBehaviourBehaviour(this IEntity entity, JumpHangingBehaviour jumpHangingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.JumpHangingBehaviour, jumpHangingBehaviour);
          }

         public static bool TryRemoveJumpHangingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<JumpHangingBehaviour>((int)GlobalBehavioursAPI.JumpHangingBehaviour);
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

        public static bool TryGetMoveSpeedupOnJumpHangingBehaviourBehaviour(this IEntity entity, out MoveSpeedupOnJumpHangingBehaviour moveSpeedupOnJumpHangingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.MoveSpeedupOnJumpHangingBehaviour, out moveSpeedupOnJumpHangingBehaviour);
         }

         public static bool TryAddMoveSpeedupOnJumpHangingBehaviourBehaviour(this IEntity entity, MoveSpeedupOnJumpHangingBehaviour moveSpeedupOnJumpHangingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.MoveSpeedupOnJumpHangingBehaviour, moveSpeedupOnJumpHangingBehaviour);
          }

         public static bool TryRemoveMoveSpeedupOnJumpHangingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<MoveSpeedupOnJumpHangingBehaviour>((int)GlobalBehavioursAPI.MoveSpeedupOnJumpHangingBehaviour);
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

        public static bool TryGetJumpInputHandlingBehaviourBehaviour(this IEntity entity, out JumpInputHandlingBehaviour jumpInputHandlingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.JumpInputHandlingBehaviour, out jumpInputHandlingBehaviour);
         }

         public static bool TryAddJumpInputHandlingBehaviourBehaviour(this IEntity entity, JumpInputHandlingBehaviour jumpInputHandlingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.JumpInputHandlingBehaviour, jumpInputHandlingBehaviour);
          }

         public static bool TryRemoveJumpInputHandlingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<JumpInputHandlingBehaviour>((int)GlobalBehavioursAPI.JumpInputHandlingBehaviour);
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

        public static bool TryGetUseInputHandlingBehaviourBehaviour(this IEntity entity, out UseInputHandlingBehaviour useInputHandlingBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.UseInputHandlingBehaviour, out useInputHandlingBehaviour);
         }

         public static bool TryAddUseInputHandlingBehaviourBehaviour(this IEntity entity, UseInputHandlingBehaviour useInputHandlingBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.UseInputHandlingBehaviour, useInputHandlingBehaviour);
          }

         public static bool TryRemoveUseInputHandlingBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<UseInputHandlingBehaviour>((int)GlobalBehavioursAPI.UseInputHandlingBehaviour);
          }

        public static bool TryGetFollowTargetWithDistanceAndLerpBehaviourBehaviour(this IEntity entity, out FollowTargetWithDistanceAndLerpBehaviour followTargetWithDistanceAndLerpBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.FollowTargetWithDistanceAndLerpBehaviour, out followTargetWithDistanceAndLerpBehaviour);
         }

         public static bool TryAddFollowTargetWithDistanceAndLerpBehaviourBehaviour(this IEntity entity, FollowTargetWithDistanceAndLerpBehaviour followTargetWithDistanceAndLerpBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.FollowTargetWithDistanceAndLerpBehaviour, followTargetWithDistanceAndLerpBehaviour);
          }

         public static bool TryRemoveFollowTargetWithDistanceAndLerpBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<FollowTargetWithDistanceAndLerpBehaviour>((int)GlobalBehavioursAPI.FollowTargetWithDistanceAndLerpBehaviour);
          }

        public static bool TryGetDeathBehaviourBehaviour(this IEntity entity, out DeathBehaviour deathBehaviour)
         {
            return entity.TryGetBehaviour((int)GlobalBehavioursAPI.DeathBehaviour, out deathBehaviour);
         }

         public static bool TryAddDeathBehaviourBehaviour(this IEntity entity, DeathBehaviour deathBehaviour)
          {
            return entity.TryAddBehaviour((int)GlobalBehavioursAPI.DeathBehaviour, deathBehaviour);
          }

         public static bool TryRemoveDeathBehaviourBehaviour(this IEntity entity)
          {
            return entity.TryRemoveBehaviour<DeathBehaviour>((int)GlobalBehavioursAPI.DeathBehaviour);
          }

         #endregion
     }
}
