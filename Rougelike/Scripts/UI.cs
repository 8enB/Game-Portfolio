using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    public GameObject deathScreen;
    public string sceneToLoad;

    public Text coinText;
    public Text SGText;
    public Text RifleText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public Image hurtScreen;
    public float hurtSpeed;
    private bool fadeToHurt, fadeOutHurt;
    public GameObject pauseMenu;

    public GameObject manager;

    private void Awake()
    {
        instance=this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack=true;
        fadeToBlack=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed*Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }
        if(fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed*Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if(fadeOutHurt)
        {
            hurtScreen.color = new Color(hurtScreen.color.r, hurtScreen.color.g, hurtScreen.color.b, Mathf.MoveTowards(hurtScreen.color.a, 0f, hurtSpeed*Time.deltaTime));
            if(hurtScreen.color.a == 0f)
            {
                fadeOutHurt = false;
            }
        }
        if(fadeToHurt)
        {
            hurtScreen.color = new Color(hurtScreen.color.r, hurtScreen.color.g, hurtScreen.color.b, 0.5f);
            if(hurtScreen.color.a == 0.5f)
            {
                fadeToHurt = false;
                fadeOutHurt = true;
            }
        }
    }
    public void Hurt()
    {
        fadeToHurt=true;
    }
    public void StartFadeToBlack()
    {
        fadeToBlack=true;
        fadeOutBlack=false;
    }
    public void StartFadeOutBlack()
    {
        fadeToBlack=false;
        fadeOutBlack=true;
    }
    public void BackToMenu()
    {

        SceneManager.LoadScene(sceneToLoad);
        Destroy(manager);
        Destroy(gameObject);
        
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
