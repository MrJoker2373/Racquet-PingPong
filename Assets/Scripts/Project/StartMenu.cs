namespace RacquetPingPong
{
    using UnityEngine;
    using Common;

    [RequireComponent(typeof(AnimationHandler))]
    public class StartMenu : MonoBehaviour
    {
        private AnimationHandler _animation;
        private BallCounter _counter;

        private void Start()
        {
            _counter = FindFirstObjectByType<BallCounter>();
            _animation = GetComponent<AnimationHandler>();
            _animation.Initialize();
            _animation.SetInstant(1f);
            PauseManager.Instance.Stop();
            InputReader.Instance.Subscribe<float>(InputID.Click, OnClickPerformed);
        }

        private async void OnClickPerformed(float input)
        {
            if (input > 0)
            {
                PauseManager.Instance.Play();
                await _animation.SetSmoothAsync(0f);
                _counter.Show();
                InputReader.Instance.Unsubscribe<float>(InputID.Click, OnClickPerformed);
            }
        }
    }
}