using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;
using guns.Control;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject[] logs;
   // [SerializeField] AudioClip woodenLogsSfx;
    [SerializeField] [Range(0, 1f)] float woodenLogsSfxVolume;

    MeshRenderer mesh;

    bool logsActive = false;
    timeManager time;
    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<timeManager>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(logsActive && !time.timeTriggered)
        {
            logsActive = false;
            FindObjectOfType<GameManager>().source.PlayOneShot(FindObjectOfType<GameManager>().woodenLogs, woodenLogsSfxVolume);
            Destroy(gameObject);
;        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player Projectile")
        {
            logsActive = true;
            //AudioSource.PlayClipAtPoint(woodenLogsSfx, transform.position, woodenLogsSfxVolume);
            foreach(GameObject log in logs)
            {
                log.GetComponent<Rigidbody>().isKinematic = false;
            }
            mesh.enabled = false;
        }
    }

   
}
