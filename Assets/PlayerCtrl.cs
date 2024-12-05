using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject BloodMagic;
    public float PlayerSpeed = 0.1f;
    public static int Health = 10;

    // Start is called before the first frame update
    void Start()
    {
        GameCtrl.PlayerGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + 0.3 <= GameCtrl.SCREEN_HEIGHT && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            transform.Translate(new Vector2(0, PlayerSpeed));
        }
        else if (transform.position.y - 0.3 >= -GameCtrl.SCREEN_HEIGHT && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            transform.Translate(new Vector2(0, -PlayerSpeed));
        }
        else if (transform.position.x - 0.3 >= -GameCtrl.SCREEN_WIDTH && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate(new Vector2(-PlayerSpeed, 0));
        }
        else if (transform.position.x + 0.3 <= GameCtrl.SCREEN_WIDTH && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Translate(new Vector2(PlayerSpeed, 0));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BloodMagic, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
        }
    }

    private void LateUpdate()
    {

    }
}
