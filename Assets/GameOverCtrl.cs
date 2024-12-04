using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverCtrl : MonoBehaviour
{
    public TMP_Text LogoText, KillCountText, TotalDamageText, HintText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameCtrl.IsGameClear)
        {
            LogoText.text = "Game Clear!";
            LogoText.color = Color.yellow;
        }
        else
        {
            LogoText.text = "YOU DIED!";
            LogoText.color = Color.red;
        }

        if (GameCtrl.KillCount > GameCtrl.BestKillCount)
        {
            GameCtrl.BestKillCount = GameCtrl.KillCount;
            KillCountText.text = "Kill Count: " + GameCtrl.KillCount + " (New record!)";
        }
        else
        {
            KillCountText.text = "Kill Count: " + GameCtrl.KillCount;
        }

        if (GameCtrl.TotalDamege > GameCtrl.BestTotalDamege)
        {
            GameCtrl.BestTotalDamege = GameCtrl.TotalDamege;
            TotalDamageText.text = "Total Damage: " + GameCtrl.TotalDamege + " (New record!)";
        }
        else
        {
            TotalDamageText.text = "Total Damage: " + GameCtrl.TotalDamege;
        }

        HintText.text = "- Press ESC to Quit -";
        //HintText.text = "- Press Space to Replay, Press ESC to Quit -";
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GameCtrl.ResetGame();
            SceneManager.LoadScene("StartMenu");
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
