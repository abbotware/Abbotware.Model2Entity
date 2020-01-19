// <copyright file="EntityKeyExpressionTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Model2Entity.UnitTests
{
    using Abbotware.Using.Castle;
    using Abbotware.Utility.UnitTest.Using.EntityFramework;
    using Abbotware.Utility.UnitTest.Using.EntityFramework.Models;
    using Castle.Windsor;
    using NUnit.Framework;

    [TestFixture]
    public class SampleContextTests
    {
        [Test]
        public void RegisterInMemory()
        {
            using IWindsorContainer c = new WindsorContainer();

            c.AddDbContext<SampleContext>()
                .UseInMemory();

            var ctx1 = c.Resolve<SampleContext>();
            Assert.NotNull(ctx1);

            using IWindsorContainer c2 = new WindsorContainer();

            c2.AddDbContext<SampleContext>()
                .UseInMemory("Test");

            var ctx2 = c2.Resolve<SampleContext>();
            Assert.NotNull(ctx2);
        }

        [Test]
        public void VerifyIsolationInMemory()
        {
            {
                using IWindsorContainer c = new WindsorContainer();

                c.AddDbContext<SampleContext>()
                    .UseInMemory();

                var ctx1 = c.Resolve<SampleContext>();

                Assert.That(ctx1.ModelIntKeys, Is.Empty);

                ctx1.ModelIntKeys.Add(new ModelIntKey { Id = 1, Name = "test" });
                ctx1.SaveChanges();

                Assert.That(ctx1.ModelIntKeys, Is.Not.Empty);
            }

            {
                // verify second container is blank
                using IWindsorContainer c2 = new WindsorContainer();

                c2.AddDbContext<SampleContext>()
                    .UseInMemory();

                var ctx2 = c2.Resolve<SampleContext>();
                Assert.That(ctx2.ModelIntKeys, Is.Empty);
            }
        }

        [Test]
        public void VerifyInMemoryPersitanceAcrossContexts()
        {
            {
                using IWindsorContainer c = new WindsorContainer();

                c.AddDbContext<SampleContext>()
                    .UseInMemory("ABC");

                var c1ctx1 = c.Resolve<SampleContext>();

                Assert.That(c1ctx1.ModelIntKeys, Is.Empty);

                var toInsert = new ModelIntKey { Id = 1, Name = "test" };

                c1ctx1.ModelIntKeys.Add(toInsert);
                c1ctx1.SaveChanges();

                Assert.That(c1ctx1.ModelIntKeys, Is.Not.Empty);

                var c1ctx2 = c.Resolve<SampleContext>();

                Assert.That(c1ctx2.ModelIntKeys, Is.Not.Empty);
            }

            {
                using IWindsorContainer c2 = new WindsorContainer();

                c2.AddDbContext<SampleContext>()
                .UseInMemory("ABC");

                var ctx2 = c2.Resolve<SampleContext>();

                Assert.That(ctx2.ModelIntKeys, Is.Not.Empty);
            }
        }
    }
}