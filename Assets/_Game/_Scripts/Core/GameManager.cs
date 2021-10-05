using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.movement;
using guns.Control;

namespace guns.Core
{
    public class GameManager : MonoBehaviour
    {
        public Animator CinemachineCam;

        public GameObject crosshair;
        public float slowMotionDelay = 0.5f;

        [HideInInspector]
        public int TapCount = 0;
        public int maxTapCount = 6;


        public bool StartShooting;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && TapCount <= maxTapCount)
            {
                TapCount++;
                FindObjectOfType<playerController>().raycaster();
                if (!FindObjectOfType<timeManager>().timeTriggered)
                {
                    StartCoroutine(startSlowmotion(slowMotionDelay));
                    FindObjectOfType<GameManager>().CinemachineCam.SetBool("3to1", true);
                    FindObjectOfType<playerMovement>().anime.SetBool("stand", true);
                    FindObjectOfType<timeManager>().timeTriggered = true;
                }
            }
        }

        IEnumerator startSlowmotion(float t)
        {
            yield return new WaitForSeconds(t);
            FindObjectOfType<timeManager>().DoSlowmotion();

        }
    }

}
