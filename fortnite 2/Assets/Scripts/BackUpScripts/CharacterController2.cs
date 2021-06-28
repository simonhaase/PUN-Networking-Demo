using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector3 dir;
    private Vector3 playerRot;
    public Rigidbody rb;
    private float moveforce = 200f;
    private float jumpforce = 20f;
    [SerializeField]
    private FPSAnimation2 fpsAnim;
    private PhotonView PV;
    private float threshold = 0.01f;
    public float maxSpeed;


    public float counterMovement = 0.175f;
    public float moveSpeed = 4500;

    public Inputsys controls;

    private void Awake()
    {
        controls = new Inputsys();
        controls.Player.Movement.performed += ctx => Walk(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => Walk(new Vector2(0f, 0f));
        controls.Player.Shoot.performed += ctx => Shoot();
        controls.Player.Reload.performed += ctx => Reload();
        controls.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        PV = GetComponentInParent<PhotonView>();
        rb = GetComponentInParent<Rigidbody>();
        if (!PV.IsMine)
        {
            Destroy(GetComponent<PlayerInput>());
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<Canvas>().enabled = false;
            Destroy(rb);
            this.enabled = false;
            return;
        }
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    void Walk(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            fpsAnim.StopAnimWalk();
        }
        else
        {
            fpsAnim.PlayAnimWalk();
        }

        dir = new Vector3(-direction.x, 0f, direction.y);
        playerRot = new Vector3(rb.rotation.x, rb.rotation.y, rb.rotation.z);


        float lookAngle = transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
    }

    void Shoot()
    {
        fpsAnim.PlayAnimShoot();
    }

    void Reload()
    {
        fpsAnim.PlayAnimReload();
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpforce, 0), ForceMode.Impulse);
        fpsAnim.Jump();
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }



    private void CounterMovement(float x, float y, Vector2 mag)
    {
        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
    }
    void Update()
    {
        //horizontal = c * speed;
        //vertical = Input.GetAxisRaw("Vertical") * speed;

        //Vector3 dir = new Vector3(vertical, 0f, -horizontal);
        if (!PV.IsMine)
            return;


        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;
        float maxSpeed = this.maxSpeed;

        CounterMovement(dir.z, dir.x, mag);

        rb.AddForce(transform.forward * dir.x * moveforce);
        rb.AddForce(transform.right * dir.z * moveforce);
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

}

