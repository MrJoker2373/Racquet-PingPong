namespace RacquetPingPong
{
    using UnityEngine;
    using Common;

    [ExecuteAlways]
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField]
        private LocalizationID _id;

        private void OnValidate() => Initialize(_id);

        private void Start()
        {
            if (DebugManager.Instance.IsDebug == false)
            {
                var lang = YandexHandler.GetLanguage();
                switch (lang)
                {
                    case "en":
                        Initialize(LocalizationID.En);
                        break;
                    default:
                        Initialize(LocalizationID.Ru);
                        break;
                }
            }
        }

        private void Initialize(LocalizationID id)
        {
            var handlers = FindObjectsByType<LocalizationHandler>(FindObjectsSortMode.None);
            foreach (var h in handlers)
                h.Initialize(_id);
        }
    }
}