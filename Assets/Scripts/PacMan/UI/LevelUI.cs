using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Logic for the game interface (View), is completely decoupled from the game logic of the LevelManager (Controller).
    /// Manages the level interface, player lives, scoring, and the appearance of scene elements such as the score when devouring a ghost, 
    /// or the ready! text at the start of the game.
    /// </summary>
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Image[] _lives;
        [SerializeField] private Image[] _fruitsLevel;
        [SerializeField] private Sprite[] _spriteFruits;
        [SerializeField] private TMP_Text _readyText;
        [SerializeField] private TMP_Text[] _scoreEatGhostTexts;
        [SerializeField] private TMP_Text _2UPHeader;
        [SerializeField] private TMP_Text _1UPScore;
        [SerializeField] private TMP_Text _2UPScore;
        [SerializeField] private TMP_Text _HighScore;


        private void Awake()
        {
            for (int i = 0; i < _scoreEatGhostTexts.Length; ++i)
            {
                _scoreEatGhostTexts[i].gameObject.SetActive(false);
            }
        }

        public void SetLevelUI(int level, int lives)
        {
            SetLives(lives);

            int indxSprCount = level;
            
            for (int i = _fruitsLevel.Length - 1; i >= 0; --i)
            {
                if (i <= level)
                {
                    int indxSprAux = indxSprCount;
                    if (indxSprAux >= _spriteFruits.Length)
                    {
                        indxSprAux = _spriteFruits.Length - 1;
                    }

                    _fruitsLevel[i].enabled = true;
                    _fruitsLevel[i].sprite = _spriteFruits[indxSprAux];

                    indxSprCount--;
                }
                else
                {
                    _fruitsLevel[i].enabled = false;
                }
            }
        }

        public void SetLives(int lives)
        {
            for (int i = 0; i < _lives.Length; ++i)
            {
                _lives[i].enabled = i < lives;
            }
        }

        public void ActiveReadyText(bool active)
        {
            _readyText.gameObject.SetActive(active);
        }

        public void Active2Players(bool active)
        {
            _2UPHeader.gameObject.SetActive(active);
            _2UPScore.gameObject.SetActive(active);
        }

        public void Set1UPScore(int score)
        {
            SetScore(_1UPScore, score);
        }

        public void Set2UPScore(int score)
        {
            SetScore(_2UPScore, score);
        }

        public void SetHighScore(int score)
        {
            SetScore(_HighScore, score);
        }

        public void ActiveScoreEatGhost(int score, Vector3 pos)
        {
            TMP_Text scoreText = null;

            for (int i = 0; i < _scoreEatGhostTexts.Length; ++i)
            {
                if (!_scoreEatGhostTexts[i].gameObject.activeSelf)
                {
                    scoreText = _scoreEatGhostTexts[i];
                    break;
                }
            }

            if (scoreText == null)
            {
                Debug.LogError("There is no _scoreEatGhostTexts available.");
            }

            scoreText.gameObject.SetActive(true);

            scoreText.transform.position = pos;

            scoreText.text = score.ToString();

            StartCoroutine(DeactiveScoreEatGhostCoroutine(scoreText.gameObject));
        }

        private void SetScore(TMP_Text text, int score)
        {
            text.text = score.ToString();
        }

        private IEnumerator DeactiveScoreEatGhostCoroutine(GameObject scoreText)
        {
            yield return new WaitForSeconds(0.1f);

            scoreText.SetActive(false);
        }
    }
}