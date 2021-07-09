using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Inputsys controls;
    private PhotonView PV;
    private bool menuOpen;

    [SerializeField] private Button quitbut;

    private void Awake()
    {
        PV = GetComponentInParent<PhotonView>();
        if (!PV.IsMine) return;
        controls = new Inputsys();
        controls.Enable();
    }
    private void OnEnable()
    {
        controls.Player.Menu.performed += ctx => ToggleMenu();
    }
    private void OnDisable()
    {
        if (!PV.IsMine) return;
        controls.Disable();
    }

    private void Start()
    {
        menuOpen = false;
        quitbut.gameObject.SetActive(false);
    }
    public void QuitApplication() 
    {
        RoomManager.Instance.QuitApplication();
    }
    public void ToggleMenu()
    {
        menuOpen = !menuOpen;

        quitbut.gameObject.SetActive(menuOpen);

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
