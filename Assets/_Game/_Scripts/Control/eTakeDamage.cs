using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace guns.Control
{
    public class eTakeDamage : MonoBehaviour
    {
        public enum collisionType { Head, Body, Arm}
        public collisionType damageType;
        public enemyContoller controller;

        public void HIT(float value)
        {
            try
            {
                controller.health -= value;
                if (controller.health <= 0)
                    controller.die();
            }
            catch
            {
                print("Enemy Controller is missing............" + transform.name);
            }
        }
    }
}
