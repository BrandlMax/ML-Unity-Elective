using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainingsbuddy_Agents : Agent
{

    // GLOBAL STUFF
    public Transform Target;
    public GameObject Gun;

    public Material PlayerMaterial;
    public Material GunFiredMaterial;
    Material GunMaterial;
    public Material HitMaterial;
    Material BodyMaterial;
    Material TargetMaterial;

    public float moveSpeed = 1;

    Rigidbody rBody;
    RayPerception rayPer;

    string objectName;
    bool targetLocked = false;
    GameObject shotAgent;
    bool Peng = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rayPer = GetComponent<RayPerception>();
        objectName = gameObject.name;
        GunMaterial = Gun.GetComponent<Renderer>().material;
        BodyMaterial = gameObject.GetComponent<Renderer>().material;
        TargetMaterial = Target.GetComponent<Renderer>().material;
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
                //print("TARGET LOCKED");
                if (Peng)
                {
                    Gun.GetComponent<Renderer>().material = GunFiredMaterial;
                    Debug.DrawLine(ray.origin, hitInfo.point, Color.yellow);
                }
                else
                {
                    Gun.GetComponent<Renderer>().material = GunMaterial;
                    Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
                }

                //print(hitInfo.collider.gameObject.name);
                //print(Target.gameObject.name);
                //print(hitInfo.distance);
                targetLocked = true;
                shotAgent = hitInfo.collider.gameObject;
            }
            else
            {
                targetLocked = false;
                if (Peng)
                {
                    Gun.GetComponent<Renderer>().material = GunFiredMaterial;
                    Debug.DrawLine(ray.origin, hitInfo.point, Color.yellow);
                }
                else
                {
                    Gun.GetComponent<Renderer>().material = GunMaterial;
                    Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                }
            }

        }

    }


    public override void CollectObservations()

    {
        // Observation current position
        AddVectorObs(rBody.position);
        AddVectorObs(rBody.transform.rotation);

        // Observe Distances to Walls & Tagets via Ray Casts
        //float rayDistance = 20f;
        //float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f };
        //string[] detectableObjects;
        //detectableObjects = new string[] { "ArenaWall", "PlayerTarget" };
        //AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        //AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1f, 0f));

        // Observe Distance to Target
        Vector3 relativePosition = Target.position - this.transform.position;
        AddVectorObs(relativePosition.x);
        AddVectorObs(relativePosition.z);

        // Observe Target Position
        AddVectorObs(Target.position);
        AddVectorObs(Target.transform.rotation);
    }


    public void MoveAgent(float[] act)
    {
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(act[0]);
        Peng = false;
        switch (action)
        {
            case 0:
                dirToGo = transform.forward * 1f;
                break;
            case 1:
                dirToGo = transform.forward * -1f;
                break;
            case 2:
                rotateDir = transform.up * 1f;
                break;
            case 3:
                rotateDir = transform.up * -1f;
                break;
            case 4:
                dirToGo = transform.right * -0.75f;
                break;
            case 5:
                dirToGo = transform.right * 0.75f;
                break;
            case 6:
                //print("Shoooting from:");
                //print(gameObject.name);

                // SHOT!
                Peng = true;
                if (targetLocked)
                {
                    shotAgent.GetComponent<Renderer>().material = HitMaterial;
                    //print("Shot Agent:");
                    //print(shotAgent.name);
                    //print("Winner:");
                    //print(gameObject.name);
                    //Done();
                }
                break;
            case 7:
                break;
        }

        transform.Rotate(rotateDir, Time.deltaTime * 100f);
        rBody.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
    }


    public override void AgentAction(float[] vectorAction, string textAction)
    {

        // Monitoring
        Monitor.Log("Reward", GetCumulativeReward(), MonitorType.text, this.transform);
        // print(GetReward());
        // print(GetCumulativeReward());


        MoveAgent(vectorAction);

        // Reward for shooting the target
        if (Peng && targetLocked)
        {
            AddReward(1.0f);
            print("TB");
            print(GetCumulativeReward());
            Done();
        }

        // Reward for finding Target
        if (targetLocked)
        {
            AddReward(0.001f);
        }

        // Penalty for every shot
        if (Peng)
        {
            AddReward(-0.001f);
        }

        // When the Agent get hit
        if (this.GetComponent<Renderer>().material.name == "Hit (Instance)")
        {
            print("TB Dead");
            AddReward(-1.0f);
            print(GetCumulativeReward());
            Done();
        }

        // Time Penalty
        AddReward(-0.001f);

        // EndTime Penalty
        // if(GetStepCount() == 5000){
        //   AddReward(-100.0f);
        // };

    }



    public override void AgentReset()
    {

        rBody.position = new Vector3(Random.value * 8 - 4,
                                          0.5f,
                                          Random.value * 8 - 4);
        Target.position = new Vector3(Random.value * 8 - 4,
                                          0.5f,
                                          Random.value * 8 - 4);
        gameObject.GetComponent<Renderer>().material = PlayerMaterial;
        Target.GetComponent<Renderer>().material = TargetMaterial;
    }

}
