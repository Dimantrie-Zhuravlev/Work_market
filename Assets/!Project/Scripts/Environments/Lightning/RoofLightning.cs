using UnityEngine;

public class RoofLightning : AbstractElectricity
{
    private EnvironmentsPersonMessage m_message;

    private void Start()
    {
        m_message = GetComponent<EnvironmentsPersonMessage>();
        m_message.SetCurrentMessage(Title);
    }
    public override void ElectricityComponentLaunch()
    {
        if (isMechanismActive) GetComponent<Light>().enabled = true;
    }

    public override void ElectricityComponentStop()
    {
        GetComponent<Light>().enabled = false;
    }

    public override void ElectricityComponentInclude()
    {
        isMechanismActive = !isMechanismActive;
        GetComponent<Light>().enabled = isMechanismActive;
    }

}
