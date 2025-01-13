using System;
using System.Collections.Generic;
using GameplayConstructorFramework.APIs;
using UnityEngine;

namespace GameplayConstructorFramework.Unity.Editor
{
    [Serializable]
    public class Section
    {
        [SerializeField] public string SectionName = null;
        [SerializeField] public List<DeclaratoryPair> Pairs = new();
    }
}