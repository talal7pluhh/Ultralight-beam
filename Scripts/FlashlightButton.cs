using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FlashlightButton : MonoBehaviour
{
    public static FlashlightButton instance;
    [SerializeField] private GameObject cameraLight;
    [SerializeField] private GameObject flash3D;
    [SerializeField] private RawImage Icon;
    private bool hasFlashlight = false;
    public bool isFlashLightOn = false;
    public CanvasGroup sliderCanvasGroup;
    public AudioSource flash;
     private void Awake()
    {
        if(instance == null) 
           instance = this;   
   }
    private void Start()
    {
        sliderCanvasGroup.alpha = 0f;
        sliderCanvasGroup.interactable = false;
        if (Icon != null)
        {
            Icon.enabled = false;      
        }

        if (cameraLight != null)
        {
            cameraLight.SetActive(false);
        }

        if (flash3D != null)
        {
            flash3D.SetActive(true);
        }
    }
    public void unlockFlashlight()
    {
        isFlashLightOn = true;
        hasFlashlight = true;
        sliderCanvasGroup.alpha = 1f;
        sliderCanvasGroup.interactable = true;
        sliderCanvasGroup.blocksRaycasts = true;
        if (Icon != null)
        {
            Icon.enabled = true;
        }

        if (cameraLight != null)
        {
            cameraLight.SetActive(true);
        }

        if (flash3D != null)
        {
            flash3D.SetActive(false);
        }

    }

    public void ToggleFlashlight()
    {
        flash.Play();
        if (!hasFlashlight)
        {
            return;
        }

        isFlashLightOn = !isFlashLightOn;

        if (cameraLight != null)
        {
            cameraLight.SetActive(isFlashLightOn);
        }
    }


}
