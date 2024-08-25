using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 16f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputMoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputMoveDir.x = +1f;
        }

        Vector3 movementVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += movementVector * movementSpeed * Time.deltaTime;

        Vector3 rotationVector = Vector3.zero;
        if (Input.GetKey(KeyCode.Q)) {
            rotationVector.y = -1f;
        }
        if (Input.GetKey(KeyCode.E)) {
            rotationVector.y = +1f;
        }
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;

    }
}
