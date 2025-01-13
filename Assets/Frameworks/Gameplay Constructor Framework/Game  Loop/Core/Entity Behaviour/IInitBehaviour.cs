namespace GameplayConstructor.Enitity.Behaviours
{
    public interface IInitBehaviour : IGameLoopBehaviour
    {
        public void Init();
        public void OnInit();
        
        public void Destroy();
        public void OnDestroy();
    }
}