using Moonthsoft.PacMan.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Moonthsoft.PacMan
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Image[] _lives;

        [SerializeField] private Image[] _fruitsLevel;

        [SerializeField] private Sprite[] _spriteFruits;

        [SerializeField] private TMP_Text _readyText;

        [SerializeField] private TMP_Text _2UPHeader;
        [SerializeField] private TMP_Text _1UPScore;
        [SerializeField] private TMP_Text _2UPScore;
        [SerializeField] private TMP_Text _HighScore;


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

        private void SetScore(TMP_Text text, int score)
        {
            text.text = score.ToString();
        }
    }
}