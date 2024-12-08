using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRobeCtrl : EnemyScript
{
    // Start is called before the first frame update
    GameObject player;
    public Rigidbody2D rb;
    public float distanceToPlayer;
    public float speed = 1.8f;
    public float obstacleAvoidanceDistance = 1f; // �P��ê���O�����̤p�Z��
    public float obstacleCheckRadius = 1.5f;    // �˴���ê�����d��b�|
    public bool OB = false;
    Vector2 directionToPlayer;
    void Start()
    {
        tag = "WhiteRobe";
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

        if (GameCtrl.TimeCounter % 60 == 0)
        {
            UseBloodMagic();
        }
    }
    private void FixedUpdate()
    {
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
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Circle"))
        {
            OB = true;
            //�p��Ϯg��V�A�קK��ê��
            Debug.Log("OB");
            // ²��a�p���V�A���ĤH������ê��
            Vector2 awayFromObstacle = (transform.position - collision.transform.position).normalized;
            directionToPlayer = Vector2.Lerp(directionToPlayer, awayFromObstacle, 0.5f).normalized;
            rb.velocity = directionToPlayer * speed;
            Debug.Log("Avoiding obstacle: " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OB = false;
    }
}
