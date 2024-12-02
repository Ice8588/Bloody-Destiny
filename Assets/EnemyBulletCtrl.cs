using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCtrl : MonoBehaviour
{
    public float BulletSpeed = 0.1f;
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
        transform.Translate(new Vector2(0, BulletSpeed));

        if (transform.position.y >= 10f || transform.position.y <= -10f || transform.position.x >= 10f || transform.position.x <= -10f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCtrl>().Health -= ATK;
            Destroy(this.gameObject);
        }
    }
}
