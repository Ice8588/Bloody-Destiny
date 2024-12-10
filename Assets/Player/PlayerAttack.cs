using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackRange = 1f; // 攻擊範圍
    public int AttackDamage = 2; // 攻擊傷害
    public float AttackCooldown = 3000f; // 攻擊冷卻時間
    public float AttackAngle = 120f; // 扇形範圍角度
    public float EffectDuration = 1000f; // 攻擊效果持續時間
    public int Segments = 120; // 圓形的段數
    public LayerMask EnemyLayer; // 敵人圖層
    private Vector2 AttackPointPosition; // 攻擊點位置
    private LineRenderer lineRenderer; // LineRenderer 組件
    private float lastAttackTime = 0f; // 上次攻擊時間
    public Animator animator;
    public int counter = 0;

    void Awake()
    {
        EnemyLayer = LayerMask.GetMask("Enemy");
        AttackPointPosition = transform.position;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Segments;
        lineRenderer.useWorldSpace = true; // 使用世界座標
    }

    void Start()
    {
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + AttackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
            Invoke(nameof(ClearAttackCone), EffectDuration); // 在指定時間後清除
        }
    }

    void FixedUpdate()
    {
        if(counter>0)counter--;
    }

    void Attack()
    {
        AttackPointPosition = transform.position;
        DrawAttackCone(AttackPointPosition);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointPosition, AttackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // 計算敵人相對於玩家的方向
            Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // 計算玩家的正前方方向
            Vector2 playerForward = transform.up; // 以 X 軸方向為玩家的正前方

            // 計算角度（用餘弦公式檢查角度範圍是否小於 60 度）
            float angle = Vector2.Angle(playerForward, directionToEnemy);

            if (angle <= AttackAngle / 2) // 在 120 度範圍內
            {
                Debug.Log("擊中敵人：" + enemy.name);

                EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
                
                if (enemyScript)
                {
                    enemyScript.TakeDamage(AttackDamage);
                    PlayerCtrl.BloodGroove += AttackDamage / 2;
                }
            }
        }
    }


    private void DrawAttackCone(Vector2 AttackPointPosition)
    {
        animator.SetFloat("slash", 1);
        Vector3 direction = transform.up; // 玩家面向方向
        lineRenderer.enabled = true;

        float angleStep = AttackAngle / Segments;
        for (int i = 0; i < Segments; i++)
        {
            float currentAngle = -AttackAngle / 2 + angleStep * i;
            Vector2 rotatedDirection = Quaternion.Euler(0, 0, currentAngle) * direction;
            Vector2 point = AttackPointPosition + rotatedDirection.normalized * AttackRange;
            lineRenderer.SetPosition(i, point);
        }


    }


    void ClearAttackCone()
    {
        lineRenderer.enabled = false;
        animator.SetFloat("slash", 0);
    }
}