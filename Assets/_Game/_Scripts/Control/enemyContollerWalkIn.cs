using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;
using guns.movement;

namespace guns.Control
{
    public class enemyContollerWalkIn : MonoBehaviour
    {
        public Animator anime;

        public GameObject bullet;
        public Transform shootPoint;
        public Transform player_position;
        public WayPoint wp;
        public Vector2 MaxXYOffset;
        public Vector2 MinXYOffset;
        public Vector2 normalFireRateMaxMin;
        public Vector3 offset;
        [SerializeField]private Vector3 FirstDirectionToShoot;



        public float health;
        public float RotationSpeed = 50;
        public float targetHeightOffset;
        public float fireForce = 10;

        public float bulletTimeFireRateMultipliar;

      
        public List<GameObject> colliders = new List<GameObject>();
        [HideInInspector]public Vector3 directionToShoot;
        private float xTime;
        private float fireTimeData;

        private void Start()
        {
            fireTimeData = 0.5f;
            directionToShoot = FirstDirectionToShoot;
        }

        private void Update()
        {
            if (!anime.GetCurrentAnimatorStateInfo(0).IsName("BCover") && wp.isPlayerRecherdHere)
            {
                fireTimeData -= Time.deltaTime;
                Rotation();
                /*transform.LookAt(directionToShoot);*/
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

        private Transform rotationTarget;
        private Quaternion Rotate;
        public void Rotation()
        {
            //rotationTarget.position = directionToShoot;
            Vector3 direction = directionToShoot - transform.position;
            Rotate = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, Rotate, RotationSpeed);
        }

        public void check()
        {
            if (wp.isPlayerRecherdHere)
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


       [HideInInspector]public bool isDead = false;

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
