using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviours : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _wanderTarget = Vector3.zero;

    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
    }

    public void Seek(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    public void Flee(Vector3 location)
    {
        Vector3 locationDirection = location - this.transform.position;
        _agent.SetDestination(this.transform.position - locationDirection);
    }

public void Pursue(GameObject target)
{
    if (target == null)
    {
        Debug.LogError("Target is null in Pursue method.");
        return;
    }

    Vector3 targetDiff = target.transform.position - this.transform.position;
    float lookAhead = targetDiff.magnitude / (this._agent.speed + 1); // Use a default speed value if needed

    Seek(target.transform.position + target.transform.forward * lookAhead * 5);
}

   public void Evade(GameObject target)
{
    if (target == null)
    {
        Debug.LogError("Target is null in Evade method.");
        return;
    }

    Vector3 targetDiff = target.transform.position - this.transform.position;
    float lookAhead = targetDiff.magnitude / (this._agent.speed + 1); // Use a default speed value if needed

    Flee(target.transform.position + target.transform.forward * lookAhead * 5);
}
    public void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 5;

        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        _wanderTarget.Normalize();

        _wanderTarget *= wanderRadius;
        Vector3 targetlocal = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.transform.InverseTransformVector(targetlocal);

        Seek(targetWorld);
    }
}