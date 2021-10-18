using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace guns.Control
{
    public class enemyAnimationController : MonoBehaviour
    {
        EnemyWaveProgress EWP;
        private void Start()
        {
            EWP = FindObjectOfType<EnemyWaveProgress>();
        }
        public void slowTime()
        {
            if (EWP.currentWaveTotalEnemies == EWP.currentWaveCurrentEnemyIdx)
            {
                FindObjectOfType<timeManager>().SlowMotion();
            }
;
        }
    }
}
