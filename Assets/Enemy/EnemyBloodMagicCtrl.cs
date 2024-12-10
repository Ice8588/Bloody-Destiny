using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyBloodMagicCtrl : MonoBehaviour
{

    public int ATK = 2;
    public ShotConfig SC = new ShotConfig();
    int traceCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        float AngleZ = Mathf.Atan2(PlayerCtrl.PlayerPos.y - transform.position.y, PlayerCtrl.PlayerPos.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, AngleZ * Mathf.Rad2Deg - 90);
        transform.position = SC.InitPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(SC.Speed);

        if (transform.position.y <= -GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage] - 5 || transform.position.y >= GameCtrl.SCREEN_HEIGHT[GameCtrl.Stage] + 5 ||
            transform.position.x <= -GameCtrl.SCREEN_WIDTH[GameCtrl.Stage] - 5 || transform.position.x >= GameCtrl.SCREEN_WIDTH[GameCtrl.Stage] + 5)
        {
            Destroy(this.gameObject);
        }

        if (SC.IsTrace)
        {
            traceCounter++;

            if (traceCounter >= SC.TraceTime && SC.TraceNum > 0)
            {
                float AngleZ = Mathf.Atan2(PlayerCtrl.PlayerPos.y - transform.position.y, PlayerCtrl.PlayerPos.x - transform.position.x);
                transform.rotation = Quaternion.Euler(0, 0, AngleZ * Mathf.Rad2Deg - 90);
                traceCounter = 0;
                SC.TraceNum--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCtrl playerHeal = collision.GetComponent<PlayerCtrl>();
            playerHeal.TakeDamage(ATK);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Circle")
        {
            Destroy(this.gameObject);
        }
    }

    public class ShotConfig
    {
        public Quaternion InitAngle;
        public Vector3 InitPosition;
        public Vector2 Speed = new Vector2(0, 0.1f);
        public bool IsTrace = true;
        public int TraceTime = 5, TraceNum = 10;
    }
}
