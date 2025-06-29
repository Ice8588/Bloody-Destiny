using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBloodMagicCtrl : MonoBehaviour
{
    public float Speed = 0.1f;
    public int ATK = 2;
    public Quaternion InitAngle;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = InitAngle;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, Speed));

        if (transform.position.y <= -GameCtrl.SCREEN_HEIGHT - 5 || transform.position.y >= GameCtrl.SCREEN_HEIGHT + 5 ||
            transform.position.x <= -GameCtrl.SCREEN_WIDTH - 5 || transform.position.x >= GameCtrl.SCREEN_WIDTH + 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();

            if (enemyScript)
            {
                enemyScript.TakeDamage(ATK);
            }

            Destroy(this.gameObject);
        }
    }
}
