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
    Vector2 directionToPlayer, lastPosition, awayFromObstacle;

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
        lastPosition = transform.position;
        base.Update();
        //transform.Translate(new Vector3(0.1f, 0, 0));  //test

        //if(GameCtrl.TimeCounter % 60 == 0)
        //{
        //    UseBloodMagic();
        //}
    }
    private void FixedUpdate()
    {
        float x = transform.position.x, y = transform.position.y;
        bool tflag = false;

        if (transform.position.x >= 19.3)
        {
            tflag = true;
            x = 19;
        }
        else if (transform.position.x <= -19.3)
        {
            tflag = true;
            x = -19;
        }
        if (transform.position.y >= 19.3)
        {
            tflag = true;
            y = 19;
        }
        else if (transform.position.y <= -19.3)
        {
            tflag = true;
            y = -19;
        }
        if (tflag)
        {
            transform.Translate(new Vector2(x, y) - (Vector2)transform.position);
        }
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        // �p��e�����a����V
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

        // ���ª��a
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  // �¥k
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // �¥�
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Circle") || collision.CompareTag("Enemy"))
        {
            OB = true;

            //�p��Ϯg��V�A�קK��ê��
            Debug.Log("OB");

            // ²��a�p���V�A���ĤH������ê��
            //if (awayFromObstacle.y < 0)
            //{
            //    CanUp = true;
            //    transform.Translate(new Vector2(0, -0.1f));
            //}
            //else if (awayFromObstacle.y > 0)
            //{
            //    CanDown = true;
            //    transform.Translate(new Vector2(0, 0.1f));
            //}
            //if (awayFromObstacle.x < 0)
            //{
            //    CanRight = true;
            //    transform.Translate(new Vector2(-0.1f, 0));
            //}
            //else if (awayFromObstacle.x > 0)
            //{
            //    CanLeft = true;
            //    transform.Translate(new Vector2(0.1f, 0));
            //}

            directionToPlayer = Vector2.Lerp(directionToPlayer, awayFromObstacle, 0.5f).normalized;
            rb.velocity = directionToPlayer * speed;
            transform.position = lastPosition;
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
