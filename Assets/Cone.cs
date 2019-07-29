using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    [SerializeField] private float xLimit = 5f,
                                   zLimit = 5f;

	void Update ()
    {
        ConstrainMovement();
    }

    void ConstrainMovement()
    {
        Vector3 constrainedPosition = transform.position;

        constrainedPosition.x = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        constrainedPosition.z = Mathf.Clamp(transform.position.z, -zLimit, zLimit);

        transform.position = constrainedPosition;
    }
}
