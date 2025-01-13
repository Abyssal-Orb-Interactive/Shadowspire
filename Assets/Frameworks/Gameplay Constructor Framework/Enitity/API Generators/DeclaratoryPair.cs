using System;

namespace GameplayConstructorFramework.APIs
{
    [Serializable]
    public struct DeclaratoryPair
    {
        public string Key;
        public string Type;

        public DeclaratoryPair(string key, string type)
        {
            Key = key;
            Type = type;
        }
    }
}