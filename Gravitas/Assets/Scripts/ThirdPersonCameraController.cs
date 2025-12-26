using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    private const float YMin = -50f;
    private const float YMax = 50f;

    public Transform lookAt;
    public float distance = 10f;
    public float sensitivity = 200f;

    float currentX;
    float currentY;
    public float currentZ;

    public LayerMask collisionLayers = ~0;
    public float collisionRadius = 0.3f;
    public float collisionOffset = 0.2f;

    void Start()
    {
        LockCursor();
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        float zRad = currentZ * Mathf.Deg2Rad;

        float rotatedX = mouseX * Mathf.Cos(zRad) - mouseY * Mathf.Sin(zRad);
        float rotatedY = mouseX * Mathf.Sin(zRad) + mouseY * Mathf.Cos(zRad);

        currentX += rotatedX;
        currentY -= rotatedY;
        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, currentZ);
        Vector3 desiredPosition = lookAt.position + rotation * new Vector3(0, 0, -distance);

        Vector3 direction = desiredPosition - lookAt.position;
        float targetDistance = direction.magnitude;

        RaycastHit hit;
        
        if (Physics.SphereCast(lookAt.position, collisionRadius, direction.normalized, out hit, targetDistance, collisionLayers))
        {
            desiredPosition = hit.point - direction.normalized * collisionOffset;
        }

        transform.position = desiredPosition;
        transform.rotation = rotation;
    }

    public void SetCameraRoll(float zAngle)
    {
        currentZ = zAngle;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
