namespace RacquetPingPong
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class YandexHandler : MonoBehaviour
    {
        public event Action Revived;

        public static YandexHandler Instance { get; private set; }

        [DllImport("__Internal")]
        public static extern void FullscreenAd();
        [DllImport("__Internal")]
        public static extern void ReviveAd();
        [DllImport("__Internal")]
        public static extern void SetUserData(string data);
        [DllImport("__Internal")]
        public static extern string GetUserData();
        [DllImport("__Internal")]
        public static extern string SetLeaderboardScore(int score);
        [DllImport("__Internal")]
        public static extern string GetLanguage();
        [DllImport("__Internal")]
        public static extern string GetUserName();
        [DllImport("__Internal")]
        public static extern string GetUserAvatar();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void ReviveCompleted() => Revived?.Invoke();
    }
}