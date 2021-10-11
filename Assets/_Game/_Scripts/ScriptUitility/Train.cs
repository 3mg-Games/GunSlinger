using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* No need to call this script as it will be called by
 * 'TrafficLight' script when bullet hits the 'Traffic Light' gameobject.
 */

public class Train : MonoBehaviour
{
    [SerializeField] Animator animator;
   
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
        
        EnemyHenchman enemyHenchman = other.GetComponent<EnemyHenchman>();
        if(enemyHenchman)
        {
            other.GetComponent<Rigidbody>().isKinematic = false;   //set the rigidbody of enemey to Kinematic
            enemyHenchman.Die();           // activate death anim on enemy
        }
    }

    public void ActivateTrain()
    {
        animator.SetTrigger("Move");     //activate move anim on train
    }
}
