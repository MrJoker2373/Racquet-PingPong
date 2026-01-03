namespace Common
{
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : AnimationHandler
    {
        private CanvasGroup _group;
        private float _start;
        private float _target;

        public override void Initialize()
        {
            _group = GetComponent<CanvasGroup>();
            base.Initialize();
        }

        protected override void HandleInstant(float value)
        {
            _target = value;
            _group.alpha = _target;
        }

        protected override void HandlePrepareSmooth(float value)
        {
            _start = _group.alpha;
            _target = value;
        }

        protected override void HandleUpdateSmooth(float t)
        {
            _group.alpha = Mathf.Lerp(_start, _target, t);
        }
    }
}