namespace RacquetPingPong
{
    using UnityEngine;

    public class ColorRandomizer : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private Color[] _colors;

        private void Start()
        {
            _camera.backgroundColor = _colors[Random.Range(0, _colors.Length)];
        }
    }
}