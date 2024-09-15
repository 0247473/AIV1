using UnityEngine;

public class CharacterUtils: MonoBehaviour
{
    
    /**
     * Given an array of GameObject it returns the gameObject with the
     * shortest distance. If the GameObject array is empty, returns null
     */
    public GameObject GetClosestGameObject(GameObject[] gameObjects)
    {
        float minDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject currentObj in gameObjects)
        {
            float distance = Vector3.Distance(this.transform.position, currentObj.transform.position);

            if (distance < minDistance)
            {
                closestObject = currentObj;
                minDistance = distance;
            }
        }

        return closestObject; 
    }

    /**
     * Returns the Closest Cop, null if there is no cops
     */
    public GameObject GetClosestCop()
    {
        GameObject[] cops = GameObject.FindGameObjectsWithTag("Cop");

        return GetClosestGameObject(cops);
    }
    
    /**
     * Returns the Closest Pedestrian, null if there is no cops
     */
    public GameObject GetClosestPedestrian()
    {
        GameObject[] cops = GameObject.FindGameObjectsWithTag("Pedestrians");

        return GetClosestGameObject(cops);
    }
    
    /**
     * Returns the Closest Robber, null if there is no cops
     */
    public GameObject GetClosestRobber()
    {
        GameObject[] cops = GameObject.FindGameObjectsWithTag("Robber");

        return GetClosestGameObject(cops);
    }
}
