using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Control;

namespace guns.Core
{
    public class GameOverUI : MonoBehaviour
    {
        public GameObject WinUI;
        public GameObject confetti;
        public Animator scoreUI;
        public float delay = 1;
        public bool L1, L2, L3,L4;

        private GameManager Gm;
        private void Start()
        {
            WinUI.SetActive(false) ;
            confetti.SetActive(false) ;
            Gm = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if(Gm.GameOver)
                StartCoroutine(winGame(delay));
        }

        IEnumerator winGame(float T)
        {
            yield return new WaitForSeconds(0.5f);
            confetti.SetActive(true);
            yield return new WaitForSeconds(T);
            WinUI.SetActive(true);
            checkScore();           
        }
        void checkScore()
        {
            if (L1)
            {
                if(Gm.numberOfBulletsUsed == 3)
                {
                    scoreUI.Play("3H");
                }
                if (Gm.numberOfBulletsUsed == 4)
                {
                    scoreUI.Play("2H");
                }
                if (Gm.numberOfBulletsUsed > 4)
                {
                    scoreUI.Play("1H");
                }
            }

            if (L2)
            {
                if (Gm.numberOfBulletsUsed == 1)
                {
                    scoreUI.Play("3H");
                }
                if (Gm.numberOfBulletsUsed == 3)
                {
                    scoreUI.Play("2H");
                }
                if (Gm.numberOfBulletsUsed > 3)
                {
                    scoreUI.Play("1H");
                }
            }


            if (L3)
            {
                if (Gm.numberOfBulletsUsed == 2)
                {
                    scoreUI.Play("3H");
                }
                if (Gm.numberOfBulletsUsed == 3)
                {
                    scoreUI.Play("2H");
                }
                if (Gm.numberOfBulletsUsed > 3)
                {
                    scoreUI.Play("1H");
                }
            }

            if (L4)
            {
                if (Gm.numberOfBulletsUsed == 6)
                {
                    scoreUI.Play("3H");
                }
                if (Gm.numberOfBulletsUsed == 7)
                {
                    scoreUI.Play("2H");
                }
                if (Gm.numberOfBulletsUsed > 7)
                {
                    scoreUI.Play("1H");
                }
            }

        }
    }
}
