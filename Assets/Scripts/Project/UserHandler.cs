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
                _scoreLabel.text = _data.Score.ToString();
                _nameLabel.gameObject.SetActive(false);
                _avatar.gameObject.SetActive(false);

                if (DebugManager.Instance.IsDebug == false)
                {
                    LocalizationManager.Instance.Initialize(YandexHandler.GetLanguage());
                    if (YandexHandler.IsAuthorized() == true)
                    {
                        YandexHandler.DataLoaded += LoadData;
                        YandexHandler.LoadData();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            if (DebugManager.Instance.IsDebug == false && YandexHandler.IsAuthorized() == true)
                YandexHandler.DataLoaded -= LoadData;
        }

        public void SetScore(int score)
        {
            if (_data.Score < score)
            {
                _data.Score = score;
                _scoreLabel.text = _data.Score.ToString();
                if (DebugManager.Instance.IsDebug == false && YandexHandler.IsAuthorized())
                    SaveData();
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

        private void SaveData()
        {
            YandexHandler.SaveData(JsonUtility.ToJson(_data));
            YandexHandler.SetLeaderboard(_data.Score);
        }

        private async void LoadData(string data)
        {
            _nameLabel.gameObject.SetActive(true);
            _avatar.gameObject.SetActive(true);
            _nameLabel.text = YandexHandler.GetName();
            _avatar.texture = await LoadAvatar(YandexHandler.GetAvatar());
            _data = JsonUtility.FromJson<UserData>(data);
            _scoreLabel.text = _data.Score.ToString();
        }
    }
}