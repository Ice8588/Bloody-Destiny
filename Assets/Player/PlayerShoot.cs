using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 gunPosition; // 槍口位置
    public GameObject bulletPrefab; // 子彈的預製件
    public LayerMask enemyLayer; // 敵人所在的圖層
    public float aimDuration = 3f; // 瞄準持續時間
    public float cooldownTime = 1f; // 冷卻時間
    public float bulletSpeed = 100f; // 子彈速度
    public Image greyFilter; // 灰色濾鏡

    private EnemyScript lockedEnemy; // 鎖定的敵人
    private bool isAiming = false; // 是否正在瞄準
    private bool isOnCooldown = false; // 是否處於冷卻狀態
    private Coroutine aimingCoroutine; // 紀錄瞄準協程

    void Start()
    {
        greyFilter.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); // 設置半透明
        greyFilter.gameObject.SetActive(false); // 初始關閉灰色濾鏡
        enemyLayer = LayerMask.GetMask("Enemy"); // 設置敵人圖層
    }

    void Update()
    {
        gunPosition = transform.position + Vector3.up * 0.5f; // 更新槍口位置

        if (Input.GetMouseButton(1) && !isOnCooldown && !isAiming) // 按下右鍵開始瞄準
        {
            aimingCoroutine = StartCoroutine(StartAiming());
        }

        Debug.Log("isAiming: " + isAiming);
        if (isAiming && Input.GetMouseButtonDown(0) && lockedEnemy != null) // 按下左鍵射擊
        {
            ShootAtEnemy();
        }

        if (Input.GetMouseButtonUp(1) && isAiming)
        {
            ExitAiming();
        }
    }

    private IEnumerator StartAiming()
    {
        isAiming = true;
        greyFilter.gameObject.SetActive(true); // 啟用灰色濾鏡
        Time.timeScale = 0.5f; // 慢動作效果

        float timer = 0f;

        while (timer < aimDuration)
        {
            // 如果放開右鍵，結束瞄準
            if (!Input.GetMouseButton(1))
            {
                ExitAiming();
                yield break;
            }

            timer += Time.unscaledDeltaTime; // 計算未受 TimeScale 影響的時間
            LockOntoClosestEnemy(); // 鎖定最近的敵人

            if (lockedEnemy != null)
            {
                lockedEnemy.ShowLockOnIndicator(); // 顯示敵人鎖定圖示
            }

            yield return null;
        }

        ExitAiming(); // 結束瞄準
        yield return new WaitForSecondsRealtime(cooldownTime); // 進入冷卻時間
        isOnCooldown = false; // 結束冷卻
    }

    private void LockOntoClosestEnemy()
    {
        // 搜索附近敵人
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 15f, enemyLayer);

        float shortestDistance = Mathf.Infinity;
        EnemyScript nearestEnemy = null;
        Debug.Log("enemies.Length: " + enemies.Length);

        foreach (Collider2D enemyCollider in enemies)
        {

            // 嘗試獲取敵人的 Enemy 組件
            EnemyScript enemy = enemyCollider.GetComponent<EnemyScript>();
            if (enemy == null) continue;

            float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        lockedEnemy = nearestEnemy; // 鎖定最近的敵人

        if (lockedEnemy != null)
        {
            lockedEnemy.ShowLockOnIndicator(); // 顯示鎖定圖示
        }
    }

    private void ShootAtEnemy()
    {
        if (lockedEnemy == null) return;

        GameObject bullet = Instantiate(bulletPrefab, gunPosition, Quaternion.identity);
        Vector2 direction = ((Vector2)lockedEnemy.transform.position - gunPosition).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        lockedEnemy.TakeDamage(2);

        ExitAiming(); // 發射後退出瞄準
    }

    private void ExitAiming()
    {
        if (!isAiming) return;

        isAiming = false;
        greyFilter.gameObject.SetActive(false); // 關閉灰色濾鏡
        Time.timeScale = 1f; // 恢復正常速度

        if (lockedEnemy != null)
        {
            lockedEnemy.HideLockOnIndicator();
        }

        if (aimingCoroutine != null)
        {
            StopCoroutine(aimingCoroutine); // 停止瞄準協程
        }

        lockedEnemy = null; // 清除鎖定的敵人
        isOnCooldown = true; // 進入冷卻
        StartCoroutine(Cooldown()); // 啟動冷卻計時
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(cooldownTime);
        isOnCooldown = false;
    }
}
