namespace RacquetPingPong
{
    using UnityEngine;
    using Common;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Racquet : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private Collider2D _handle;

        private Rigidbody2D _rigidBody;
        private Vector2 _startPosition;
        private Vector2 _position;
        private Vector2 _offset;
        private bool _isGrabbing;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _startPosition = transform.position;
            Enable();
        }

        private void OnDestroy() => Disable();

        public void Enable()
        {
            InputReader.Instance.Subscribe<Vector2>(InputID.Position, OnPositionPerformed);
            InputReader.Instance.Subscribe<float>(InputID.Click, OnClickPerformed);
            transform.position = _startPosition;
        }

        public void Disable()
        {
            InputReader.Instance.Unsubscribe<Vector2>(InputID.Position, OnPositionPerformed);
            InputReader.Instance.Unsubscribe<float>(InputID.Click, OnClickPerformed);
            _isGrabbing = false;
        }

        private void OnPositionPerformed(Vector2 input)
        {
            _position = _camera.ScreenToWorldPoint(input);
            if (_isGrabbing == true)
                _rigidBody.MovePosition(_position - _offset);
        }

        private void OnClickPerformed(float input)
        {
            _isGrabbing = input > 0 && IsContains();
            if (_isGrabbing == true)
                _offset = _position - _rigidBody.position;
        }

        private bool IsContains()
        {
            var colliders = Physics2D.OverlapCircleAll(_position, 0.25f);
            foreach (var c in colliders)
                if (c == _handle)
                    return true;
            return false;
        }
    }
}