// ReSharper disable once CheckNamespace - Common convention to locate extensions in Microsoft namespaces for simplifying autocompletion as a consumer.

namespace Microsoft.Extensions.DependencyInjection
{
	using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.MongoDB;
    using MongoDB.Driver;

	public static class MongoIdentityBuilderExtensions
	{
		/// <summary>
		///     This method only registers mongo stores, you also need to call AddIdentity.
		///     Consider using AddIdentityWithMongoStores.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="connectionString">Must contain the database name</param>
		public static IdentityBuilder RegisterMongoStores<TUser, TRole>(this IdentityBuilder builder, IMongoDatabase database)
			where TRole : IdentityRole
			where TUser : IdentityUser
		{
			return builder.RegisterMongoStores(
				p => database.GetCollection<TUser>("ApplicationUser"),
				p => database.GetCollection<TRole>("ApplicationRole"));
		}

		/// <summary>
		///     If you want control over creating the users and roles collections, use this overload.
		///     This method only registers mongo stores, you also need to call AddIdentity.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <param name="builder"></param>
		/// <param name="usersCollectionFactory"></param>
		/// <param name="rolesCollectionFactory"></param>
		public static IdentityBuilder RegisterMongoStores<TUser, TRole>(this IdentityBuilder builder,
			Func<IServiceProvider, IMongoCollection<TUser>> usersCollectionFactory,
			Func<IServiceProvider, IMongoCollection<TRole>> rolesCollectionFactory)
			where TRole : IdentityRole
			where TUser : IdentityUser
		{
			if (typeof(TUser) != builder.UserType)
			{
				var message = "User type passed to RegisterMongoStores must match user type passed to AddIdentity. "
				              + $"You passed {builder.UserType} to AddIdentity and {typeof(TUser)} to RegisterMongoStores, "
				              + "these do not match.";
				throw new ArgumentException(message);
			}
			if (typeof(TRole) != builder.RoleType)
			{
				var message = "Role type passed to RegisterMongoStores must match role type passed to AddIdentity. "
				              + $"You passed {builder.RoleType} to AddIdentity and {typeof(TRole)} to RegisterMongoStores, "
				              + "these do not match.";
				throw new ArgumentException(message);
			}
			builder.Services.AddSingleton<IUserStore<TUser>>(p => new UserStore<TUser, TRole>(usersCollectionFactory(p), rolesCollectionFactory(p)));
			builder.Services.AddSingleton<IRoleStore<TRole>>(p => new RoleStore<TRole>(rolesCollectionFactory(p)));
			return builder;
		}

        /// <summary>
        ///     This method registers identity services and MongoDB stores using the IdentityUser and IdentityRole types.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="database">mongodb database connext</param>
        public static IdentityBuilder AddIdentityWithMongoStores(this IServiceCollection services, IMongoDatabase database)
		{
			return services.AddIdentityWithMongoStoresUsingCustomTypes<IdentityUser, IdentityRole>(database);
		}

		/// <summary>
		///     This method allows you to customize the user and role type when registering identity services
		///     and MongoDB stores.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <param name="services"></param>
		/// <param name="connectionString">Connection string must contain the database name</param>
		public static IdentityBuilder AddIdentityWithMongoStoresUsingCustomTypes<TUser, TRole>(this IServiceCollection services, IMongoDatabase database)
			where TUser : IdentityUser
			where TRole : IdentityRole
		{
			return services.AddIdentity<TUser, TRole>()
				.RegisterMongoStores<TUser, TRole>(database);
		}
	}
}