namespace Common
{
    using UnityEngine;

    public class PauseManager : MonoBehaviour
    {
        private float _lastTimeScale;

        public static PauseManager Instance { get; private set; }
        public bool IsPaused { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Time.timeScale = 1f;
            }
        }

        public void Play()
        {
            if (IsPaused == true)
            {
                Time.timeScale = _lastTimeScale;
                IsPaused = false;
            }
        }

        public void Stop()
        {
            if (IsPaused == false)
            {
                IsPaused = true;
                _lastTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
        }
    }
}