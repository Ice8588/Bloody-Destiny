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
    public const int CREEN_WIDTH = 9, SCREEN_HEIGHT = 5;
    private bool firstStart = true;
    private static GameCtrl instance;

    public static GameCtrl Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameCtrlObject = new GameObject("GameCtrl");
                instance = gameCtrlObject.AddComponent<GameCtrl>();

                DontDestroyOnLoad(gameCtrlObject);
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (firstStart)
        {
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
