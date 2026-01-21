namespace Common
{
    using UnityEngine;
    using TMPro;

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationHandler : MonoBehaviour
    {
        [SerializeField]
        private string _ru;
        [SerializeField]
        private string _en;

        private void Start() => Initialize();

        public void Initialize()
        {
            var label = GetComponent<TextMeshProUGUI>();
            switch (LocalizationManager.Instance.ID)
            {
                case LocalizationID.En:
                    label.text = _en;
                    break;
                default:
                    label.text = _ru;
                    break;
            }
        }
    }
}