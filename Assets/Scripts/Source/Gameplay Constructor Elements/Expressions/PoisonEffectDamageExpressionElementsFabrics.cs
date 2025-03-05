using System;
using AtomicFramework.AtomicStructures;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFrameworkAPIs;
using Unity.Mathematics;

namespace GameplayConstructorElements.Expressions
{
    [Serializable]
    public sealed class PoisonEffectDamageExpressionBaseDamageElementFabric : IEntityFloatExpressionElementFabric
    {
        public Func<float> CreateFor(IEntity entity)
        {
            return () => entity.TryGetPoisoningEffectDamageInPercentsData(out var damage) ? damage.CurrentValue : 0f;
        }
    }
    [Serializable]
    public sealed class PoisonEffectDamageExponentialGrowthElementFabric : IEntityFloatExpressionElementFabric
    {
        public Func<float> CreateFor(IEntity entity)
        {
            return () =>
            {
                if (!entity.TryGetStartPoisoningDamageModifierData(out var startModifier))
                    return 1f;
                if(!entity.TryGetPoisoningDamageGrowthModifierData( out var growthModifier))
                    return 1f + startModifier.CurrentValue;
                if(!entity.TryGetNumberOfPoisoningEffectsData(out var effectCount))
                    return 1f + startModifier.CurrentValue;
                if (!entity.TryGetMaxNumberOfPoisoningEffectsData(out var maxEffects))
                    return 0f;
                
                if (effectCount.CurrentValue == 1) 
                    return 1f;
                
                var multiplier = 1f + startModifier.CurrentValue * math.exp(growthModifier.CurrentValue * effectCount.CurrentValue);
                var maxMultiplier = 1f + startModifier.CurrentValue * math.exp(growthModifier.CurrentValue * maxEffects.CurrentValue);

                return math.min(multiplier, maxMultiplier);
            };
        }
    }
    
    [Serializable]
    public sealed class PoisonEffectDamageLimitationFactorElementFabric : IEntityFloatExpressionElementFabric
    {
        public Func<float> CreateFor(IEntity entity)
        {
            return () =>
            {
                if (!entity.TryGetNumberOfPoisoningEffectsData(out var effectCount))
                    return 0f;
                if (!entity.TryGetMaxNumberOfPoisoningEffectsData(out var maxEffects))
                    return 0f;
                if (effectCount.CurrentValue == 1)
                    return 1f;
                if(effectCount.CurrentValue >= maxEffects.CurrentValue)
                    return 1f;
                
                var factor = 1f - (effectCount.CurrentValue - 1f) / (maxEffects.CurrentValue - 1f);
                
                return factor;
            };
        }
    }
}