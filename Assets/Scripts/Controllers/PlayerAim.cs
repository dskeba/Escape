
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera mainCamera;
    private float targetAngle;
    private GameObject playerHipsObject;
    private Animator animator;
    private Rigidbody rb;
    private Vector3 input;

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerHipsObject = GameObject.FindGameObjectWithTag("PlayerHips");
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (targetAngle == 180)
        {
            targetAngle = 0;
        }
        else if (targetAngle > 90)
        {
            var offset = targetAngle - 90;
            targetAngle = -offset;
        } 
        else if (targetAngle < -90)
        {
            var offset = targetAngle + 90;
            targetAngle = -offset;
        }
        transform.eulerAngles = new Vector3(0, yawCamera + targetAngle, 0);
        //Quaternion deltaRotation = Quaternion.Euler(0, yawCamera + targetAngle, 0);
        //rb.MoveRotation(deltaRotation);
        playerHipsObject.transform.localEulerAngles = new Vector3(0f, -targetAngle, 0f);

    }
}
