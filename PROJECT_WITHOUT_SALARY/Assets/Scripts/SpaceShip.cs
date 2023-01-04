using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class SpaceShip : MonoBehaviour
{
    private Rigidbody _body => GetComponent<Rigidbody>();

    public float LookRateSpeed = 90f;
    private Vector2 _lookInput, _screenCenter, _mouseDistance;

    public float ForwardSpeed = 25f;
    private float _activeForwardSpeed;
    private float _forwardAcceleration = 2.5f;

    private float _rollInput;
    public float RollSpeed = 90f, rollAcceleration = 3.5f;


    void Awake()
    {
        _body.useGravity = false;
        _body.drag = 0.5f;
        _screenCenter.x = Screen.width * .5f;
        _screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {
        LookInput();

        MovementInput();

        Move();

        Rotation();

    }


    void LookInput()
    {
        _lookInput.x = Input.mousePosition.x;
        _lookInput.y = Input.mousePosition.y;

        _mouseDistance.x = (_lookInput.x - _screenCenter.x) / _screenCenter.y;
        _mouseDistance.y = (_lookInput.y - _screenCenter.y) / _screenCenter.y;
    }


    void MovementInput()
    {
        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed,
           Input.GetAxis("Vertical") * ForwardSpeed,
           _forwardAcceleration * Time.deltaTime);
    }

    void Move()
    {
        var _boost = Input.GetKey(KeyCode.Space) ? 2f : 1f;
        _body.drag = Input.GetKey(KeyCode.LeftShift) ? 10f : 0.5f;

        // _body.velocity = transform.forward * _activeForwardSpeed * _boost * Time.deltaTime;

        _body.AddForce(_activeForwardSpeed * _boost * Time.deltaTime * transform.forward, ForceMode.VelocityChange);
    }

    void Rotation()
    {
        _mouseDistance = Vector2.ClampMagnitude(_mouseDistance, 1f);

        _rollInput = Mathf.Lerp(_rollInput, Input.GetAxis("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-_mouseDistance.y * LookRateSpeed * Time.deltaTime,
            _mouseDistance.x * LookRateSpeed * Time.deltaTime, _rollInput * RollSpeed * Time.deltaTime, Space.Self);
    }

    
    
}
