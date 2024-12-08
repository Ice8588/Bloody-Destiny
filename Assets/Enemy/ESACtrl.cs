using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESACtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider;

    public Vector2[] Area = new Vector2[2];
    public GameObject Corpse, GrayRobe, WhiteRobe;
    public int EnemyNum, CorpseNum=3, GrayRobeNum=2, WhiteRobeNum=1;
    void Start()
    {
        InvokeRepeating("SpawnCorpse", 3f, 15f);
        InvokeRepeating("SpawnWhiteRobe", 5f, 1f);
        InvokeRepeating("SpawnGrayRobe", 7f, 25f);
        EnemyNum = CorpseNum+GrayRobeNum+WhiteRobeNum;
        boxCollider = GetComponent<BoxCollider2D>();
        Area[0] = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.max.y);
        Area[1] = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnCorpse()
    {
        // �H���ͦ���m
        if (CorpseNum > 0)
        {
            float x = Random.Range(Area[0].x, Area[1].x);
            float y = Random.Range(Area[0].y, Area[1].y);
            Vector2 spawnPosition = new Vector2(x, y);
            //Debug.Log(spawnPosition);
            GameObject Enemy = Instantiate(Corpse, spawnPosition, new Quaternion(0, 0, 0, 0));
            Enemy.GetComponent<CorpseCtrl>().ESA = this.gameObject;
            CorpseNum--;
            EnemyNum--;
        }
    }
    public void SpawnWhiteRobe()
    {
        // �H���ͦ���m
        if (GrayRobeNum > 0)
        {
            float x = Random.Range(Area[0].x, Area[1].x);
            float y = Random.Range(Area[0].y, Area[1].y);
            Vector2 spawnPosition = new Vector2(x, y);
            Debug.Log(spawnPosition);
            GameObject Enemy2 = Instantiate(WhiteRobe, spawnPosition, new Quaternion(0, 0, 0, 0));
            Enemy2.GetComponent<WhiteRobeCtrl>().ESA = this.gameObject;
            WhiteRobeNum--;
            EnemyNum--;
        }
    }
    public void SpawnGrayRobe()
    {
        // �H���ͦ���m
        if (WhiteRobeNum > 0)
        {
            float x = Random.Range(Area[0].x, Area[1].x);
            float y = Random.Range(Area[0].y, Area[1].y);
            Vector2 spawnPosition = new Vector2(x, y);
            Debug.Log(spawnPosition);
            GameObject Enemy3 = Instantiate(GrayRobe, spawnPosition, new Quaternion(0, 0, 0, 0));
            Enemy3.GetComponent<GrayRobeCtrl>().ESA = this.gameObject;
            GrayRobeNum--;
            EnemyNum--;
        }
    }
}
