using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;



    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
                                           //put Vec3 as its 2d game so position in vec 2 but cant add vec2 and vec 3 so we cast it to a vec3 in Vec
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;//unit circle has radius of 1. times shakeMag to change radius of circle of where camera is
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();//waits until end of frame before loop checks if it needs to do more
        }    
        transform.position = initialPosition;//puts camera back to normal pos
    }
}
