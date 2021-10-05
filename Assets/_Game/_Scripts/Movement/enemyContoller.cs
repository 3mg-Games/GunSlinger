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

        public float shootForce = 10;
        public float shootTime = 2;


        private Transform player_position;
        private float shootTimeData = 0;

        private void Start()
        {
            player_position = GameObject.Find("Player").transform;
        }

        private void Update()
        {
            shootTimeData -= Time.deltaTime;
            shootPoint.transform.LookAt(player_position.position);

            if(shootTimeData <= 0)
            {
                shoot();
                shootTimeData = shootTime;
            }

        }

        void shoot()
        {
            GameObject Bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(Bullet, 3f);
        }
    }
}
