using UnityEngine;
using guns.Control;

/* HOW TO USE
 * Serialize all the required fields and place the TNT gameobject where required.
 * When any rigidbody will collide with tnt then it will check whether the colliding
 * object has a specific tag and if it has then the Tnt explosion will take place.
 * You might have to change the Explode function based on your need to trigger the 
 * exact enemy death animation or trigger any other behaviour. */

public class Tnt : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float explosionEffectDuration = 1.5f;
    [Tooltip("The force with which the tnt will throw away all gameobjects with rigidbody colliders in the blast radius.")]
    [SerializeField] float force = 500f;
    [Tooltip("Radius of the tnt blast radius")]
    [SerializeField] float radius = 5f;              
    [SerializeField] AudioClip explosionSfx;         // add the require explosion sfx
    [SerializeField] [Range(0, 1f)] float explosionSfxVolume;

    bool hasExploded = false;
    //AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
       // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetButtonDown("Fire1"))   // for testing purpose
         //   SetHasExploded(true);
                
        if(hasExploded)
        {
            Explode();
            hasExploded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")           //add 'Enemy' tag to all Enemies, also add a rigidbody kinematic/non-kinematic
        {
           // Debug.Log("trigger");
            SetHasExploded(true);
        }

        else if(other.tag == "Player Projectile")   //add 'Player Projectile' tag to Player bullet
        {
            SetHasExploded(true);
        }

        //if there are other objects which will trigger tnt explosion then, make a seperate
        // tag for them and add as another 'else if' statement in this function like the above ones
    }

    private void SetHasExploded(bool val)  // Call this function to activate tnt explosion. Pass 'true' as pararmeter.
    {
        hasExploded = val;
    }

    [SerializeField] Collider[] colliders;
    private void Explode()
    {
        
        GameObject explosion = Instantiate(explosionEffect,                  //Touch this function at your own risk.
            transform.position, transform.rotation);

        Destroy(explosion, explosionEffectDuration);    //destroy explosion Vfx after certain duration

        /*Collider[]*/ colliders = Physics.OverlapSphere(transform.position, radius);  //storing all colliders in an array which are in the blast radius.
        foreach(Collider nearbyObject in colliders)
        {
            if(nearbyObject.tag == "Enemy")       // if nearby object has an 'Enemy' tag, then only apply blast force to it and trigger its death.
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();   // rigidbody of the nearby enemy.

                if(rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(force, transform.position, radius);
                    nearbyObject.GetComponent<enemyContoller>().TNTHit();
                    //calling the die function in enemy.
                }
            }
        }

        AudioSource.PlayClipAtPoint(explosionSfx, transform.position, explosionSfxVolume);

        //Destroy(gameObject);
        destroy();


    }

    void destroy()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
