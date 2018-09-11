namespace Microsoft.AspNetCore.Identity.MongoDB
{
	using global::MongoDB.Driver;

	public static class IndexChecks
	{
        public static void EnsureUniqueIndexOnNormalizedUserName<TUser>(IMongoCollection<TUser> users)
            where TUser : IdentityUser
        {
            var userBuilder = Builders<TUser>.IndexKeys;
            var uniqueUserName = new CreateIndexModel<TUser>(userBuilder.Ascending(t => t.NormalizedUserName), new CreateIndexOptions { Unique = true });
            users.Indexes.CreateOneAsync(uniqueUserName);
		}

		public static void EnsureUniqueIndexOnNormalizedRoleName<TRole>(IMongoCollection<TRole> roles)
			where TRole : IdentityRole
		{
            var roleBuilder = Builders<TRole>.IndexKeys;
            var uniqueRoleName = new CreateIndexModel<TRole>(roleBuilder.Ascending(t => t.NormalizedName), new CreateIndexOptions { Unique = true });
			roles.Indexes.CreateOneAsync(uniqueRoleName);
		}

		public static void EnsureUniqueIndexOnNormalizedEmail<TUser>(IMongoCollection<TUser> users)
			where TUser : IdentityUser
		{
            var userBuilder = Builders<TUser>.IndexKeys;
            var uniqueEmail = new CreateIndexModel<TUser>(userBuilder.Ascending(t => t.NormalizedEmail), new CreateIndexOptions { Unique = true });
			users.Indexes.CreateOneAsync(uniqueEmail);
		}

		/// <summary>
		///     ASP.NET Core Identity now searches on normalized fields so these indexes are no longer required, replace with
		///     normalized checks.
		/// </summary>
		public static class OptionalIndexChecks
		{
			public static void EnsureUniqueIndexOnUserName<TUser>(IMongoCollection<TUser> users)
				where TUser : IdentityUser
			{
                var userBuilder = Builders<TUser>.IndexKeys;
                var uniqueUserName = new CreateIndexModel<TUser>(userBuilder.Ascending(t => t.UserName), new CreateIndexOptions { Unique = true });
                users.Indexes.CreateOneAsync(uniqueUserName);
			}

			public static void EnsureUniqueIndexOnRoleName<TRole>(IMongoCollection<TRole> roles)
				where TRole : IdentityRole
			{
                var roleBuilder = Builders<TRole>.IndexKeys;
                var uniqueRoleName = new CreateIndexModel<TRole>(roleBuilder.Ascending(t => t.Name), new CreateIndexOptions { Unique = true });
                roles.Indexes.CreateOneAsync(uniqueRoleName);
			}

			public static void EnsureUniqueIndexOnEmail<TUser>(IMongoCollection<TUser> users)
				where TUser : IdentityUser
            {
                var userBuilder = Builders<TUser>.IndexKeys;
                var uniqueEmail = new CreateIndexModel<TUser>(userBuilder.Ascending(t => t.Email), new CreateIndexOptions { Unique = true });
                users.Indexes.CreateOneAsync(uniqueEmail);
            }
		}
	}
}