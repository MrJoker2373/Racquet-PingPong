namespace Common
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private float _delay = 0.75f;

        private const string SCENE_SUFFIX = "Scene";
        private ISceneListener[] _listeners;
        private ISceneTransition _transition;

        public static SceneLoader Instance { get; private set; }
        public ISceneTransition Transition => IsLoading ? null : _transition;
        public bool IsLoading { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _transition = GetComponentInChildren<ISceneTransition>(includeInactive: true);
                _transition.Initialize();
                _listeners = GetComponentsInChildren<ISceneListener>(includeInactive: true);
                foreach (var l in _listeners)
                    l.Initialize();
            }
        }

        public async void Load(SceneID id) => await LoadAsync(id);

        public async Awaitable LoadAsync(SceneID id)
        {
            if (IsLoading == false)
            {
                IsLoading = true;
                await _transition.SetSmoothAsync(1f);

                foreach (var l in _listeners)
                    l.HandleStart();

                string name = id.ToString() + SCENE_SUFFIX;
                var operation = SceneManager.LoadSceneAsync(name);
                while (operation.isDone == false)
                {
                    float progress = Mathf.Clamp01(operation.progress / 0.9f);
                    foreach (var l in _listeners)
                        l.HandleUpdate(progress);
                    await Awaitable.NextFrameAsync();
                }

                foreach (var l in _listeners)
                    l.HandleFinish();

                float time = 0f;
                while (time < _delay)
                {
                    time += Time.unscaledDeltaTime;
                    await Awaitable.NextFrameAsync();
                }

                await _transition.SetSmoothAsync(0f);
                IsLoading = false;
            }
        }
    }
}