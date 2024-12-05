using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int MaxHealth = 5, Health;
    public int SwordATK = 2;
    public int BloodMagicATK = 3;
    public GameObject BloodMagic;
    public GameObject HealthBar;
    protected Vector3 InitialPosition = new Vector3(-13, 2, 0);  //test
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (HealthBar != null)
        {
            HealthBar.transform.localScale = new Vector3((float)Health / MaxHealth, 0.1f, 0);
        }
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
            transform.position.x <= (GameCtrl.SCREEN_WIDTH * -1) - 5 || transform.position.x >= GameCtrl.SCREEN_WIDTH + 5)
        {
            Stage1.EnemyNum--;
            Destroy(this.gameObject);
        }
    }

    protected void UseBloodMagic()
    {
        Vector3 shotAngle = new Vector3(0, 0, 0);
        Vector3 shotPos = transform.position;

        GameObject newBloodMagic = Instantiate(BloodMagic, transform.position, new Quaternion(0, 0, 0, 0));
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.InitAngle = Quaternion.Euler(shotAngle);
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.InitPosition = shotPos;
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.Speed = new Vector2(0, 0.1f);
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.IsTrace = true;
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.TraceTime = 5;
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.TraceNum = 10;
    }
}
