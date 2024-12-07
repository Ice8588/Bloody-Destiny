using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackRange = 0.5f;
    public int AttackDamage = 2;
    public float AttackCooldown = 1f;
    public float AttackOffset = 0.5f;
    public int CircleSegments = 100; // 圓形的段數

    public LayerMask EnemyLayer;
    private Vector3 AttackPointPosition;
    private LineRenderer lineRenderer;

    private float lastAttackTime = 0f;

    void Awake()
    {
        EnemyLayer = LayerMask.GetMask("Enemy");
        AttackPointPosition = transform.position;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = CircleSegments + 1; // +1 表示閉合圓形
        lineRenderer.useWorldSpace = true; // 使用世界座標
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + AttackCooldown)
        {
            UpdateAttackPointPosition();
            Attack();
            DrawCircle();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointPosition, AttackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("擊中敵人：" + enemy.name);
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(AttackDamage);
            }
        }
    }

    void UpdateAttackPointPosition()
    {
        // 獲取玩家當前的面對方向（假設面向右是正方向）
        Vector3 direction = transform.right; // 面向右側為正方向

        // 動態計算 AttackPoint 的位置
        AttackPointPosition = transform.position + direction * AttackOffset;
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPointPosition == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPointPosition, AttackRange);
    }

    void DrawCircle()
    {
        float angleStep = 360f / CircleSegments;

        for (int i = 0; i <= CircleSegments; i++) // 繪製完整的圓形
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            Vector3 point = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * AttackRange + AttackPointPosition;
            lineRenderer.SetPosition(i, point);
        }
    }
}