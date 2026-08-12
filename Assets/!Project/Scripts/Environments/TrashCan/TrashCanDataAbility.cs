using UnityEngine;

public class TrashCanDataAbility : MonoBehaviour
{
    public void DisableActiveEmptyBox(GameObject boxInScene, string boxName)
    {
        if (boxName == EnumBoxesName.EmptyBox)
        {
            PoolEmptyBoxes.Instance.Release(boxInScene);
        }
    }
}
