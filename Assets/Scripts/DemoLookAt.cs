using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLookAt : MonoBehaviour
{
    public Transform Target;

    public enum EMethod
    {
        SimpleVectorMath,
        TransformLookAt,
        QuaternionLookAt,
        QuaternionSmoothLookAt
    }

    public EMethod Method = EMethod.SimpleVectorMath;
    public float RotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Method == EMethod.SimpleVectorMath)
        {
            // change our forward to point at the target
            transform.forward = (Target.position - transform.position).normalized;
        }
        else if (Method == EMethod.TransformLookAt)
        {
            // use the built in look at method
            transform.LookAt(Target, Vector3.up);
        }
        else if (Method == EMethod.QuaternionLookAt)
        {
            // use the quaternion look rotation
            transform.rotation = Quaternion.LookRotation((Target.position - transform.position).normalized, Vector3.up);
        }
        else if (Method == EMethod.QuaternionSmoothLookAt)
        {
            // work out desired look rotation
            Quaternion desiredRotation = Quaternion.LookRotation((Target.position - transform.position).normalized, Vector3.up);

            // rotate towards the desired rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, RotationSpeed * Time.deltaTime);
        }
    }
}
