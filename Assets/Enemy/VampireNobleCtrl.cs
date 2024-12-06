using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireNobleCtrl : EnemyScript
{
    // Start is called before the first frame update

    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        //transform.position = InitialPosition;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //transform.Translate(new Vector3(0.1f, 0, 0));  //test
        
    }
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
    }
    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * 0.5f * Time.deltaTime;

        // ­±´Âª±®a
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);  // ´Â¥k
        else
            transform.localScale = new Vector3(-1, 1, 1); // ´Â¥ª
    }
}
