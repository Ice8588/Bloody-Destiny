using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRobeAttack : MonoBehaviour
{
    public float AttackRange = 1f; // 攻擊範圍
    public int AttackDamage = 5; // 攻擊傷害
    public float AttackCooldown = 1f; // 攻擊冷卻時間
    public float AttackAngle = 360f; // 扇形範圍角度
    public float EffectDuration = 0.05f; // 攻擊效果持續時間
    public int Segments = 360; // 圓形的段數
    public LayerMask PlayerLayer; // 敵人圖層
    private Vector2 AttackPointPosition; // 攻擊點位置
    private LineRenderer lineRenderer; // LineRenderer 組件
    private float lastAttackTime = 0f; // 上次攻擊時間
    public GameObject player;
    float distanceToPlayer;


    void Awake()
    {
        PlayerLayer = LayerMask.GetMask("Player");
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

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time >= lastAttackTime + AttackCooldown)
        {
            Debug.Log(distanceToPlayer);
            Attack();
            lastAttackTime = Time.time;
            Invoke(nameof(ClearAttackCone), EffectDuration); // 在指定時間後清除
        }
    }

    void Attack()
    {
        AttackPointPosition = transform.position;
        DrawAttackCone(AttackPointPosition);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointPosition, AttackRange, PlayerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("擊中敵人：" + enemy.name);
            PlayerCtrl playerScript = enemy.GetComponent<PlayerCtrl>();

            if (playerScript)
            {
                playerScript.TakeDamage(AttackDamage);
                PlayerCtrl.BloodGroove += AttackDamage / 2;
            }
        }
    }


    private void DrawAttackCone(Vector2 AttackPointPosition)
    {
        Vector3 direction = player.transform.position - transform.position;
        //Vector3 direction = transform.up; // 玩家面向方向
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
    }
}
