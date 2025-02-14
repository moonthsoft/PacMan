using Moonthsoft.Core;
using UnityEngine;

namespace Moonthsoft.PacMan.Config
{
    [CreateAssetMenu(menuName = "PacMan/Configuration")]
    public class Configuration : ScriptableObject
    {
        [Header("The number of lives the player has befora a game over")]
        [SerializeField] private int _numPlayerLives = 2;

        [Header("The level at which the game starts. This should be 0 by default, but can be changed to try other levels easily.")]
        [SerializeField] private int _firstLevel = 0;

        [Header("The level at which ghosts no longer enter in Frightened or Scatter mode")]
        [SerializeField] private int _lastLevel = 20;

        [Header("The time left until the ghosts start blinking in Frightened mode")]
        [SerializeField] private float _timeBlinkPowerUp = 2f;

        [Header("The time it takes for each ghost to spawn at the start of the level, in order:\nBlinky, Pinky, Inky, Clyde")]
        [SerializeField] private float[] _timeSpawnGhost = new float[] { 0f, 4f, 8f, 16f };

        [Header("Power up duration per level")]
        [SerializeField] private float[] _durationPowerUp = new float[] { 6f, 5f, 4f, 3f, 2f };

        [Header("The amount of score each element gives:\n0: Dot, 1: PowerUp, 2: Ghost1, 3: Ghost2, 4: Ghost3, 5: Ghost4,\n6: Cherry, 7: Strawberry, 8: Orange, 9: Apple, 10: Pineapple, 11: Galaxian, 12: Bell, 13: Key")]
        [SerializeField] private int[] _score = new int[] { 10 };

        [Header("The duration of each mode per level, always starts with scatter, and then alternates with chase\nScatter, Chase, Scatter, Chase, Scatter, Chase, Scatter\nRaw: Level, Column: Time")]
        [SerializeField] private SerializableMatrix<float> _timeGhostMode = new(new float[][]
        {
            //Scatter, Chase, Scatter, Chase, Scatter, Chase, Scatter (After that, it's always in Chase mode)
            new float[] { 7f, 20f, 7f, 20f, 5f, 20, 5f }, //Level 0
            new float[] { 7f, 20f, 7f, 20f, 5f, 20, 0f }, //Level 1
            new float[] { 7f, 20f, 7f, 20f, 5f, 20, 0f }, //Level 2
            new float[] { 7f, 20f, 7f, 20f, 5f, 20, 0f }, //Level 3
            new float[] { 5f, 20f, 5f, 20f, 5f, 20, 0f }, //Level +4
        });

        [Header("The percentage multiplier for speed for each level based on the following elements:\nPlayer, Ghost, Ghost Frightened, Ghost in Tunnel, Ghost Eated, Blinky Speed Up.\nRaw: Level, Column: Mode")]
        [SerializeField] private SerializableMatrix<float> _speedPercentage = new(new float[][]
        {
            //            Player, Ghost, Ghost Frightened, Ghost Tunnel, Ghost Eated, Blinky SpeedUp
            new float[] { 0.80f,  0.75f,    0.50f,          0.40f,          1.50f,        0.80f }, //Level 0
            new float[] { 0.90f,  0.85f,    0.55f,          0.45f,          1.60f,        0.90f }, //Level 1
            new float[] { 0.90f,  0.85f,    0.55f,          0.45f,          1.60f,        0.90f }, //Level 2
            new float[] { 0.90f,  0.85f,    0.55f,          0.45f,          1.60f,        0.90f }, //Level 3
            new float[] { 1.00f,  0.95f,    0.60f,          0.50f,          1.70f,        1.00f }, //Level +4
        });

        public int NumPlayerLives { get { return _numPlayerLives; } }
        public int FirstLevel { get { return _firstLevel; } }
        public int LastLevel { get { return _lastLevel; } }
        public float TimeBlinkPowerUp { get { return _timeBlinkPowerUp; } }
        public float[] TimeSpawnGhost { get { return _timeSpawnGhost; } }
        public float[] DurationPowerUp { get { return _durationPowerUp; } }
        public int[] Score { get { return _score; } }
        public SerializableMatrix<float> TimeGhostMode { get { return _timeGhostMode; } }
        public SerializableMatrix<float> SpeedPercentage { get { return _speedPercentage; } }

        private void OnValidate()
        {
            _timeGhostMode.ValidateMatrix();
            _speedPercentage.ValidateMatrix();
        }
    }
}

namespace Moonthsoft.PacMan
{
    public enum TypeSpeedPercentage
    {
        Player,
        Ghost,
        GhostFrightened,
        GhostTunnel,
        GhostEated,
        BlinkySpeedUp
    }

    public enum TypeScore
    {
        Dot,
        PowerUp,
        Ghost1,
        Ghost2,
        Ghost3,
        Ghost4,
        Cherry,
        Strawberry,
        Orange,
        Apple,
        Pineapple,
        Galaxian,
        Bell,
        Key
    }
}