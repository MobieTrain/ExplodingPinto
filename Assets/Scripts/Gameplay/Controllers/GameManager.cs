using UI;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game variables")]
        public int chanceToSpawnFood = 10;
        public int scorePerFood = 2;
        [Tooltip("Player game score")]
        public int score;

        void Start()
        {
            TilemapMaster.Instance.SpawnRandomFood();
        }

        public void UpdateScore(int ammount)
        {
            score += ammount;
            UIController.Instance.UpdateScoreText(score.ToString());
        }
    }
}