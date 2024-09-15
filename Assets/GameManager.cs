using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEditor.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI conter;
    private int _robbersAlive = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        _robbersAlive = GameObject.FindGameObjectsWithTag("Robber").Length;
        conter.text = _robbersAlive.ToString();
    }

    
    /**
     * It will update the score and show it on display
     */
    public void RobberCaught()
    {
        _robbersAlive -= 1;
        conter.text = _robbersAlive.ToString();
        
        if (_robbersAlive == 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
