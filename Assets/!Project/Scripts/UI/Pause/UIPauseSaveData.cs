using UnityEngine;
using UnityEngine.UI;

public class UIPauseSaveData : MonoBehaviour
{
    private Button _saveButton;

    public void ChangeInteractable(bool interactable)
    {
        if (_saveButton == null)
        {
            _saveButton = gameObject.GetComponent<Button>();
        }
        _saveButton.interactable = interactable;
    }   

}
