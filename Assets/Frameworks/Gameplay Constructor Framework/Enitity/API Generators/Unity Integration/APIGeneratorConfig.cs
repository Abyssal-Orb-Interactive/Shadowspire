using GameplayConstructorFramework.APIs;
using UnityEngine;

namespace GameplayConstructorFramework.Unity
{
    [CreateAssetMenu(fileName = nameof(APIGeneratorConfig), menuName = "Tools/Configs/" + nameof(APIGeneratorConfig))]
    public class APIGeneratorConfig : ScriptableObject, IAPIGeneratorConfig
    {
        [field: SerializeField] public string APIsFolderPath { get; private set; } =
            "D:/Projects/Civitanomica/Assets/Scripts/Generated/DataKeyAPIs";
        [field: SerializeField] public string APIsNamespace { get; private set; } =
            "GameplayConstructorFrameworkAPIs";
        
    }
}