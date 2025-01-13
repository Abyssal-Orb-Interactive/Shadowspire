using System;
using System.Collections.Generic;
using System.Linq;
using GameplayConstructor.Enitity.Behaviours;

namespace GameplayConstructorFramework.APIs
{
    public static class BehavioursScanner
    {
        public static IEnumerable<Type> GetAllBehaviours()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => typeof(IGameLoopBehaviour).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToArray();

            return types;
        } 
    }
}