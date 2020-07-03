using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int objectsTilemapLayer = 1;
    TilemapMaster tileController;

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
                tileController.objectsTilemap.WorldToCell(mouseWorldPos),
                objectsTilemapLayer
            );

            Debug.Log(pos);

            print(tileController.GetTileAt(pos));
        }


    }
}
