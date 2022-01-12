using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    private PlayerController playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Animator animator;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private GameObject topDownCamera;
    [SerializeField]
    private GameObject objBullet;
    [SerializeField]
    private Transform bulletDirection;
    [SerializeField]
    private Button btnShoot;
    private bool isShooting;
    private bool canShoot;
    private bool isClipFull = true;
    private int weaponClip = 24;
    private float fireDelay = 0.3f;
    private float reloadTime = 2.5f;
    private int remainingBullet;
    private Coroutine crReloading;
    //[SerializeField]
    //private float jumpHeight = 1.0f;
    //[SerializeField]
    //private float gravityValue = -9.81f;

    private void Awake()
    {
        playerInput = new PlayerController();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetInteger("WeaponType_int", 6);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        //btnShoot.onClick.AddListener(PlayerShoot);
        remainingBullet = weaponClip;
        canShoot = true;
        //playerInput.Player.Shoot.performed += _ => PlayerShoot();
    }
    
    public void SetIsShooting (bool b)
    {
        isShooting = b;
    }

    public void PlayerShoot()
    {
        if (!isShooting) return;
        if (!canShoot) return;
        if (!isClipFull)
        {
            if (crReloading == null)
                crReloading = StartCoroutine(_Reloading());
            return;
        }
        crReloading = null;
        GameObject b = Instantiate(objBullet, bulletDirection.position, bulletDirection.rotation);
        remainingBullet -= 1;
        if (remainingBullet < 1)
            isClipFull = false;

        StartCoroutine(_CanShoot());
    }

    IEnumerator _CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    IEnumerator _Reloading ()
    {
        animator.SetBool("Reload_b", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("Reload_b", false);
        remainingBullet = weaponClip;
        isClipFull = true;
    }

    void Update()
    {
        if (isShooting)
            PlayerShoot();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector2 lookInput = playerInput.Player.Look.ReadValue<Vector2>();

        Vector3 move = new Vector3(movementInput.x * 1f, 0f, movementInput.y * 1f);
        if (!topDownCamera.activeSelf)
            move = move.x * transform.right.normalized + move.z * transform.forward.normalized;
        
        Vector3 look = new Vector3(lookInput.x * 1f, 0f, lookInput.y * 1f);
        controller.Move(move * Time.deltaTime * playerSpeed);
        animator.SetFloat("Speed_f", move.magnitude);

        if (look != Vector3.zero)
        {
            if (topDownCamera.activeSelf)
                isShooting = true;
            //if (!topDownCamera.activeSelf)
            //{
                Quaternion newDirection = Quaternion.LookRotation(look);
                gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * playerSpeed);
            //}
        } else
        {
            if (topDownCamera.activeSelf)
                isShooting = false;
        }

        
    }
}
