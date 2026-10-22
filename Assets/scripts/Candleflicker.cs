using UnityEngine;

[RequireComponent(typeof(Light))]
public class CandleFlicker : MonoBehaviour
{
    [Header("Intensity")]
    [SerializeField] float minIntensity = 1.5f;
    [SerializeField] float maxIntensity = 3.5f;

    [Header("Flicker")]
    [Tooltip("How fast the flame wavers. Higher = more nervous.")]
    [SerializeField] float flickerSpeed = 6f;

    [Header("Range wobble (optional)")]
    [SerializeField] bool wobbleRange = true;
    [SerializeField] float rangeAmount = 0.3f;

    Light candleLight;
    float baseRange;
    float seed;

    void Awake()
    {
        candleLight = GetComponent<Light>();
        baseRange = candleLight.range;

        // different start for every pumpkin so they don't flicker in sync
        seed = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Perlin noise gives smooth, random-looking values between 0 and 1
        float noise = Mathf.PerlinNoise(seed, Time.time * flickerSpeed);
        candleLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

        if (wobbleRange)
        {
            candleLight.range = baseRange + (noise - 0.5f) * 2f * rangeAmount;
        }
    }
}