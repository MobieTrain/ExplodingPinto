using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Chick : MonoBehaviour
    {
        public float speed = 5f;
        public int foodEaten = 0;
        public int explodValue = 2;
        public float growScale = 0.2f;
        public GameObject ExplosionObject;

        bool isMoving;
        Vector2 movingPos;
        float viewRadius;

        int layerMask = 1 << 8;

        private void Awake()
        {
            viewRadius = 200f;
        }

        void Start()
        {
            PlayerController.OnFoodClicked += ValidateClickedFood;
            ExplosionObject.SetActive(false);
        }

        void Update()
        {
            // Check for food
            if (!isMoving)
                CheckNearestForFood();

            if (isMoving)
                MoveTo();

            if (foodEaten >= explodValue)
                StartCoroutine(Die());
        }

        void CheckNearestForFood()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
                new Vector2(transform.position.x, transform.position.y),
                viewRadius,
                layerMask
            );
            if (hitColliders != null && hitColliders.Length > 0)
            {
                var closestCollider = hitColliders[0];

                if (closestCollider != null)
                    StartMovingTo(closestCollider.transform.position);
            } else
            {
                TilemapMaster.Instance.SpawnRandomFood();
            }
        }

        IEnumerator Die()
        {
            print("Game over!");
            ExplosionObject.SetActive(true);
            StopMoving();
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Food food = collision.gameObject.GetComponent<Food>();
            if (food != null)
            {
                EatFood();
                Destroy(food.gameObject);
            }
        }

        public void StartMovingTo(Vector2 pos)
        {
            isMoving = true;
            movingPos = pos;
        }
        public void StopMoving()
        {
            isMoving = false;
            movingPos = Vector2.zero;
        }

        void MoveTo()
        {
            print("Moving to " + movingPos);
            transform.position = Vector2.MoveTowards(transform.position, movingPos, speed * Time.deltaTime);

            // V = dist/deltaT <=> deltaT = dist/V
            var dist = Vector3.Distance(transform.position, movingPos);
            var timeToDestination = dist / speed;

            if (timeToDestination <= 0) StopMoving();
        }

        void EatFood()
        {
            // grow size increase
            transform.localScale += new Vector3(growScale, growScale, growScale);
            foodEaten++;
            speed *= 1.3f;
        }

        void ValidateClickedFood(Vector2 pos)
        {
            if (movingPos.Equals(pos))
            {
                print("SAME");
                StopMoving();
            }
        }
    }
}