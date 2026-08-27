using DiasGames;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerObjectController : MonoBehaviour, IInteractable
{
    private bool isPlayerInZone = false;

    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject ComputerCanvas;

    public ComputerObjectController Instance { get; private set; }

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AbilityScheduler>(out var player))
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInZone = false;
    }
    public void Interact()
    {
        if (isPlayerInZone && HandObjectsController.Instance.CurrentObjectInHand == null)
        {
            Time.timeScale = 0;
            mainCanvas.SetActive(false);
            ComputerCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
