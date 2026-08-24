using UnityEngine;

public abstract class AbstractElectricity : MonoBehaviour
{
    [SerializeField] public int countElectricityIn20Minutes;

    public bool isMechanismActive = true;

    public abstract void ElectricityComponentStop();

    public abstract void ElectricityComponentLaunch();

}
