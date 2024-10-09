using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    //Thiago Grossi esteve aqui...

    public Rigidbody rigidbody;
    public Transform ball;
    public GameObject player;
    public Transform target;
    public Transform shootingPosition;
    public Transform PosDribble;
    public Vector3 InitialVelocity;
    public float gravity;
    public int pathResolution;
    public bool showDebugPath;
    public bool BallInHands;
    public int time = 0;

    public bool InBallHands = false;
    public float releaseOffset;

    private bool isLaunching;

    public bool prepareToShoot;

    public GameObject teammateGameObject;

    public bool bola;

    //Muito cuidado ao mexer em qualquer coisa, pode fazer o sistema inteiro da bola parar de funcionar!

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float durationTime;

        public LaunchData(Vector3 velocity, float time)
        {
            this.initialVelocity = velocity;
            this.durationTime = time;
        }
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - this.transform.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - shootingPosition.transform.position.x, 0, target.position.z - shootingPosition.transform.position.z);
        float targetDistance = Vector3.Distance(this.transform.position, target.transform.position);

        // Ajuste a altura máxima do arco
        float curveHeight = Mathf.Clamp(targetDistance * 0.5f, 0.5f, 6); // Reduzido para 50% da distância total

        float value = (displacementY - curveHeight) / gravity;
        float clampedCurve = Mathf.Clamp(value, 0, value);
        float time = Mathf.Sqrt(-2 * curveHeight / gravity) + Mathf.Sqrt(2 * clampedCurve);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHeight * 0.8f); // Ajuste de altura

        Vector3 velocityXZ = (displacementXZ * releaseOffset) / time;
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void Awake()
    {
        if (bola == true && BallInHands == true)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
        }
    }

    void Start()
    {
        if (bola == true && BallInHands == true)
        {
            this.isLaunching = false;
            Physics.gravity = Vector3.up * this.gravity;
        }
    }

    void Update()
    {
        if (InBallHands)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                DrawPath();
                if (bola == true && BallInHands == true)
                {
                    ball.position = shootingPosition.position;
                }

                prepareToShoot = true;

                bola = true;

                releaseOffset += .002f;
            }
            else if (!isLaunching)
            {
                ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (prepareToShoot)
                {
                    Launch();
                    bola = false;
                    BallInHands = false;
                    releaseOffset = 0; // Reseta o releaseOffset
                }
            }
        }
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        if (float.IsNaN(launchData.initialVelocity.y) || float.IsInfinity(launchData.initialVelocity.y))
        {
            return;
        }

        Vector3 originalPosition = this.transform.position;

        Vector3[] positions = new Vector3[this.pathResolution + 1];
        for (int i = 0; i <= this.pathResolution; i++)
        {
            float simulationTime = (i / (float)this.pathResolution) * launchData.durationTime;
            Vector3 displacement = launchData.initialVelocity * simulationTime + (Vector3.up * gravity) * simulationTime * simulationTime / 2f;
            positions[i] = originalPosition + displacement;
        }

        this.InitialVelocity = launchData.initialVelocity;
    }

    void Launch()
    {
        if (bola == true && BallInHands == true)
        {
            this.isLaunching = true;
            shootingPosition.transform.LookAt(target.transform);
            rigidbody.useGravity = true;

            if (prepareToShoot)
            {
                rigidbody.velocity = CalculateLaunchData().initialVelocity;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ball")
        {
            isLaunching = false;
            BallInHands = true;
            InBallHands = true;
        }
    }
}
