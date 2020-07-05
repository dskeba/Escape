
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera _mainCamera;
    private float _targetAngle;
    [SerializeField]
    private GameObject _playerHipsObject;
    [SerializeField]
    private GameObject _playerTorsoObject;
    private Animator _animator;
    private Vector3 _input;

    private void Start()
    {
        _mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _input = Vector2.ClampMagnitude(_input, 1);
        _animator.SetFloat("MoveX", _input.x);
        _animator.SetFloat("MoveY", _input.y);

        float yawCamera = _mainCamera.transform.rotation.eulerAngles.y;

        float pitchCamera = _mainCamera.transform.rotation.eulerAngles.x;
        if (pitchCamera > 30 && pitchCamera < 90)
        {
            pitchCamera = 30;
        }
        else if (pitchCamera > 270 && pitchCamera < 330)
        {
            pitchCamera = 330;
        }

        Vector3 direction = new Vector3(_input.x, 0f, _input.y).normalized;
        _targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (_targetAngle == 180)
        {
            _targetAngle = 0;
        }
        else if (_targetAngle > 90)
        {
            var offset = _targetAngle - 90;
            _targetAngle = -offset;
        } 
        else if (_targetAngle < -90)
        {
            var offset = _targetAngle + 90;
            _targetAngle = -offset;
        }
        transform.eulerAngles = new Vector3(0, yawCamera + _targetAngle, 0);
        _playerHipsObject.transform.localEulerAngles = new Vector3(0f, -_targetAngle, 0f);
    }
}
