using Gameplay;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int objectsTilemapLayer = 1;
    TilemapMaster tileController;
    GameManager game;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        if (!game) Debug.LogError("Could not find a Game Master instance");
    }

    void Update()
    {
        HandleFoodClick();
        tileController = TilemapMaster.Instance;
    }

    void HandleFoodClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int pos = TilemapMaster.CreateLayeredPosition(
                tileController.foodTilemap.WorldToCell(mouseWorldPos),
                objectsTilemapLayer
            );

            GameTile selectedTile = tileController.GetTileAt(pos);
            if (selectedTile)
            {
                if (selectedTile.type == GameTiles.FOOD)
                {
                    tileController.DestroyTileAt(pos);
                    game.UpdateScore(game.scorePerFood);
                    FindObjectOfType<Chick>().StartMovingTo(new Vector2(mouseWorldPos.x, mouseWorldPos.y));
                }
            }
        }
    }
}
