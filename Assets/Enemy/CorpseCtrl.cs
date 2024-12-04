using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseCtrl : EnemyScript
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = InitialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0.1f, 0, 0));  //test

        if (GameCtrl.TimeCounter % 180 == 0)
        {
            BloodMagic();
        }
    }
}
