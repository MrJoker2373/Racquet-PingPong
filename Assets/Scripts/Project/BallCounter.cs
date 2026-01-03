namespace RacquetPingPong
{
    using UnityEngine;
    using TMPro;
    using Common;

    public class BallCounter : MonoBehaviour
    {
        private AnimationHandler _animation;
        private TextMeshProUGUI _countLabel;
        private Ball[] _balls;

        public int Count { get; private set; }

        private void Start() 
        {
            _animation = GetComponentInChildren<AnimationHandler>();
            _countLabel = GetComponentInChildren<TextMeshProUGUI>();
            _balls = FindObjectsByType<Ball>(FindObjectsSortMode.None);

            _animation.Initialize();
            foreach (var b in _balls)
                b.Hitted += OnBallHitted;
            UpdateText();
        }

        public void Show() => _animation.SetSmooth(1f);

        public void Hide() => _animation.SetSmooth(0f);

        private void OnBallHitted()
        {
            Count++;
            UpdateText();
        }

        private void UpdateText()
        {
            _countLabel.text = Count.ToString();
        }
    }
}