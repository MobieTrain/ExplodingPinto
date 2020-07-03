using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public enum GameTiles { NONE, FOOD }


    [CreateAssetMenu(fileName = "New Game Tile", menuName = "Create Game Tile")]
    public class GameTile : Tile
    {
        public GameTiles type;
    }
}

