using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject Corpse, GrayRobe, WhiteRobe;
    public static int EnemyNum = 10;
    public static int MAXEnemyNum = 10;
    public static string MissionContent = "清理"+ MAXEnemyNum+"隻異形(剩餘"+EnemyNum+"隻)";
    // Start is called before the first frame update
    void Start()
    {
        GameCtrl.Stage = 0;
        EnemyNum = MAXEnemyNum;
        var gameCtrl = GameCtrl.Instance;
        // InvokeRepeating("SpawnCorpse", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        MissionContent = "清理" + MAXEnemyNum + "隻異形(剩餘" + EnemyNum + "隻)";
        if (GameCtrl.TimeCounter == 240)
        {
            //Instantiate(enemy[1]);
        }
        else if (GameCtrl.TimeCounter % 120 == 0)
        {
            //Instantiate(enemy[2]);
        }


    }

    void LateUpdate()
    {
        if (EnemyNum <= 0)
        {
            GameCtrl.GameClear();
        }
    }


}
