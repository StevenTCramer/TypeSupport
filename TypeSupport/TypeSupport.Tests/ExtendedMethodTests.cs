﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TypeSupport.Tests.TestObjects;
using TypeSupport.Extensions;

namespace TypeSupport.Tests
{
    [TestFixture]
    public class ExtendedMethodTests
    {
        [Test]
        public void Should_CreateExtendedMethod()
        {
            var methods = typeof(BasicObject).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var em = new ExtendedMethod(methods.First());
            Assert.NotNull(em);
            Assert.IsNotEmpty(em.Name);
            Assert.IsTrue(em.IsGetter);
            Assert.IsTrue(em.IsAutoPropertyAccessor);
            Assert.NotNull(em.MethodInfo);
            Assert.AreEqual(typeof(BasicObject), em.MethodInfo.ReflectedType);
        }

        [Test]
        public void Should_DiscoverGetter()
        {
            var methods = typeof(BasicObject).GetMethods(MethodOptions.All);
            var method = methods.FirstOrDefault(x => x.Name == "get_Id");
            Assert.NotNull(method);
            Assert.IsTrue(method.IsGetter);
            Assert.IsTrue(method.IsAutoPropertyAccessor);
        }

        [Test]
        public void Should_DiscoverSetter()
        {
            var methods = typeof(BasicObject).GetMethods(MethodOptions.All);
            var method = methods.FirstOrDefault(x => x.Name == "set_Id");
            Assert.NotNull(method);
            Assert.IsTrue(method.IsSetter);
            Assert.IsTrue(method.IsAutoPropertyAccessor);
        }

        [Test]
        public void Should_DiscoverOverriddenMethods()
        {
            var methods = typeof(BasicObject).GetMethods(MethodOptions.All);
            var method = methods.FirstOrDefault(x => x.Name == "ToString" && x.DeclaringType == typeof(BasicObject));
            Assert.NotNull(method);
            Assert.IsTrue(method.IsOverride);
        }

        [Test]
        public void Should_DiscoverBaseMethods()
        {
            var methods = typeof(BasicObject).GetMethods(MethodOptions.All);
            var method = methods.FirstOrDefault(x => x.Name == "ToString" && x.DeclaringType == typeof(object));
            Assert.NotNull(method);
            Assert.IsFalse(method.IsOverride);
            Assert.IsTrue(method.IsOverridden);
        }
    }
}
