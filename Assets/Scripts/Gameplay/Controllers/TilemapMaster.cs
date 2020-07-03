using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        [Tooltip("Game tilemap")]
        public Tilemap foodTilemap;
        public GameTile groundTile;
        [Space]
        [Header("Food tiles")]
        public GameTile[] foodTiles;

        public Dictionary<Vector3Int, GameTile> Tiles = new Dictionary<Vector3Int, GameTile>();


        /// <summary>
        /// Randomly spawns a food tile in the tilemap
        /// </summary>
        /// <param name="chanceToSpawn">Percentage of how probable it should spawn food per tile</param>
        public void SpawnRandomFood(int chanceToSpawn)
        {
            foreach (Vector3Int pos in foodTilemap.cellBounds.allPositionsWithin)
            {
                if (ShouldSpawn(chanceToSpawn) && !GetTileAt(pos))
                {
                    GameTile rndFood = foodTiles[Random.Range(0, foodTiles.Length - 1)];
                    var layeredPos = CreateLayeredPosition(pos, 1);

                    foodTilemap.SetTile(layeredPos, rndFood);
                    Tiles.Add(layeredPos, rndFood);
                }
            }
        }

        bool ShouldSpawn(int chanceToSpawn)
        {
            var randomPercenatge = Random.Range(0, 100);
            return randomPercenatge <= chanceToSpawn;
        }

        void DebugTilemap()
        {
            foreach (var item in Tiles)
                print(item);
        }

        public GameTile GetTileAt(Vector3Int pos)
        {
            GameTile tile;
            Tiles.TryGetValue(pos, out tile);
            return tile;
        }
        public bool DestroyTileAt(Vector3Int pos)
        {
            var tile = GetTileAt(pos);
            if (!tile) return false;

            Tiles.Remove(pos);
            // in the future consider using a TileMapLayer controller 
            foodTilemap.SetTile(pos, null);

            return true;
        }

        public static Vector3Int CreateLayeredPosition(Vector3Int pos, int layer)
        {
            return new Vector3Int(pos.x, pos.y, layer);
        }
    }
}
