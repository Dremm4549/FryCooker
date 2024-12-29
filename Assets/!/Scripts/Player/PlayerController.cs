using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float moveSpeed = 4.0f;

    [SerializeField] private float mouseSpeed = 4.0f;
    [SerializeField] private float xRot;

    [SerializeField] InputHandler inputHandler;

    [SerializeField] Camera cam;
    [SerializeField] Transform playerBody;
    [SerializeField] Vector2 camRotation = Vector2.zero;
    [SerializeField] Vector2 camTargetRotation = Vector2.zero;

    private void Awake()
    {
        playerBody = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();  
        inputHandler = GetComponent<InputHandler>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Vector3 move = transform.right * inputHandler.moveInput.x + transform.forward * inputHandler.moveInput.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        camRotation.x += mouseSpeed * inputHandler.mouseInput.x;
        camRotation.y = Mathf.Clamp(camRotation.y - mouseSpeed * inputHandler.mouseInput.y, -90, 90);

        camTargetRotation.x += transform.eulerAngles.x + mouseSpeed * inputHandler.mouseInput.x;
        transform.rotation = Quaternion.Euler(0f, camTargetRotation.x, 0f);

        cam.transform.rotation = Quaternion.Euler(camRotation.y, camRotation.x, 0f);
    }





}