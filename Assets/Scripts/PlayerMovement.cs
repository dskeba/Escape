
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 6f;
    private float jumpForce = 10f;

    private float jumpTimer;
    private float jumpRate = 2f;
    private float jumpDelay = 0.2f;

    bool isGrounded = false;
    bool performJump = false;
    Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            performJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        animator.SetBool("Jumping", false);
        Debug.Log("FALSE");
    }

    private void CheckJump()
    {
        jumpTimer += Time.deltaTime;
        if (performJump && jumpTimer >= jumpRate && isGrounded)
        {
            performJump = false;
            isGrounded = false;
            jumpTimer = 0f;
            animator.SetBool("Jumping", true);
            StartCoroutine(DelayJump());
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(jumpDelay);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        CheckJump();

        var camera = Camera.main;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
        var forward = camera.transform.forward;
        var right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        transform.position += (forward * input.y + right * input.x) * Time.deltaTime * speed;
    }
}
