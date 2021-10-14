using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace guns.Control
{
    public class eTakeDamage : MonoBehaviour
    {
        public bool isWalkIn = false;
        public enum collisionType { Head, Body, Arm }
        public collisionType damageType;
        public enemyContoller controller;
        public enemyContollerWalkIn controller2;

        public void HIT(float value)
        {
            if (!isWalkIn)
            {
                try
                {
                    controller.health -= value;

                }
                catch
                {
                    print("Enemy Controller is missing............" + transform.name);
                }
            }
            if (isWalkIn)
            {
                try
                {
                    controller2.health -= value;

                }
                catch
                {
                    print("Enemy Controller is missing............" + transform.name);
                }
            }
        }
    }
}
