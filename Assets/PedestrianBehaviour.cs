using System;
using UnityEngine;

public class PedestrianBehaviour : MonoBehaviour
{
    private CharacterUtils _characterUtils;
    private CharacterBehaviours _characterBehaviours;

    [SerializeField] private float fleeFromThiefRange;
    
    private GameObject _closestThief;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            _characterBehaviours = this.GetComponent<CharacterBehaviours>();
            _characterUtils = this.GetComponent<CharacterUtils>();
        }
        catch
        {
            throw new Exception("Missing components Character Behaviours and Characters Utils");
        }
    }

    private void Update()
    {
        _closestThief = _characterUtils.GetClosestRobber();
        
        // If there is no thief, wander by default
        if (!_closestThief)
        {
            _characterBehaviours.Wander();
            return;
        }

        // If the thief is within the flee range, flee
        float distanceFromThief = Vector3.Distance(_closestThief.transform.position, this.transform.position);
        
        if (distanceFromThief < fleeFromThiefRange)
            _characterBehaviours.Flee(_closestThief.transform.position);
        else
            _characterBehaviours.Wander();
    }
    
    //Pedestrian Capture
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robber"))
        {
            Destroy(this.gameObject);
        }
    }
}