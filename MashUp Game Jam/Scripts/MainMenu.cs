using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string level1;
    public string level2;
    public string level3;
    public string level4;
    public string level5;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Start1()
    {
        SceneManager.LoadScene(level1);
    }
    public void Start2()
    {
        SceneManager.LoadScene(level2);
    }
    public void Start3()
    {
        SceneManager.LoadScene(level3);
    }
    public void Start4()
    {
        SceneManager.LoadScene(level4);
    }
    public void Start5()
    {
        SceneManager.LoadScene(level5);
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}
