using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class TilemapMaster : MonoBehaviour
    {
        #region SINGLETON
        public static TilemapMaster _instance;
        public static TilemapMaster Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TilemapMaster>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("TilemapMaster");
                        _instance = container.AddComponent<TilemapMaster>();
                    }
                }

                return _instance;
            }
        }
        #endregion

        [Header("Food tiles")]
        public Food[] foodTiles;

        public Dictionary<Vector3Int, Food> Tiles = new Dictionary<Vector3Int, Food>();


        /// <summary>
        /// Randomly spawns a food tile in the tilemap
        /// </summary>
        /// <param name="chanceToSpawn">Percentage of how probable it should spawn food per tile</param>
        public void SpawnRandomFood()
        {
            int itemsToSpawn = 5;

            for (int i = 0; i < itemsToSpawn; i++)
            {
                // TODO: to improve, find another method, use a collider or something to define the area visualy
                float rndX = Random.Range(-9.88f, 9.88f);
                float rndY = Random.Range(4.19f, -3.88f);
                Food rndFood = foodTiles[Random.Range(0, foodTiles.Length - 1)];

                Instantiate(rndFood, new Vector3(rndX, rndY, 0), Quaternion.identity);
            }
        }
    }
}
