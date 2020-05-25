using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    Vector3 initialPosition;
    float shakeMagnitude = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = mainCamera.transform.parent.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        initialPosition = mainCamera.transform.parent.localPosition;
    }

    public void Shake()
    {
        InvokeRepeating("Shaking", 0f, 0.005f);
        Invoke("StopShaking", 0.5f);
    }

    void Shaking()
    {
        float offsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float offsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;

        Vector3 shakePosition = mainCamera.transform.parent.localPosition; ;
        shakePosition.x += offsetX;
        shakePosition.y += offsetY;

        mainCamera.transform.localPosition = shakePosition;
    }

    void StopShaking()
    {
        CancelInvoke("Shaking");
        mainCamera.transform.position = initialPosition;
    }
}
