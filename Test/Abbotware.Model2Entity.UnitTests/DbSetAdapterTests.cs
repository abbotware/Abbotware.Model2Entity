// <copyright file="EntityKeyExpressionTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Model2Entity.UnitTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Model2Entity;
    using Abbotware.Using.Castle;
    using Abbotware.Utility.UnitTest.Using.EntityFramework;
    using Abbotware.Utility.UnitTest.Using.EntityFramework.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using AutoMapper;
    using NUnit.Framework;

    [TestFixture]
    public class DbSetAdapterTests : BaseNUnitTest
    {
        [Test]
        public async Task VerfiyDbSetAdapter()
        {
            using var c = CreateTestContainer();

            c.AddEntityFrameworkSupport();
            c.AddModel2EntitySupport();

            c.AddDbContext<SampleContext>()
                .UseInMemory();

            var f = c.Resolve<IDbContextFactory>();

            using (var db = f.Create<SampleContext>())
            {
                db.ModelIntKeys.Add(new ModelIntKey { Id = 1, Name = "1" });
                db.ModelIntKeys.Add(new ModelIntKey { Id = 2, Name = "2" });
                db.ModelIntKeys.Add(new ModelIntKey { Id = 3, Name = "3" });
                db.SaveChanges();
            }

            c.AddAutoMapper<DataProfile>();

            c.AddDbSetAdapterSimpleKey<OtherClass, ModelIntKey, int, SampleContext>();

            var fac = c.Resolve<IModelSetFactory>();

            var ms = fac.CreateReadOnly<OtherClass, int>();
            {
                var a = await ms.AllAsync(default);
                Assert.IsNotNull(a);
                Assert.AreEqual(3, a.Count());
            }

            {
                var w = await ms.WhereAsync(x => x.SomeId > 1, default);
                Assert.IsNotNull(w);
                Assert.AreEqual(2, w.Count());
                Assert.AreEqual(3, w.Last().SomeId);
            }

            {
                var w2 = await ms.SingleOrDefaultAsync(1, default);
                Assert.IsNotNull(w2);
                Assert.AreEqual(1, w2.SomeId);
            }

            {
                var w = await ms.WhereAsync(x => x.SomeEnum == SomeEnum.Basic, default);
                Assert.IsNotNull(w);
                Assert.AreEqual(3, w.Count());
            }

            {
                var w = await ms.WhereAsync(x => x.SomeEnum == SomeEnum.Unknown, default);
                Assert.IsNotNull(w);
                Assert.AreEqual(0, w.Count());
            }
        }

        public class OtherClass : BaseClass
        {
            public int SomeId { get; set; }

            public string SomeName { get; set; }

            public override SomeEnum SomeEnum => SomeEnum.Basic;
        }

        public abstract class BaseClass
        {
            public abstract SomeEnum SomeEnum { get; }
        }

        public class DataProfile : Profile
        {
            public DataProfile()
            {
                this.CreateMap<ModelIntKey, OtherClass>()
                    .ForMember(dest => dest.SomeId, m => m.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SomeEnum, m => m.MapFrom(src => src.Setting))
                    .ForMember(dest => dest.SomeName, m => m.MapFrom(src => src.Name));
            }
        }
    }
}