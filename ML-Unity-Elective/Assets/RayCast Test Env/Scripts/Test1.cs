using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        // RAYCAST
        // Start + Direction
        Ray ray = new Ray(transform.position, transform.forward);

        // Will be filled with Infos
        RaycastHit hitInfo;

        // IF & Fill  (third = max distance)
        if(Physics.Raycast(ray, out hitInfo, 100)){
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            print(hitInfo.collider.gameObject.name);
            print(hitInfo.distance);
        } else{
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }


		
	}
}
