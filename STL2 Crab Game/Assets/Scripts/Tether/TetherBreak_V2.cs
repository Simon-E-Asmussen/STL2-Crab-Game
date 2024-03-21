using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    private void Awake()
    {
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

                //jointLeft.useSpring = true;

                boneRigidBody[i].GetComponent<HingeJoint>().connectedBody = charLeft.GetComponent<Rigidbody>();
            } 
            else if (i == boneCount - 1)
            {
                jointRight = boneRigidBody[i].GetComponent<HingeJoint>();

                HingeJoint[] joints = boneRigidBody[i].GetComponents<HingeJoint>();
                joints[0].connectedBody = boneRigidBody[i - 1];
                joints[1].connectedBody  = charRight.GetComponent<Rigidbody>();
            }
            else boneRigidBody[i].GetComponent<HingeJoint>().connectedBody = boneRigidBody[i-1];
        }
      

    }


    void Start()
    {
        float dist = Vector3.Distance(charLeft.transform.position, charRight.transform.position);
        Debug.Log("Distance is: " + dist);
    }


    void Update()
    {
        float dist = Vector3.Distance(charLeft.transform.position, charRight.transform.position);
        if (dist >= 4)
        {
            Debug.Log("Distance is: " + dist);
        }

        if (dist >= 5.5)
        {
            jointLeft.connectedBody = null;
            jointRight.connectedBody = null;
            Destroy(jointLeft);
            Destroy(jointRight);
            foreach (Rigidbody itr in boneRigidBody) AddMass(itr);
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
