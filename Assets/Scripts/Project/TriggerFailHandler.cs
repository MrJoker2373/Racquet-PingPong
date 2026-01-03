namespace RacquetPingPong
{
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class TriggerFailHandler : MonoBehaviour
    {
        private FinishMenu _menu;

        private void Start()
        {
            _menu = FindFirstObjectByType<FinishMenu>(FindObjectsInactive.Include);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Ball>())
                _menu.Show();
        }
    }
}