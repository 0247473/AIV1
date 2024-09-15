using System;
using UnityEngine;

public class CopBehaviour : MonoBehaviour
{
    private CharacterUtils _characterUtils;
    private CharacterBehaviours _characterBehaviours;

    [SerializeField] private float pursueRobberRange;
    
    private GameObject _closestRobber;

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
        _closestRobber = _characterUtils.GetClosestRobber();
        
        //if there's no robbers, then the game is over.
        if (!_closestRobber) return;


        float distanceFromRobber = Vector3.Distance(_closestRobber.transform.position, this.transform.position);
        
        //it will start pursuing the closest robber if is in the range
        if (distanceFromRobber < pursueRobberRange)
        {
            _characterBehaviours.Pursue(_closestRobber);
        }
        else
        {
            _characterBehaviours.Wander();
        }
    }
}
