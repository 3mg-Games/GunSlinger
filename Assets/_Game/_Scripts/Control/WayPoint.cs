using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.movement;

namespace guns.Control
{
    public class WayPoint : MonoBehaviour
    {
        public bool isPlayerRecherdHere = false;
        public bool multipleWaves = false;
        public bool isFastMove = false;
        public string val;
        public List<enemyContoller> ec = new List<enemyContoller>();

        private void Update()
        {
            if (multipleWaves)
                checkForEnemy();
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
    }
}
