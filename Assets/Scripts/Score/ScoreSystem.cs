using UnityEngine;

namespace Score
{
    internal interface IScoreSystem
    {
        void UpdateScore();
    }
    public class ScoreSystem : MonoBehaviour, IScoreSystem
    {
        private int _currentScore;
        
        public void UpdateScore()
        {
            _currentScore += 1;
            if (_currentScore > PlayerPrefs.GetInt("bestScore"))
                PlayerPrefs.SetInt("bestScore", _currentScore);
        }
    }
}
