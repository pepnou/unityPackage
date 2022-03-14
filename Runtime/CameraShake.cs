using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //[SerializeField] float shakeIntensity = .1f;
    Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        /*pos = transform.position;
        float cTime = Time.realtimeSinceStartup;

        while(Time.realtimeSinceStartup - cTime < time)
        {
            transform.position = pos + transform.TransformDirection(Vector3.up) * Random.Range(-1f, 1f) * shakeIntensity + transform.TransformDirection(Vector3.right) * Random.Range(-1f, 1f) * shakeIntensity;
            yield return null;
        }

        transform.position = pos;*/

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }
}
