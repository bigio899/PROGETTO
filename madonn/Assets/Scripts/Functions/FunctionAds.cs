using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FunctionAds : MonoBehaviour
{
    //GAMEOBJECT TO DISACTIVE END LEVEL
    public GameObject endLevel;
    public GameObject gamebuttonText;
    public GameObject textAdvisePassedLevel;
    public GameObject clickerButton;
    public GameObject backMenuFromPlaying;
    public GameObject timer;

    [SerializeField] public GameObject loadingSubScene;  //loading screen gameobject. 

    // Update is called once per frame
    private void Update()
    {

    }

    //(LEVELS)function that starts if the player click on the "BackToMenuButton".
    public void BackMenu()
    {
        clickerButton.gameObject.SetActive(false);
        timer.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        backMenuFromPlaying.gameObject.SetActive(false);
        endLevel.gameObject.SetActive(false);
        gamebuttonText.gameObject.SetActive(false);
        textAdvisePassedLevel.gameObject.SetActive(false);
        loadingSubScene.gameObject.SetActive(true);
    }

    //(LEVELS)function that starts if the player click on the "NextlevelButton".
    public void NextLevel()
    {
        clickerButton.gameObject.SetActive(false);
        timer.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        backMenuFromPlaying.gameObject.SetActive(false);
        endLevel.gameObject.SetActive(false);
        gamebuttonText.gameObject.SetActive(false);
        textAdvisePassedLevel.gameObject.SetActive(false);

        loadingSubScene.gameObject.SetActive(true);
    }

    public void LevelAvancementPersistence()
    {
        DataPersistence.instanceDataPersistence.levelAvancement = SceneManager.GetActiveScene().buildIndex + 1;
    }
}
