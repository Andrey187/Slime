using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 1f;
    [SerializeField] private float _sideSpeed = 1f;
    [SerializeField] private float _moveBound = 8f;
    [SerializeField] private float _angleBound = 45f;
    [SerializeField] private FloatingJoystick _floatingJoystick;
    public bool moveEnable = true;
    private void Update()
    {
        if (moveEnable)
        {
            transform.position += transform.forward * _forwardSpeed * Time.deltaTime;
            PCMoveSides(Input.GetAxisRaw("Horizontal"));
            PCMoveSides(_floatingJoystick.Horizontal);
        }
        //else
        //{
        //    GetComponent<Rigidbody>().useGravity = false;
        //}
    }

    private void PCMoveSides(float sideDirection)
    {
        bool conditionLeftSide = sideDirection < 0 && transform.position.x >= -_moveBound;
        bool conditionRightSide = sideDirection > 0 && transform.position.x <= _moveBound;
        if (conditionLeftSide || conditionRightSide)
        {
            transform.position += new Vector3(sideDirection * _sideSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, sideDirection * _angleBound, 0), Time.deltaTime * _sideSpeed);
        }
        else { transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f); }
    }
}
