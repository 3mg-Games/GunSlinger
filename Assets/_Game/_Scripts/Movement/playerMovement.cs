using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using guns.Core;

namespace guns.movement
{
    public class playerMovement : MonoBehaviour
    {
        public Transform[] wayPoints;
        public float speed;
        

        [HideInInspector]
        public Animator anime;
           

        private void Start()
        {
            anime = GetComponent<Animator>();
        }
    }

}
