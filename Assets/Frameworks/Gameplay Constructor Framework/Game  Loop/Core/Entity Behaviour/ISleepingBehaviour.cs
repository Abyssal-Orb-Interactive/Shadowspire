namespace GameplayConstructor.Enitity.Behaviours
{
    public interface ISleepingBehaviour : IGameLoopBehaviour
    {
        public void Sleep();
        public void Awake();
        
        public void OnSleep();
        public void OnAwake();
    }
}