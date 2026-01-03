namespace Common
{
    using UnityEngine;

    public interface ISceneTransition
    {
        public void Initialize();

        public void SetInstant(float value);

        public void SetSmooth(float value);

        public Awaitable SetSmoothAsync(float value);
    }
}