using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Librer�a para que funcione el New Input System

public class PlayerController2D : MonoBehaviour
{

    
    //Referencias generales
    [SerializeField] Rigidbody2D playerRb; //Reff al rigidbody del player.
    [SerializeField] PlayerInput playerInput; //Ref a� gestor del input del jugador
    [SerializeField] Animator playerAnim; //Ref al animator para gestionar las transiciones de animaci�n


    [Header("Movement Parameters")]
    private Vector2 moveInput;//Almac�n del input del player.
    public float speed;
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    //variables para el GroundCheck
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadious = 0.1f;
    [SerializeField] LayerMask groundLayer;



    // Start is called before the first frame update
    void Start()
    {
        //Autoreferenciar componentes: nombre de variable = GetComponent()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
        isFacingRight = true;

    }

    // Update is called once per frame
    void Update()
    {

        HandleAnimations();
        GroundCheck();


        //Flip
        if (moveInput.x > 0)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        if (moveInput.x < 0)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
       playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight; //nombre de bool = !nombre de bool (cambio al estado contrario)
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, groundLayer);
    }



    void HandleAnimations()
    {
        //Conector de valores generales con par�metros de cambios de animaci�n.
        playerAnim.SetBool("IsJumping", !isGrounded);
        if (moveInput.x != 0) playerAnim.SetBool("IsRunning", true);
        else playerAnim.SetBool("IsRunning", false);
    }

    #region Input Events
    //Para crear un evento:
    //Se definde PUBLIC sin tipo de dato (VOID) y con una referencia al input (Callback.Context)
    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }

         
    }



    #endregion

}
