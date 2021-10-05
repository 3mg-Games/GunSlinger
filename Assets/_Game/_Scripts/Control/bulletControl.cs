using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Control;

public class bulletControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
