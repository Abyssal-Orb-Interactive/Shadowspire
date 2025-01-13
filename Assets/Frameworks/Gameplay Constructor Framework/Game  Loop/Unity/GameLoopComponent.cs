using UnityEngine;

namespace Base.UnityIntegration
{
    public class GameLoopComponent : MonoBehaviour
    {
        [SerializeField] private float _tickDelta = 0f;
        
        private float _tickTimer = 0f;

        public EntityBehaviourGameLoopService GameLoop = new();
        private void Start()
        {
            GameLoop.Awake();
        }

        private void Awake()
        {

            DontDestroyOnLoad(gameObject);
            
            GameLoop.Init();
        }

        private void OnEnable()
        {
            GameLoop.Awake();
        }

        private void Update()
        {
            GameLoop.FrameUpdate();
            
            _tickTimer += Time.deltaTime;

            if (_tickTimer < _tickDelta) return;
            
            _tickTimer = 0f;
            GameLoop.TickUpdate();
        }

        private void FixedUpdate()
        {
            GameLoop.PhysicsUpdate();
        }

        private void LateUpdate()
        {
            GameLoop.EndOfFrameUpdate();
        }

        private void OnDisable()
        {
            GameLoop.Sleep();
        }

        private void OnDestroy()
        {
            GameLoop.Destroy();
        }
    }
}