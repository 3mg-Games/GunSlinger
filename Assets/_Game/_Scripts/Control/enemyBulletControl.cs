using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control
{
    public class enemyBulletControl : MonoBehaviour
    {
        public TrailRenderer trail;
        public float speed = 50;
        public Vector3 target;
        public float time = 1;
        private float trailTimer = 0;
        private void Start()
        {
            FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().BulletFiredbyEnemy, 0.7f);
        }


        private void Update()
        {
            trailTimer += Time.deltaTime;
            if (trailTimer >= time)
            {
                trailTimer = time;
                trail.time -= Time.deltaTime;
            }



            //target = FindObjectOfType<enemyContoller>().directionToShoot;
            //TravalToThePlayer();
        }

        void TravalToThePlayer()
        {
            try
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
            }
            catch
            {
                print("target is missing.........");
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().EnemyBulletImpactOnGround, 0.7f);
            Destroy(gameObject);
        }
    }
}
