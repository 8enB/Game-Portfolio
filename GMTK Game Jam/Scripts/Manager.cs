using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public float waitToLoad = 1;

    public string nextlevel;

    public bool isPaused;

    public int currentProgress = 0;

    public int currentGoal = 1000;


    public int currentWave = 1;

    public int pointsForNextWave = 1000;





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
        PlayerControl.instance.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Spawner.instance.allowedAmmount = (currentWave * 13);
        UI.instance.waveText.text = currentWave.ToString();
        UI.instance.progressSlider.value = currentProgress;
        UI.instance.progressSlider.maxValue = currentGoal;

        if (PlayerControl.instance.currentPoints >= pointsForNextWave && currentWave != 5)
        {
            PlayerControl.instance.health = PlayerControl.instance.maxHealth;
            Spawner.instance.currentAmmount = 0;
            currentProgress = 0;
            currentWave++;
            pointsForNextWave += currentWave * 1000;
            currentGoal = currentWave * 1000;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerControl.instance.canMove = false;

        UI.instance.StartFadeToBlack();



        yield return new WaitForSeconds(waitToLoad);

        PlayerControl.instance.transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene(nextlevel);
        PlayerControl.instance.canMove = true;
        UI.instance.StartFadeOutBlack();

    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UI.instance.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UI.instance.pauseMenu.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }

}