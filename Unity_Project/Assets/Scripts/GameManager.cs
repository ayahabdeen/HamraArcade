using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject heart;
    public Slider negativeSlider;
    public Slider positiveSlider;
    public bool isHamra = false;
    public bool isAragoz = false;
    public GameObject HamraWinning;
    public GameObject AragozWinning;
    public GameObject Hamra;
    public GameObject Aragoz;
    public bool gameIsPaused;

    public void Start() 
    {
        ResetGame();
        Hamra = GameObject.FindGameObjectWithTag("Hamra");
        Aragoz = GameObject.FindGameObjectWithTag("aragoz");
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            LoadNextScene();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        /*if (Vector3.Distance(Hamra.transform.position, Aragoz.transform.position) < 1)
        {
            Debug.Log("Less than one.");
        }
        else
        {
            Debug.Log("Greater than one.");
        }*/
    }
    public void SetSliderValue()
    {
        if (isAragoz == true)
        {
            hamraHealth();
            if (Hamra.GetComponent<HamraController>().lives > 8)
            {
                gameIsPaused = true;
                HamraWinning.SetActive(true);
            }
            isAragoz = false;
        }

        else if (isHamra == true)
        {
            aragozHealth();
            if (Aragoz.GetComponent<AragozController>().lives > 8)
            {
                gameIsPaused = true;
                AragozWinning.SetActive(true);
            }
            isHamra = false;
        }       
    }

    public void hamraHealth()
    {
        GameObject hamraLives;
        int index1 = Hamra.GetComponent<HamraController>().lives;
        hamraLives = Instantiate(heart, new Vector2(-900 , 385 - 40 * index1), Quaternion.identity);
        hamraLives.GetComponent<Image>().color = new Color (255, 0, 0, 255);
        GameObject canvas = GameObject.FindGameObjectWithTag("UI");
        hamraLives.transform.SetParent(canvas.transform, false);
        Hamra.GetComponent<HamraController>().lives++;
        Aragoz.GetComponent<AragozController>().AragozCry();
        Debug.Log("hamra hit aragoz" + Hamra.GetComponent<HamraController>().lives);
    }

    public void aragozHealth()
    {
        GameObject AragozLives;
        int index2 = Aragoz.GetComponent<AragozController>().lives;
        AragozLives = Instantiate(heart, new Vector2(900, 385 - 40 * index2), Quaternion.identity);
        AragozLives.GetComponent<Image>().color = new Color(0, 202, 255);
        GameObject canvas = GameObject.FindGameObjectWithTag("UI");
        AragozLives.transform.SetParent(canvas.transform, false);
        Aragoz.GetComponent<AragozController>().lives++;
        Hamra.GetComponent<HamraController>().HamraCry();
        Debug.Log("Aragoz hit Hamra" + Aragoz.GetComponent<AragozController>().lives);
    }

    public void ResetGame()
    {
        isHamra = false;
        isAragoz = false;
        HamraWinning.SetActive(false);
        AragozWinning.SetActive(false);
        gameIsPaused = false;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

}
