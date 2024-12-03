using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public static double TimeCounter = 0f;
    public static GameObject PlayerGameObject;
    public static Vector3 PlayerPos;
    public static int KillCount = 0, BestKillCount = 0;
    public static int TotalDamege = 0, BestTotalDamege = 0;
    private bool firstStart = true;

    void Awake()
    {
        if (!firstStart)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (firstStart)
        {
            DontDestroyOnLoad(this.gameObject);
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            firstStart = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter++;

        if (PlayerGameObject != null)
        {
            PlayerPos = PlayerGameObject.transform.position;
        }
    }
}
