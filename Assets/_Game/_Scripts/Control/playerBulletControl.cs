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
            expand();


        }

        [SerializeField]float d;
        void expand()
        {
            if(target !=null)
                d = Vector3.Distance(transform.position, target.position);
            if (d <= 2)
            {
                GetComponent<SphereCollider>().radius += speed * Time.deltaTime;
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
                if (target == null)
                    Destroy(this.gameObject);
                print("target is missing.........");
            }
                        
        }



        IEnumerator soundDelay(float t)
        {
            yield return new WaitForSeconds(t);
            FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().BulletFiredbyPlayer);
        }


        private void OnTriggerEnter(Collider other)
        {
            try
            {
                etakedam = other.transform.GetComponent<eTakeDamage>();
                switch (etakedam.damageType)
                {
                    case eTakeDamage.collisionType.Head:etakedam.HIT(FindObjectOfType<playerController>().damageAmount);
                        FindObjectOfType<GameManager>().feedback(Color.green,"Perfect Shot");
                        //Destroy(this.gameObject);
                        break;

                    case eTakeDamage.collisionType.Body: etakedam.HIT(FindObjectOfType<playerController>().damageAmount / 2);
                        FindObjectOfType<GameManager>().feedback(Color.green, "Nice Shot");
                        other.gameObject.GetComponent<enemyContoller>().anime.SetTrigger("Take Damage");
                        //Destroy(this.gameObject);
                        break;

                    case eTakeDamage.collisionType.Arm: armDetect();
                        //Destroy(this.gameObject);
                        break;
                }                
            }
            catch
            {
                print("Erorr......");
            }

            Destroy(this.gameObject);
        }

        void armDetect()
        {
            Destroy(GetComponent<SphereCollider>());
            int x = Random.Range(0, 2);
            if (x == 1)
                FindObjectOfType<GameManager>().feedback(Color.red, "Try Again");
            if (x==0)
                FindObjectOfType<GameManager>().feedback(Color.red, "Opps");
        }
    }
}
