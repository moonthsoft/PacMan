namespace Moonthsoft.Core.Managers
{
    /// <summary>
    /// Interface for the DataManager.
    /// DataManager is responsible for managing persistent information between scenes.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// The player's current score.
        /// </summary>
        public int Score { get; }

        /// <summary>
        /// The highest score obtained.
        /// </summary>
        public int HighScore { get; }

        /// <summary>
        /// Adds score points to the current score.
        /// </summary>
        /// <param name="score">The score points you want to add.</param>
        public void AddScore(int score);

        /// <summary>
        /// Resets the current score to 0.
        /// </summary>
        public void ResetScore();
    }
}