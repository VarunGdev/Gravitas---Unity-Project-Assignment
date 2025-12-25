using UnityEngine;

public static class GravityController
{
    public static Vector3 CurrentGravity { get; private set; } = Vector3.down;

    public static void SetGravity(Vector3 newGravity)
    {
        CurrentGravity = newGravity.normalized;
        Physics.gravity = CurrentGravity * 9.81f;
    }
}
