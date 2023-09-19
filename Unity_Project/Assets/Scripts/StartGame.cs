using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            SceneManager.LoadScene("Hamra", LoadSceneMode.Single);
        } 
            
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Hamra", LoadSceneMode.Single);
    }
    
}
