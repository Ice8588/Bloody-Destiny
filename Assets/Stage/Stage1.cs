using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject[] enemy = new GameObject[3];
    public static int EnemyNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        var gameCtrl = GameCtrl.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCtrl.TimeCounter % 360 == 0)
        {
            Instantiate(enemy[2]);
            EnemyNum++;
        }
        else if (GameCtrl.TimeCounter % 240 == 0)
        {
            Instantiate(enemy[1]);
            EnemyNum++;
        }
        else if (GameCtrl.TimeCounter % 120 == 0)
        {
            Instantiate(enemy[0]);
            EnemyNum++;
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
