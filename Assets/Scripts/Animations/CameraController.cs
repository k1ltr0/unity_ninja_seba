using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    bool earthquake;
    private Quaternion targetRotation;
    public Transform playerTransform;
    private Transform thisTransform;
    private float currentShakeIntensity, currentEarthquakeIntensity;
    private float shakeDelta, earthquakeDelta;
    public float shakeDuration, earthquakeDuration;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        thisTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 relativeTargetRotationVec = targetRotation.eulerAngles;
        relativeTargetRotationVec.y += playerTransform.eulerAngles.y;
        Quaternion relativeTargetRotation = thisTransform.rotation;
        relativeTargetRotation.eulerAngles = relativeTargetRotationVec;


        if (earthquake)
        {
            Vector3 rotation = relativeTargetRotation.eulerAngles;
            rotation.Set(rotation.x + Random.Range(-currentEarthquakeIntensity, currentEarthquakeIntensity),
                rotation.y /*+ Random.Range(-currentEarthquakeIntensity, currentEarthquakeIntensity)*/, // Descomentar si se quiere temblar en Y
                rotation.z = -10);
            currentEarthquakeIntensity -= earthquakeDelta * Time.deltaTime; // slowly wind down



            earthquakeDuration -= Time.deltaTime;
            //thisTransform.eulerAngles = rotation;
    
            thisTransform.position = rotation;

            if (earthquakeDuration < 0)
            {
                earthquake = false; // will end on the next update loop
            }
        }
    }

    public void Shake(float duration, float intensity, float waitTime) {

        StartCoroutine(WaitShake(duration, intensity, waitTime));

    }


    private IEnumerator WaitShake(float duration, float intensity, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (earthquake || intensity == 0 || duration == 0)
            yield return null;

        earthquakeDuration = duration;
        earthquake = true;
        //earthquakeStartTime = Time.time;
        currentEarthquakeIntensity = intensity;
        earthquakeDelta = intensity / duration;
    }
}
