using UnityEngine;

public class audioManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] footStepsClips;
    public Rigidbody rb;
    public float stepInterval = 0.5f;
    public float minMoveSpeed = 0.1f;

    private float stepTimer = 0f;
    private int lastClipIndex = -1;


    public Transform groundCheck; // empty object at the player's feet
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool useGroundCheck = false;
    public float startDelay = 12f;
    private void Update()
    {
        bool isGrounded = true;

        if (Time.timeSinceLevelLoad < startDelay)
        {
            stepTimer = 0f;
            return;
        }

        if (useGroundCheck && groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // ignore vertical fall/jump speed
        bool isMoving = (horizontalVelocity.magnitude > minMoveSpeed) && isGrounded;
        if (rb.linearVelocity.sqrMagnitude > 0.05f && isGrounded)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }


    }
    private void PlayFootstep()
    {
        if (footStepsClips.Length == 0 || audioSource == null)
        {
            return;
        }
        int index;
        do
        {
            index = Random.Range(0, footStepsClips.Length);
        }
        while (index == lastClipIndex && footStepsClips.Length > 1);
        lastClipIndex = index;
        audioSource.PlayOneShot(footStepsClips[index]);
    }

}
