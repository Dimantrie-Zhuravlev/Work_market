using UnityEngine;

public struct StructureObjectPosition
{
    public readonly Vector3 ObjectPosition;
    public readonly Quaternion ObjectRotation;

    public StructureObjectPosition(Vector3 position, Quaternion rotation)
    {
        ObjectPosition = position;
        ObjectRotation = rotation;
    }
}
