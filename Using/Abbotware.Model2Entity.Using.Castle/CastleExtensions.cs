// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
namespace Abbotware.Using.Castle
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Data.Configuration;
    using Abbotware.Data.Using.Castle.Fluent;
    using Abbotware.Data.Using.Castle.Fluent.Implementation;
    using Abbotware.Interop.AutoMapper;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Model2Entity;
    using Abbotware.Model2Entity.Adapters;
    using Abbotware.Model2Entity.Configuration;
    using Abbotware.Model2Entity.Configuration.Models;
    using AutoMapper;
    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Extension methods for castle registraion
    /// </summary>
    public static class CastleExtensions
    {
        /// <summary>
        /// Adds and configures entity framework features
        /// </summary>
        /// <param name="container">container</param>
        public static void AddEntityFrameworkSupport(this IWindsorContainer container)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<IDbContextFactory>().AsFactory());
        }

        /// <summary>
        /// Adds and configures Model2Entity / ModelSet features
        /// </summary>
        /// <param name="container">container</param>
        public static void AddModel2EntitySupport(this IWindsorContainer container)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<IModelSetFactory>().AsFactory());
        }

        /// <summary>
        /// Creates and registers a mapper with the provided profile
        /// </summary>
        /// <typeparam name="TProfile">mapping profile</typeparam>
        /// <param name="container">container</param>
        /// <param name="useExpressionMapper">flag to indicate expression mapper support</param>
        public static void AddAutoMapper<TProfile>(this IWindsorContainer container, bool useExpressionMapper = false)
            where TProfile : Profile, new()
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var mapper = AutoMapperHelper.Create<TProfile>(useExpressionMapper);

            container.Register(Component.For<IMapper>().Instance(mapper));
        }

        /// <summary>
        /// Creates and registers a mapper with the provided profile by resolving the profile's dependency's from the container
        /// </summary>
        /// <typeparam name="TProfile">mapping profile</typeparam>
        /// <param name="container">container</param>
        /// <param name="useExpressionMapper">flag to indicate expression mapper support</param>
        public static void AddAutoMapperUsingResolve<TProfile>(this IWindsorContainer container, bool useExpressionMapper = false)
            where TProfile : Profile
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var profile = container.RegisterAndResolve<TProfile>();

            var mapper = AutoMapperHelper.Create(useExpressionMapper, profile);

            container.Register(Component.For<IMapper>().Instance(mapper));
        }

        /// <summary>
        /// Adds a ModelSet Adapter
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <param name="container">container</param>
        /// <param name="configCallback">callback for configuration</param>
        public static void AddModelSetAdapter<TModel, TEntity>(this IWindsorContainer container, Action<ModelSetConfiguration<TEntity>> configCallback = null)
            where TEntity : class
        {
            Arguments.NotNull(container, nameof(container));

            var component = Component.For<ISearchableModelSet<TModel>>()
                .ImplementedBy<ModelSetAdapter<TModel, TEntity>>()
                .LifestyleTransient();

            if (configCallback != null)
            {
                var config = new ModelSetConfiguration<TEntity>();

                configCallback(config);

                component = component.DependsOn(Dependency.OnValue<IModelSetConfiguration<TEntity>>(config));
            }

            container.Register(component);
        }

        /// <summary>
        /// Adds a Findable adapter
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TKey">Key Type</typeparam>
        /// <param name="container">container</param>
        /// <param name="configCallback">callback for configuration</param>
        public static void AddFindableAdapter<TModel, TEntity, TKey>(this IWindsorContainer container, Action<FindableConfiguration<TEntity, TKey>> configCallback = null)
            where TEntity : class
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            Arguments.NotNull(container, nameof(container));

            var component = Component.For<IFindable<TModel, TKey>>()
                .ImplementedBy<Findable<TModel, TEntity, TKey>>()
                .LifestyleTransient();

            if (configCallback != null)
            {
                var config = new FindableConfiguration<TEntity, TKey>();

                configCallback(config);

                component = component.DependsOn(Dependency.OnValue<IFindableConfiguration<TEntity, TKey>>(config));
            }

            container.Register(component);
        }

        /// <summary>
        /// Adds a RelatedFindable adapter
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TKey">Key Type</typeparam>
        /// <param name="container">container</param>
        /// <param name="configCallback">callback for configuration</param>
        public static void AddRelatedFindableAdapter<TModel, TEntity, TKey>(this IWindsorContainer container, Action<RelatedFindableConfiguration<TEntity, TKey>> configCallback = null)
            where TEntity : class
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            Arguments.NotNull(container, nameof(container));

            var component = Component.For<IRelatedFindable<TModel, TKey>>()
                .ImplementedBy<RelatedFindable<TModel, TEntity, TKey>>()
                .LifestyleTransient();

            if (configCallback != null)
            {
                var config = new RelatedFindableConfiguration<TEntity, TKey>();

                configCallback(config);

                component = component.DependsOn(Dependency.OnValue<IRelatedFindableConfiguration<TEntity, TKey>>(config));
            }

            container.Register(component);
        }

        /// <summary>
        /// Adds a DbSet Adapter
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TContext">Context Type</typeparam>
        /// <param name="container">container</param>
        /// <param name="configCallback">callback for configuration</param>
        public static void AddDbSetAdapter<TModel, TEntity, TContext>(this IWindsorContainer container, Action<DbSetConfiguration<TEntity>> configCallback = null)
            where TEntity : class
            where TContext : DbContext
        {
            Arguments.NotNull(container, nameof(container));

            var component = Component.For<ISearchableModelSet<TModel>>()
                .ImplementedBy<DbSetAdapter<TModel, TEntity, TContext>>()
                .LifestyleTransient();

            if (configCallback != null)
            {
                var config = new DbSetConfiguration<TEntity>();

                configCallback(config);

                component = component.DependsOn(Dependency.OnValue<IDbSetConfiguration<TEntity>>(config));
            }

            container.Register(component);
        }

        /// <summary>
        /// Adds a DbSetAdapter with key lookup
        /// </summary>
        /// <typeparam name="TModel">Model Type</typeparam>
        /// <typeparam name="TEntity">Entity Type</typeparam>
        /// <typeparam name="TKey">Key Type</typeparam>
        /// <typeparam name="TContext">Context Type</typeparam>
        /// <param name="container">container</param>
        /// <param name="configCallback">callback for configuration</param>
        public static void AddDbSetAdapterSimpleKey<TModel, TEntity, TKey, TContext>(this IWindsorContainer container, Action<DbSetConfiguration<TEntity>> configCallback = null)
            where TEntity : class
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
            where TContext : DbContext
        {
            Arguments.NotNull(container, nameof(container));

            var component = Component.For<ISearchableModelSet<TModel, TKey>, ISearchableModelSet<TModel>>()
                .ImplementedBy<DbSetAdapterSimpleKey<TModel, TEntity, TKey, TContext>>()
                .LifestyleTransient();

            if (configCallback != null)
            {
                var config = new DbSetConfiguration<TEntity>();

                configCallback(config);

                component = component.DependsOn(Dependency.OnValue<IDbSetConfiguration<TEntity>>(config));
            }

            container.Register(component);
        }

        /// <summary>
        /// Adds and configures a DbContext to the configure
        /// </summary>
        /// <typeparam name="TContext">context type</typeparam>
        /// <param name="container">container</param>
        /// <returns>Add Context</returns>
        public static IAddDbContext<TContext> AddDbContext<TContext>(this IWindsorContainer container)
            where TContext : DbContext
        {
            Arguments.NotNull(container, nameof(container));

            return new AddDbContext<TContext>(container);
        }

        /// <summary>
        /// Registers an Abbotware BaseContext
        /// </summary>
        /// <typeparam name="TContext">context type</typeparam>
        /// <param name="container">container</param>
        /// <param name="options">connection configuration</param>
        /// <param name="adapter">options adapter</param>
        public static void RegisterDbContext<TContext>(this IWindsorContainer container, ISqlConnectionOptions options, IDbContextOptionsAdapter<TContext> adapter)
            where TContext : DbContext
        {
            container = Arguments.EnsureNotNull(container, nameof(container));
            adapter = Arguments.EnsureNotNull(adapter, nameof(adapter));

            Arguments.NotNull(options, nameof(options));

            container.Register(Component.For<TContext>()
                .ImplementedBy<TContext>()
                .LifestyleTransient()
                .DependsOn(Dependency.OnValue<DbContextOptions<TContext>>(adapter.Convert(options))));
        }
    }
}
