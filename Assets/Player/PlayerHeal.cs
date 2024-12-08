using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    public int HealAmount;
    public float HealDuration = 2f;
    public Image HealProgressRing;
    private bool isHealing = false, isInterrupted = false;
    private Rigidbody2D rb;
    private Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HealProgressRing.gameObject.SetActive(false); // 隱藏進度條
        HealProgressRing.fillAmount = 0; // 初始化進度為 0
        HealAmount = PlayerCtrl.BloodGroove;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 開始回血
        if (Input.GetKeyDown(KeyCode.Q) && !isHealing)
        {
            StartCoroutine(HealOverTime());
        }

        // 如果玩家移動，打斷回血
        if (movement.magnitude > 0 && isHealing)
        {
            isInterrupted = true;
        }
    }
    private IEnumerator HealOverTime()
    {
        isHealing = true;
        isInterrupted = false;
        HealProgressRing.gameObject.SetActive(true);
        HealProgressRing.fillAmount = 0;
        float healTimer = 0f;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        HealProgressRing.transform.position = screenPosition;

        while (healTimer < HealDuration)
        {
            // 如果被打斷，停止回血
            if (isInterrupted)
            {
                HealProgressRing.gameObject.SetActive(false);
                isHealing = false;
                yield break;
            }

            // 更新進度條
            healTimer += Time.deltaTime;
            HealProgressRing.fillAmount = healTimer / HealDuration;

            yield return null;
        }

        HealAmount = PlayerCtrl.BloodGroove;
        PlayerCtrl.Health = Mathf.Min(PlayerCtrl.Health + HealAmount, PlayerCtrl.MaxHealth);
        PlayerCtrl.BloodGroove = 0;
        
        HealProgressRing.gameObject.SetActive(false);
        isHealing = false;
    }

    // 打斷回血
    public void InterruptHealing()
    {
        isInterrupted = true;
    }
}

