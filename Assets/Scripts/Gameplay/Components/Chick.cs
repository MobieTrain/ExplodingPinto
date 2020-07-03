using System.Collections;
using UnityEngine;

namespace Gameplay { 
    public class Chick : MonoBehaviour
    {
        public float speed = 5f;
        public int growSize = 0;
        public int explodValue = 2;

        bool isMoving;
        Vector2 movingPos;

        void Update()
        {
            if (isMoving) 
                MoveTo();

            if (growSize >= explodValue)
            {
                // Game over
                Destroy(gameObject);
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
    }
}