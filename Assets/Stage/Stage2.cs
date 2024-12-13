using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    public GameObject Corpse, GrayRobe, WhiteRobe;
    public static int EnemyNum = 0;
    public static int MAXEnemyNum = 10;
    public static string MissionContent = "找到正確的傳送門";


    // Start is called before the first frame update
    void Start()
    {
        GameCtrl.Stage = 1;
        var gameCtrl = GameCtrl.Instance;
        // InvokeRepeating("SpawnCorpse", 0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCtrl.TimeCounter == 240)
        {
            //Instantiate(enemy[1]);
        }

        if (GameCtrl.TimeCounter == 360)
        {
            //Instantiate(enemy[2]);
        }
    }

    void LateUpdate()
    {
        // if (EnemyNum <= 0)
        // {
        //     GameCtrl.GameClear();
        // }
    }


}
