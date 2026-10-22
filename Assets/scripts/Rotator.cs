using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] bool rotate = true;
    [SerializeField] float degreesPerSecond = 90f;

    [Header("Bobbing")]
    [SerializeField] bool bob = true;
    [Tooltip("How far the pumpkin moves up and down from its starting height.")]
    [SerializeField] float bobHeight = 0.25f;
    [Tooltip("Full up-and-down cycles per second.")]
    [SerializeField] float bobSpeed = 1f;
    [Tooltip("Start each pumpkin at a different point in the cycle so they don't all move in step.")]
    [SerializeField] bool randomiseStart = true;

    Vector3 startPosition;
    float timeOffset;

    void Start()
    {
        // remember where the pumpkin was placed so it bobs around that point
        startPosition = transform.localPosition;

        if (randomiseStart)
        {
            timeOffset = Random.Range(0f, 10f);
        }
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.World);
        }

        if (bob)
        {
            float offset = Mathf.Sin((Time.time + timeOffset) * bobSpeed * 2f * Mathf.PI) * bobHeight;
            transform.localPosition = startPosition + new Vector3(0f, offset, 0f);
        }
    }
}