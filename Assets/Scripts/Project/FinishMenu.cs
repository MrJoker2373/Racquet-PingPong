namespace RacquetPingPong
{
    using UnityEngine;
    using UnityEngine.UI;
    using Common;

    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(AnimationHandler))]
    public class FinishMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _reviveButton;
        [SerializeField]
        private float _delay;

        private AnimationHandler _animation;
        private CanvasGroup _group;
        private BallCounter _counter;
        private Racquet _racquet;
        private Ball _ball;
        private bool _isRevived;

        private void Start()
        {
            _counter = FindFirstObjectByType<BallCounter>(FindObjectsInactive.Include);
            _group = GetComponent<CanvasGroup>();
            _animation = GetComponent<AnimationHandler>();
            _animation.Initialize();

            _racquet = FindFirstObjectByType<Racquet>();
            _ball = FindFirstObjectByType<Ball>();

            _restartButton.onClick.AddListener(Restart);
            _reviveButton.onClick.AddListener(StartRevive);

            if (DebugManager.Instance.IsDebug == false)
            {
                YandexHandler.Instance.ReviveFinished += FinishRevive;
                YandexHandler.Instance.ReviveCompleted += CompleteRevive;
            }
        }

        public async void Show()
        {
            PauseManager.Instance.Stop();
            _racquet.Disable();
            _group.blocksRaycasts = true;
            await _animation.SetSmoothAsync(1f);
            _group.interactable = true;
            if (DebugManager.Instance.IsDebug == false)
                YandexHandler.FullscreenAd();
        }

        public async Awaitable Hide()
        {
            _group.interactable = false;
            await _animation.SetSmoothAsync(0f);
            _group.blocksRaycasts = false;
            PauseManager.Instance.Play();
        }

        private void Restart()
        {
            UserHandler.Instance.SetScore(_counter.Count);
            SceneLoader.Instance.Load(SceneID.Game);
        }

        private async void StartRevive()
        {
            await SceneLoader.Instance.Transition.SetSmoothAsync(1f);
            if (DebugManager.Instance.IsDebug == false)
                YandexHandler.ReviveAd();
            else
            {
                CompleteRevive();
                FinishRevive();
            }
        }

        private async void FinishRevive()
        {
            float time = 0f;
            while (time < _delay)
            {
                time += Time.unscaledDeltaTime;
                await Awaitable.NextFrameAsync();
            }

            if (_isRevived == false)
                Restart();
            else
            {
                _isRevived = false;
                _racquet.Enable();
                _ball.ResetPosition();
                await Hide();
                await SceneLoader.Instance.Transition.SetSmoothAsync(0f);
            }
        }

        private void CompleteRevive() => _isRevived = true;
    }
}