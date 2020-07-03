using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game variables")]
        public int chanceToSpawnFood = 10;

        void Start()
        {
            TilemapMaster.Instance.RandomSpawnFood(chanceToSpawnFood);
        }

        void Update()
        {

        }
    }
}