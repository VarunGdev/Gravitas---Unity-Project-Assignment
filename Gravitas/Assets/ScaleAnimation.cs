using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [Header("Scale Settings")]
    public float scaleAmount = 1.1f;   // How big it grows
    public float speed = 4f;           // Pulse speed

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.unscaledTime * speed) * (scaleAmount - 1);
        transform.localScale = startScale * scale;
    }
}
