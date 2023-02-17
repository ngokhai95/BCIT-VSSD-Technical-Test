using System.Collections.Generic;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float moveSpeed = 5f;

    private UserInteraction userInteraction;
    private bool isRotating;
    private bool isMoving;
    private bool isPartMoving;
    private Vector3 initialMousePosition;
    private Vector3 initialPartPosition;
    private Vector3 lastMousePosition;

    private void Start()
    {
        userInteraction = FindObjectOfType<UserInteraction>();
    }

    private void Update()
    {
        RotateModel();
        MoveModel();
        MoveSelectedPart();
    }

    private void MoveSelectedPart()
    {
        GameObject selectedPart = userInteraction.selectedObject;
        if (selectedPart == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            isPartMoving = true;
            initialMousePosition = Input.mousePosition;
            initialPartPosition = selectedPart.transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPartMoving = false;
        }

        if (isPartMoving)
        {
            // Calculate the distance based on the mouse movement (scale with screen size)
            Vector3 mouseDelta = (Input.mousePosition - initialMousePosition) / Screen.width;
            Vector3 targetPosition = initialPartPosition + new Vector3(mouseDelta.x, mouseDelta.y, 0f) * moveSpeed;

            selectedPart.transform.position = targetPosition;
        }
    }

    private void RotateModel()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Calculate the rotation amount based on the mouse movement
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationX = delta.x * rotationSpeed;
            float rotationY = delta.y * rotationSpeed;

            // Rotate the object by y-axis
            transform.Rotate(Vector3.up, rotationX, Space.World);
            // Rotate the object by x-axis
            transform.Rotate(Vector3.right, rotationY, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }

    private void MoveModel()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isMoving = true;
            initialMousePosition = Input.mousePosition;
            initialPartPosition = transform.position;
        }

        if (Input.GetMouseButtonUp(2))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            // Calculate the distance based on the mouse movement (scale with screen size)
            Vector3 mouseDelta = (Input.mousePosition - initialMousePosition) / Screen.width;
            Vector3 targetPosition = initialPartPosition + new Vector3(mouseDelta.x, mouseDelta.y, 0f) * moveSpeed;

            transform.position = targetPosition;
        }
    }
}