using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviours : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _wanderTarget = Vector3.zero;

    private CharacterUtils _characterUtils;

    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        try
        {
            _characterUtils = this.GetComponent<CharacterUtils>();
        }
        catch
        {
            _characterUtils = null;
        }
    }
    
    /**
     * Will set the agent destination towards the desired destiantion
     */
    private void Seek(Vector3 destination)
    {
        this._agent.SetDestination(destination);
    }
    
    /***
     * Will set the agent destination to the opposite direction from the location;
     */
    private void Flee(Vector3 location)
    {
        Vector3 locationDirection = location - this.transform.position;
        this._agent.SetDestination(this.transform.position - locationDirection);
    }
    
    /**
     * Will predict the next step of the target and seek it.
     */
    public void Pursue(GameObject target)
    {
        Vector3 targetDiff = target.transform.position - this.transform.position;
        float lookAhead = targetDiff.magnitude / (this._agent.speed + target.GetComponent<Drive>().currentSpeed);
        if (target.GetComponent<Drive>().currentSpeed == 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        Seek(target.transform.position + target.transform.forward * lookAhead * 5);
    }
    
    /**
     * Will predict the next step of the target and flee it
     */
    public void Evade(GameObject target)
    {
        Vector3 targetDiff = target.transform.position - this.transform.position;
        float lookAhead = 1f;
        float currentSpeed = 0f; 
        
        // tries, to get the speed from the NavMesh or the Drive if is the player
        try
        {
            currentSpeed = target.GetComponent<NavMeshAgent>().speed;
        }
        catch
        {
            currentSpeed = target.GetComponent<Drive>().currentSpeed;
        }
        finally
        {
            lookAhead = targetDiff.magnitude / (this._agent.speed + currentSpeed);
            
        }
        if (currentSpeed <= 0.01f)
        {
            Flee(target.transform.position);
            return;
        }
        Flee(target.transform.position + target.transform.forward * (lookAhead * 5));
    }
    
    /**
     * Wander in a random direction but naturally
     */
    public void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 5;

        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        _wanderTarget.Normalize();

        _wanderTarget *= wanderRadius;
        Vector3 targetlocal = _wanderTarget + new Vector3(0,0,wanderDistance);
        
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetlocal);

        Seek(targetWorld);
    }
    
    /**
     * Will go to the closest hiding spot
     */
    public void HideFrom(GameObject target)
    {
        GameObject[] hidingSpots = GameObject.FindGameObjectsWithTag("hide");
        Vector3 chosenSpot = Vector3.zero;

        if (hidingSpots.Length < 1)
        {
            return;
        }

        float closestDistance = Mathf.Infinity;
        
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
    
    
}
