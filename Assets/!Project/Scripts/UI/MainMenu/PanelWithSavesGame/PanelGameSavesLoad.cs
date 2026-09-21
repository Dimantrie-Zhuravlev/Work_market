using UnityEngine;

public class PanelGameSavesLoad : MonoBehaviour
{
    public void OnEnable()
    {
        Transform firstChild = transform.GetChild(0);
        for (int i = 0; i < firstChild.childCount - 1; i++) //Из-за кнопки -1
        {
           firstChild.GetChild(i).GetComponent<SaveFileData>().LoadSystem(i);
        }
    }

}
