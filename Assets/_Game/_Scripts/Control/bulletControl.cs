using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control
{
    public class bulletControl : MonoBehaviour
    {
        public TrailRenderer trail;
        public float time = 1;

        private float trailTimer = 0;
        private eTakeDamage etakedam;
        private void Update()
        {

            trailTimer += Time.deltaTime;
            if (trailTimer >= time)
            {
                trailTimer = time;
                trail.time -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision other)

        {

            try
            {
                etakedam = other.transform.GetComponent<eTakeDamage>();
                switch (etakedam.damageType)
                {
                    case eTakeDamage.collisionType.Head: etakedam.HIT(FindObjectOfType<playerController>().damageAmount);
                        Destroy(Instantiate(FindObjectOfType<GameManager>().PS, transform.position, Quaternion.identity), 2f);
                        break;

                    case eTakeDamage.collisionType.Body: etakedam.HIT(FindObjectOfType<playerController>().damageAmount / 2);
                        Destroy(Instantiate(FindObjectOfType<GameManager>().NS, transform.position, Quaternion.identity), 2f);
                        break;
                }
                Destroy(gameObject);
            }
            catch
            {

            }
        }
    }
}
