using UnityEngine;

public class RobberBehaviour : MonoBehaviour
{
    private CharacterUtils _characterUtils;
    private CharacterBehaviours _characterBehaviours;

    [SerializeField] private float followPedestrianRange;

    private GameObject _closestCop;
    private GameObject _closestPedestrian;

    void Start()
    {
        _characterBehaviours = this.GetComponent<CharacterBehaviours>();
        _characterUtils = this.GetComponent<CharacterUtils>();
    }

    private void Update()
    {
        _closestPedestrian = _characterUtils.GetClosestPedestrian();

        if (!_closestPedestrian)
        {
            _closestCop = _characterUtils.GetClosestCop();
            _characterBehaviours.Evade(_closestCop);
            return;
        }

        if (Vector3.Distance(_closestPedestrian.transform.position, this.transform.position) < followPedestrianRange)
        {
            _characterBehaviours.Pursue(_closestPedestrian);
        }
        else
        {
            _closestCop = _characterUtils.GetClosestCop();
            _characterBehaviours.Evade(_closestCop);
        }
    }
}
