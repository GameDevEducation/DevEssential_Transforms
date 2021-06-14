using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekTarget : MonoBehaviour
{
    public Transform Target;

    public float MovementSpeed = 1f;
    public float RotationSpeed = 30f;
    public float ArrivedThreshold = 0.1f;

    public enum EMethod
    {
        SimpleTransformPosition,
        UsingMoveTowards,
        UsingDirection,
        UsingSmoothedRotation
    }

    public EMethod Method = EMethod.SimpleTransformPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Method == EMethod.SimpleTransformPosition)
        {
            // just move based on direction vector
            transform.position += (Target.position - transform.position).normalized * MovementSpeed * Time.deltaTime;
        }
        else if (Method == EMethod.UsingMoveTowards)
        {
            // move directly towards the target using MoveTowards
            transform.position = Vector3.MoveTowards(transform.position, Target.position, MovementSpeed * Time.deltaTime);
        }
        else if (Method == EMethod.UsingDirection)
        {
            // turn to face the target
            transform.LookAt(Target, Vector3.up);

            // move directly towards the target
            transform.position = Vector3.MoveTowards(transform.position, Target.position, MovementSpeed * Time.deltaTime);
        }
        else if (Method == EMethod.UsingSmoothedRotation)
        {
            // if we're farther than ArrivedThreshold away then use smoothed movement - otherwise move directly
            if (Vector3.Distance(transform.position, Target.position) > ArrivedThreshold)           
            {
                // work out desired look rotation
                Quaternion desiredRotation = Quaternion.LookRotation((Target.position - transform.position).normalized, Vector3.up);

                // rotate towards the desired rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, RotationSpeed * Time.deltaTime);

                // move based upon how we are facing
                transform.position += transform.forward * MovementSpeed * Time.deltaTime;
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, Target.position, MovementSpeed * Time.deltaTime);
        }
    }
}
