using Moonthsoft.Core.Definitions.Scenes;

namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// Interface for the LoadSceneManager.
    /// LoadSceneManager handles switching between scenes.
    /// </summary>
    public interface ILoadSceneManager
    {
        /// <summary>
        /// Change the current scene to a new one (It can be the same one if you want to reset it).
        /// </summary>
        /// <param name="scene">The new scene you want to load.</param>
        public void LoadScene(Scenes scene);
    }
}
