using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float mouseXam;
    private float mouseYam;

    public float sensh = 60f;
    public float sensv = 60f;
    private Animator animator;

    public float xRotation;
    public Transform playerbody;
    private Inputsys controls;
    private PhotonView PV;


    private void Awake()
    {
        PV = GetComponentInParent<PhotonView>();
        if (!PV.IsMine)
        {
            this.enabled = false;
            return;
        }
        controls = new Inputsys();
        controls.Player.Camera.performed += ctx => ChangeCamHor(ctx.ReadValue<Vector2>());
        controls.Player.Camera.canceled += ctx => ChangeCamHor(new Vector2(0f,0f));
    }

    private void OnEnable()
    {
        if (!PV.IsMine) return;
            controls.Enable();
    }
    private void OnDisable()
    {
        if (!PV.IsMine) return;
            controls.Disable();
    }

    void ChangeCamHor(Vector2 mouse)
    {
        mouseX = sensv * mouse.x * Time.deltaTime;
        mouseY = -sensh * mouse.y * Time.deltaTime;
    }
    void ChangeCamVert(float value)
    {
        mouseYam = value;
        
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {  

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //pitch = Mathf.Clamp(pitch, -60f, 90f);
        transform.localRotation = Quaternion.Euler(0f, 0f, xRotation);
        playerbody.Rotate(Vector3.up * mouseX);
    }
}
