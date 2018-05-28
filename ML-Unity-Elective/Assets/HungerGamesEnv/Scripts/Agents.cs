using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agents : Agent {

    public Transform Target;

    public float moveSpeed = 3;
    //PlayerController controller;

    Rigidbody rBody;
    string objectName;

    bool targetLocked = false;
    string shotAgentname;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        objectName = gameObject.name;
    }

    private void Update()
    {
        
        // Start + Direction
        Ray ray = new Ray(rBody.transform.position, rBody.transform.forward);

        // Will be filled with Infos
        RaycastHit hitInfo;

        // IF & Fill  (third = max distance)
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (Target.gameObject.name == hitInfo.collider.gameObject.name)
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
                //print(hitInfo.collider.gameObject.name);
                //print(Target.gameObject.name);
                //print(hitInfo.distance);
                targetLocked = true;
                shotAgentname = hitInfo.collider.gameObject.name;
            }
            else
            {
                targetLocked = false;
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.red);
            }
        }
 
    }

    public override void CollectObservations()
    {
        // Observations
        AddVectorObs(rBody.position);

        // Observe Distances to Walls / Ray Casts


    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions
        //if(vectorAction[0] == 1)
        //{
        //    print("Up!");
        //}

        //if (vectorAction[1] == 1)
        //{
        //    print("Left!");
        //}

        //if (vectorAction[2] == 1)
        //{
        //    print("Down!");
        //}

        //if (vectorAction[3] == 1)
        //{
        //    print("Right!");
        //}

        // Shooting
        if (vectorAction[4] == 1)
        {
            
            // SHOT!
            if(targetLocked)
            {
                //print(objectName);
                //print(shotAgent);
                if(objectName == shotAgentname){
                    AddReward(1.0f);
                }else{
                    print("Winner:");
                    print(shotAgentname);
                    AddReward(-1.0f);
                }
                Done();

            }
        }

        // Reward for Finding
        if (targetLocked)
        {
            AddReward(0.1f);
        }

        // Movement
        if(objectName == "Agent01"){
            // Alternate Way of Movement Control
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal_P1"), 0, Input.GetAxis("Vertical_P1"));
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;
            rBody.MovePosition(rBody.position + moveVelocity * Time.fixedDeltaTime); 
        }else{
            // Alternate Way of Movement Control
            Vector3 moveInput2 = new Vector3(Input.GetAxis("Horizontal_P2"), 0, Input.GetAxis("Vertical_P2"));
            Vector3 moveVelocity2 = moveInput2.normalized * moveSpeed;
            rBody.MovePosition(rBody.position + moveVelocity2 * Time.fixedDeltaTime);   
        }




    }

}
