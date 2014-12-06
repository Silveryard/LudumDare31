using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float MovementSpeed;
    public float RotationSpeed;

    private CharacterController _characterController;

    protected void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    protected void Update() {
        Vector3 movement = Vector3.zero;

        movement.z = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;
        movement.x = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;

        _characterController.Move(movement);

        float rotation = Input.GetAxis("Rotation") * RotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotation, 0));
    }
	
}
