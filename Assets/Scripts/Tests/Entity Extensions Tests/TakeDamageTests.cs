using AtomicFramework.AtomicStructures;
using FluentAssertions;
using GameplayConstructorElements.EntityExtensions;
using GameplayConstructorFramework.Entity;
using GameplayConstructorFramework.Entity.Unity;
using GameplayConstructorFrameworkAPIs;
using NUnit.Framework;
using Tests.EntityExtensionsTests.TestsScriptLanguage;
using UnityEngine;

namespace Tests.EntityExtensionsTests
{
    public class TakeDamageTests
    {
        [Test]
        public void WhenEntityTryTakeDamage_AndEntityDoesNotHaveHealth_ThenShouldBeFalse()
        {
            // Arrange.
            var entity = Create.Entity();
            const float damage = 1f;
            
            // Act.
            var result = entity.TryTakeDamage(damage);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndEntityHasHealth_ThenShouldBeTrue()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            const float damage = 1f;
            
            // Act.
            var result = entity.TryTakeDamage(damage);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = true});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerEntityDoesNotHaveDamage_ThenShouldBeFalse()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damager = Create.Entity();
            
            // Act.
            var result = entity.TryTakeDamageFrom(damager);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerEntityHaveDamage_ThenShouldBeTrue()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damager = SetUp.EntityWithDamage(1f);
            
            // Act.
            var result = entity.TryTakeDamageFrom(damager);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = true});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerGameObjectDoesNotHaveEntity_ThenShouldBeFalse()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damagerGameObject = Create.GameObject();
            
            // Act.
            var result = entity.TryTakeDamageFrom(damagerGameObject);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerGameObjectHaveEntityWithoutDamage_ThenShouldBeFalse()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damagerGameObject = SetUp.DamagerGameObjectWithEntity();

            // Act.
            var result = entity.TryTakeDamageFrom(damagerGameObject);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerGameObjectHaveEntityWithDamage_ThenShouldBeTrue()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damagerGameObject = SetUp.DamagerGameObjectWithEntityWithDamage(1f);

            // Act.
            var result = entity.TryTakeDamageFrom(damagerGameObject);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = true});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerComponentDoesNotHaveEntity_ThenShouldBeFalse()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damagerComponent = Create.DamgerComponent();

            // Act.
            var result = entity.TryTakeDamageFrom(damagerComponent);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerComponentHaveEntityWithoutDamage_ThenShouldBeFalse()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damagerComponent = SetUp.DamagerComponentWithEntity();

            // Act.
            var result = entity.TryTakeDamageFrom(damagerComponent);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenEntityTryTakeDamage_AndDamagerComponentHaveEntityWithDamage_ThenShouldBeTrue()
        {
            // Arrange.
            var entity = SetUp.EntityWithHealth(10f);
            var damagerComponent = SetUp.DamagerComponentWithEntityWithDamage(1f);

            // Act.
            var result = entity.TryTakeDamageFrom(damagerComponent);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = true});
        }
        
        [Test]
        public void WhenColliderTryTakeDamage_AndColliderDoesNotHaveEntity_ThenShouldBeFalse()
        {
            // Arrange.
            var entityCollider = SetUp.EntityCollider();
            var damagerComponent = SetUp.DamagerComponentWithEntityWithDamage(1f);

            // Act.
            var result = entityCollider.TryTakeDamageFrom(damagerComponent);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenColliderTryTakeDamage_AndColliderHaveEntityWithoutHealth_ThenShouldBeFalse()
        {
            // Arrange.
            var entityCollider = SetUp.EntityColliderWithEntity();
            var damagerComponent = SetUp.DamagerComponentWithEntityWithDamage(1f);

            // Act.
            var result = entityCollider.TryTakeDamageFrom(damagerComponent);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = false});
        }
        
        [Test]
        public void WhenColliderTryTakeDamage_AndColliderHaveEntityWithHealth_ThenShouldBeTrue()
        {
            // Arrange.
            var entityCollider = SetUp.EntityColliderWithEntityWithHealth(10f);
            var damagerComponent = SetUp.DamagerComponentWithEntityWithDamage(1f);

            // Act.
            var result = entityCollider.TryTakeDamageFrom(damagerComponent);

            // Assert.
            new {DamageRecived = result}.Should().Be(new {DamageRecived = true});
        }
    }
}