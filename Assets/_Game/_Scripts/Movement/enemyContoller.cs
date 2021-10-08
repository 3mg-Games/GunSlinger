using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace guns.Control
{
    public class enemyContoller : MonoBehaviour
    {
        public Material mat1;
        public GameObject bullet;
        public Transform shootPoint;

        public Vector2 MaxXYOffset;
        public Vector2 MinXYOffset;


        public float health;

        public float targetHeightOffset;
        public float fireForce = 10;
        public float fireRate = 2;

        private Vector3 directionToShoot;
        private Transform player_position;
        private float fireTimeData = 0;

        private void Start()
        {
            player_position = GameObject.Find("Player").transform.GetChild(0).GetChild(3).transform;
        }

        private void Update()
        {
            fireTimeData -= Time.deltaTime;
            shootPoint.transform.LookAt(directionToShoot);
            lookAtPlayerInRange();
            if (fireTimeData <= 0)
            {
                shoot();
                fireTimeData = fireRate;
            }

        }

        float x, y;
        public void lookAtPlayerInRange()
        {
           
            x = Random.Range(player_position.position.x + MaxXYOffset.x, player_position.position.x - MinXYOffset.x );
            y = Random.Range(player_position.position.y + MaxXYOffset.y,  player_position.position.y - MinXYOffset.y);

            directionToShoot = new Vector3(x, y, 0);
        }

        void shoot()
        {
            GameObject Bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * fireForce, ForceMode.Impulse);
            Destroy(Bullet, 3f);
        }



        public void die()
        {

        }
    }
}
