// <copyright file="ModelSetAdapterTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Model2Entity.UnitTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Model2Entity;
    using Abbotware.Using.Castle;
    using Abbotware.Utility.UnitTest.Using.EntityFramework;
    using Abbotware.Utility.UnitTest.Using.EntityFramework.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using AutoMapper;
    using NUnit.Framework;

    [TestFixture]
    public class ModelSetAdapterTests : BaseNUnitTest
    {
        [Test]
        public async Task VerfiyModelSetAdapter()
        {
            using var c = CreateTestContainer();

            c.AddModel2EntitySupport();
            c.AddAutoMapper<DataProfile>();

            c.AddModelSetAdapter<OtherClass, ModelIntKey>(cfg =>
            {
                var l = SampleContext.CreateData();

                cfg.Queryable = (ct) => l.AsQueryable();
            });

            var fac = c.Resolve<IModelSetFactory>();

            var ms = fac.CreateReadOnly<OtherClass>();
            {
                var a = await ms.AllAsync(default);
                Assert.IsNotNull(a);
                Assert.AreEqual(10, a.Count());
            }

            {
                var w = await ms.WhereAsync(x => x.SomeId > 6, default);
                Assert.IsNotNull(w);
                Assert.AreEqual(3, w.Count());
                Assert.That(w.Select(x => x.SomeId), Has.All.GreaterThan(6));
            }

            {
                var w = await ms.WhereAsync(x => x.SomeEnum == SomeEnumType.Basic, default);
                Assert.IsNotNull(w);
                Assert.IsTrue(w.Any());
            }
        }

        [Test]
        public async Task VerfiyFindable()
        {
            using var c = CreateTestContainer();

            c.AddModel2EntitySupport();
            c.AddAutoMapper<DataProfile>();

            c.AddFindableAdapter<OtherClass, ModelIntKey, int>(cfg =>
            {
                var l = SampleContext.CreateData();

                cfg.SingleOrDefaultAsync = (key, ct) => Task.FromResult(l.SingleOrDefault(x => x.Id == key));
            });

            var fac = c.Resolve<IModelSetFactory>();

            var ms = fac.CreateFindable<OtherClass, int>();
            {
                var w = await ms.SingleOrDefaultAsync(5, default);
                Assert.IsNotNull(w);
                Assert.AreEqual(5, w.SomeId);
            }
        }

        public class OtherClass
        {
            public int SomeId { get; set; }

            public string SomeName { get; set; }

            public SomeEnumType SomeEnum { get; set; }
        }

        public class DataProfile : Profile
        {
            public DataProfile()
            {
                this.CreateMap<ModelIntKey, OtherClass>()
                    .ForMember(dest => dest.SomeId, m => m.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SomeName, m => m.MapFrom(src => src.Name))
                    .ForMember(dest => dest.SomeEnum, m => m.MapFrom(src => src.Setting))
                    .ReverseMap();
            }
        }
    }
}