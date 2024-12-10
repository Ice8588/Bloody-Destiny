using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject BloodMagic;
    private GameObject[] traps;
    public float WalkSpeed = 5f, RunSpeed = 8f, dodgeSpeed = 30f;
    public float dodgeDuration = 0.05f, dodgeCooldown = 0.5f;
    public static int MaxHealth = 100, Health = 100, MaxBloodPower = 0, BloodPower = 0, BloodGroove = 0, BloodGrooveMax = 20;
    public int BloodPowerCost = 2;
    private Vector2 lastPosition; // 記錄角色的上一次位置
    public static Vector3 PlayerPos;
    private Vector2 movement;           // 玩家移動向量
    private Rigidbody2D rb;             // 玩家剛體
    private bool isDodging = false, isRunning = false;     // 是否處於閃避狀態
    private float lastDodgeTime = -Mathf.Infinity; // 上次閃避的時間
    public bool CanUp = false, CanRight = false, CanLeft = false, CanDown = false;
    private bool isTrapActive = true;
    public float visibleTime = 1f;
    public float hiddenTime = 1f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        traps = GameObject.FindGameObjectsWithTag("Stab");
        Health = MaxHealth;
        MaxBloodPower = Health;
        BloodPower = MaxBloodPower;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ToggleTrap());
    }

    IEnumerator ToggleTrap()
    {
        while (true)
        {
            foreach (GameObject trap in traps)
            {
                SetTrapState(trap, true);
                isTrapActive = true;
            }
            yield return new WaitForSeconds(visibleTime);

            foreach (GameObject trap in traps)
            {
                SetTrapState(trap, false);
                isTrapActive = false;
            }

            yield return new WaitForSeconds(hiddenTime);
        }
    }

    void SetTrapState(GameObject trap, bool isVisible)
    {
        SpriteRenderer renderer = trap.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = isVisible ? Mathf.Clamp01(0.3f) : Mathf.Clamp01(1f);
            renderer.color = color;
        }

        trap.GetComponent<Collider2D>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerPos = transform.position;
        lastPosition = transform.position;
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //if (!CanUp && transform.position.y + 0.3 <= GameCtrl.SCREEN_HEIGHT && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        //{
        //    movement+=new Vector2(0, 1);
        //}
        //else if (!CanDown && transform.position.y - 0.3 >= -GameCtrl.SCREEN_HEIGHT && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        //{
        //    movement+=new Vector2(0, -1);
        //}
        //if (!CanLeft && transform.position.x - 0.3 >= -GameCtrl.SCREEN_WIDTH && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        //{
        //    movement+=new Vector2(-1, 0);
        //}
        //else if (!CanRight && transform.position.x + 0.3 <= GameCtrl.SCREEN_WIDTH && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        //{
        //    movement+=new Vector2(1, 0);
        //}
        this.FaceMouse();

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

        BloodGroove = Mathf.Max(0, Mathf.Min(BloodGrooveMax, BloodGroove));
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
        if(movement != Vector2.zero)
        animator.SetFloat("run",Mathf.Abs(PlayerSpeed));
        else
            animator.SetFloat("run", Mathf.Abs(0));
        Vector2 newPosition = rb.position + movement.normalized * PlayerSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -GameCtrl.SCREEN_WIDTH[GameCtrl.Stage], GameCtrl.SCREEN_WIDTH[GameCtrl.Stage]);
        newPosition.y = Mathf.Clamp(newPosition.y, -GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage], GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage]);
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
            newPosition.x = Mathf.Clamp(newPosition.x, -GameCtrl.SCREEN_WIDTH[GameCtrl.Stage], GameCtrl.SCREEN_WIDTH[GameCtrl.Stage]);
            newPosition.y = Mathf.Clamp(newPosition.y, -GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage], GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage]);

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
        //Debug.Log(mousePosition);

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
    void OnTriggerEnter2D(Collider2D other)
    {
        
        Vector2 direction = other.transform.position - transform.position;

        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Touch");
            //    if (direction.y > 0 || (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) CanUp = true;
            //    if (direction.y < 0 || (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) CanDown = true;
            //    if (direction.x > 0 || (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) CanRight = true;
            //    if (direction.x < 0 || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) CanLeft = true;
            transform.position = lastPosition;
        }
        else if (other.CompareTag("Circle"))
        {
            //if (direction.y > 0) CanUp = true;
            //if (direction.y < 0) CanDown = true;
            //if (direction.x > 0) CanRight = true;
            //if (direction.x < 0) CanLeft = true;
            transform.position = PlayerPos;
        }
        else if (other.CompareTag("Trap"))
        {
            WalkSpeed *= 0.5f;
            RunSpeed *= 0.5f;
        }
        else if (other.CompareTag("Fire") || other.CompareTag("Thorns"))
        {
            TakeDamage(10);   
        }
        else if (other.CompareTag("Stab") && isTrapActive)
        {
            TakeDamage(10);
        }
        else if (other.CompareTag("Blue") || other.CompareTag("FakeTeleport"))
        {
            transform.position = new Vector2(18.5f, -19.5f);
        }
        else if (other.gameObject.name == "SmallPortal")
        {
            transform.position = new Vector2(-18.5f, -17f);
        }
        else if (other.gameObject.name == "SmallPortal (1)")
        {
            transform.position = new Vector2(12.29f, -1.26f);
        }
        else if (other.gameObject.name == "SmallPortal (2)")
        {
            transform.position = new Vector2(18.5f, 13.5f);
        }
        else if (other.gameObject.name == "SmallPortal (3)")
        {
            transform.position = new Vector2(-19f, -1f);
        }
        else if (other.gameObject.name == "SmallPortal (4)")
        {
            transform.position = new Vector2(6f, 16f);
        }
        else if (other.gameObject.name == "SmallPortal (5)")
        {
            transform.position = new Vector2(12f, 3f);
        }
        else if (other.gameObject.name == "SmallPortal (7)")
        {
            //SceneManager.LoadScene("GameOverMenu");
        }
        else if (other.gameObject.name == "SmallPortal (9)")
        {
            transform.position = new Vector2(-15.5f, 18.5f);
        }
        else if (other.gameObject.name == "SmallPortal (10)")
        {
            transform.position = new Vector2(11f, 13f);
        }
        else if (other.gameObject.name == "SmallPortal (11)")
        {
            transform.position = new Vector2(-13f, -11f);
        }
        else if (other.gameObject.name == "SmallPortal (12)")
        {
            transform.position = new Vector2(-10f, 12f);
        }
        else if (other.gameObject.name == "SmallPortal (13)")
        {
            transform.position = new Vector2(8.5f, -10.5f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Vector2 direction = other.transform.position - transform.position;

        if (other.CompareTag("Obstacle"))
        {
            transform.position = lastPosition;
            //    if (direction.y > 0 || (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            //    {
            //        CanUp = true;
            //        transform.Translate(new Vector2(0,-0.2f));
            //    }
            //    else if (direction.y < 0 || (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            //    {
            //        CanDown = true;
            //        transform.Translate(new Vector2(0, 0.2f));
            //    }
            //    if (direction.x > 0 || (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            //    {
            //        CanRight = true;
            //        transform.Translate(new Vector2(-0.2f,0));
            //    }
            //    else if(direction.x < 0 || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            //    {
            //        CanLeft = true;
            //        transform.Translate(new Vector2(0.2f, 0));
            //    }
        }
        //else if (other.CompareTag("Circle"))
        //{
        //    if (direction.y > 0) CanUp = true;
        //    if (direction.y < 0) CanDown = true;
        //    if (direction.x > 0) CanRight = true;
        //    if (direction.x < 0) CanLeft = true;
        //}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("hi");
        if (other.CompareTag("Trap"))
        {
            WalkSpeed *= 2f;
            RunSpeed *= 2f;
        }
        if (other.CompareTag("Obstacle") || other.CompareTag("Circle"))
        {
            CanUp = false;
            CanDown = false;
            CanRight = false;
            CanLeft = false;
        }
    }
}
