using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Health = 5;
    public int SwordATK = 2;
    public int BloodMagicATK = 3;
    public GameObject Bullet;
    protected Vector3 InitialPosition = new Vector3(-13, 2, 0);  //test
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (Health <= 0)
        {
            GameCtrl.KillCount++;
            Stage1.EnemyNum--;
            Destroy(this.gameObject);
        }

        if (transform.position.y <= (GameCtrl.SCREEN_HEIGHT * -1) - 5 || transform.position.y >= GameCtrl.SCREEN_HEIGHT + 5 ||
            transform.position.x <= (GameCtrl.CREEN_WIDTH * -1) - 5 || transform.position.x >= GameCtrl.CREEN_WIDTH + 5)
        {
            Stage1.EnemyNum--;
            Destroy(this.gameObject);
        }
    }

    protected void BloodMagic()
    {
        Vector3 shotAngle = new Vector3(0, 0, 0);
        Vector3 shotPos = transform.position;

        GameObject bullet = Instantiate(Bullet, transform.position, new Quaternion(0, 0, 0, 0));
        bullet.GetComponent<EnemyBulletCtrl>().SC.InitAngle = Quaternion.Euler(shotAngle);
        bullet.GetComponent<EnemyBulletCtrl>().SC.InitPosition = shotPos;
        bullet.GetComponent<EnemyBulletCtrl>().SC.Speed = new Vector2(0, 0.1f);
        bullet.GetComponent<EnemyBulletCtrl>().SC.IsTrace = true;
        bullet.GetComponent<EnemyBulletCtrl>().SC.TraceTime = 5;
        bullet.GetComponent<EnemyBulletCtrl>().SC.TraceNum = 10;
    }
}
