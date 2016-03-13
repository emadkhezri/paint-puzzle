using UnityEngine;
using System.Collections;

public class ShakeObjectScript : MonoBehaviour
{
    private enum ShakeState
    {
        None,
        RotateLeft,
        RotateRight,
        GoBackToStart
    }

    private const float shakeDuration = 0.1f;
    private const int noShakes = 2;
    private float maxRotation = 10;

    private int currentShakeCount;
    private ShakeState shakeState;
    private float currentRotation;

    // Use this for initialization
    void Start()
    {
        shakeState = ShakeState.RotateLeft;
        currentShakeCount = 0;
        currentRotation = 0;
        maxRotation += Random.Range(-5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Time.deltaTime / (shakeDuration / noShakes / 3) * maxRotation;

        switch (shakeState)
        {
            case ShakeState.None:
                Destroy(this);
                return;
            case ShakeState.RotateLeft:
                currentRotation += rotation;
                gameObject.transform.Rotate(new Vector3(0, 0, 1), rotation);
                if (currentRotation >= maxRotation)
                    shakeState = ShakeState.RotateRight;
                break;
            case ShakeState.RotateRight:
                currentRotation -= rotation;
                gameObject.transform.Rotate(new Vector3(0, 0, 1), -rotation);
                if (currentRotation <= -maxRotation)
                    shakeState = ShakeState.GoBackToStart;
                break;
            case ShakeState.GoBackToStart:
                currentRotation += rotation;
                if (currentRotation >= 0)
                {
                    ++currentShakeCount;
                    if (currentShakeCount == noShakes)
                    {
                        gameObject.transform.Rotate(new Vector3(0, 0, 1), rotation - currentRotation);
                        shakeState = ShakeState.None;
                    }
                    else
                    {
                        gameObject.transform.Rotate(new Vector3(0, 0, 1), rotation);
                        shakeState = ShakeState.RotateLeft;
                    }
                }
                else
                    gameObject.transform.Rotate(new Vector3(0, 0, 1), rotation);
                break;
        }
    }
}
