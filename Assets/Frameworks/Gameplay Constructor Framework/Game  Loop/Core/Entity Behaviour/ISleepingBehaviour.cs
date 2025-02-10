namespace GameplayConstructor.Enitity.Behaviours
{
    public interface ISleepingBehaviour : IGameLoopBehaviour
    {
        public void Awake();
        public void OnAwake();
        public void Sleep();
        public void OnSleep();
        
    }
}