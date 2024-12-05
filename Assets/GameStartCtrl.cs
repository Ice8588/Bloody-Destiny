using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartCtrl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
