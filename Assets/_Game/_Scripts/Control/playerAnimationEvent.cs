using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.movement;
using guns.Core;

namespace guns.Control
{
    public class playerAnimationEvent : MonoBehaviour
    {
        void resetTriggerOfReload()
        {
            FindObjectOfType<GameManager>().Reload = false;
            FindObjectOfType<playerMovement>().anime.ResetTrigger("Reload");
           
        }

        void reloadSound()
        {
            FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().ReloadGun, 1);
        }
        void startTapping()
        {
            FindObjectOfType<GameManager>().StartTapping = true;

        }
        void stopTapping()
        {
            FindObjectOfType<GameManager>().StartTapping = false;

        }

        void shootSound()
        {
            //FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().BulletFiredbyPlayer);        
        }

        void ResetTime()
        {
            Time.timeScale = 1;
        }
    }
}
