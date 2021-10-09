using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  If a new wave is needed, simply dublicate the 'Wave' GameObject in the 'Enemy Progress Canvas'.
 *  U can add/remove more 'Enemy icons' in the 'Wave' Gameobject. And dont forget to put every 'Wave'
    object in the 'waves' field of this script in the inspector.

 *  How to use :-
 *  Whenever you need to activate current wave UI elements - Call 'ActivateCurrentWave()' and pass true as param
 *  Whenever you kill an enemy - Call 'illEnemyInCurrentWave()' 
 *  After killing all enemies in current wave - Call 'ActivateCurrentWave()' and pass false as param to deactivate 
                                               current wave UI elements
                                             - also Call NextWave() to iterate to the next wave 

*/

namespace guns.Control
{
    public class EnemyWaveProgress : MonoBehaviour
    {
        [SerializeField] GameObject[] waves;   // serialize all the enemy waves gameobjects in the inspector

        int currentWaveIdx;
        int wavesLength;
       public int currentWaveCurrentEnemyIdx;
       public int currentWaveTotalEnemies;
        GameObject currentWave;
        // Start is called before the first frame update
        void Start()
        {
            currentWaveIdx = 0;

            wavesLength = 0;
            wavesLength = waves.Length;
        }

        // Update is called once per frame
        void Update()
        {
            //all the call the statements in this function are just for testing purpose

            /* if (Input.GetButtonDown("Fire1"))
                 ActivateCurrentWave(true);
             if (Input.GetButtonDown("Fire2"))
                 ActivateCurrentWave(false);
             if (Input.GetButtonDown("Fire3"))
                 NextWave();
             if (Input.GetButtonDown("Jump"))
                 KillEnemyInCurrentWave(); */
        }

        public void ActivateCurrentWave(bool val)  // use this function to activate or deactivate
        {                                    // the current enemy wave progress ui
            if (currentWaveIdx < wavesLength)
            {
                currentWave = waves[currentWaveIdx];
                currentWave.SetActive(val);
                if (val)
                {
                    currentWaveCurrentEnemyIdx = 0;
                    currentWaveTotalEnemies = currentWave.transform.childCount;
                }
            }


            else
                Debug.Log("Number of waves OutOfBounds");
        }

        public void NextWave()                  //use this func to iterate to the next enemy wave 
        {
            currentWaveIdx++;
        }

        public void KillEnemyInCurrentWave()   //call this function every time an emey is killed
        {
            if (currentWaveCurrentEnemyIdx < currentWaveTotalEnemies)
            {
                currentWave.transform.GetChild(currentWaveCurrentEnemyIdx).GetChild(0).gameObject.SetActive(true);
                currentWaveCurrentEnemyIdx++;
            }

            /*
            if(currentWaveCurrentEnemyIdx >= currentWaveTotalEnemies)
            {
                ActivateCurrentWave(false);
                NextWave();
            }

        */
        }
    }
}
