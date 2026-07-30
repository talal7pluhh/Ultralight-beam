using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashLightBattery : MonoBehaviour
{
    public TextMeshProUGUI batteryText;
    public Slider Battery;
    public Light flashLight;
    public float batteryLife = 100f;
    public float drainTime = 5f;
    public int batteries = 0;
    public float normalIntensity = 300f;
    // public AudioSource rechargeSource;
    public Button rechargeButton;

    public void RechargeBattery()
    {
        if (batteries > 0)
        {
            batteryLife = 100f;
            batteries--;
            flashLight.enabled = true;
            flashLight.intensity = normalIntensity;
            Battery.value = batteryLife;
            Debug.Log("Recharge clicked, batteries=" + batteries + ", batteryLife=" + batteryLife);
          //  rechargeSource.Play();
        }
    }

        void Update()
        {

        if (rechargeButton != null && batteries > 0)
        {
            rechargeButton.interactable = true;
        }
        if (batteryLife > 0)
            {

                batteryLife -= drainTime * Time.deltaTime;
                batteryLife = Mathf.Max(batteryLife, 0);    
                Battery.value = batteryLife;
                batteryText.text = "Battery: " + Mathf.RoundToInt(batteryLife) + "%";

                if (batteryLife < 25f)
                {
                    flashLight.intensity = Random.Range(0f, 250f); // flicker the flashlight when it's almost dead
                } else
                {
                    flashLight.intensity = normalIntensity;
                }
             }
                else
                {

                    batteryText.text = "Recharge Battery";
                    flashLight.enabled = false;
                    Battery.value = 0;
                }


        }
    }
