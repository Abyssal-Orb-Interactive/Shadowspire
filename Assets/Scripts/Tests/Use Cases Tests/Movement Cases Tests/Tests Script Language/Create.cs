using UnityEngine;

namespace UseCasesTests.MovementModelTests.TestsScriptLanguage
{
    public static class Create
    {
        public static Rigidbody2D Rigidbody2D()
        {
            var gameObject = Tests.EntityExtensionsTests.TestsScriptLanguage.Create.GameObject();
            var rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            return rigidbody2D;
        }
    }
}