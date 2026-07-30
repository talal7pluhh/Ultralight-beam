using UnityEngine;

public class introUIToHide : MonoBehaviour
{

    public GameObject UItoHide;
    public float WakeUpDuration = 12f;

    private void Start()
    {
        if (UItoHide != null)
        {
            UItoHide.SetActive(false);

            Invoke("ShowUI", WakeUpDuration);
        }
    }
    void ShowUI()
    {
        if (UItoHide != null)
        {
            UItoHide.SetActive(true);
        }
    }

}
