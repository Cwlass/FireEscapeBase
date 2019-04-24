using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader: MonoBehaviour
{
    [SerializeField] bool useAutoloadTimer = false;
    [SerializeField] float autoloadTime = 10f;
    float currentAutoloadTime = 0f;

    public bool sceneCompleted = false;

    int thisSceneNumber;
    int nextSceneNumber;

    void Start()
    {
        thisSceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (thisSceneNumber >= SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneNumber = 0;
        }
        else
        {
            nextSceneNumber = thisSceneNumber + 1;
        }

    }

    void Update ()
	{
        if (useAutoloadTimer)
        {
            currentAutoloadTime += Time.deltaTime;

            if (currentAutoloadTime >= autoloadTime)
            {
                sceneCompleted = true;
            }
        }

        if (sceneCompleted || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneNumber);
        }
	}
}
