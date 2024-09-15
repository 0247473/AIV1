using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TriggerCops: MonoBehaviour
{
    public TextMeshProUGUI conter;
    public int alive = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robber"))
        {
            // Destroy the triggered object
            Destroy(other.gameObject);
            alive--;
            conter.text = Convert.ToString(alive);
            // Check if we've destroyed enough objects
            if (alive == 0)
            {
                // Change to the next scene
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
