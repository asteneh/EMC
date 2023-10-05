namespace CNET_ERP_V7.WebConstants
{
    public static class CNET_WebConstantes
    {
        public static string ClaimsIssuer => "cnetemc";
        public static string CookieScheme => "cnet.emc.v6";
        public static string IdentificationCookie => "cnet.emc.v6.myId";
        public static string BranchCookie => "cnet.emc.v6.currentBranch";

        public const int IdentificationCookieLifeTime = 10080;
        public const int IdentificationCookieDailyLifeTime = 1440;
    }
}
