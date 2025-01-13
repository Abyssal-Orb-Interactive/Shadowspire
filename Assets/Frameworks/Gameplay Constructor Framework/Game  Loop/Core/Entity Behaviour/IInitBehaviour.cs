namespace Base.Testing.Enitity
{
    public interface IInitBehaviour : IGameLoopBehaviour
    {
        public void Init();
        public void OnInit();
        
        public void Destroy();
        public void OnDestroy();
    }
}