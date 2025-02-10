using Unity.Mathematics;
using UnityEngine;

namespace UseCasesTests.MovementModelTests.TestsScriptLanguage
{
    public static class SetUp
    {
        public static Rigidbody2D Rigidbody2DWithPosition(float3 position = default)
        {
            var rigidbody2D = Create.Rigidbody2D();
            rigidbody2D.gameObject.transform.position = position;
            return rigidbody2D;
        }
    }
}