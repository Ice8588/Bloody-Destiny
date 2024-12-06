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
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = InitialPosition;
        if (player == null) 
        {
            Debug.LogError("Player object not found in the scene!");
        }
        else
        {
            Debug.LogError("find");
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
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

        // �ˬd�O�_����ê��
        Vector2 avoidanceDirection = AvoidObstacle();

        // �p�G�˴����ê���A�u���׶}��ê����V
        Vector2 finalDirection = avoidanceDirection != Vector2.zero ? avoidanceDirection : directionToPlayer;
        MoveTowardsPlayer();
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
    private Vector2 AvoidObstacle()
    {
        // �ϥ� OverlapCircle �˴��P�򪺻�ê��
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, obstacleCheckRadius);

        foreach (var hit in hits)
        {
            // �p�G�˴���a�� "Obstacle" ���Ҫ�����
            if (hit.CompareTag("Obstacle"))
            {
                // �p�⻷����ê������V
                Vector2 directionAwayFromObstacle = (transform.position - hit.transform.position).normalized;

                // �p�G��ê���L��A��^�׶}��V
                if (Vector2.Distance(transform.position, hit.transform.position) < obstacleAvoidanceDistance)
                {
                    return directionAwayFromObstacle;
                }
            }
        }

        // �S����ê���ݭn�׶}
        return Vector2.zero;
    }

    // �i�����˴��d��]�b Scene ���Ϥ��d�ݡ^
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, obstacleCheckRadius);
    }
}
