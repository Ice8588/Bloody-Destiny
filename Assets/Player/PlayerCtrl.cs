using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject BloodMagic;
    public float PlayerSpeed = 0.1f;
    public static int MaxHealth = 10, Health = 0;
    public static int MaxBloodPower = 0, BloodPower = 0;
    public int BloodPowerCost = 2;
    public static Vector3 PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        MaxBloodPower = Health;
        BloodPower = MaxBloodPower;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = transform.position;

        if (GameCtrl.TimeCounter % 300 == 0 && BloodPower < Health)
        {
            BloodPower++;
        }
        else
        {
            BloodPower = Mathf.Max(0, Mathf.Min(Health, BloodPower));
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            PlayerSpeed = 0.2f;
        }
        else
        {
            PlayerSpeed = 0.1f;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y <= GameCtrl.SCREEN_HEIGHT)
        {
            transform.Translate(new Vector2(0, PlayerSpeed));
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y >= -GameCtrl.SCREEN_HEIGHT)
        {
            transform.Translate(new Vector2(0, -PlayerSpeed));
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x >= -GameCtrl.SCREEN_WIDTH)
        {
            transform.Translate(new Vector2(-PlayerSpeed, 0));
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x <= GameCtrl.SCREEN_WIDTH)
        {
            transform.Translate(new Vector2(PlayerSpeed, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space)) // update
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y + 2 <= GameCtrl.SCREEN_HEIGHT)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 2);
            }
            else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y - 2 >= -GameCtrl.SCREEN_HEIGHT)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 2);
            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x - 2 >= -GameCtrl.SCREEN_WIDTH)
            {
                transform.position = new Vector2(transform.position.x - 2, transform.position.y);
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x + 2 <= GameCtrl.SCREEN_WIDTH)
            {
                transform.position = new Vector2(transform.position.x + 2, transform.position.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && BloodPower >= BloodPowerCost)
        {
            Instantiate(BloodMagic, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            BloodPower -= BloodPowerCost;
        }
    }

    private void LateUpdate()
    {

    }
}
