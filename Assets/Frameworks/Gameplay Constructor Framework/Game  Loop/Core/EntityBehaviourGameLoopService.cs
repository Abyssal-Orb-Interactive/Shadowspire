using System;
using System.Collections.Generic;
using Base.Testing.Enitity;
using UnityEngine;

namespace Base
{
    [Serializable]
    public sealed class EntityBehaviourGameLoopService
    {
        private readonly List<IInitBehaviour> _initBehaviours = null;
        private readonly List<ISleepingBehaviour> _sleepingBehaviours = null;
        private readonly List<IFrameRunBehaviour> _frameRunBehaviours = null;
        private readonly List<IPhysicsRunBehaviour> _physicsRunBehaviours = null;
        private readonly List<IEndOfFrameRunBehaviour> _endOfFrameRunBehaviours = null;
        private readonly List<ITickRunBehaviour> _tickRunBehaviours = null;
        private readonly HashSet<IGameLoopBehaviour> _behaviours = null;

        private readonly List<ITickRunBehaviour> _tickRunBehavioursToRemove = null;
        private readonly List<IFrameRunBehaviour> _frameRunBehavioursToRemove = null;
        private readonly List<IPhysicsRunBehaviour> _physicsRunBehavioursToRemove = null;
        private readonly List<IEndOfFrameRunBehaviour> _endOfFrameRunBehavioursToRemove = null;


        public EntityBehaviourGameLoopService()
        {
            _initBehaviours = new List<IInitBehaviour>();
            _sleepingBehaviours = new List<ISleepingBehaviour>();
            _frameRunBehaviours = new List<IFrameRunBehaviour>();
            _physicsRunBehaviours = new List<IPhysicsRunBehaviour>();
            _endOfFrameRunBehaviours = new List<IEndOfFrameRunBehaviour>();
            _tickRunBehaviours = new List<ITickRunBehaviour>();
            _behaviours = new HashSet<IGameLoopBehaviour>();
            _tickRunBehavioursToRemove = new List<ITickRunBehaviour>();
            _frameRunBehavioursToRemove = new List<IFrameRunBehaviour>();
            _physicsRunBehavioursToRemove = new List<IPhysicsRunBehaviour>();
            _endOfFrameRunBehavioursToRemove = new List<IEndOfFrameRunBehaviour>();
        }

        public void Init()
        {
            var initCount = _initBehaviours.Count;
            
            for (var i = 0; i < initCount; i++)
            {
                _initBehaviours[i].Init();
            }
        }

        public void Awake()
        {
            var sleepingCount = _sleepingBehaviours.Count;

            for (var i = 0; i < sleepingCount; i++)
            {
                _sleepingBehaviours[i].Awake();
            }
        }

        public void TickUpdate()
        {
            var tickToRemoveCount = _tickRunBehavioursToRemove.Count;

            for (var i = 0; i < tickToRemoveCount; i++)
            {
                var behaviour = _tickRunBehavioursToRemove[i];
                _tickRunBehaviours.Remove(behaviour);
            }
            
            _tickRunBehavioursToRemove.Clear();
            
            var tickCount = _tickRunBehaviours.Count;
            for (var i = 0; i < tickCount; i++)
            {
                _tickRunBehaviours[i].OnTickRun();
            }
        }

        public void FrameUpdate()
        {
            var frameToRemoveCount = _frameRunBehavioursToRemove.Count;

            for (var i = 0; i < frameToRemoveCount; i++)
            {
                var behaviour = _frameRunBehavioursToRemove[i];
                _frameRunBehaviours.Remove(behaviour);
            }
            
            _frameRunBehavioursToRemove.Clear();
            
            var frameRunCount = _frameRunBehaviours.Count;
            for (var i = 0; i < frameRunCount; i++)
            {
                _frameRunBehaviours[i].OnFrameRun();
            }
        }
        
        public void PhysicsUpdate()
        {
            var physicsToRemoveCount = _physicsRunBehavioursToRemove.Count;

            for (var i = 0; i < physicsToRemoveCount; i++)
            {
                var behaviour = _physicsRunBehavioursToRemove[i];
                _physicsRunBehaviours.Remove(behaviour);
            }
            
            _physicsRunBehavioursToRemove.Clear();
            
            var physicsRunCount = _physicsRunBehaviours.Count;
            for (var i = 0; i < physicsRunCount; i++)
            {
                _physicsRunBehaviours[i].OnPhysicsFrameRun();
            }
        }
        
        public void EndOfFrameUpdate()
        {
            var endOfFrameToRemoveCount = _endOfFrameRunBehavioursToRemove.Count;

            for (var i = 0; i < endOfFrameToRemoveCount; i++)
            {
                var behaviour = _endOfFrameRunBehavioursToRemove[i];
                _endOfFrameRunBehaviours.Remove(behaviour);
            }
            
            _endOfFrameRunBehavioursToRemove.Clear();
            
            var endOfFrameRunCount = _endOfFrameRunBehaviours.Count;
            for (var i = 0; i < endOfFrameRunCount; i++)
            {
                _endOfFrameRunBehaviours[i].OnEndOfFrameRun();
            }
        }

        public void Sleep()
        {
            var sleepingCount = _sleepingBehaviours.Count;

            for (var i = 0; i < sleepingCount; i++)
            {
                _sleepingBehaviours[i].Sleep();
            }
        }
        
        public void Destroy()
        {
            var initCount = _initBehaviours.Count;
            
            for (var i = 0; i < initCount; i++)
            {
                _initBehaviours[i].Destroy();
            }
        }

        public void Add(IGameLoopBehaviour behaviour)
        {
            if(_behaviours.Contains(behaviour)) return;
            _behaviours.Add(behaviour);
            
            if (behaviour is IInitBehaviour initBehaviour)
            {
                _initBehaviours.Add(initBehaviour);
                initBehaviour.Init();
            }

            if (behaviour is ISleepingBehaviour sleepingBehaviour)
            {
                _sleepingBehaviours.Add(sleepingBehaviour);
                sleepingBehaviour.Awake();
            }

            if (behaviour is IFrameRunBehaviour frameRunBehaviour)
            {
                _frameRunBehaviours.Add(frameRunBehaviour);
            }

            if (behaviour is ITickRunBehaviour tickRunBehaviour)
            {
                _tickRunBehaviours.Add(tickRunBehaviour);
            }

            if (behaviour is IPhysicsRunBehaviour physicsRunBehaviour)
            {
                _physicsRunBehaviours.Add(physicsRunBehaviour);
            }

            if (behaviour is IEndOfFrameRunBehaviour endOfFrameRunBehaviour)
            {
                _endOfFrameRunBehaviours.Add(endOfFrameRunBehaviour);
            }

        }
        
        public void Remove(IGameLoopBehaviour behaviour)
        {
            if (behaviour is IInitBehaviour initBehaviour)
            {
                initBehaviour.Destroy();
                _initBehaviours.Remove(initBehaviour);
            }

            if (behaviour is ISleepingBehaviour sleepingBehaviour)
            {
                sleepingBehaviour.Sleep();
                _sleepingBehaviours.Remove(sleepingBehaviour);
            }
            if (behaviour is IFrameRunBehaviour frameRunBehaviour) _frameRunBehavioursToRemove.Add(frameRunBehaviour);
            if(behaviour is ITickRunBehaviour tickRunBehaviour) _tickRunBehavioursToRemove.Add(tickRunBehaviour);
            if(behaviour is IPhysicsRunBehaviour physicsRunBehaviour) _physicsRunBehavioursToRemove.Add(physicsRunBehaviour);
            if(behaviour is IEndOfFrameRunBehaviour endOfFrameRunBehaviour) _endOfFrameRunBehavioursToRemove.Add(endOfFrameRunBehaviour);
        }
    }
}