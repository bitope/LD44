using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float walkSpeed = 8f;
    public float jumpForce = 220;
    public LayerMask groundedMask;

    public static bool ControlIsLocked;

    Transform cameraTransfrom;
    float verticalLookRotation;
    Vector3 moveAmount;
    Vector3 rotateAmount;
    Vector3 smoothMoveVelocity;
    Vector3 smoothCamera;
    Rigidbody rb;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cameraTransfrom = Camera.main.transform;
    }

    private void Update()
    {
        if (CheckLocked())
        {
            return;
        }

        var euler = Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX;
        rotateAmount = Vector3.SmoothDamp(rotateAmount, euler, ref smoothCamera, 0.05f);
        transform.Rotate(rotateAmount);

        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraTransfrom.localEulerAngles = (Vector3.left * verticalLookRotation);

        var moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        var targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.1f);

        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
                rb.AddForce(transform.up * jumpForce);
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        return Physics.Raycast(ray, out hit, 1f + 0.1f, groundedMask, QueryTriggerInteraction.Ignore);
    }

    private bool CheckLocked()
    {
        if (ControlIsLocked)
        {
            Debug.Log("Locked.");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            moveAmount = Vector3.zero;
            return true;
        }

        Debug.Log("Unlocked.");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        return false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
