﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control
{
    public class timeManager : MonoBehaviour
    {
        public float slowdownFactor = 0.05f;
        public float slowdownLength = 2f;
        public float firedownLength = 2f;

        [HideInInspector]
        public float slowdownLimit = 0;

        public float firedownLimit = 0;

        public bool timeTriggered;

        bool reduceTime;

        private void Update()
        {
            if (reduceTime)
            {
                if(slowdownLimit>0)
                    slowdownLimit -= Time.deltaTime;

                if (slowdownLimit <= 0)
                {
                    //Time.timeScale = 1;
                    reduceTime = false;
                    FindObjectOfType<GameManager>().CinemachineCam.SetBool("3to1", false);
                    FindObjectOfType<GameManager>().StartShooting = true;
                }
            }

            if (reduceTime)
            {
                if (firedownLimit > 0)
                    firedownLimit -= Time.deltaTime;

                if (firedownLimit <= 0)
                {
                    Time.timeScale = 1;
                }
            }


            if (FindObjectOfType<GameManager>().TapCount == FindObjectOfType<GameManager>().maxTapCount+1)
            {
                Time.timeScale = 1;
                FindObjectOfType<GameManager>().CinemachineCam.SetBool("3to1", false);
            }
        }
        public void DoSlowmotion()
        {
            reduceTime = true;
            slowdownLimit = slowdownLength;
            firedownLimit = firedownLength;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

    }
}
