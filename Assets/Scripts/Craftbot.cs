using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craftbot : MonoBehaviour
{
    #region VARIABLES
    public static Craftbot instance;

    [SerializeField] private Button    button; 
    [SerializeField] private LayerMask ground;

    [SerializeField] private float speed       = 5f,
                                   rotateSpeed = 5f;

    [SerializeField] private float xLimit = 5f,
                                   zLimit = 5f;

    [SerializeField] private float jumpForce = 5f;

    private const float radius = 0.1f;

    private Rigidbody   body;
    private Animator    anim;
    private Vector3     movement;
    private bl_Joystick joystick;
    
    public bool isGrounded = false,
                 canJump   = false;
    #endregion

    #region MONOBEHAVIOUR METHODS
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start ()
    {
        anim     = GetComponent<Animator>();
        body     = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<bl_Joystick>();

        // Add on Click event to existing button
        button = GameObject.Find("Jump").GetComponent<Button>();
        button.onClick.AddListener(Jump);
    }
	
	void Update ()
    {
        isGrounded = Physics.OverlapSphere(transform.position, radius, ground).Length > 0;
        
        movement = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        
        // Set animation states (idle or walk)
        anim.SetFloat("Speed", movement.sqrMagnitude);

        // Custom methods calls
        FaceMoveDirection();
        ConstrainMovement();
    }

    // Physics only
    void FixedUpdate()
    {
        body.AddForce(movement * speed);

        if (canJump)
        {
            body.AddForce(Vector3.up * jumpForce);
            canJump = false;
        }
    }
    #endregion
    
    #region CUSTOM METHODS
    void FaceMoveDirection ()
    {
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(-movement),
                                                  Time.deltaTime * rotateSpeed);
        }
    }

    // Prevent from falling
    void ConstrainMovement ()
    {
        Vector3 constrainedPosition = transform.position;

        constrainedPosition.x = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        constrainedPosition.z = Mathf.Clamp(transform.position.z, -zLimit, zLimit);

        transform.position = constrainedPosition;
    }

    // For UI button
    public void Jump ()
    {
        if (isGrounded) canJump = true;
    }
    #endregion
}
