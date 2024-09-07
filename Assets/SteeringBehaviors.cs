using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;


public class SteeringBehaviors : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Pursue()
    {
        Vector3 targetDiff = target.transform.position - this.transform.position;
        float lookAhead = targetDiff.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        if (target.GetComponent<Drive>().currentSpeed == 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        Seek(target.transform.position + target.transform.forward * lookAhead * 5);
    }

    void Evade()
    {
        Vector3 targetDiff = target.transform.position - this.transform.position;
        float lookAhead = targetDiff.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        if (target.GetComponent<Drive>().currentSpeed == 0.01f)
        {
            Flee(target.transform.position);
            return;
        }
        Flee(target.transform.position + target.transform.forward * lookAhead * 5);
    }
    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 5;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();

        wanderTarget *= wanderRadius;
        Vector3 targetlocal = wanderTarget + new Vector3(0,0,wanderDistance);
        
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetlocal);

        Seek(targetWorld);
    }

    GameObject[] getHidingPlaces()
    {
        GameObject[] hidingSpots;
        hidingSpots = GameObject.FindGameObjectsWithTag("hide");
        return hidingSpots;
    }

    void Hide()
    {
        float closestDistance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        
        GameObject[] hidingSpots = getHidingPlaces();
        for (int i = 0; i < hidingSpots.Length; i++)
        {
            Vector3 hideDirection = hidingSpots[i].transform.position - target.transform.position;
            Vector3 hidePosition = hidingSpots[i].transform.position + hideDirection.normalized * 5;

            if (Vector3.Distance(this.transform.position, hidePosition) < closestDistance)
            {
                chosenSpot = hidePosition;
                closestDistance = Vector3.Distance(this.transform.position, hidePosition);
            }
        }
        Seek(chosenSpot);
    }
    // Update is called once per frame
    void Update()
    {
        //Seek(target.transform.position);
        //Flee(target.transform.position);
        //Pursue();
        //Evade();
        //Wander();
        Hide();
    }
}
