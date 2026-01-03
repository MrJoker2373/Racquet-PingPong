namespace RacquetPingPong
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private float _maxVelocity = 15f;
        [SerializeField]
        private float _minVelocity = 13f;
        [SerializeField]
        private float _minImpulse = 0.3f;
        [SerializeField]
        private float _impulseDivider = 1500f;
        [SerializeField]
        private float _pitchOffset = 0.2f;

        private Rigidbody2D _rigidBody;
        private AudioSource _audio;
        private Vector2 _startPosition;
        private float _velocity;

        public event Action Hitted;

        public float Magnitude => _rigidBody.linearVelocity.magnitude;
        public bool IsSleeping => _rigidBody.IsSleeping();

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _audio = GetComponentInChildren<AudioSource>();
            _rigidBody.Sleep();
            _velocity = _maxVelocity;
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            _rigidBody.linearVelocity = Vector2.ClampMagnitude(_rigidBody.linearVelocity, _velocity);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Racquet>())
            {
                float impulse = collision.GetContact(0).normalImpulse / _impulseDivider;
                if (impulse < _minImpulse)
                    return;

                _velocity = UnityEngine.Random.Range(_minVelocity, _maxVelocity);
                _audio.pitch = UnityEngine.Random.Range(1f - _pitchOffset, 1f + _pitchOffset);
                _audio.volume = impulse;
                _audio.Play();
                Hitted?.Invoke();
            }
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
            _rigidBody.Sleep();
        }
    }
}