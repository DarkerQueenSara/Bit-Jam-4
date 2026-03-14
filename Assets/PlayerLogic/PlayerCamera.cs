using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    private const string hInput = "Mouse X";
    private const string vInput = "Mouse Y";
    private Vector2 rotation = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis(hInput);
        rotation.y += Input.GetAxis(vInput);
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        //transform.RotateAround(transform.position, Vector3.up, rotateH * sensitivity * Time.deltaTime);
        //transform.RotateAround(Vector3.zero, transform.right, rotateV * sensitivity * Time.deltaTime);
        //transform.Rotate(Vector3.up, rotateH * sensitivity * Time.deltaTime);
        //transform.Rotate(Vector3.left, rotateV * sensitivity * Time.deltaTime);
        var xRot = Quaternion.AngleAxis(rotation.x * sensitivity, Vector3.up);
        var yRot = Quaternion.AngleAxis(rotation.y * sensitivity, Vector3.left);
        transform.localRotation = xRot * yRot;
    }

    private void FixedUpdate()
    {
        
    }
}
