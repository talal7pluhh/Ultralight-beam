using UnityEngine;
using UnityEngine.UI;

public class ItemPickupTrigger : MonoBehaviour
{
    public bool isFlashlight;
    private Button pickupButtonComponent;
    public GameObject PickupButton;
    [SerializeField] private FlashlightButton flashlightButton;
    private bool hasBeenPickedUp = false;
    [SerializeField] private CanvasGroup flashCanvasGroup;
    [SerializeField] private CanvasGroup batteryCanvasGroup;
    public AudioSource pickUpSound;
    void Start()
    {
        if (PickupButton != null)
        {
            pickupButtonComponent = PickupButton.GetComponent<Button>();
        }
        HideButtons();

    }


    private void HideButtons()
    {

        if (flashCanvasGroup != null)
        {
            SetCanvasGroupState(flashCanvasGroup, false);
        }
        if (batteryCanvasGroup != null)
        {
            SetCanvasGroupState(batteryCanvasGroup, false);
        }

    }

    private void ShowButtons()
    {

        if (isFlashlight && flashCanvasGroup != null)
        {
            SetCanvasGroupState(flashCanvasGroup, true);
        }
        else if (!isFlashlight && batteryCanvasGroup != null)
        {
            SetCanvasGroupState(batteryCanvasGroup, true);
        }
    }

    private void SetCanvasGroupState(CanvasGroup group, bool isVisible)
    {
        group.alpha = isVisible ? 1f : 0f;
        group.interactable = isVisible;
        group.blocksRaycasts = isVisible;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenPickedUp || !other.CompareTag("Player")) return;
        ShowButtons();

        if (pickupButtonComponent != null)
        {
            pickupButtonComponent.onClick.RemoveAllListeners();
            pickupButtonComponent.onClick.AddListener(Pickup);
        }

        PickupButton.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasBeenPickedUp || !other.CompareTag("Player")) return;
        ShowButtons();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupButtonComponent != null)
            {
                pickupButtonComponent.onClick.RemoveAllListeners();
            }
            HideButtons();
        }
    }
    public void Pickup()
    {
        pickUpSound.Play();
        if (hasBeenPickedUp) return;
        hasBeenPickedUp = true;
        var flashlight = FindAnyObjectByType<FlashLightBattery>(FindObjectsInactive.Include);
        if(PickupButton != null) { PickupButton.SetActive(false); }

        if (isFlashlight)        
        {   
            if (flashlightButton != null) flashlightButton.unlockFlashlight(); 
        }                 

        else
        {
            Debug.Log("picked up battery");
            flashlight.batteries += 1;      
        }
        HideButtons();
        gameObject.SetActive(false);
    }
}
