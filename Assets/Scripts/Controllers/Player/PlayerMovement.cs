
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    public PostProcessProfile postProcessProfile;

    private float speed = 6f;
    private float jumpForce = 18f;
    private float jumpTimer;
    private float jumpRate = 1f;
    private float jumpDelay = 0.2f;
    private Camera mainCamera;
    private bool isGrounded = false;
    private Rigidbody rb;
    private Animator animator;
    private Vector2 input;

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        jumpTimer += Time.deltaTime;

        if (Input.GetButtonDown("Jump") && CanJump())
        {
            isGrounded = false;
            jumpTimer = 0f;
            animator.SetBool("Jumping", true);
            StartCoroutine(DelayJump());
        }

        Vignette vignette;
        ChromaticAberration chromaticAberration;
        postProcessProfile.TryGetSettings<Vignette>(out vignette);
        postProcessProfile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        if (Input.GetButton("Fire3") && input.y > 0)
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0.35f;
            animator.SetBool("Running", true);
            speed = 11f;
            
        } 
        else
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0f;
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
        var forward = mainCamera.transform.forward;
        var right = mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        var destination = transform.position + (forward * input.y + right * input.x) * Time.deltaTime * speed;
        rb.MovePosition(destination);
    }

}
