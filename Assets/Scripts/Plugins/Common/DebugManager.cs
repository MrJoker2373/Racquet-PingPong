namespace Common
{
    using UnityEngine;

    public class DebugManager : MonoBehaviour
    {
        [SerializeField]
        private int _frameRate;
        [SerializeField]
        private bool _isMobile;
        [SerializeField]
        private bool _isConsole;

        public static DebugManager Instance { get; private set; }
        public DeviceType Device { get; private set; }
        public int FrameRate => Mathf.FloorToInt(1f / Time.deltaTime);
        public bool IsDebug => Application.isEditor;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                if (Application.isEditor == false)
                {
                    if (Application.isMobilePlatform == true)
                        Device = DeviceType.Handheld;
                    else if (Application.isConsolePlatform == true)
                        Device = DeviceType.Console;
                    else
                        Device = DeviceType.Desktop;
                }
                else
                {
                    if (_isMobile == true)
                        Device = DeviceType.Handheld;
                    else if (_isConsole == true)
                        Device = DeviceType.Console;
                    else
                        Device = DeviceType.Desktop;
                    Application.targetFrameRate = _frameRate;
                }
            }
        }
    }
}