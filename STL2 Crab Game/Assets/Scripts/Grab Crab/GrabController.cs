using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform handTransform; // The transform representing the character's hand
    public float grabRange = 2f; // The range within which the character can grab objects

    private bool isGrabbing = false;
    private Rigidbody currentGrabbedObject;

    void Update()
    {
        // Check for grab input
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Input Got");
            ToggleGrab();
        }
    }

    void ToggleGrab()
    {
        if (!isGrabbing)
        {
            Debug.Log("loop stage 1");
            // Attempt to grab an object
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
            { 
                Debug.Log("Loop Stage 2");
                if (hit.collider.GetComponent<Rigidbody>() != null)
                {
                    Debug.Log("Loop stage 3");
                    currentGrabbedObject = hit.collider.GetComponent<Rigidbody>();
                    currentGrabbedObject.isKinematic = true;
                    currentGrabbedObject.transform.parent = handTransform;
                    currentGrabbedObject.transform.localPosition = Vector3.zero;
                    currentGrabbedObject.transform.localRotation = Quaternion.identity;
                    isGrabbing = true;
                }
            }
        }
        else
        {
            Debug.Log("loop else");
            // Release the grabbed object
            if (currentGrabbedObject != null)
            {
                currentGrabbedObject.isKinematic = false;
                currentGrabbedObject.transform.parent = null;
                currentGrabbedObject = null;
                isGrabbing = false;
            }
        }
    }
}

