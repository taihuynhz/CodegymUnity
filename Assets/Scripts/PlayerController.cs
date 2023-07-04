using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance => instance;

    [SerializeField] protected new Rigidbody rigidbody;

    [Header("===== Drive Mode =====")]
    [SerializeField] protected bool onAutomatic;
    [SerializeField] protected bool onManual;

    [Header("===== Car =====")]
    [SerializeField] protected float maxAcceleration = 30.0f;
    [SerializeField] protected float brakeAcceleration = 50.0f;
    [SerializeField] protected float turnSensitivity = 1.0f;
    [SerializeField] protected float maxSteerAngle = 30.0f;
    [SerializeField] protected float autoSpeed = 15f;
    [SerializeField] protected float autoRotateSpeed = 2.5f;
    [SerializeField] protected List<Wheel> wheels;

    [Header("===== Checkpoints =====")]
    [SerializeField] protected int laps = 0;
    [SerializeField] protected int currentPoint = 0;
    [SerializeField] protected float minDistanceAuto = 0.1f;
    [SerializeField] protected float minDistanceManual = 10f;
    [SerializeField] protected List<Vector3> checkPoints;

    [Header("===== Car Models =====")]
    [SerializeField] protected List<Transform> models;

    protected enum CarModel { Car5A, Car5B, Car7A }
    protected CarModel model = CarModel.Car5A;

    protected enum Axel { Front, Rear }
    [Serializable] protected struct Wheel
    {
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    protected enum DriveMode { Manual, Automatic }
    protected DriveMode mode = DriveMode.Manual;
    protected Vector3 lastPosition;
    protected float playerSpeed;


    protected void Awake()
    {
        CreateSingleton();
    }

    protected void Start()
    {
        rigidbody.centerOfMass = Vector3.zero;
    }

    private void Update()
    {
        ChangeModel();
        GetPlayerSpeed();
    }

    protected void FixedUpdate()
    {
        CarMode();
    }

    protected void CarMode()
    {
        if (Input.GetKey(KeyCode.Alpha1)) mode = DriveMode.Automatic;
        else if (Input.GetKey(KeyCode.Alpha2)) mode = DriveMode.Manual;

        if (mode == DriveMode.Automatic)
        {
            onAutomatic = true;
            onManual = false;
            CarAutoMode();
        }
        else if (mode == DriveMode.Manual)
        {
            onAutomatic = false;
            onManual = true;
            CarManualMode();
        }
    }

    protected void CarManualMode()
    {
        ApplyMotorTorque();
        Steering();
        Braking();
        CheckPointsCounter();
    }

    public void ApplyMotorTorque()
    {
        // Forward/backward movement
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Rear)
            {
                wheel.wheelCollider.motorTorque = Input.GetAxis("Vertical") * 600 * maxAcceleration * Time.deltaTime;
            }
        }

    }

    public void Steering()
    {
        // Apply sterring
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = Input.GetAxis("Horizontal") * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    public void Braking()
    {
        // Apply braking
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    protected void CarAutoMode()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkPoints[currentPoint], Time.deltaTime * autoSpeed);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, checkPoints[currentPoint] - transform.position, Time.deltaTime * autoRotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        CheckPointsCounter();
    }

    protected void CheckPointsCounter()
    {
        if (mode == DriveMode.Automatic)
        {
            if (Vector3.Distance(checkPoints[currentPoint], transform.position) < minDistanceAuto) currentPoint++;
        }
        else if (mode == DriveMode.Manual)
        {
            if (Vector3.Distance(checkPoints[currentPoint], transform.position) < minDistanceManual) currentPoint++;
        }

        if (currentPoint == checkPoints.Count)
        {
            currentPoint = 0;
            laps++;
        }
    }

    protected void ChangeModel()
    {
        if (Input.GetKeyDown(KeyCode.F)) model++;
        if ((int)model >= 3) model = 0;

        for (int i = 0; i < models.Count; i++)
        {
            if (i == (int)model) models[i].gameObject.SetActive(true);
            else models[i].gameObject.SetActive(false);
        }
    }

    protected void GetPlayerSpeed()
    {
        playerSpeed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;
        lastPosition = transform.position;
    }

    protected void CreateSingleton()
    {
        if (PlayerController.instance != null) return;
        PlayerController.instance = this;
    }
}
