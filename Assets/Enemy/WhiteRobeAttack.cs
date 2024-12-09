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

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time >= lastAttackTime + AttackCooldown)
        {
            Debug.Log(distanceToPlayer);
            Attack();
            lastAttackTime = Time.time;
            Invoke(nameof(ClearAttackCone), EffectDuration); // �b���w�ɶ���M��
        }
    }

    void Attack()
    {
        AttackPointPosition = transform.position;
        DrawAttackCone(AttackPointPosition);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPointPosition, AttackRange, PlayerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("�����ĤH�G" + enemy.name);
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
        //Vector3 direction = transform.up; // ���a���V��V
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
