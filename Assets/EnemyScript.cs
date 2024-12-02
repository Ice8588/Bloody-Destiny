using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Health = 5;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shotAngle = new Vector3(0, 0, 0);//UnityEngine.Random.Range(0, 360)

        if (Ctrl.TimeCounter % 300 == 0)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletCtrl>().InitAngle = Quaternion.Euler(shotAngle);
        }
    }
}
