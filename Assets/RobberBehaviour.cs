using UnityEngine;
using System;
using UnityEditor;

public class RobberBehaviour : MonoBehaviour
{
    private CharacterUtils _characterUtils;
    private CharacterBehaviours _characterBehaviours;


    [SerializeField] private float followPedestrianRange;

    private GameObject _closestCop;
    private GameObject _closestPedestrian;
    
    void Start()
    {
        
        try
        {
            _characterBehaviours = this.GetComponent<CharacterBehaviours>();
            _characterUtils = this.GetComponent<CharacterUtils>();
        }
        catch (Exception exception)
        {
            throw new Exception("Missing components Character Behaviours and Characters Utils");
        }
    }

    private void Update()
    {
        _closestPedestrian = _characterUtils.GetClosestPedestrian();
        
        //if there is not pedestrians, it will automatically evade the closest cop; 
        if (!_closestPedestrian)
        {
            _closestCop = _characterUtils.GetClosestCop();
            _characterBehaviours.Evade(_closestCop);
            return; 
        }
        
        //it will start pursuing the pedestrian if is close enough
        if (Vector3.Distance(_closestPedestrian.transform.position, this.transform.position) < followPedestrianRange)
        {
            _characterBehaviours.Pursue(_closestPedestrian);
        }else
        {
            _closestCop = _characterUtils.GetClosestCop();
            _characterBehaviours.Evade(_closestCop);
        }
    }
}
