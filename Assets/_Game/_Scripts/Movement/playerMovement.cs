using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using guns.Core;
using guns.Control;

namespace guns.movement
{
    public class playerMovement : MonoBehaviour
    {
        [HideInInspector]public NavMeshAgent agent;
        public WayPoint wp;
        public Animator anime;
        public float rediusForCheckNextWayPoint = 5;

        public bool isWalking = false;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        float checkTime = 2;
        private void Update()
        {
            if (wp != null)
            {
                walkingBool();
                walkPoint();
                movementAnimationTrigger();
            }

            if (wp==null)
            {
                checkTime -= Time.deltaTime;
                if(checkTime>0)
                    checkForNearestWayPoint();                
            }
        }
        public void checkForNearestWayPoint()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, rediusForCheckNextWayPoint);
            foreach(Collider nearbyWP in colliders)
            {
                if (nearbyWP.gameObject.CompareTag("wayP"))
                    wp = nearbyWP.gameObject.GetComponent<WayPoint>();
            }
/*            if (wp!=null || checkTime <= 0)
            {
                
                for (int i = 0; i < colliders.Length - 1; i++)
                {
                    colliders[i] = null;
                }
            }*/
                
        }

        void walkingBool()
        {
            if (agent.velocity.magnitude > 0)
            {
                isWalking = true;
                anime.SetBool("Walk", true);
            }
                
            if (agent.velocity.magnitude <= 0)
            {
                isWalking = false;
                anime.SetBool("Walk", false);
            }
        }

        bool isntRot = false;
        [HideInInspector] public bool isTookCover = false;
        void movementAnimationTrigger()
        {
            if (!isWalking && wp.isPlayerRecherdHere)
            {
                if (!isTookCover)
                {

                    anime.SetBool("Take Cover", true);
                    FindObjectOfType<playerController>().transform.rotation = Quaternion.Euler(0, 0, 0);
                    isTookCover = true;

                }
                if (!isntRot)
                    rotationD();
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (!isWalking && !wp.isPlayerRecherdHere)
            {
                if (!isntRot)
                    rotationD();
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
            }


        }
        void walkPoint()
        {
            if (!wp.isPlayerRecherdHere)
            {
                agent.SetDestination(wp.transform.position);
            }
        }
        Quaternion rotation;
        void rotationD()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), 5 * Time.deltaTime);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("wayP"))
            {
                wp.isPlayerRecherdHere = true;
                //rotationD();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            wp.isPlayerRecherdHere = false;
        }


    }

}
