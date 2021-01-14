using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 1;

    public string nextlevel;

    public bool isPaused;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1f;
        PlayerControl.instance.canMove = true;
        PlayerControl.instance.transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerControl.instance.canMove = false;

        UI.instance.StartFadeToBlack();

        

        yield return new WaitForSeconds(waitToLoad);

        PlayerControl.instance.transform.position = new Vector3(0,0,0);
        SceneManager.LoadScene(nextlevel);
        PlayerControl.instance.canMove = true;
        UI.instance.StartFadeOutBlack();
        
    }

    public void PauseUnpause()
    {
        if(!isPaused)
        {
            UI.instance.pauseMenu.SetActive(true);
            isPaused=true;

            Time.timeScale = 0f;
        }
        else
        {
            UI.instance.pauseMenu.SetActive(false);
            isPaused=false;

            Time.timeScale = 1f;
        }
    }

}