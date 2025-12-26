using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 90, 0);
    [SerializeField] private bool useLocalSpace = true; 

    void Update()
    {
        if (useLocalSpace){transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);}
        else {transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);}
    }
}
