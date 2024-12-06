using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject Corpse,GrayRobe,WhiteRobe;
    public static int EnemyNum = 0;
    public static int MAXEnemyNum = 10;
    // Start is called before the first frame update
    void Start()
    {
        var gameCtrl = GameCtrl.Instance;
        InvokeRepeating("SpawnCorpse", 0f, 10f);
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
        if (EnemyNum <= 0)
        {
            GameCtrl.GameClear();
        }
    }

    void SpawnCorpse()
    {
        // 隨機生成位置
        float x = Random.Range(5, GameCtrl.SCREEN_WIDTH - 5);
        float y = Random.Range(5, GameCtrl.SCREEN_HEIGHT - 5);
        Vector2 spawnPosition = new Vector2(x, y);
        Debug.Log(spawnPosition);
        Instantiate(Corpse, spawnPosition, new Quaternion(0, 0, 0, 0));
    }
}
