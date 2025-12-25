using UnityEngine;
using UnityEngine.InputSystem;

public class HologramPreview : MonoBehaviour
{
    [Header("Hologram")]
    [SerializeField] private GameObject hologramPrefab;
    [SerializeField] private Transform headTransform; // Player head or camera
    [SerializeField] private float previewOffset = 2f;

    private Transform hologram;
    private Vector3 previewGravity = Vector3.down;
    private bool isPreviewing;

    void Start()
    {
        if (headTransform == null)
        {
            Debug.LogError("Head Transform not assigned in HologramPreview!");
            return;
        }

        // Instantiate hologram at head position
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
            if (hologram != null)
                hologram.gameObject.SetActive(false);
        }
    }

    void ReadDirectionInput()
    {
        if (Keyboard.current.downArrowKey.isPressed)
            previewGravity = Vector3.down;
        else if (Keyboard.current.upArrowKey.isPressed)
            previewGravity = Vector3.up;
        else if (Keyboard.current.rightArrowKey.isPressed)
            previewGravity = Vector3.forward;
        else if (Keyboard.current.leftArrowKey.isPressed)
            previewGravity = Vector3.back;
     
    }

    void UpdateHologramTransform()
    {
        // Position hologram at head offset in gravity direction
        hologram.position = headTransform.position - previewGravity * previewOffset;

        // Rotate hologram to align with gravity direction
        Quaternion targetRotation = Quaternion.FromToRotation(headTransform.up, -previewGravity) * headTransform.rotation;
        hologram.rotation = targetRotation;
    }

    public bool IsPreviewing() => isPreviewing;
    public Vector3 GetPreviewGravity() => previewGravity;
}
