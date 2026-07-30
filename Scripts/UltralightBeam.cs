using UnityEngine;

public class UltralightBeam : MonoBehaviour
{
    /*
     foot on the devil's neck 'til it drifted pangaea
     i'm moving all my family from chatham to zambia....
     i made sunday candy i'm never going to hell
     i met kanye west i'm never going to fail
     */

    [Header("Settings")]
    public float beamDistance = 13f;
    public float spreadAngle = 6f;
    private void Update()
    {

        if (FlashlightButton.instance != null && FlashlightButton.instance.isFlashLightOn)
        {
            Vector3 centerDir = transform.forward;
            Vector3 leftDir = Quaternion.Euler(0, -spreadAngle, 0) * transform.forward;
            Vector3 rightDir = Quaternion.Euler(0, spreadAngle, 0) * transform.forward;

            CheckBeamRay(centerDir);
            CheckBeamRay(leftDir);
            CheckBeamRay(rightDir);

            Debug.DrawRay(transform.position, centerDir * beamDistance, Color.yellow);
            Debug.DrawRay(transform.position, leftDir * beamDistance, Color.yellow);
            Debug.DrawRay(transform.position, rightDir * beamDistance, Color.yellow);

        }

    }

    private void CheckBeamRay(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, beamDistance))
        {
            // 2. Check if the object or any of its parents are tagged "Enemy"
            if (hit.collider.CompareTag("Enemy") || hit.transform.root.CompareTag("Enemy"))
            {
                ZombieTeleport zombie = hit.collider.GetComponent<ZombieTeleport>();
                if (zombie == null) zombie = hit.collider.GetComponentInParent<ZombieTeleport>();
                if (zombie == null) zombie = hit.transform.root.GetComponent<ZombieTeleport>();

                if (zombie != null)
                {
                    zombie.Die();
                }
                else
                {
                }
            }
        }
    }
}
