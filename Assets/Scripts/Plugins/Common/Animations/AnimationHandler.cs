namespace Common
{
    using System.Threading;
    using UnityEngine;

    public abstract class AnimationHandler : MonoBehaviour
    {
        [SerializeField]
        private float _duration = 0.25f;
        [SerializeField]
        private EasingType _easing;

        private CancellationTokenSource _tokenSource;

        public bool IsActive => gameObject.activeSelf;
        public bool IsAnimating => _tokenSource != null;

        public virtual void Initialize() => SetInstant(0f);

        public void SetActive(bool value) => gameObject.SetActive(value);

        public void SetDuration(float value) => _duration = Mathf.Clamp(value, 0, float.MaxValue);

        public void SetEasing(EasingType easing) => _easing = easing;

        public void SetInstant(float value)
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;

            HandleInstant(value);
            SetActive(value > 0);
        }

        public async void SetSmooth(float value) => await SetSmoothAsync(value);

        public async Awaitable SetSmoothAsync(float value)
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = new();

            try
            {
                SetActive(true);
                HandlePrepareSmooth(value);
                float time = 0f;
                while (time < _duration)
                {
                    time += Time.unscaledDeltaTime;
                    float t = EasingHandler.Ease(time / _duration, _easing);
                    HandleUpdateSmooth(t);
                    await Awaitable.NextFrameAsync(_tokenSource.Token);
                }
                SetActive(value > 0);
                _tokenSource = null;
            } catch { }
        }

        protected abstract void HandleInstant(float value);

        protected abstract void HandlePrepareSmooth(float value);

        protected abstract void HandleUpdateSmooth(float t);
    }
}