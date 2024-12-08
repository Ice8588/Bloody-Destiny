using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject BloodMagic;
    public float WalkSpeed = 5f, RunSpeed = 8f, dodgeSpeed = 30f;
    public float dodgeDuration = 0.05f, dodgeCooldown = 0.5f;
    public static int MaxHealth = 10, Health = 0, MaxBloodPower = 0, BloodPower = 0, BloodGroove = 0, BloodGrooveMax = 10;
    public int BloodPowerCost = 2;
    public static Vector3 PlayerPos;
    private Vector2 movement;           // 玩家移動向量
    private Rigidbody2D rb;             // 玩家剛體
    private bool isDodging = false, isRunning = false;     // 是否處於閃避狀態
    private float lastDodgeTime = -Mathf.Infinity; // 上次閃避的時間

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        MaxBloodPower = Health;
        BloodPower = MaxBloodPower;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = transform.position;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        FaceMouse();

        // 嘗試進行閃避
        if (Input.GetKeyDown(KeyCode.Space) && CanDodge())
        {
            StartCoroutine(Dodge());
        }

        isRunning = Input.GetKey(KeyCode.LeftShift);


        if (Input.GetKeyDown(KeyCode.F) && BloodPower >= BloodPowerCost)
        {
            Instantiate(BloodMagic, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            BloodPower -= BloodPowerCost;
        }
    }

    void LateUpdate()
    {
        if (GameCtrl.TimeCounter % 300 == 0 && BloodPower < Health)
        {
            BloodPower++;
        }
        else
        {
            BloodPower = Mathf.Max(0, Mathf.Min(Health, BloodPower));
        }
    }

    void FixedUpdate()
    {
        // 如果正在閃避，不執行普通移動邏輯
        if (!isDodging)
        {
            Move();
        }
    }

    void Move()
    {
        float PlayerSpeed = isRunning ? RunSpeed : WalkSpeed;

        Vector2 newPosition = rb.position + movement.normalized * PlayerSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -GameCtrl.SCREEN_WIDTH, GameCtrl.SCREEN_WIDTH);
        newPosition.y = Mathf.Clamp(newPosition.y, -GameCtrl.SCREEN_HEIGHT, GameCtrl.SCREEN_HEIGHT);
        rb.MovePosition(newPosition);
    }

    bool CanDodge()
    {
        return Time.time >= lastDodgeTime + dodgeCooldown && movement.magnitude > 0;
    }

    IEnumerator Dodge()
    {
        isDodging = true;
        lastDodgeTime = Time.time;

        // 獲取閃避方向
        Vector2 dodgeDirection = movement.normalized;

        float dodgeEndTime = Time.time + dodgeDuration;
        while (Time.time < dodgeEndTime)
        {
            // 閃避時的移動
            Vector2 newPosition = rb.position + dodgeDirection * dodgeSpeed * Time.fixedDeltaTime;

            // 限制在邊界內
            newPosition.x = Mathf.Clamp(newPosition.x, -GameCtrl.SCREEN_WIDTH, GameCtrl.SCREEN_WIDTH);
            newPosition.y = Mathf.Clamp(newPosition.y, -GameCtrl.SCREEN_HEIGHT, GameCtrl.SCREEN_HEIGHT);

            rb.MovePosition(newPosition);
            yield return null; // 等待下一幀
        }

        isDodging = false;
    }

    void FaceMouse()
    {
        // 獲取滑鼠位置的世界座標
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 確保 Z 軸為 0

        // 計算玩家到滑鼠的方向向量
        Vector2 direction = (mousePosition - transform.position).normalized;

        // 使用 Mathf.Atan2 計算旋轉角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 設置玩家的旋轉角度，讓 +Y 軸指向滑鼠
        //transform.rotation = Quaternion.Euler(0, 0, angle - 90); // 減去 90 度讓正面對齊 +Y

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        PlayerHeal playerHeal = GetComponent<PlayerHeal>();

        if (playerHeal != null)
        {
            playerHeal.InterruptHealing();
        }
    }

}
