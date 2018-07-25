﻿using System;
using Caster.TestScenario.Implementations;
using Caster.TestScenario.Interfaces;
using NUnit.Framework;

namespace Caster.TestScenario
{
    [TestFixture]
    public class ClassCasterTests
    {
        [Test]
        public void ClassCaster_DirectObject_CastsCorrectly()
        {
            // arrange
            var concrete = new Concrete();
            IInterface concreteToInterface = concrete;

            // act
            var result = concreteToInterface.Cast<Concrete>();

            // assert
            Assert.AreSame(concrete, result);
        }

        [Test]
        public void ClassCaster_WrongType_CastBreaks()
        {
            // arrange
            var concrete = new Concrete();
            IInterface concreteToInterface = concrete;

            // act
            Assert.Throws<InvalidCastException>(() =>
            {
                var result = concreteToInterface.Cast<DifferentConcrete>();
            });
        }
    }
}
