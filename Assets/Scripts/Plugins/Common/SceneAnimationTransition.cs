namespace Common
{
    using UnityEngine;

    [RequireComponent(typeof(AnimationHandler))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneAnimationTransition : MonoBehaviour, ISceneTransition
    {
        private CanvasGroup _group;
        private AnimationHandler _animation;

        public void Initialize()
        {
            _group = GetComponent<CanvasGroup>();
            _animation = GetComponent<AnimationHandler>();
            _animation.Initialize();
        }

        public void SetInstant(float value)
        {
            _animation.SetInstant(value);
            _group.interactable = value > 0;
            _group.blocksRaycasts = value > 0;
        }

        public async void SetSmooth(float value) => await SetSmoothAsync(value);

        public async Awaitable SetSmoothAsync(float value)
        {
            _group.blocksRaycasts = value > 0;
            _group.interactable = value == 0;
            await _animation.SetSmoothAsync(value);
            _group.interactable = value > 0;
            _group.blocksRaycasts = value == 0;
        }
    }
}