using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;
using guns.movement;

namespace guns.Control
{
    public class enemyContoller : MonoBehaviour
    {
        public Animator anime;

        public GameObject bullet;
        public Transform shootPoint;
        public List<GameObject> colliders = new List<GameObject>();
        public Vector2 MaxXYOffset;
        public Vector2 MinXYOffset;
        public Vector2 normalFireRateMaxMin;
        public Vector3 offset;



        public float health;

        public float targetHeightOffset;
        public float fireForce = 10;
        public float bulletTimeFireRateMultipliar;
        public Transform player_position;
        [HideInInspector]
        public Vector3 directionToShoot;
        private float xTime;
        private float fireTimeData;

        private void Awake()
        {
            fireTimeData = 0.5f;
        }

        private void Update()
        {
            if (!anime.GetCurrentAnimatorStateInfo(0).IsName("BCover") && FindObjectOfType<playerMovement>().inPosition)
            {
                fireTimeData -= Time.deltaTime;
                transform.LookAt(directionToShoot);
            }
                
            if (fireTimeData <= 0 && !isDead)
            {
                lookAtPlayerInRange();
                radomTime();
                shoot();
                fireTimeData = xTime;
            }
            die();
            check();
        }

        float x, y;
        


        public void check()
        {
            if (FindObjectOfType<playerMovement>().inPosition)
            {
                anime.SetTrigger("Shoot");               
            }
        }

        void shoot()
        {
            if (shootPoint == null)
                return;

            if(shootPoint != null)
            {                              
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
                for(int i=0;i< colliders.Count; i++)
                {
                    colliders[i].GetComponent<Collider>().enabled = false;
                }
                isDead = true;
            }
        }
        private void OnCollisionEnter(Collision ot)
        {
            if (ot.gameObject.CompareTag("Enemy"))
                health = 0;

            else if (ot.gameObject.CompareTag("Shootable"))
                health = 0;
        }

        public void TNTHit()
        {
            health = 0;
            die();
        }
    }
}
