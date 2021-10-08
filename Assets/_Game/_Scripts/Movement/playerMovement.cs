using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using guns.Core;

namespace guns.movement
{
    public class playerMovement : MonoBehaviour
    {
        private NavMeshAgent agent;

        public Transform[] waypoint;

        public Animator anime;
        public bool isWalking = false;
        public bool inPosition = false;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            walkingBool();
            walkPoint();
            movementAnimationTrigger();


            if(waypoint.Length < 0)
            {
                return;
            }
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
        bool isTookCover = false;
        void movementAnimationTrigger()
        {
            if (!isWalking && inPosition)
            {
                if (!isTookCover)
                {
                    anime.SetBool("Take Cover", true);
                    isTookCover = true;

                }
                if (!isntRot)
                    rotationD();
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (!isWalking && !inPosition)
            {
                if (!isntRot)
                    rotationD();
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
            }


        }
        void walkPoint()
        {
            if (!inPosition)
            {
                agent.SetDestination(waypoint[0].position);
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
                inPosition = true;
                //rotationD();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            inPosition = false;
        }


    }

}
