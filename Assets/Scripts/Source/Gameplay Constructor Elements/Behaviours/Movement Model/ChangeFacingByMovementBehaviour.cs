using System;
using AtomicFramework.AtomicStructures;
using GameData;
using GameplayConstructor.Enitity.Behaviours;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;

namespace GameplayConstructorElements.Behaviours.MovementModel
{
    [Serializable]
    public sealed class ChangeFacingByMovementBehaviour : BehaviourBase, IInitBehaviour, ISleepingBehaviour, IDisposable
    {
        #region Cache Varables

        private IAtomicVariable<Facing> _currentFacing = null;
        private IReadonlyAtomicReactiveProperty<IEntity> _inputHandler = null;
        private ReactiveLibraryFacade.IObservable<float2> _movementInputAction = null;
        private IAtomicValue<bool> _canFace = null;

        #endregion
        
        #region Subscriptions

        private IDisposable _subscription = null;

        #endregion
        
        #region Constructors

        public ChangeFacingByMovementBehaviour() {}
        public ChangeFacingByMovementBehaviour(IEntity entity) : base(entity) {}
        
        #endregion

        #region Life Cycle Methods
        
        public void Init()
        {
            _entity.TryGetInputHandlerData(out var inputHandler);
            _inputHandler = inputHandler;
            
            _entity.TryGetCurrentFacingData(out var currentFacing);
            _currentFacing = currentFacing;

            _entity.TryGetCanFaceData(out var canFace);
            _canFace = canFace;
            
            OnInit();
        }

        public void OnInit()
        {
            _inputHandler.CurrentValue.TryGetMovementInputActionData(out var movementInputAction);
            _movementInputAction = movementInputAction;
        }
        
        public void Awake()
        {
            Dispose();
            OnAwake();
        }

        public void OnAwake()
        {
            _subscription = _movementInputAction.Subscribe(OnMovement);
        }

        private void OnMovement(float2 direction)
        {
            if (!_canFace.CurrentValue) return;
            
            _currentFacing.Value = direction.x switch
            {
                > 0 => Facing.Right,
                < 0 => Facing.Left,
                _ => _currentFacing.CurrentValue
            };
        }

        public void Sleep()
        {
            OnSleep();
        }

        public void OnSleep()
        {
            Dispose();
        }

        public void Destroy()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
        
        #endregion
    }
}