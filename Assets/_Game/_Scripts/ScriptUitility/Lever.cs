using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Lever : MonoBehaviour
{

    [SerializeField] Animator leverAnimator;
    //[SerializeField] Animator trapDoorAnimator;
    [SerializeField] TrapDoor trapDoor;
    [SerializeField] Outline outline;
    [SerializeField] AudioClip leverSfx;
    [SerializeField] [Range(0, 1f)] float leverSfxVolume;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player Projectile")
        {
            //Debug.Log("Lever activated");
            Destroy(other);
            leverAnimator.SetTrigger("Activate");
            AudioSource.PlayClipAtPoint(leverSfx, transform.position, leverSfxVolume);
            outline.enabled = false;
            trapDoor.ActivateTrapDoor();
           
              

        }
    }
}
