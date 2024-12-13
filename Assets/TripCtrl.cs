using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isTrapActive;
    public Animator animator;
    public GameObject player;
    void Start()
    {
        //animator.SetFloat("hunt", 1);
    }

    // Update is called once per frame
    void Update()
    {
        isTrapActive = player.GetComponent<PlayerCtrl>().isTrapActive;
        if (isTrapActive)
        {
            animator.SetFloat("hurt", 1);
            Debug.Log("123");
        }
        else if (isTrapActive==false)
        {
            animator.SetFloat("hurt", 0);
            Debug.Log("456");
        }
    }
}
