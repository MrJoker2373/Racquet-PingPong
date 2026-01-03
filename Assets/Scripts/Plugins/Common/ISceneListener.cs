namespace Common
{
    public interface ISceneListener
    {
        public void Initialize();

        public void HandleStart();

        public void HandleUpdate(float progress);

        public void HandleFinish();
    }
}