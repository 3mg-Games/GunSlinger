using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control
{
    public class enemyContoller : MonoBehaviour
    {
        public Animator anime;

        public GameObject bullet;
        public Transform shootPoint;

        public Vector2 MaxXYOffset;
        public Vector2 MinXYOffset;
        public Vector2 normalFireRateMaxMin;
        public Vector3 offset;
        public Vector3 targetOffset;



        public float health;

        public float targetHeightOffset;
        public float fireForce = 10;
        public float bulletTimeFireRateMultipliar;
        public Transform player_position;
        [HideInInspector]
        public Vector3 directionToShoot;
        private float fireTimeData = 0;
        private float xTime;

        private void Start()
        {
            //player_position = GameObject.Find("Player").transform.GetChild(0).GetChild(3).transform;
        }

        private void Update()
        {


            fireTimeData -= Time.deltaTime;
            transform.LookAt(directionToShoot + targetOffset);
            if (fireTimeData <= 0 && !isDead)
            {
                radomTime();
                shoot();
                fireTimeData = xTime;
            }
            die();

        }

        float x, y;
        

        void shoot()
        {
            if (shootPoint == null)
                return;

            if(shootPoint != null)
            {
                lookAtPlayerInRange();
                anime.SetTrigger("Shoot");
                GameObject Bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                Bullet.GetComponent<Rigidbody>().AddForce((transform.forward+offset) * fireForce, ForceMode.Impulse);
                Destroy(Bullet, 3f);
            }            
        }

        public void lookAtPlayerInRange()
        {
            x = Random.Range(player_position.position.x + MaxXYOffset.x, player_position.position.x - MinXYOffset.x);
            y = Random.Range(player_position.position.y + MaxXYOffset.y, player_position.position.y - MinXYOffset.y);
            directionToShoot = new Vector3(x, y, player_position.position.z);
        }

        void radomTime()
        {
            if(!FindObjectOfType<timeManager>().timeTriggered)
                xTime = Random.Range(normalFireRateMaxMin.x , normalFireRateMaxMin.y);
            if(FindObjectOfType<timeManager>().timeTriggered)
                xTime = Random.Range(normalFireRateMaxMin.x / bulletTimeFireRateMultipliar, normalFireRateMaxMin.y / bulletTimeFireRateMultipliar);
        }


        bool isDead = false;

        public void die()
        {
            if(health <= 0 && !isDead)
            {
                anime.SetTrigger("Die");
                Destroy(gameObject, 2);
                FindObjectOfType<EnemyWaveProgress>().KillEnemyInCurrentWave();
                isDead = true;
            }
        }

        public void TNTHit()
        {
            health = 0;
            die();
        }
    }
}
