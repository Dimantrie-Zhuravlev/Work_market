using UnityEngine;

public struct StructureBoxCollider
{
    public readonly Vector3 BoxColliderCenter;
    public readonly Vector3 BoxColliderSize;
    
    public StructureBoxCollider(Vector3 center, Vector3 size)
    {
        BoxColliderCenter = center;
        BoxColliderSize = size;
    }    
}
