using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    [Header("Shrink Settings")]
    [SerializeField] private float shrinkDuration;
    [SerializeField] private Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private float destroyDelay = 0.1f;

    private Vector3 originalScale;
    private bool shrinking = false;
    private float timer = 0f;

    void Awake()
    {
        shrinkDuration = 10f;
        originalScale = transform.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (shrinking) return;

        if (other.CompareTag("Player"))
        {
            shrinking = true;
        }
    }

    void Update()
    {
        if (!shrinking) return;

        timer += Time.deltaTime;
        float t = timer / shrinkDuration;

        transform.localScale = Vector3.Lerp(originalScale, minScale, t);

        if (t >= 1f)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    public void ResetPlatform()
    {
        shrinking = false;
        timer = 0f;
        transform.localScale = originalScale;
    }
}
