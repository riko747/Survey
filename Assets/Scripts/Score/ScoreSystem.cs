using UnityEngine;

namespace Score
{
    public class ScoreSystem : MonoBehaviour
    {
        public int CurrentScore { get; private set; }
        public bool PassedTest { get; set; }

        public static ScoreSystem Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }
        
        public void UpdateScore()
        {
            CurrentScore += 1;
            SaveScoreInPlayerPrefs();
        }

        private void SaveScoreInPlayerPrefs()
        {
            var bestScore = PlayerPrefs.GetInt("bestScore", 0);
            if (CurrentScore <= bestScore) return;
            
            PlayerPrefs.SetInt("bestScore", CurrentScore);
            PlayerPrefs.Save();
        }
    }
}
