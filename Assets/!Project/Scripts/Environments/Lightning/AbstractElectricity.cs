using UnityEngine;

public abstract class AbstractElectricity : MonoBehaviour
{
    [SerializeField] public int currentConsumption;
    [SerializeField] public string Title;

    [HideInInspector]
    public bool isMechanismActive = true;

    public abstract void ElectricityComponentStop();

    public abstract void ElectricityComponentLaunch();

    public abstract void ElectricityComponentInclude();
    

}
