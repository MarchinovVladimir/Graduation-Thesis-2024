namespace AIOCommon
{
    public static class GeneralAppConstants
    {
        public const string AppName = "AIO";
        public const int AppReleaseYear = 2024;

		public const int CurrentPageDefaultValue = 1;
		public const int ProductsPerPageDefaultValue = 3;

        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "admin@admin.bg";

        public const string UsersCacheKey = "UsersCache";
        public const int UsersCacheDurationInMinutes = 5;

        public const string OnlineUsersCookieName = "IsOnline";
        public const int LastActivityBeforeOfflineMinutes = 10;   
	}
}