using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private bool RotateTowardMouse;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    public Camera look;
    public Vector3 offset;
    public SpawnManager spawnManager;
    private LightingManager lightingManager;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        lightingManager = GameObject.Find("LightManager").GetComponent<LightingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var targetVector = new Vector3(0, 0, _input.InputVector.y);

        if (_input.InputVector.y > 0){
            var movementVector = MoveTowardTarget(targetVector);
        }

        if (_input.InputVector.y == 1)
        {
            lightingManager.SunRotate();
        }

        // if (!RotateTowardMouse)
        // {
        //     RotateTowardMovementVector(movementVector);
        // }
        // if (RotateTowardMouse)
        // {
        //     RotateFromMouseVector();
        // }

        look.transform.position = this.gameObject.transform.position + offset;
    }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;
        // transform.Translate(targetVector * (MovementSpeed * Time.deltaTime)); Demonstrate why this doesn't work
        //transform.Translate(targetVector * (MovementSpeed * Time.deltaTime), Camera.gameObject.transform);

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TiggerBox"))
        {
            spawnManager.SpawnTriggerEntered();
        }
    }
}
