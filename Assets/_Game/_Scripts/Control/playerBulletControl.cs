using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control
{
    public class playerBulletControl : MonoBehaviour
    {
        public TrailRenderer trail;
        public float speed = 10;
        public float time = 1;
        public float soundDelayTime = 0.2f;
        private float trailTimer = 0;
        private eTakeDamage etakedam;
        public Transform target;

        private void Start()
        {
            StartCoroutine(soundDelay(soundDelayTime));
        }
        private void Update()
        {
            TravalToThePlayer();
            trailTimer += Time.deltaTime;
            if (trailTimer >= time)
            {
                trailTimer = time;
                trail.time -= Time.deltaTime;
            }
        }

        void TravalToThePlayer()
        {
            try
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            catch
            {
                print("target is missing.........");
            }
                        
        }



        IEnumerator soundDelay(float t)
        {
            yield return new WaitForSeconds(t);
            /*FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().BulletFiredbyPlayer);*/
        }

        private void OnCollisionEnter(Collision other)
        {

            try
            {
                etakedam = other.transform.GetComponent<eTakeDamage>();
                switch (etakedam.damageType)
                {
                    case eTakeDamage.collisionType.Head: etakedam.HIT(FindObjectOfType<playerController>().damageAmount);
                        Destroy(Instantiate(FindObjectOfType<GameManager>().PS, transform.position, Quaternion.identity), 2f);
                        break;

                    case eTakeDamage.collisionType.Body: etakedam.HIT(FindObjectOfType<playerController>().damageAmount / 2);
                        Destroy(Instantiate(FindObjectOfType<GameManager>().NS, transform.position, Quaternion.identity), 2f);
                        other.gameObject.GetComponent<enemyContoller>().anime.SetTrigger("Take Damage");
                        break;
                    case eTakeDamage.collisionType.Arm: armDetect();
                        break;
                }
                Destroy(gameObject);
            }
            catch
            {
                print("Erorr......");
            }
        }
        void armDetect()
        {
            int x = Random.Range(0, 2);
            if (x == 1)            
                Destroy(Instantiate(FindObjectOfType<GameManager>().TA, transform.position, Quaternion.identity), 2f);
            if(x==0)
                Destroy(Instantiate(FindObjectOfType<GameManager>().OPS, transform.position, Quaternion.identity), 2f);
        }
    }
}
