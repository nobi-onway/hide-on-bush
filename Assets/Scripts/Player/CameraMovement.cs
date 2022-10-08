using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform lookAt;

    [SerializeField] private float boundX = 0.15f;
    [SerializeField] private float boundY = 0.05f;

    private void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;
        //check if inside bound on X axis
        if (deltaX > boundX || deltaX < boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }
        if (deltaY > boundY || deltaY < boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }

    public void ResetTarget()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}
