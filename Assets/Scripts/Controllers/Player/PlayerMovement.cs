﻿
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    public PostProcessProfile postProcessProfile;

    private float _speed = 6f;
    private float _jumpForce = 18f;
    private float _jumpTimer;
    private float _jumpRate = 1f;
    private float _jumpDelay = 0.2f;
    private Camera _mainCamera;
    private bool _isGrounded = false;
    private Rigidbody _rb;
    private Animator _animator;
    private Vector2 _input;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
        if (Input.GetButton("Fire3") && _input.y > 0)
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0.35f;
            _animator.SetBool("Running", true);
            _speed = 11f;

        }
        else
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0f;
            _animator.SetBool("Running", false);
            _speed = 6f;
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
        PreventIdleMovement();

        var forward = _mainCamera.transform.forward;
        var right = _mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        var destination = transform.position + (forward * _input.y + right * _input.x) * Time.deltaTime * _speed;
        _rb.MovePosition(destination);
    }

    private void PreventIdleMovement()
    {
        if (_isGrounded && _input.x == 0 && _input.y == 0)
        {
            _rb.velocity = Vector3.zero;
        }
    }

}
