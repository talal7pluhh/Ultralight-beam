using UnityEngine;
using UnityEngine.UI;   
public class JoyStickMove : MonoBehaviour
{
    public FloatingJoystick movementJoystick;
    public float PlayerSpeed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        float MoveX = movementJoystick.Direction.x;
        float MoveZ = movementJoystick.Direction.y;

        if (movementJoystick.Direction.magnitude > 0)
        {
            Vector3 moveDirection = (transform.forward * MoveZ) + (transform.right * MoveX);
            rb.linearVelocity = new Vector3(moveDirection.x * PlayerSpeed, rb.linearVelocity.y, moveDirection.z * PlayerSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

    }
}
