using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 100f;

    void LateUpdate()
    {
        Vector3 pos = player.position;
        pos.y = height;
        transform.position = pos;
    }
}