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
        //Vector3 shotAngle = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        Vector3 shotPos = transform.position;

        if (Ctrl.TimeCounter % 120 == 0)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, new Quaternion(0, 0, 0, 0));
            // bullet.GetComponent<EnemyBulletCtrl>().SC.InitAngle = Quaternion.Euler(shotAngle);
            bullet.GetComponent<EnemyBulletCtrl>().SC.InitPosition = shotPos;
            bullet.GetComponent<EnemyBulletCtrl>().SC.Speed = new Vector2(0, 0.1f);
            bullet.GetComponent<EnemyBulletCtrl>().SC.IsTrace = true;
            bullet.GetComponent<EnemyBulletCtrl>().SC.TraceTime = 5;
            bullet.GetComponent<EnemyBulletCtrl>().SC.TraceNum = 10;
        }
    }

    private void LateUpdate()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
