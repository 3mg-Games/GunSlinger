using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* HOW TO USE -
 * Serialize the 'red light', 'green light' and the 'Train' gameobjects
 * in the inspector.
 * 
 * Add 'Player Projectile' tag to the Player Bullet.
 * Whenever bullet will be fired at traffic light then this script
 * will deactivate 'Red Light' and activate 'Red Light' and call
 * the 'Activate Train()' from the 'Train' script wich will basically move the 
 * train from left to right.
 * */

public class TrafficLight : MonoBehaviour
{
    [SerializeField] GameObject redLight;
    [SerializeField] GameObject greenLight;
    [SerializeField] Train train;
    [SerializeField] Outline outline;
    [SerializeField] GameObject sparkVfxPrefab;
    [SerializeField] float duartionOfSparkVfx = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetButtonDown("Fire1"))   //for testing purpose
          //  ActivateTrainProtocol();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player Projectile")   //if   colliding gameobject has 'Player Projectile' tag
        {                                      // then swich lights and activate the train
            ActivateTrainProtocol(other);
        }
    }

    private void ActivateTrainProtocol(Collider other)
    {
        GameObject sparkVfx = Instantiate(sparkVfxPrefab,
            other.transform.position,
            Quaternion.identity);
        Destroy(sparkVfx, duartionOfSparkVfx);
        redLight.SetActive(false);
        greenLight.SetActive(true);
        train.ActivateTrain();
        outline.enabled = false;
    }
}
