using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using guns.Core;
using guns.movement;

namespace guns.Control {
    public class playerController : MonoBehaviour
    {
        public GameObject bullet;
        public Transform shootPoint;
        /*public Transform Rotation;
        public Rig animationRig;*/
        public Vector3 crosshairOffset;
        public Vector3 rotationOffset;
        
        public float fireForce = 10;
        public float shootDelayTime = 2;
        public float damageAmount = 10;
        public float rotationSpeed = 5;

        public float fireDelayTime = 1;
        public List<Vector3> crosshairPosition = new List<Vector3>();
        [HideInInspector]public List<Transform> crosshairTransforms = new List<Transform>();

        float shoot;

        private void Start()
        {
            //animationRig.weight = 0;
        }

        void Update()
        {
            if (shoot > 0)
                shoot -= Time.deltaTime;

            if (FindObjectOfType<GameManager>().StartShooting && rotationCount < crosshairTransforms.Count)
            {                
                if (shoot <= 0)
                {
                    fire();
                    shoot = shootDelayTime;
                }
            }
            if(crosshairTransforms.Count >= 1 && rotationCount <= crosshairTransforms.Count && FindObjectOfType<GameManager>().StartShooting)
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            //shootPoint.rotation = Quaternion.Lerp(shootPoint.rotation, rotation, rotationSpeed * Time.deltaTime);
            //Rotation.rotation = Quaternion.Lerp(Rotation.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        public void raycaster()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {

                if (!crosshairPosition.Contains(hit.point))
                    crosshairPosition.Add(hit.point);
            }
            //CrosshairPlacer();
        }

        public int crosshairPlacingNumber = 0;
        public void CrosshairPlacer()
        {
            if (crosshairPosition.Count >= 1)
            { 
                GameObject c_hair =  Instantiate(FindObjectOfType<GameManager>().crosshair, crosshairPosition[crosshairPlacingNumber] - crosshairOffset, Quaternion.identity) ;
                c_hair.transform.parent = GameObject.Find("Crosshair_Collection").transform;
                crosshairTransforms.Add(c_hair.transform);
                crosshairPlacingNumber++;
            }
    
        }

        private Transform rotationShootTarget;
        public int rotationCount = 0;
        private Quaternion rotation;


        public void fire()
        {
            if (crosshairTransforms.Count < 1)
                return;

            if (crosshairTransforms.Count >= 1 /*&& rotationCount < crosshairTransforms.Count+1*/)
            {                
                rotationShootTarget = (crosshairTransforms[rotationCount]);
                Vector3 direction = rotationShootTarget.position - transform.position;
                rotation = Quaternion.LookRotation(direction + rotationOffset);
                StartCoroutine(spwanEvent(fireDelayTime, crosshairTransforms[rotationCount]));
                rotationCount++;
            }
                
        }

        IEnumerator spwanEvent(float t, Transform obj)
        {
            FindObjectOfType<playerMovement>().anime.Play("Shooting");           
            yield return new WaitForSeconds(t);            
            GameObject Bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            Bullet.GetComponent<playerBulletControl>().target = obj;
            FindObjectOfType<GameManager>().numberOfBulletsUsed++;
        }


    }

}
