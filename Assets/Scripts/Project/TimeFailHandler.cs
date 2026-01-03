namespace RacquetPingPong
{
    using System.Threading;
    using UnityEngine;
    using TMPro;
    using Common;

    public class TimeFailHandler : MonoBehaviour
    {
        [SerializeField]
        private float _minMagnitude = 0.5f;

        private AnimationHandler _animation;
        private TextMeshProUGUI _timeLabel;
        private Ball _ball;
        private FinishMenu _menu;
        private CancellationTokenSource _tokenSource;

        private void Start()
        {
            _timeLabel = GetComponentInChildren<TextMeshProUGUI>();
            _animation = GetComponentInChildren<AnimationHandler>();
            _animation.Initialize();
            _ball = FindFirstObjectByType<Ball>();
            _menu = FindFirstObjectByType<FinishMenu>(FindObjectsInactive.Include);
        }

        private void Update()
        {
            if (_ball.Magnitude < _minMagnitude && _ball.IsSleeping == false)
                StartTime();
            else
                StopTime();
        }

        private async void StartTime()
        {
            if (_tokenSource == null)
            {
                _tokenSource = new CancellationTokenSource();

                try
                {
                    _timeLabel.text = "3";
                    await Awaitable.WaitForSecondsAsync(1f, _tokenSource.Token);
                    _animation.SetSmooth(1f);
                    await Awaitable.WaitForSecondsAsync(1f, _tokenSource.Token);
                    _timeLabel.text = "2";
                    await Awaitable.WaitForSecondsAsync(1f, _tokenSource.Token);
                    _timeLabel.text = "1";
                    await Awaitable.WaitForSecondsAsync(1f, _tokenSource.Token);
                    _timeLabel.text = "0";
                    await Awaitable.WaitForSecondsAsync(1f, _tokenSource.Token);
                    await _animation.SetSmoothAsync(0f);
                    _menu.Show();
                    _tokenSource = null;
                } catch { }
            }
        }

        private void StopTime()
        {
            if (_tokenSource != null)
            {
                _tokenSource?.Cancel();
                _tokenSource?.Dispose();
                _tokenSource = null;
                _animation.SetSmooth(0f);
            }
        }
    }
}