using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public static double TimeCounter = 0f;
    public static int KillCount = 0, BestKillCount = 0;
    public static int TotalDamege = 0, BestTotalDamege = 0;
    public const int SCREEN_WIDTH = 9, SCREEN_HEIGHT = 5;
    public static bool IsGameClear = false;
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
    }

    void LateUpdate()
    {
        if (PlayerCtrl.Health <= 0)
        {
            SceneManager.LoadScene("GameOverMenu");
        }
    }

    public static void ResetGame()
    {
        TimeCounter = 0;
        KillCount = 0;
        TotalDamege = 0;
        PlayerCtrl.Health = 10;
        SceneManager.LoadScene("GameBoard");
    }

    public static void GameClear()
    {
        IsGameClear = true;
        SceneManager.LoadScene("GameOverMenu");
    }
}
