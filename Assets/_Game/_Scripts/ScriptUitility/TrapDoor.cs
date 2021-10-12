using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] GameObject trapDoorEnemy;
    [SerializeField] Animator animator;
   
    Rigidbody rb;
   //Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = trapDoorEnemy.GetComponent<Rigidbody>();
       // collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTrapDoor()
    {
        animator.SetTrigger("Activate");
        
        rb.isKinematic = false;
       // collider.isTrigger = true;
    }
}
