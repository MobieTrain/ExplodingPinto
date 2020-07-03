using Gameplay;
using UnityEngine;


namespace Gameplay
{
    public delegate void FoodClicked(Vector2 pos);

    public class PlayerController : MonoBehaviour
    {
        int objectsTilemapLayer = 1;
        TilemapMaster tileController;
        GameManager game;

        public static event FoodClicked OnFoodClicked;


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

                // Cast a ray straight down.
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    Food food = hit.collider.GetComponent<Food>();
                    if (food)
                    {
                        game.UpdateScore(game.scorePerFood);
                        Destroy(hit.collider.gameObject);

                        OnFoodClicked?.Invoke( new Vector2(
                            hit.collider.transform.position.x,
                            hit.collider.transform.position.y)
                        );
                    }
                }
            }
        }
    }
}