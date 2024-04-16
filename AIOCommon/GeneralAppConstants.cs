namespace AIOCommon
{
    /// <summary>
    /// Common constants for the application. 
    /// </summary>
    public static class GeneralAppConstants
    {
        /// <summary>
        /// Application name
        /// </summary>
        public const string AppName = "AIO";

        /// <summary>
        /// Application release year
        /// </summary>
        public const int AppReleaseYear = 2024;

        /// <summary>
        /// The default value for the current page.
        /// </summary>
		public const int CurrentPageDefaultValue = 1;

        /// <summary>
        /// The default value for the products per page.
        /// </summary>
		public const int ProductsPerPageDefaultValue = 3;

		/// <summary>
		/// The name of the area for the administrator.
		/// </summary>
		public const string AdminAreaName = "Admin";

        /// <summary>
        /// The name of the role for the administrator.
        /// </summary>
        public const string AdminRoleName = "Administrator";

		/// <summary>
		/// The email of the administrator.
		/// </summary>
		public const string DevelopmentAdminEmail = "admin@admin.bg";

        /// <summary>
        /// The uses cache key.
        /// </summary>
        public const string UsersCacheKey = "UsersCache";

        /// <summary>
        /// The uses cache duration in minutes.
        /// </summary>
        public const int UsersCacheDurationInMinutes = 5;

        /// <summary>
        /// Tne name of the cookie for the online users.
        /// </summary>
        public const string OnlineUsersCookieName = "IsOnline";

        /// <summary>
        /// The duration of the cookie for the online users.
        /// </summary>
        public const int LastActivityBeforeOfflineMinutes = 10;   
	}
}