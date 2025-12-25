using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 90, 0); // Degrees per second
    [SerializeField] private bool useLocalSpace = true; // Rotate in local space

    void Update()
    {
        // Rotate smoothly every frame
        if (useLocalSpace)
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        else
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}
