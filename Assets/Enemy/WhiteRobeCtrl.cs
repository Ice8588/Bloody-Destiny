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
    public float obstacleAvoidanceDistance = 1f; // 與障礙物保持的最小距離
    public float obstacleCheckRadius = 1.5f;    // 檢測障礙物的範圍半徑
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
        // 計算前往玩家的方向
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

        // 檢查是否有障礙物
        Vector2 avoidanceDirection = AvoidObstacle();

        // 如果檢測到障礙物，優先避開障礙物方向
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
    private Vector2 AvoidObstacle()
    {
        // 使用 OverlapCircle 檢測周圍的障礙物
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, obstacleCheckRadius);

        foreach (var hit in hits)
        {
            // 如果檢測到帶有 "Obstacle" 標籤的物件
            if (hit.CompareTag("Obstacle"))
            {
                // 計算遠離障礙物的方向
                Vector2 directionAwayFromObstacle = (transform.position - hit.transform.position).normalized;

                // 如果障礙物過近，返回避開方向
                if (Vector2.Distance(transform.position, hit.transform.position) < obstacleAvoidanceDistance)
                {
                    return directionAwayFromObstacle;
                }
            }
        }

        // 沒有障礙物需要避開
        return Vector2.zero;
    }

    // 可視化檢測範圍（在 Scene 視圖中查看）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, obstacleCheckRadius);
    }
}
