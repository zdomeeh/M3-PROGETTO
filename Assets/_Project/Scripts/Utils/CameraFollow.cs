using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 camera = new Vector3(target.position.x, target.position.y, -10);
            transform.position = camera;
        }
    }
}
