using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using guns.Core;

namespace guns.Control {
    public class playerController : MonoBehaviour
    {
        public float shootRange = 10;
        public GameObject bullet;
        public Transform shootPoint;
        public Vector3 crosshairOffset;

        public List<Vector3> crosshairPsition = new List<Vector3>();

        void Update()
        {
            
        }
        public void raycaster()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {

                if (!crosshairPsition.Contains(hit.point))
                    crosshairPsition.Add(hit.point);
            }
            CrosshairPlacer();
        }

        

        public void CrosshairPlacer()
        {

            int i;
            i = 1;
            if (crosshairPsition.Count >= 1)
            {
                Instantiate(FindObjectOfType<GameManager>().crosshair, crosshairPsition[i] - crosshairOffset, Quaternion.identity).transform.parent = GameObject.Find("Crosshair_Collection").transform;
                i++;
            }
        }
        
    }
}
