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
    public bool CanUp = false, CanRight = false, CanLeft = false, CanDown = false;

    // Start is called before the first frame update
    void Start()
    {
        GameCtrl.PlayerGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanUp && transform.position.y + 0.3 <= GameCtrl.SCREEN_HEIGHT && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            transform.Translate(new Vector2(0, PlayerSpeed));
        }
        else if (!CanDown && transform.position.y - 0.3 >= -GameCtrl.SCREEN_HEIGHT && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            transform.Translate(new Vector2(0, -PlayerSpeed));
        }
        if (!CanLeft && transform.position.x - 0.3 >= -GameCtrl.SCREEN_WIDTH && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate(new Vector2(-PlayerSpeed, 0));
        }
        else if (!CanRight && transform.position.x + 0.3 <= GameCtrl.SCREEN_WIDTH && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
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

    void OnTriggerEnter2D(Collider2D other)
    {
        
        Vector2 direction = other.transform.position - transform.position;

        if (other.CompareTag("Obstacle"))
        {
            if (direction.y > 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) CanUp = true;
            if (direction.y < 0 && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) CanDown = true;
            if (direction.x > 0 && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) CanRight = true;
            if (direction.x < 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) CanLeft = true;
        }
        else if (other.CompareTag("Circle"))
        {
            if (direction.y > 0) CanUp = true;
            if (direction.y < 0) CanDown = true;
            if (direction.x > 0) CanRight = true;
            if (direction.x < 0) CanLeft = true;
        }
        else if (other.CompareTag("Trap"))
        {
            PlayerSpeed *= 0.5f;
        }
        else if (other.CompareTag("Fire"))
        {
            // damage   
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
            PlayerSpeed *= 2f;
        CanUp = false;
        CanDown = false;
        CanRight = false;
        CanLeft = false;
    }
}
