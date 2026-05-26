namespace TourCore.Application.Abstractions.Caching
{
    public static class CatalogCacheKeys
    {
        private const string Prefix = "catalog";

        public static string Countries => Prefix + ":countries";
        public static string Cities => Prefix + ":cities";
        public static string Resorts => Prefix + ":resorts";

        public static string HotelCategories => Prefix + ":hotel-categories";
        public static string Hotels => Prefix + ":hotels";

        public static string MealTypes => Prefix + ":meal-types";
        public static string RoomTypes => Prefix + ":room-types";
        public static string RoomCategories => Prefix + ":room-categories";
    }
}