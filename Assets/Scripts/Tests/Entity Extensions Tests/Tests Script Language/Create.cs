using GameplayConstructorFramework.Entity;
using UnityEngine;

namespace Tests.EntityExtensionsTests.TestsScriptLanguage
{
    public static class Create
    {
        public static IEntity Entity()
        {
            return new Entity();
        }

        public static GameObject GameObject()
        {
            return new GameObject();
        }

        public static Component DamgerComponent()
        {
            var damagerGameObject = GameObject();
            var damgerComponent = damagerGameObject.transform;
            return damgerComponent;
        }
    }
}