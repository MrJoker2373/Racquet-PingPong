namespace RacquetPingPong
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Networking;
    using TMPro;
    using Common;

    public class UserHandler : MonoBehaviour
    {
        [SerializeField]
        private RawImage _avatar;
        [SerializeField]
        private TextMeshProUGUI _nameLabel;
        [SerializeField]
        private TextMeshProUGUI _scoreLabel;

        private UserData _data;

        [field: SerializeField]
        public static UserHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                if (DebugManager.Instance.IsDebug == true)
                    SetScore(0);
                else
                {
                    YandexHandler.Instance.UserLoaded += async (d) =>
                    {
                        LocalizationManager.Instance.Initialize(YandexHandler.GetLanguage());
                        _nameLabel.text = YandexHandler.GetUserName();
                        _avatar.texture = await LoadAvatar(YandexHandler.GetUserAvatar());
                        _data = d;
                        _scoreLabel.text = _data.Score.ToString();
                    };
                    YandexHandler.GetUserData();
                }
            }
        }

        public void SetScore(int score)
        {
            if (_data.Score < score)
            {
                _data.Score = score;
                _scoreLabel.text = _data.Score.ToString();
                if (DebugManager.Instance.IsDebug == false)
                {
                    YandexHandler.SetLeaderboardScore(_data.Score);
                    YandexHandler.Instance.Save(_data);
                }
            }
        }

        private async Awaitable<Texture> LoadAvatar(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
                return null;
            return ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}