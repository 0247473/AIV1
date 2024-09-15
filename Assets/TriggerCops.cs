using UnityEngine;

public class TriggerCops: MonoBehaviour
{

    public GameManager manger; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robber"))
        {
            Destroy(other.gameObject); //Destroys the gameObject before updating the score
            manger.RobberCaught();
        }
    }
}
