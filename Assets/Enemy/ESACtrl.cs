using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESACtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider;

    public Vector2[] Area = new Vector2[2];
    public GameObject Corpse, GrayRobe, WhiteRobe;
    public int EnemyNum;
    void Start()
    {
        InvokeRepeating("SpawnCorpse", 0f, 1f);
        //EnemyNum = 1;
        boxCollider = GetComponent<BoxCollider2D>();
        Area[0] = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.max.y);
        Area[1] = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnCorpse()
    {
        if (EnemyNum > 0)
        {
            float x = Random.Range(Area[0].x, Area[1].x);
            float y = Random.Range(Area[0].y, Area[1].y);
            Vector2 spawnPosition = new Vector2(x, y);
            //Debug.Log(spawnPosition);
            GameObject Enemy = Instantiate(Corpse, spawnPosition, new Quaternion(0, 0, 0, 0));
            Enemy.GetComponent<CorpseCtrl>().ESA = this.gameObject;
            EnemyNum--;
        }
    }
}
