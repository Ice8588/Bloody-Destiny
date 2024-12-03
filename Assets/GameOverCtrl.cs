using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverCtrl : MonoBehaviour
{
    public TMP_Text KillCountText, TotalDamageText;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartMenu");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
