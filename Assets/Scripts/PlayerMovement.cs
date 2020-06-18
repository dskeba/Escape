
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 6f;
    private float jumpForce = 15f;

    private float jumpTimer;
    private float jumpRate = 1f;
    private float jumpDelay = 0.2f;

    private Camera mainCamera;

    bool isGrounded = false;
    Rigidbody rb;
    private Animator animator;

    private bool running = false;

    private Vector2 input;

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        jumpTimer += Time.deltaTime;
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            isGrounded = false;
            jumpTimer = 0f;
            animator.SetBool("Jumping", true);
            StartCoroutine(DelayJump());
        }

        if (Input.GetButton("Fire3"))
        {
            running = true;
            animator.SetBool("Running", true);
            speed = 10f;
        } else
        {
            running = false;
            animator.SetBool("Running", false);
            speed = 6f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        animator.SetBool("Jumping", false);
    }

    private bool CanJump()
    {
        return (jumpTimer >= jumpRate) && isGrounded;
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(jumpDelay);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
        var forward = mainCamera.transform.forward;
        var right = mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        transform.position += (forward * input.y + right * input.x) * Time.deltaTime * speed;
    }

}
