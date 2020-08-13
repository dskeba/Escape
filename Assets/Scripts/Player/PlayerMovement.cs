
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    public PostProcessProfile postProcessProfile;

    private float _walkSpeed = 7f;
    private float _runSpeed = 14f;
    private float _currentSpeed = 0f;
    private float _jumpForce = 18f;
    private float _jumpTimer;
    private float _jumpRate = 1f;
    private float _jumpDelay = 0.15f;
    private Camera _mainCamera;
    private bool _isGrounded = false;
    private Rigidbody _rb;
    private Animator _animator;
    private Vector2 _input;
    private PlayerStamina _playerStamina;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerStamina = GetComponent<PlayerStamina>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _input = Vector2.ClampMagnitude(_input, 1);

        CheckJumping();
        CheckRunning();
    }

    private void CheckJumping()
    {
        _jumpTimer += Time.deltaTime;
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            _isGrounded = false;
            _jumpTimer = 0f;
            _animator.SetBool("Jumping", true);
            StartCoroutine(DelayJump());
        }
    }

    private void CheckRunning()
    {
        Vignette vignette;
        ChromaticAberration chromaticAberration;
        postProcessProfile.TryGetSettings<Vignette>(out vignette);
        postProcessProfile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        if (_input.y > 0 && Input.GetButton("Fire3") && _playerStamina.CurrentStamina > 0)
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0.25f;
            _animator.SetBool("Running", true);
            _currentSpeed = _runSpeed;
            _playerStamina.RemoveStamina(Time.deltaTime, Vector3.zero);
        }
        else
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0f;
            _animator.SetBool("Running", false);
            _currentSpeed = _walkSpeed;
            _playerStamina.AddStamina(Time.deltaTime / 2, Vector3.zero);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _animator.SetBool("Jumping", false);
    }

    private bool CanJump()
    {
        return (_jumpTimer >= _jumpRate) && _isGrounded;
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(_jumpDelay);
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        var forward = _mainCamera.transform.forward;
        var right = _mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        var destination = transform.position + (forward * _input.y + right * _input.x) * Time.fixedDeltaTime * _currentSpeed;
        _rb.MovePosition(destination);
    }
}
