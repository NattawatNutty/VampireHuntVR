using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

   //public Camera cam;
   // public NavMeshAgent agent;



    //void Update() {
    //    if (Input.GetMouseButtonDown(0)) {
    //        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit)) {
    //            move our agent
    //            agent.SetDestination(hit.point);
    //        }

    //    }

    //    if (Input.GetMouseButtonDown(0)) {
    //        agent.SetDestination(Input.mousePosition);
    //    }
    //}



    // NavMesh - set destination object
    [SerializeField]
    Transform des;
    NavMeshAgent navMeshAgent;

    //  Use this for initialization

    void Start() {

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null) {
            Debug.Log("Error " + gameObject.name);
        } else {
            SetDestination();
        }
    }

    private void SetDestination() {
        if (des != null) {
            Vector3 targetVector = des.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }

    }

}
