using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    public static double TimeCounter = 0f;
    public GameObject PlayerGameObject;
    public static Vector3 PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter++;
        PlayerPos = PlayerGameObject.transform.position;
    }
}
