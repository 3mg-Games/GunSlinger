using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject[] logs;
    [SerializeField] AudioClip woodenLogsSfx;
    [SerializeField] [Range(0, 1f)] float woodenLogsSfxVolume;
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
            AudioSource.PlayClipAtPoint(woodenLogsSfx, transform.position, woodenLogsSfxVolume);
            foreach(GameObject log in logs)
            {
                log.GetComponent<Rigidbody>().isKinematic = false;
            }
            Destroy(gameObject);
        }
    }
}
