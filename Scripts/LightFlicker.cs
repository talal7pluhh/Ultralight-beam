using UnityEngine;
public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light m_LightFlicker;
    public float minIntensity = 5f;
    public float maxIntensity = 50f;
    public float timeBetweenChanges = 0.1f;
    public float timer;

    private void Start()
    {
        m_LightFlicker = GetComponent<Light>();
        if (m_LightFlicker == null)
        {
            Debug.LogError("LightFlicker script needs a Light component on the same GameObject!");
        }
    }

    private void Update()
    {
        if (m_LightFlicker == null)
        {
            return;
        }
        timer += Time.deltaTime;

        if (timer >= timeBetweenChanges)
        {
            m_LightFlicker.intensity = Random.Range(minIntensity, maxIntensity);
            timer = 0f;
        }
    }

}
