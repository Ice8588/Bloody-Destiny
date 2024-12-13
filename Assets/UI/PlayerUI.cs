using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider HealthBar, BloodPowerBar, BloodGroove;
    public TextMeshProUGUI HealthText, BloodPowerText, BloodGrooveText, MissionBoardText;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = PlayerCtrl.MaxHealth;
        HealthBar.value = PlayerCtrl.Health;
        BloodPowerBar.maxValue = PlayerCtrl.MaxBloodPower;
        BloodPowerBar.value = PlayerCtrl.BloodPower;
        BloodGroove.maxValue = PlayerCtrl.BloodGrooveMax;
        BloodGroove.value = PlayerCtrl.BloodGroove;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.maxValue = PlayerCtrl.MaxHealth;
        HealthBar.value = PlayerCtrl.Health;
        BloodPowerBar.maxValue = Mathf.Min(PlayerCtrl.Health, PlayerCtrl.MaxBloodPower);
        BloodPowerBar.value = PlayerCtrl.BloodPower;
        HealthText.text = PlayerCtrl.Health + " / " + PlayerCtrl.MaxHealth;
        BloodPowerText.text = PlayerCtrl.BloodPower + " / " + Mathf.Min(PlayerCtrl.Health, PlayerCtrl.MaxBloodPower);
        BloodGroove.maxValue = PlayerCtrl.BloodGrooveMax;
        BloodGroove.value = PlayerCtrl.BloodGroove;
        BloodGrooveText.text = PlayerCtrl.BloodGroove + "\n / \n" + PlayerCtrl.BloodGrooveMax;
        switch(GameCtrl.Stage)
        {
            case 0:
                MissionBoardText.text = Stage1.MissionContent;
                break;
            case 1:
                MissionBoardText.text = Stage2.MissionContent;
                break;
            case 2:
                MissionBoardText.text = Stage3.MissionContent;
                break;
            default:
                MissionBoardText.text ="沒有任務";
                break;
        }
        RectTransform sliderRect = BloodPowerBar.GetComponent<RectTransform>();

        float bloodPowerRate = (float)PlayerCtrl.Health / (float)PlayerCtrl.MaxBloodPower;
        if (sliderRect != null)
        {
            sliderRect.sizeDelta = new Vector2(500 * bloodPowerRate, sliderRect.sizeDelta.y);
        }
    }
}
