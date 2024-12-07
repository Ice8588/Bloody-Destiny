using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackRange = 1f; // 攻擊範圍
    public int AttackDamage = 2; // 攻擊傷害
    public float AttackCooldown = 0.5f; // 攻擊冷卻時間
    public float AttackAngle = 120f; // 扇形範圍角度
    public float EffectDuration = 0.2f; // 攻擊效果持續時間
    public int Segments = 120; // 圓形的段數
    public LayerMask EnemyLayer; // 敵人圖層
    private Vector2 AttackPointPosition; // 攻擊點位置
    private LineRenderer lineRenderer; // LineRenderer 組件
    private float lastAttackTime = 0f; // 上次攻擊時間


    void Awake()
    {
        EnemyLayer = LayerMask.GetMask("Enemy");
        AttackPointPosition = transform.position;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Segments + 2;
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

    void Attack()
    {
        AttackPointPosition = transform.position;
        DrawAttackCone(AttackPointPosition);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointPosition, AttackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("擊中敵人：" + enemy.name);
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();

            if (enemyScript)
            {
                enemyScript.TakeDamage(AttackDamage);
            }
        }
    }


    private void DrawAttackCone(Vector2 AttackPointPosition)
    {
        Vector3 direction = transform.up; // 玩家面向方向

        // 設置扇形範圍的起點
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, AttackPointPosition); // 扇形的圓心

        float angleStep = AttackAngle / Segments;
        for (int i = 0; i <= Segments; i++)
        {
            float currentAngle = -AttackAngle / 2 + angleStep * i;
            Vector2 rotatedDirection = Quaternion.Euler(0, 0, currentAngle) * direction;
            Vector2 point = AttackPointPosition + rotatedDirection.normalized * AttackRange;
            lineRenderer.SetPosition(i + 1, point);
        }

        // 關閉範圍（最後一條線連回圓心）
        lineRenderer.SetPosition(Segments + 1, AttackPointPosition);
    }


    void ClearAttackCone()
    {
        lineRenderer.enabled = false;
    }
}