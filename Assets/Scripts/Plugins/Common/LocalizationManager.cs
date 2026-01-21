namespace Common
{
    using UnityEngine;

    public class LocalizationManager : MonoBehaviour
    {
        [field: SerializeField]
        public LocalizationID ID { get; private set; }

        public static LocalizationManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void Initialize(string lang)
        {
            switch (lang)
            {
                case "en":
                    ID = LocalizationID.En;
                    break;
                default:
                    ID = LocalizationID.Ru;
                    break;
            }
        }
    }
}