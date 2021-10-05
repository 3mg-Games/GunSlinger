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
        public LayerMask WhoIsEnemy;

        public List<GameObject> enemy = new List<GameObject>();
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
            if (crosshairPsition.Count >= 1)
            {
                for (int i = 1; i < crosshairPsition.Count; i++)
                {
                    Instantiate(FindObjectOfType<GameManager>().crosshair, crosshairPsition[i] + new Vector3(0.5f, 0, 0), Quaternion.identity).transform.parent = GameObject.Find("Crosshair_Collection").transform;
                }
            }
        }



        public void shootToEnemy()
        {
            if (FindObjectOfType<GameManager>().StartShooting)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    //StartCoroutine(shootEnemy(0.5f, i));
                }
            }
        }

        IEnumerator shootEnemy(float t, int i)
        {
            transform.LookAt(enemy[i].transform.position);
            GameObject Bullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 25, ForceMode.Impulse);
            yield return new WaitForSeconds(t);
        }
    }
}
