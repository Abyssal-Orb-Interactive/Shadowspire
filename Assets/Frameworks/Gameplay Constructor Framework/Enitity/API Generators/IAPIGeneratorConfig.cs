namespace GameplayConstructorFramework.APIs
{
    public interface IAPIGeneratorConfig
    {
        public string APIsFolderPath { get; }
        public string APIsNamespace { get; }
    }
}