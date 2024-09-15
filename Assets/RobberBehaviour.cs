using System;
using UnityEngine;

public class RobberBehaviour : MonoBehaviour
{
    private CharacterUtils _characterUtils;
    private CharacterBehaviours _characterBehaviours;

    [SerializeField] private float followPedestrianRange;
    [SerializeField] private float fleeCopRange;
    [SerializeField] private GameManager _manager;

    private GameObject _closestCop;
    private GameObject _closestPedestrian;

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
        
        if (followPedestrianRange < 10) followPedestrianRange = 10; //minimum pedestrian range
        if (fleeCopRange < 5) fleeCopRange = 5;                     //minimum flee range
    }

    private void Update()
    {
        _closestPedestrian = _characterUtils.GetClosestPedestrian();    //can be a GameObject or null
        _closestCop = _characterUtils.GetClosestCop();                  //always be different from null the (player is a cop)
        
        //if there are pedestrians left
        if (_closestPedestrian)
        {
            float distanceFromPedestrian = Vector3.Distance(_closestPedestrian.transform.position, this.transform.position);
            
            if(distanceFromPedestrian < followPedestrianRange) _characterBehaviours.Pursue(_closestPedestrian);
            else _characterBehaviours.Evade(_closestCop);
        }
        else _characterBehaviours.Evade(_closestCop);

        
        float distanceFromCop = Vector3.Distance(_closestCop.transform.position, this.transform.position);

        //Detects if is in the Flee range
        if (distanceFromCop < fleeCopRange)
        {
            _characterBehaviours.Flee(_closestCop.transform.position);
        }
        
        //Robbers Capture is managed in the TriggerCops.cs script
    }
}
