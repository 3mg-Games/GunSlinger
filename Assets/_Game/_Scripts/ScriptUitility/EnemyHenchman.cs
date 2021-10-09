using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHenchman : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

}
