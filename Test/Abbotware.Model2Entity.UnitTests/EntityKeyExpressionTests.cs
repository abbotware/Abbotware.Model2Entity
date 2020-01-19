// <copyright file="EntityKeyExpressionTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Model2Entity.UnitTests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Using.Castle;
    using Abbotware.Utility.UnitTest.Using.EntityFramework;
    using Abbotware.Utility.UnitTest.Using.EntityFramework.Models;
    using Castle.Windsor;
    using NUnit.Framework;

    [TestFixture]
    public class EntityKeyExpressionTests
    {
        [Test]
        public void ModelKeys1()
        {
            var keys = EntityFrameworkHelper.PrimaryKeyNames<SingleKey>();

            Assert.That(keys, Has.Count.EqualTo(1));
            Assert.AreEqual(keys.First(), "StudentKey");
        }

        [Test]
        public void ModelKeys2()
        {
            var keys = EntityFrameworkHelper.PrimaryKeyNames<DoubleKey>();

            Assert.That(keys, Has.Count.EqualTo(2));
            Assert.AreEqual(keys.First(), "StudentKey");
            Assert.AreEqual(keys.Last(), "AdmissionNum");
        }

        [Test]
        public void ModelKeys3()
        {
            var keys = EntityFrameworkHelper.PrimaryKeyNames<TripleKey>();

            Assert.That(keys, Has.Count.EqualTo(3));
            Assert.AreEqual(keys.First(), "StudentKey");
            Assert.AreEqual(keys.Skip(1).First(), "AdmissionNum");
            Assert.AreEqual(keys.Last(), "Guid");
        }

        [Test]
        public void ModelKeys0()
        {
            var keys = EntityFrameworkHelper.PrimaryKeyNames<EntityKeyExpressionTests>();

            Assert.That(keys, Has.Count.Zero);
        }

        [Test]
        public void PrimaryKeyExpression_1()
        {
            var record = new SingleKey { StudentKey = 1 };

            var exp = EntityFrameworkHelper.PrimaryKeyExpression<SingleKey>();

            Assert.IsTrue(exp(record, new object[] { 1 }));
            Assert.IsFalse(exp(record, new object[] { 2 }));
        }

        [Test]
        public void FindExpression_1()
        {
            var record = new SingleKey { StudentKey = 1 };

            var exp1 = EntityFrameworkHelper.FindExpression<SingleKey>(1).Compile();
            var exp2 = EntityFrameworkHelper.FindExpression<SingleKey>(2).Compile();

            Assert.IsTrue(exp1(record));
            Assert.IsFalse(exp2(record));
        }

        [Test]
        public void PrimaryKeyExpression_2()
        {
            var record = new DoubleKey { StudentKey = 1, AdmissionNum = "123" };

            var exp = EntityFrameworkHelper.PrimaryKeyExpression<DoubleKey>();

            Assert.IsTrue(exp(record, new object[] { 1, "123" }));
            Assert.IsFalse(exp(record, new object[] { 2, string.Empty }));
        }

        [Test]
        public void FindExpression_2()
        {
            var record = new DoubleKey { StudentKey = 1, AdmissionNum = "123" };

            var exp1 = EntityFrameworkHelper.FindExpression<DoubleKey>(new object[] { 1, "123" }).Compile();
            var exp2 = EntityFrameworkHelper.FindExpression<DoubleKey>(new object[] { 2, string.Empty }).Compile();

            Assert.IsTrue(exp1(record));
            Assert.IsFalse(exp2(record));
        }

        [Test]
        public void PrimaryKeyExpression_3()
        {
            var g = Guid.NewGuid();
            var record = new TripleKey { StudentKey = 33, AdmissionNum = "1123223", Guid = g };

            var exp = EntityFrameworkHelper.PrimaryKeyExpression<TripleKey>();

            Assert.IsTrue(exp(record, new object[] { 33, "1123223", g }));
            Assert.IsFalse(exp(record, new object[] { 33, "1123223", Guid.NewGuid() }));
        }

        [Test]
        public void FindExpression_3()
        {
            var g = Guid.NewGuid();
            var record = new TripleKey { StudentKey = 33, AdmissionNum = "1123223", Guid = g };

            var exp1 = EntityFrameworkHelper.FindExpression<TripleKey>(new object[] { 33, "1123223", g }).Compile();
            var exp2 = EntityFrameworkHelper.FindExpression<TripleKey>(new object[] { 33, "1123223", Guid.NewGuid() }).Compile();

            Assert.IsTrue(exp1(record));
            Assert.IsFalse(exp2(record));
        }

        [Test]
        public void VerifyCompositeKey_EF_Metadata()
        {
            using IWindsorContainer c = new WindsorContainer();

            c.AddDbContext<SampleContext>()
                .UseInMemory();

            var ctx = c.Resolve<SampleContext>();

            var names = EntityFrameworkHelper.PrimaryKeyNames<CompositeStringKey>(ctx);

            Assert.That(names.Count() == 2);
            Assert.AreEqual(names.First(), "Id");
            Assert.AreEqual(names.Last(), "Name");
        }

        [Test]
        public void PrimaryKeyExpression_2_ViaContext()
        {
            using IWindsorContainer c = new WindsorContainer();

            c.AddDbContext<SampleContext>()
                .UseInMemory();

            var ctx = c.Resolve<SampleContext>();

            var record = new CompositeStringKey { Id = "1", Name = "123" };

            var exp = EntityFrameworkHelper.PrimaryKeyExpression<CompositeStringKey>(ctx);

            Assert.IsTrue(exp(record, new object[] { "1", "123" }));
            Assert.IsFalse(exp(record, new object[] { "2", string.Empty }));
        }

        public class SingleKey
        {
            [Key]
            public int StudentKey { get; set; }
        }

        public class DoubleKey
        {
            [Key]
            [Column(Order = 1)]
            public int StudentKey { get; set; }

            [Key]
            [Column(Order = 2)]
            public string AdmissionNum { get; set; }
        }

        public class TripleKey
        {
            [Key]
            [Column(Order = 1)]
            public int StudentKey { get; set; }

            [Key]
            [Column(Order = 3)]
            public Guid Guid { get; set; }

            [Key]
            [Column(Order = 2)]
            public string AdmissionNum { get; set; }
        }
    }
}