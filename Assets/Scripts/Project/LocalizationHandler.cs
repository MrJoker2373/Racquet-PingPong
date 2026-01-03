namespace RacquetPingPong
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

        public void Initialize(LocalizationID id)
        {
            var label = GetComponent<TextMeshProUGUI>();
            switch (id)
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