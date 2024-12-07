using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    public GameObject[] enemy = new GameObject[3];
    public static int EnemyNum = 3;
    // Start is called before the first frame update
    void Start()
    {
        var gameCtrl = GameCtrl.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCtrl.TimeCounter == 120)
        {
            Instantiate(enemy[0]);
            Debug.Log("Enemy 0");
        }

        if (GameCtrl.TimeCounter == 240)
        {
            Instantiate(enemy[1]);
        }

        if (GameCtrl.TimeCounter == 360)
        {
            Instantiate(enemy[2]);
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
