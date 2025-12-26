using UnityEngine;
using UnityEngine.InputSystem;

public class HologramPreview : MonoBehaviour
{
    [SerializeField] private GameObject hologramPrefab;
    [SerializeField] private Transform headTransform; 
    [SerializeField] private float previewOffset = 2f;

    private Transform hologram;
    private Vector3 previewGravity = Vector3.down;
    private bool isPreviewing;

    void Start()
    {
        hologram = Instantiate(hologramPrefab, headTransform.position, Quaternion.identity).transform;
        hologram.gameObject.SetActive(false);
    }

    void Update()
    {
        HandlePreviewState();
        if (!isPreviewing || hologram == null) return;
        ReadDirectionInput();
        UpdateHologramTransform();
    }

    void HandlePreviewState()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            isPreviewing = true;
            if (hologram != null)
                hologram.gameObject.SetActive(true);
            previewGravity = Vector3.down;
        }

        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            isPreviewing = false;
            if (hologram != null)hologram.gameObject.SetActive(false);
        }
    }

    void ReadDirectionInput()
    {
        if (Keyboard.current.downArrowKey.isPressed)previewGravity = Vector3.down;
        else if (Keyboard.current.upArrowKey.isPressed)previewGravity = Vector3.up;
        else if (Keyboard.current.rightArrowKey.isPressed)previewGravity = Vector3.forward;
        else if (Keyboard.current.leftArrowKey.isPressed)previewGravity = Vector3.back;
    }

    void UpdateHologramTransform()
    {
        hologram.position = headTransform.position - previewGravity * previewOffset;
        Quaternion targetRotation = Quaternion.FromToRotation(headTransform.up, -previewGravity) * headTransform.rotation;
        hologram.rotation = targetRotation;
    }

    public bool IsPreviewing() => isPreviewing;
    public Vector3 GetPreviewGravity() => previewGravity;
}
