using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform handTransform; // The transform representing the character's hand
    public float grabRange = 2f; // The range within which the character can grab objects

    private bool isGrabbing = false;
    private Rigidbody currentGrabbedObject;
    private GameObject item;

    void Update()
    {
        // Check for grab input
        if (Input.GetButtonDown("Fire1"))
        {
            ToggleGrab();
        }
    }

    void OnTriggerStay(Collider target)
    {
        HoldItem(target.gameObject);
        Debug.LogWarning(target.name);
        return;
    }

    void HoldItem(GameObject grabTarget)
    {
        item = grabTarget;
    }

    void ToggleGrab()
    {
        Debug.Log("loop step 1");
        if (!isGrabbing)
        {
            Debug.Log("Loop step 2");
            // Attempt to grab an object
          
                Debug.Log("Loop step 3");
                if (item.GetComponent<Rigidbody>() != null)
                {
                    Debug.Log("Loop step 4");
                    currentGrabbedObject = item.GetComponent<Rigidbody>();
                    currentGrabbedObject.isKinematic = true;
                    currentGrabbedObject.transform.parent = handTransform;
                    currentGrabbedObject.transform.localPosition = Vector3.zero;
                    currentGrabbedObject.transform.localRotation = Quaternion.identity;
                    isGrabbing = true;
                }
            
        }
        else
        {
            Debug.Log("loop else");
            // Release the grabbed object
            if (currentGrabbedObject != null)
            {
                Debug.Log("loop else 2");
                currentGrabbedObject.isKinematic = false;
                currentGrabbedObject.transform.parent = null;
                currentGrabbedObject = null;
                isGrabbing = false;
            }
        }
    }
}

