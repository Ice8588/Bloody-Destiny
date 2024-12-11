using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRobeAttack : MonoBehaviour
{
    public float AttackRange = 1f; // �����d��
    public int AttackDamage = 5; // �����ˮ`
    public float AttackCooldown = 1f; // �����N�o�ɶ�
    public float AttackAngle = 360f; // ���νd�򨤫�
    public float EffectDuration = 0.05f; // �����ĪG����ɶ�
    public int Segments = 360; // ��Ϊ��q��
    public LayerMask PlayerLayer; // �ĤH�ϼh
    private Vector2 AttackPointPosition; // �����I��m
    private LineRenderer lineRenderer; // LineRenderer �ե�
    private float lastAttackTime = 0f; // �W�������ɶ�
    public GameObject player;
    float distanceToPlayer;
    public Animator animator;


    void Awake()
    {
        PlayerLayer = LayerMask.GetMask("Player");
        AttackPointPosition = transform.position;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Segments;
        lineRenderer.useWorldSpace = true; // �ϥΥ@�ɮy��
    }

    void Start()
    {
        lineRenderer.enabled = false;
    }

    void Update()
    {

        distanceToPlayer = Vector2.Distance(transform.position, PlayerCtrl.PlayerPos);
        if (Time.time >= lastAttackTime + AttackCooldown && distanceToPlayer <= 1.5)
        {
            Attack();
            lastAttackTime = Time.time;
            Invoke(nameof(ClearAttackCone), EffectDuration); // �b���w�ɶ���M��
        }
    }

    void Attack()
    {
        AttackPointPosition = transform.position;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointPosition, AttackRange, PlayerLayer);
        DrawAttackCone(AttackPointPosition);
        animator.SetFloat("attack", 1);
        foreach (Collider2D player in hitEnemies)
        {
            PlayerCtrl playerCtrl = player.GetComponent<PlayerCtrl>();

            if (playerCtrl)
            {
                playerCtrl.TakeDamage(AttackDamage);
            }
        }
    }


    private void DrawAttackCone(Vector2 AttackPointPosition)
    {
        float AngleZ = Mathf.Atan2(PlayerCtrl.PlayerPos.y - transform.position.y, PlayerCtrl.PlayerPos.x - transform.position.x);
        Vector2 direction = (Quaternion.Euler(0, 0, AngleZ * Mathf.Rad2Deg - 90) * Vector2.up).normalized;
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
        animator.SetFloat("attack", 0);
        lineRenderer.enabled = false;
    }
}
