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
    public static readonly int[] SCREEN_WIDTH = new int[3] { 20, 20, 16 }, SCREEN_HEIGHT = new int[3] { 20, 20, 16 };
    public static bool IsGameClear = false, check = false;
    public static bool IsGameOver = false;
    private bool firstStart = true;
    private static GameCtrl instance;
    public static int Stage;
    public GameObject CirclePrefab;
    //public static AsyncOperation PreloadOperation;

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

        //PreloadOperation = SceneManager.LoadSceneAsync("Stage3");
        //PreloadOperation.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter++;
    }

    void LateUpdate()
    {
        if (PlayerCtrl.Health <= 0 && !IsGameOver)
        {
            IsGameOver = true;
            SceneManager.LoadScene("GameOverMenu");
        }
    }

    public static void ResetGame()
    {
        if (!IsGameOver)
        {
            //IsGameOver = true;
            TimeCounter = 0;
            KillCount = 0;
            TotalDamege = 0;
            PlayerCtrl.Health = 10;
            SceneManager.LoadScene("Stage1");
        }

    }

    public static void GameClear()
    {
        Instance.SpawnTeleport();
        IsGameClear = true;
       // SceneManager.LoadScene("GameOverMenu");
    }

    void SpawnTeleport()
    {
        Vector2 SpawnPoint = new Vector2(-17.5f, -17.5f);

        if (!check)
        {
            Instantiate(CirclePrefab, SpawnPoint, Quaternion.identity);
            check = true;
        }
    }
}
