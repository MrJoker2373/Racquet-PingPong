namespace RacquetPingPong
{
    using System.Runtime.InteropServices;

    public static class YandexHandler
    {
        [DllImport("__Internal")]
        public static extern void FullscreenAd();
        [DllImport("__Internal")]
        public static extern void ReviveAd();
        [DllImport("__Internal")]
        public static extern void SetLeaderboard(int score);
        [DllImport("__Internal")]
        public static extern string GetLanguage();
    }
}