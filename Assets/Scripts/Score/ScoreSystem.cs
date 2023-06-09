using UnityEngine;

namespace Score
{
    public class ScoreSystem : MonoBehaviour
    {
        public int CurrentScore { get; set; }
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
        
        //Updating the actual points scored by the player
        public void UpdateScore()
        {
            CurrentScore += 1;
            SaveScoreInPlayerPrefs();
        }

        //Storing the player's highest point value in PlayerPrefs
        private void SaveScoreInPlayerPrefs()
        {
            var bestScore = PlayerPrefs.GetInt("bestScore", 0);
            if (CurrentScore <= bestScore) return;
            
            PlayerPrefs.SetInt("bestScore", CurrentScore);
            PlayerPrefs.Save();
        }
    }
}
