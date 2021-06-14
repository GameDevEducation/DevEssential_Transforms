using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRotation : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public Vector3 RotationSpeed;

    public enum EMethod
    {
        TransformEulerAngles,
        TransformRotate,
        CumulativeEulerAngles
    }

    public EMethod Method = EMethod.TransformEulerAngles;

    Vector3 CumulativeEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        // start at the current rotation
        CumulativeEulerAngles = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Method == EMethod.TransformEulerAngles)
        {
            // directly modify euler angles - can gimbal lock
            transform.eulerAngles += RotationSpeed * Time.deltaTime;
        }
        else if (Method == EMethod.TransformRotate)
        {
            // use the built in rotation method - cannot gimbal lock
            transform.Rotate(RotationSpeed * Time.deltaTime);
        }
        else if (Method == EMethod.CumulativeEulerAngles)
        {
            // build up the euler angles over time and then set - cannot gimbal lock
            CumulativeEulerAngles += RotationSpeed * Time.deltaTime;
            transform.eulerAngles = CumulativeEulerAngles;
        }
    }
}
