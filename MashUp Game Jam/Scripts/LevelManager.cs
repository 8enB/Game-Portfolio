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

    public int currentStage = 1;

    public GameObject camera;
    public float shiftSpeed;

    public List<GameObject> enemiesStage1 = new List<GameObject>();
    public List<GameObject> enemiesStage2 = new List<GameObject>();
    public List<GameObject> enemiesStage3 = new List<GameObject>();
    public List<GameObject> enemiesStage4 = new List<GameObject>();
    public List<GameObject> enemiesStage5 = new List<GameObject>();

    public Transform stage1;
    public Transform stage2;
    public Transform stage3;
    public Transform stage4;
    public Transform stage5;

    public bool loadNext = false;
    public bool bossDed = false;

    public bool destroyBullets = false;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PlayerControl.instance.canMove = true;
        //PlayerControl.instance.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
        if (currentStage == 1)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, stage1.position, shiftSpeed * Time.deltaTime);
            if(camera.transform.position != stage1.position)
            {
                destroyBullets = true;
            }
            else
            {
                destroyBullets = false;
            }
            for (int i = 0; i < enemiesStage1.Count; i++)
            {
                if (enemiesStage1[i] == null)
                {
                    enemiesStage1.RemoveAt(i);
                    i--;
                }
            }
            if (enemiesStage1.Count == 0)
            {
                currentStage++;
            }
        }

        if (currentStage == 2)
        {
            if (camera.transform.position != stage2.position)
            {
                destroyBullets = true;
            }
            else
            {
                destroyBullets = false;
            }
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, stage2.position, shiftSpeed * Time.deltaTime);
            for (int i = 0; i < enemiesStage2.Count; i++)
            {
                if (enemiesStage2[i] == null)
                {
                    enemiesStage2.RemoveAt(i);
                    i--;
                }
            }
            if (enemiesStage2.Count == 0)
            {
                currentStage++;
            }
        }

        if (currentStage == 3)
        {
            if (camera.transform.position != stage3.position)
            {
                destroyBullets = true;
            }
            else
            {
                destroyBullets = false;
            }
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, stage3.position, shiftSpeed * Time.deltaTime);
            for (int i = 0; i < enemiesStage3.Count; i++)
            {
                if (enemiesStage3[i] == null)
                {
                    enemiesStage3.RemoveAt(i);
                    i--;
                }
            }
            if (enemiesStage3.Count == 0)
            {
                currentStage++;
            }
        }

        if (currentStage == 4)
        {
            if (camera.transform.position != stage4.position)
            {
                destroyBullets = true;
            }
            else
            {
                destroyBullets = false;
            }
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, stage4.position, shiftSpeed * Time.deltaTime);
            for (int i = 0; i < enemiesStage4.Count; i++)
            {
                if (enemiesStage4[i] == null)
                {
                    enemiesStage4.RemoveAt(i);
                    i--;
                }
            }
            if (enemiesStage4.Count == 0)
            {
                currentStage++;
            }
        }

        if (currentStage == 5)
        {
            if (camera.transform.position != stage5.position)
            {
                destroyBullets = true;
            }
            else
            {
                destroyBullets = false;
            }
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, stage5.position, shiftSpeed * Time.deltaTime);
            for (int i = 0; i < enemiesStage5.Count; i++)
            {
                if (enemiesStage5[i] == null)
                {
                    enemiesStage5.RemoveAt(i);
                    i--;
                }
            }
            if (enemiesStage5.Count == 0)
            {
                StartCoroutine(LevelEnd());
            }
            
        }
        if (bossDed)
        {
            destroyBullets = true;
            StartCoroutine(LevelEnd());
        }


    }

    public IEnumerator LevelEnd()
    {
        loadNext = true;
        // PlayerControl.instance.canMove = false;

        UI.instance.fadeToWin = true;
        UI.instance.StartFadeToBlack();



        yield return new WaitForSeconds(waitToLoad);

        //PlayerControl.instance.transform.position = new Vector3(0, 0, 0);*/
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