using System;
using UnityEngine;

public class PedestrianBehaviour : MonoBehaviour
{
    private CharacterUtils _characterUtils;

    private CharacterBehaviours _characterBehaviours;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
