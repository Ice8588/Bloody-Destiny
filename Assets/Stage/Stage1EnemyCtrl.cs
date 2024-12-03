using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1EnemyCtrl : MonoBehaviour
{
    public GameObject[] enemy = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameCtrl.TimeCounter == 60)
        {
            Instantiate(enemy[0]);
        }

        if (GameCtrl.TimeCounter == 120)
        {
            Instantiate(enemy[1]);
        }

        if (GameCtrl.TimeCounter == 180)
        {
            Instantiate(enemy[2]);
        }
    }
}
