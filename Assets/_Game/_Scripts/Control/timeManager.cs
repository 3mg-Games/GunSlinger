using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control
{
    public class timeManager : MonoBehaviour
    {
        public float slowdownFactor = 0.05f;
        public float slowdownLength = 2f;
        private float slowdownLimit = 0;

        public bool timeTriggered;

        bool reduceTime;

        private void Update()
        {
            if (reduceTime)
            {
                slowdownLimit -= Time.deltaTime;
                if (slowdownLimit <= 0)
                {
                    Time.timeScale = 1;
                    reduceTime = false;
                    FindObjectOfType<GameManager>().StartShooting = true;
                }
            }

            if (FindObjectOfType<GameManager>().TapCount == FindObjectOfType<GameManager>().maxTapCount+1)
            {
                Time.timeScale = 1;
            }
        }
        public void DoSlowmotion()
        {
            reduceTime = true;
            slowdownLimit = slowdownLength;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

    }
}
