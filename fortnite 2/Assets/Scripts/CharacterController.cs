using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector3 dir;
    private Rigidbody rb;
    private float force = 20f;


    //public PlayerInput playerInput;
    public Inputsys controls;
    [SerializeField]
    private FPSAnimation fpsAnim;

    PhotonView PV;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!PV.IsMine)
        {
            
            Destroy(rb);
            
        }
    }
    private void Awake()
    {
        

    }

    private void OnEnable()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            Destroy(GetComponent<PlayerInput>());
            GetComponentInChildren<Camera>().enabled = false;
            this.enabled = false;
            return;
        }

        controls = new Inputsys();

        controls.Player.Movement.performed += ctx => Walk(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => Walk(new Vector2(0f, 0f));
        controls.Player.Shoot.performed += ctx => Shoot();
        controls.Player.Reload.performed += ctx => Reload();
        controls.Player.Jump.performed += ctx => Jump();
        
        

        fpsAnim = gameObject.GetComponent<FPSAnimation>();
        controls.Enable();
    }
    private void OnDisable()
    {
        if (!PV.IsMine)
            return;
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

        dir = new Vector3(direction.y, 0f, -direction.x);

    }

    void Shoot()
    {
        //var animator = GetComponent<Animator>();
        //animator.SetTrigger("Shoot");
        fpsAnim.PlayAnimShoot();
    }

    void Reload()
    {
        fpsAnim.PlayAnimReload();
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        Debug.Log("Jump");
    }

    void Update()
    {
        //horizontal = c * speed;
        //vertical = Input.GetAxisRaw("Vertical") * speed;

        //Vector3 dir = new Vector3(vertical, 0f, -horizontal);
        if (!PV.IsMine)
            return;
        transform.Translate(dir);     
    }

}

