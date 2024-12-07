using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayRobeCtrl : EnemyScript
{
    // Start is called before the first frame update
    GameObject player;
    public Rigidbody2D rb;
    public float distanceToPlayer;
    public float speed = 0.9f;
    private float speedbuf, delay = 0;
    public bool OB = false, CanUp = false, CanRight = false, CanLeft = false, CanDown = false, d_flag = true;
    Vector2 directionToPlayer;

    void Start()
    {
        delay = Time.time + 0.5f;
        speedbuf = speed;
        tag = "GrayRobe";
        player = GameObject.Find("Player");
        //transform.position = InitialPosition;
        if (player == null)
        {
            Debug.Log("Player object not found in the scene!");
        }
        else
        {
            Debug.Log("find");
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //transform.Translate(new Vector3(0.1f, 0, 0));  //test

        if (GameCtrl.TimeCounter % 100 == 0)
        {
            UseBloodMagic();
        }
    }
    private void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        // 計算前往玩家的方向
        directionToPlayer = (player.transform.position - transform.position).normalized;
        if (!OB)
        {
            MoveTowardsPlayer();
        }
    }
    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        //transform.position += direction * 0.5f * Time.deltaTime;
        if (distanceToPlayer > 1)
        {
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        // 面朝玩家
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  // 朝右
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // 朝左
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Circle"))
        {
            OB = true;

            //計算反射方向，避免障礙物
            Debug.Log("OB");
            // 簡單地計算方向，讓敵人遠離障礙物
            Vector2 awayFromObstacle = transform.position - collision.transform.position;
            directionToPlayer = Vector2.Lerp(directionToPlayer, awayFromObstacle, 0.5f).normalized;
            rb.velocity = directionToPlayer * speed;
            Debug.Log("Avoiding obstacle: " + collision.gameObject.name);
        }
    }
    void Delay()
    {
        Debug.Log("delay");
        d_flag = false;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        OB = false;
        speed = speedbuf;
    }
}
