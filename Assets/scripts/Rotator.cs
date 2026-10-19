using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float degreesPerSecond = 90f;

    void Update()
    {
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.World);
    }
}