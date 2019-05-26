using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform  target;
    [SerializeField] public Vector3    offset;
    [SerializeField] public bool       enforceBounds;
    [SerializeField] public Rect       bounds;
    [SerializeField] public float      cameraSpeed = 0.1f;

    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = target.position + offset;
        Vector3 delta = newPos - transform.position;

        newPos = transform.position + delta * cameraSpeed;

        if (enforceBounds)
        {
            float sizeY = camera.orthographicSize;
            float sizeX = sizeY * camera.aspect;

            newPos.x = Mathf.Clamp
                (newPos.x, bounds.xMin + sizeX, bounds.xMax - sizeX);
            newPos.y = Mathf.Clamp
                (newPos.y, bounds.yMin + sizeY, bounds.yMax - sizeY);
        }

        newPos.z = transform.position.z;

        transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
