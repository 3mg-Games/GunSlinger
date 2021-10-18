﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.movement;

namespace guns.Control
{
    public class WayPoint : MonoBehaviour
    {
        public bool isPlayerRecherdHere = false;
        public bool multipleWaves = false;
        public bool walkin = false;
        public bool isFastMove = false;
        public string val;
        public List<enemyContoller> ec = new List<enemyContoller>();
        public List<enemyContollerWalkIn> ecwi = new List<enemyContollerWalkIn>();

        private void Update()
        {
            if (multipleWaves && !walkin)
                checkForEnemy();

            if (multipleWaves && walkin)
                checkForEnemy2();
        }

        void checkForEnemy()
        {

            ec.RemoveAll(ec => ec == null);

            if (ec.Count <= 0)
            {
                if (isFastMove)
                    FindObjectOfType<playerMovement>().agent.speed = 20;
                if(!isFastMove)
                    FindObjectOfType<playerMovement>().agent.speed = 5;
                if (val !="")
                {
                    FindObjectOfType<cameraMovement>().ThirdPerson.SetTrigger(val);
                    FindObjectOfType<cameraMovement>().FirstPerson.SetTrigger(val);
                }
                FindObjectOfType<playerMovement>().isTookCover = false;
                Destroy(this.gameObject);
            }
        }
        void checkForEnemy2()
        {

            ecwi.RemoveAll(ec => ec == null);

            if (ecwi.Count <= 0)
            {
                if (isFastMove)
                    FindObjectOfType<playerMovement>().agent.speed = 20;
                if (!isFastMove)
                    FindObjectOfType<playerMovement>().agent.speed = 5;
                if (val != "")
                {
                    FindObjectOfType<cameraMovement>().ThirdPerson.SetTrigger(val);
                    FindObjectOfType<cameraMovement>().FirstPerson.SetTrigger(val);
                }
                FindObjectOfType<playerMovement>().isTookCover = false;
                Destroy(this.gameObject);
            }
        }
    }
}