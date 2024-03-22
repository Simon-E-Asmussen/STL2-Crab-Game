using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TetherBreak_V2 : MonoBehaviour
{
    private string tetherMiddle_Name = "Tether_Middle";
    public GameObject charLeft;
    public GameObject charRight;

    // Left/Right attactment hinge joins
    public HingeJoint jointLeft;
    public HingeJoint jointRight;

    // Bones rigid bodies
    private int boneCount = 11;
    public List<Rigidbody> boneRigidBody = new();

    private List<HingeJoint> hingeJoint = new();
    private Renderer rend;

    private float maxDist = 5.5f;



    private void Awake()
    {
        rend = FindChildWithName(transform, "Cylinder").GetComponent<Renderer>();

        charLeft = GameObject.FindWithTag("LeftPart");
        charRight = GameObject.FindWithTag("RightPart");


        for (int i = 0; i < boneCount; i++)
        {
            string str = "Bone.";
            string numStr = i.ToString();
            while (numStr.Length < 3) numStr = '0' + numStr;
            str += numStr;
            boneRigidBody.Add(FindChildWithName(transform, str).GetComponent<Rigidbody>());

            if (i == 0)
            {
                jointLeft = boneRigidBody[i].GetComponent<HingeJoint>();
                boneRigidBody[i].GetComponent<HingeJoint>().connectedBody = charLeft.GetComponent<Rigidbody>();
                //
                hingeJoint.Add(jointLeft);
            }
            else if (i == boneCount - 1)
            {
                jointRight = boneRigidBody[i].GetComponent<HingeJoint>();

                HingeJoint[] joints = boneRigidBody[i].GetComponents<HingeJoint>();
                joints[0].connectedBody = boneRigidBody[i - 1];
                joints[1].connectedBody = charRight.GetComponent<Rigidbody>();
                //
                foreach (HingeJoint itr in joints) hingeJoint.Add(itr);
            }
            else
            {
                HingeJoint hj = boneRigidBody[i].GetComponent<HingeJoint>();
                hj.connectedBody = boneRigidBody[i - 1];
                //
                hingeJoint.Add(hj);
            }
        }

        foreach (Rigidbody itr in boneRigidBody) itr.mass = 0;
    }


    void Start()
    {
        float dist = Vector3.Distance(charLeft.transform.position, charRight.transform.position);
        Debug.Log("Distance is: " + dist);
    }


    void Update()
    {
        float dist = Vector3.Distance(charLeft.transform.position, charRight.transform.position);
        
        //if (dist >= 4) Debug.Log("Distance is: " + dist);
        

        if (dist >= maxDist)
        {
            jointLeft.connectedBody = null;
            jointRight.connectedBody = null;
            Destroy(jointLeft);
            Destroy(jointRight);
            foreach (Rigidbody itr in boneRigidBody) AddMass(itr);
        }

        //foreach (HingeJoint itr in hingeJoint) StretchControl(itr, 20, 0, 0);
        Debug.Log("Distance: " + dist);
        ChangeColor(dist);
    }

    void ChangeColor(float dist)
    {
        float col = 1;
        if (dist < maxDist) col = dist / maxDist;

        rend.material.color = new Color(col, 0, 0, 1);
    } 


    void StretchControl(HingeJoint hingeJoint, float maxStretch, float springForce, float damper)
    {
        hingeJoint.useSpring = true;
        
        float distance = Vector3.Distance(hingeJoint.connectedBody.transform.position, transform.position);

        // If the distance exceeds the maximum stretch, apply spring force
        if (distance < maxStretch)
        {
            JointSpring jointSpring = new JointSpring
            {
                spring = springForce,
                damper = damper
            };
            hingeJoint.spring = jointSpring;
        }
        else
        {
            // If the distance is within the maximum stretch, reset the spring
            JointSpring jointSpring = new JointSpring();
            jointSpring.spring = 0;
            hingeJoint.spring = jointSpring;
        }
    }


    GameObject FindChildWithName(Transform transform, string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
            else
            {
                GameObject child_child = FindChildWithName(child, name);
                if (child_child)
                    return child_child;
            }
        }

        return null;
    }


    private void AddMass(Rigidbody rb)
    {
        rb.mass = 1;
        rb.useGravity = true;
    }
}
