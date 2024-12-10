using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int MaxHealth = 5, Health;
    public int SwordATK = 2;
    public int BloodMagicATK = 3;
    public GameObject ESA;
    public GameObject BloodMagic;
    public GameObject HealthBar;
    public UnityEngine.UI.Image lockOnIndicator; // 鎖定圖示
    public new string tag = "origin";
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

            ESACtrl component = ESA.GetComponent<ESACtrl>();
            component.EnemyNum++;

            switch (tag)
            {
                case "Corpse":
                    component.CorpseNum++;
                    break;
                case "WhiteRobe":
                    component.WhiteRobeNum++;
                    break;
                case "GrayRobe":
                    component.GrayRobeNum++;
                    break;
                default:
                    break;
            }

            Destroy(this.gameObject);
        }

        if (transform.position.y <= -GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage] - 5 || transform.position.y >= GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage] + 5 ||
            transform.position.x <= -GameCtrl.SCREEN_WIDTH[GameCtrl.Stage] - 5 || transform.position.x >= GameCtrl.SCREEN_WIDTH[GameCtrl.Stage] + 5)
        {
            Stage1.EnemyNum--;
            ESACtrl component = ESA.GetComponent<ESACtrl>();
            component.EnemyNum++;

            switch (tag)
            {
                case "Corpse":
                    component.CorpseNum++;
                    break;
                case "WhiteRobe":
                    component.WhiteRobeNum++;
                    break;
                case "GrayRobe":
                    component.GrayRobeNum++;
                    break;
                default:
                    break;
            }
            Debug.Log("destroy");
            Debug.Log(this.transform.position);

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

    public void TakeDamage(int damage)
    {
        Health -= damage;
        GameCtrl.TotalDamege += damage;
    }

    public void ShowLockOnIndicator()
    {
        if (lockOnIndicator != null)
        {
            lockOnIndicator.gameObject.SetActive(true);
        }
    }

    public void HideLockOnIndicator()
    {
        if (lockOnIndicator != null)
        {
            lockOnIndicator.gameObject.SetActive(false);
        }
    }
}
