using UnityEngine;

public class RoofLightning : AbstractElectricity, IInteractable
{
    private EnvironmentsPersonMessage m_message;
    private bool canInteract = true;//в зависимости от наличия электричества

    private void Start()
    {
        m_message = GetComponent<EnvironmentsPersonMessage>();
        m_message.SetCurrentMessage("Выключить на E");
    }
    public override void ElectricityComponentLaunch()
    {
        canInteract = true;
        m_message.SetCurrentMessage(isMechanismActive ? "Выключить на E" : "Включить на E");
        if (isMechanismActive) GetComponent<Light>().enabled = true;
    }

    public override void ElectricityComponentStop()
    {
        canInteract = false;
        GetComponent<Light>().enabled = false;
        m_message.SetCurrentMessage("");
    }

    public void Interact()
    {
        if (canInteract)
        {
            isMechanismActive = !isMechanismActive;
            GetComponent<Light>().enabled = isMechanismActive;
            m_message.SetCurrentMessage(isMechanismActive ? "Выключить на E" : "Включить на E");
        }
    }

}
