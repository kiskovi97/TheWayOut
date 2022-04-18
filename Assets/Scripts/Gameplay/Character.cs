using System;
using UnityEngine;

namespace TheWayOut.Gameplay
{
    class Character : MonoBehaviour
    {
        private Vector3[] positions = new Vector3[0];
        private int currentindex = 0;

        private Action OnFinished;

        public void StartGoing(Vector3[] positions, Action OnFinished)
        {
            this.positions = positions;
            currentindex = 0;
            this.OnFinished = OnFinished;
        }

        private void Update()
        {
            if (positions.Length <= currentindex)
            {
                return;
            }
            var currentPos = positions[currentindex];

            transform.position = Vector3.MoveTowards(transform.position, currentPos, Time.deltaTime * 100f);

            if ((transform.position - currentPos).magnitude < 0.1f) {
                currentindex++;
                if (positions.Length <= currentindex)
                {
                    OnFinished?.Invoke();
                    OnFinished = null;
                }
            }
        }
    }
}
