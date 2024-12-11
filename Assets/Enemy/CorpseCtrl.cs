using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CorpseCtrl : EnemyScript
{
    //public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        tag = "Corpse";
       // transform.position = InitialPosition;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // transform.Translate(new Vector3(0.1f, 0, 0));  //test

        if (GameCtrl.TimeCounter % 120 == 0)
        {
            shoot();
        }

        if(Health<=0)
        {
            animator.SetFloat("corpse", 1);
        }
    }
    
    private void shoot()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 delta = player.transform.position-this.transform.position;
        //Debug.Log(delta.normalized);
        Vector3 shotAngle = new Vector3(0, 0, Vector3.SignedAngle(Vector3.right, delta, Vector3.forward));
        Vector3 shotPos = transform.position;

        GameObject newBloodMagic = Instantiate(BloodMagic, transform.position, new Quaternion(0, 0, 0, 0));
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.InitAngle = new Quaternion(0, 0, 0, 0);
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.InitPosition = shotPos;     
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.Speed = new Vector2(0,0.1f);
        newBloodMagic.GetComponent<EnemyBloodMagicCtrl>().SC.IsTrace = false;
    }
}
