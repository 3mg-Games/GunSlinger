using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace guns.Control
{
    public class BulletTimeSlider : MonoBehaviour
    {
        public Image bulletTime;
        float CurrentTime, MaxTime;





        //private Slider bulletTime;

        private void Start()
        {
            //bulletTime = GetComponent<Slider>();
        }

        private void Update()
        {
            BulletTimeFiller();
            CurrentTime = FindObjectOfType<timeManager>().slowdownLimit;
        }


        public void BulletTimeFiller()
        {
            bulletTime.fillAmount = CurrentTime / MaxTime;
            Color timeColor = Color.Lerp(Color.red, Color.green, (CurrentTime / MaxTime));
            bulletTime.color = timeColor;
        }

        public void setDefsultValueToSlider(float max)
        {
            MaxTime = max;
        }

    }
}
