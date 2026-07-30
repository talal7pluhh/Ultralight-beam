using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class IntroManager : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator holderAnimator;
    [SerializeField] private string wakeUpClipName = "Wake_Up";
    public float animationLength = 10f;
 

    [Header("buttons and UI")]
    [SerializeField] private GameObject pickupButton;
    [SerializeField] private ItemPickupTrigger pickupSystem;
    [SerializeField] private CanvasGroup pauseCanvasGroup;



    [Header("controls")]
    public TouchLook touchLook; 
    public JoyStickMove stickMove;
        private void Start()
        {
            SetupInitialState();
            StartCoroutine(PlayWakeUpSequence());
            PauseHide();
        }


        
        private void PauseHide()
        {
            pauseCanvasGroup.alpha = 0f;
            pauseCanvasGroup.interactable = false;
            pauseCanvasGroup.blocksRaycasts = false;
        }

        private void PauseShow()
        {
            pauseCanvasGroup.alpha = 1f;
            pauseCanvasGroup.interactable = true;
            pauseCanvasGroup.blocksRaycasts = true;
        }
        void SetupInitialState()
        {
            if (pickupButton != null) { pickupButton.SetActive(false); }
            if (pickupSystem != null) { pickupSystem.enabled = false; }
            if (touchLook != null) {touchLook.enabled = false;}
            if (stickMove != null) {stickMove.enabled = false;}
        }
        private IEnumerator PlayWakeUpSequence()
        {

            if (holderAnimator == null)
            {
            yield break;
            }

            holderAnimator.Play(wakeUpClipName);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(animationLength);
            PauseShow();
            holderAnimator.enabled = false;
            pickupButton.SetActive(false);
        /*
                if (player != null)
                {
                    player.position = standPosition;
                    player.rotation = Quaternion.Euler(standRotation);
                }
        */

        if (pickupSystem != null) { pickupSystem.enabled = true; }
        if (touchLook != null) { touchLook.enabled = true; }
        if (stickMove != null) { stickMove.enabled = true; }

        }
}
