namespace MagicVilla_Utility
{
    public static class SD
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }

        public static string SessionToken { get; set; } = "JWTToken";
    }
}
