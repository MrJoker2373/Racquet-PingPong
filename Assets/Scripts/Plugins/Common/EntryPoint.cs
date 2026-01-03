namespace Common
{
    using UnityEngine;

    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private float _delay = 0.25f;
        [SerializeField]
        private SceneID _startScene;

        private async void Awake()
        {
            DontDestroyOnLoad(gameObject);
            await Awaitable.WaitForSecondsAsync(_delay);
            SceneLoader.Instance.Load(_startScene);
        }
    }
}