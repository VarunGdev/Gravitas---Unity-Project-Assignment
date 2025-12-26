using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public float scaleAmount = 1.1f;  
    public float speed = 4f;          

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
