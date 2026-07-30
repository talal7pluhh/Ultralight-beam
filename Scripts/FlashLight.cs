using UnityEngine;
using UnityEngine.UI;
public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject FlashingLight;
    public bool hasBeenCollected = false;
    private void Start()
    {
        FlashingLight.SetActive(false);
        gameObject.SetActive(true);
    }

    public void CollectFlashlight()
    {
        hasBeenCollected = true;
        FlashingLight.SetActive(true);
        gameObject.SetActive(false);
    }

}
