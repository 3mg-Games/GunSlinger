using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using guns.movement;
using guns.Control;

namespace guns.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Scene Count")]
        public int CurrentScene;
        public int NextScene;



        public Animator CinemachineCam;
        public Transform collection;
        public TextMeshProUGUI BulletCount;
        public TextMeshProUGUI TextFeedback;
        [Header("Prefebs")]
        public GameObject crosshair;
        public GameObject PS, NS, OPS, TA;

        [Header("Float")]
        public float slowMotionDelay = 0.5f;
        public float reloadDelay = 0.5f;

        [Header("Int")]
        public int maxTapCount = 6;
        public int NumOfHenchmanToKill = 3;
        public int numberOfBulletsUsed;

        //[HideInInspector]
        public int TapCount = 1;

        [HideInInspector]
        public int killedHenchman;

        [Header("Bool")]
        public bool StartShooting;
        public bool StartTapping;
        public bool Reload;
        public bool GameOver;

        [Header("Audio")]
        public AudioSource source;

        public AudioClip BulletFiredbyPlayer;
        public AudioClip BulletFiredbyEnemy;
        public AudioClip EnemyBulletImpactOnGround;
        public AudioClip ReloadGun;
        public AudioClip LevelComplete;

        private void Awake()
        {
            UI();
        }
        private void Update()
        {
            if (!GameOver)
            {
                BulletCount.text = numberOfBulletsUsed.ToString();
                tapToLeaveCover();
                reload();
            }           
            win();



            if(Time.timeScale == 1)
            {
                TextFeedback.transform.GetComponentInParent<Animator>().speed = 1;
            }else if (Time.timeScale<1)
            {
                TextFeedback.transform.GetComponentInParent<Animator>().speed = 40;
            }
        }

        public void feedback(Color c, string s)
        {
            TextFeedback.text = s;
            TextFeedback.color = c;
            TextFeedback.transform.GetComponentInParent<Animator>().Play("spwan");
        }
        void win()
        {
            if (FindObjectOfType<EnemyWaveProgress>().currentWaveCurrentEnemyIdx >= FindObjectOfType<EnemyWaveProgress>().currentWaveTotalEnemies)
            {
                GameOver = true;
                FindObjectOfType<playerMovement>().anime.SetTrigger("win");
                FindObjectOfType<playerController>().enabled = false;
            }
        }



        void reload()
        {
            if (StartShooting && FindObjectOfType<playerController>().rotationCount == FindObjectOfType<playerController>().crosshairPlacingNumber && !Reload)
            {
                
                if (TapCount < maxTapCount)
                {
                    StartCoroutine(lightReload(0));
                }
                if (TapCount >= maxTapCount)
                {
                    StartCoroutine(heavyReload(reloadDelay));
                }
                
            }

            if (StartShooting && FindObjectOfType<playerController>().rotationCount == FindObjectOfType<playerController>().crosshairTransforms.Count + 1)
                StartCoroutine(resetTime(0.2f));
                
        }

        IEnumerator resetTime(float t)
        {
            yield return new WaitForSeconds(t);
            Time.timeScale = 1;
        }

        IEnumerator heavyReload(float t)
        {
            yield return new WaitForSeconds(t);            
            foreach (Transform obj in collection)
            {
                Destroy(obj.gameObject);
            }
            FindObjectOfType<playerMovement>().anime.SetTrigger("Reload");
            FindObjectOfType<playerController>().crosshairPosition.Clear();
            FindObjectOfType<playerController>().crosshairPlacingNumber = 0;
            FindObjectOfType<playerController>().rotationCount = 0;
            FindObjectOfType<playerController>().crosshairTransforms.Clear();
            FindObjectOfType<playerController>().transform.rotation = Quaternion.Euler(0,0,0);
            FindObjectOfType<timeManager>().timeTriggered = false;
            TapCount = 1;
            StartShooting = false;
            Reload = true;
        }


        public void restart()
        {
                SceneManager.LoadScene(CurrentScene);
        }

        public void next()
        {
            SceneManager.LoadScene(NextScene);
        }

        void tapToLeaveCover()
        {
            if (!FindObjectOfType<playerMovement>().isWalking)
            {
                if (Input.GetMouseButtonDown(0) && !StartShooting && TapCount <= maxTapCount)
                {                    
                    if (StartTapping)
                    {
                        TapCount++;
                        FindObjectOfType<playerController>().raycaster();
                    }

                    if (!FindObjectOfType<timeManager>().timeTriggered)
                    {
                        FindObjectOfType<BulletTimeSlider>().setDefsultValueToSlider(FindObjectOfType<timeManager>().slowdownLength);

                        StartCoroutine(startSlowmotion(slowMotionDelay));
                        CinemachineCam.SetBool("3to1", true);
                        FindObjectOfType<playerMovement>().anime.SetTrigger("stand");
                        FindObjectOfType<timeManager>().timeTriggered = true;
                    }
                }
                
                if (Input.GetMouseButtonUp(0) && !StartShooting && TapCount <= maxTapCount)
                {
                    if (StartTapping)
                    {
                        FindObjectOfType<playerController>().CrosshairPlacer();
                    }
                }
            }            
        }

        IEnumerator lightReload(float t)
        {
            yield return new WaitForSeconds(t);
            FindObjectOfType<playerMovement>().anime.SetTrigger("Reload");
            FindObjectOfType<playerController>().crosshairPosition.Clear();
            FindObjectOfType<playerController>().crosshairPlacingNumber = 0;
            FindObjectOfType<playerController>().rotationCount = 0;
            FindObjectOfType<playerController>().crosshairTransforms.Clear();
            FindObjectOfType<playerController>().transform.rotation = Quaternion.Euler(0, 0, 0);
            FindObjectOfType<timeManager>().timeTriggered = false;            
            StartShooting = false;
            Reload = true;
        }
        IEnumerator startSlowmotion(float t)
        {
            yield return new WaitForSeconds(t);
            FindObjectOfType<timeManager>().DoSlowmotion();

        }

        private void UI()
        {
            FindObjectOfType<EnemyWaveProgress>().ActivateCurrentWave(true);
            FindObjectOfType<EnemyWaveProgress>().currentWaveTotalEnemies = NumOfHenchmanToKill;

        }

    }

}
