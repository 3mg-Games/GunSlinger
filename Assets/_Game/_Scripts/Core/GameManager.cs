using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using guns.movement;
using guns.Control;

namespace guns.Core
{
    public class GameManager : MonoBehaviour
    {
        public Animator CinemachineCam;

        [Header("Prefebs")]
        public GameObject crosshair;
        public GameObject PS, NS, OPS, TA;

        [Header("Float")]
        public float slowMotionDelay = 0.5f;
        public float reloadDelay = 0.5f;
        public int maxTapCount = 6;


        [HideInInspector]
        public int TapCount = 0;

        [Header("Bool")]
        public bool StartShooting;
        public bool Reload;
        private void Update()
        {
            tapToLeaveCover();
            reload();
            restart();
        }


        void reload()
        {
            if (StartShooting && FindObjectOfType<playerController>().rotationCount == FindObjectOfType<playerController>().crosshairTransforms.Count)
            {
                StartCoroutine(returnCameraIdle(reloadDelay));
            }
        }


        void restart()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        void tapToLeaveCover()
        {
            if (!FindObjectOfType<playerMovement>().isWalking)
            {
                if (Input.GetMouseButtonDown(0) && !StartShooting && TapCount <= maxTapCount)
                {
                    TapCount++;
                    FindObjectOfType<playerController>().raycaster();
                    if (!FindObjectOfType<timeManager>().timeTriggered)
                    {
                        FindObjectOfType<BulletTimeSlider>().setDefsultValueToSlider(FindObjectOfType<timeManager>().slowdownLength);
                        StartCoroutine(startSlowmotion(slowMotionDelay));
                        CinemachineCam.SetBool("3to1", true);
                        FindObjectOfType<playerMovement>().anime.SetTrigger("stand");
                        FindObjectOfType<timeManager>().timeTriggered = true;
                    }
                }
            }            
        }

        IEnumerator returnCameraIdle(float t)
        {
            FindObjectOfType<playerController>().animationRig.weight = 0;
            yield return new WaitForSeconds(t);            
            FindObjectOfType<timeManager>().timeTriggered = false;
            StartShooting = false;
            Reload = true;
            FindObjectOfType<playerMovement>().anime.SetTrigger("Reload");
        }
        IEnumerator startSlowmotion(float t)
        {
            yield return new WaitForSeconds(t);
            FindObjectOfType<timeManager>().DoSlowmotion();

        }
    }

}
