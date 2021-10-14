using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Control;
using guns.Core;

/* No need to call this script as it will be called by
 * 'TrafficLight' script when bullet hits the 'Traffic Light' gameobject.
 */

public class Train : MonoBehaviour
{
    [SerializeField] Animator animator;
    //[SerializeField] AudioClip trainSfx;
    [SerializeField] [Range(0f, 1f)] float trainSfxVolume;
   
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
      //  Debug.Log("Enemy knock off happened");
        
        enemyContoller enemyHenchman = other.GetComponent<enemyContoller>();
        if(enemyHenchman)
        {
            other.GetComponent<Rigidbody>().isKinematic = false;   //set the rigidbody of enemey to Kinematic
            enemyHenchman.health = 0;        // activate death anim on enemy
        }
    }

    public void ActivateTrain()
    {
        animator.SetTrigger("Move");     //activate move anim on train
        
        //AudioSource.PlayClipAtPoint(trainSfx, Camera.main.transform.position, trainSfxVolume);
    }

    public void PlayTrainSfx()
    {
        FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().trainSfx, trainSfxVolume);
    }
}
