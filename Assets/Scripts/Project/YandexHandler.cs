namespace RacquetPingPong
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class YandexHandler : MonoBehaviour
    {
        public static event Action ReviveFinished;
        public static event Action ReviveCompleted;
        public static event Action<string> DataLoaded;

        [DllImport("__Internal")]
        public static extern void FullscreenAd();
        [DllImport("__Internal")]
        public static extern void ReviveAd();
        [DllImport("__Internal")]
        public static extern bool IsAuthorized();
        [DllImport("__Internal")]
        public static extern string GetLanguage();
        [DllImport("__Internal")]
        public static extern string GetName();
        [DllImport("__Internal")]
        public static extern string GetAvatar();
        [DllImport("__Internal")]
        public static extern string SetLeaderboard(int score);
        [DllImport("__Internal")]
        public static extern void SaveData(string data);
        [DllImport("__Internal")]
        public static extern void LoadData();

        public void OnReviveFinished() => ReviveFinished?.Invoke();

        public void OnReviveCompleted() => ReviveCompleted?.Invoke();

        public void OnDataLoaded(string data) => DataLoaded?.Invoke(data);
    }
}