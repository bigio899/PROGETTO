using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class GameManager1 : MonoBehaviour
{
    //variables that are used for identify the actual level scene.  
    private string nameOftheCurrentScene; //in this variable we get the name of the current scene .
    private static string nameFirstLevelScene = "Level1"; //variable that is used to compare the "nameOfTheCurrentScene" to the first level scene.
    private static string nameSecondLevelScene = "Level2"; //variable that is used to compare the "nameOfTheCurrentScene" to the second level scene.
    private static string nameThirdLevelScene = "level3"; //variable that is used to compare the "nameofthecurrentscene" to the third level scene.

    //this variable is used for take the count of the click of the main button.
    [SerializeField] private GameObject counterClickerButtonAusiliarVar;

    //ausiliar gameobjects.
    [SerializeField] private GameObject ausiliarGO1Look; //gameobject used for block the camera movement. 
    [SerializeField] private GameObject ausiliarGO2Move;  //gameobject used for block the player movement.
    [SerializeField] private GameObject ausiliarG03TimerStop; //gameobject used to verify that the player have ended the level and the timer must be stopped.
    [SerializeField] private GameObject ausiliarAddingTimeGameManager; //gameobject used for add time to the gamemanager.

    //declaration variables.
    private int levelNumber = 0; //this variable is used to identify the level and load the scene of the relative level if the time ends. 
    private bool isFailureCoroutineEnded = false; //boolean that verify that the timer is ended(zero value). 

    //declaration gameobjects's variables 
    [SerializeField] private GameObject loadingSubScene;  //loading screen gameobject. 
    [SerializeField] private GameObject gameButtons; //buttons in the menu gameobjects.

    //variables that are used for the countdown.
    [SerializeField] private TextMeshProUGUI countdownTextUI; //countdown timer text.
    [SerializeField] private TextMeshProUGUI failureLevelAdviseUI; //end countdown timer text advise.
    private float valueTimeForCountdown;  //variable that contain the value in seconds of the timer and it is used for slides the time how the reality.
    private bool isGameAlreadyEnded;  //boolean that verify if the timer is expired(scaduto).

    //Chapters Introductions
    [SerializeField] private VideoPlayer introductionChapterVideosource; // Chapter 1-2-3 Introduction Video texted with sound.
    private bool ausiliarIntroductionChapters = false; //ausiliar variable used for the coroutine.
    private bool ausiliarSecondFrameVideo = false; //ausiliar variable used for do the condition of the introduction form the second frame after the playing of the video.  

    //reawrd another time with ads variable utilities.
    [SerializeField] private GameObject failureMenuUtilitiesVar;
    // Start is called before the first frame update.
    void Start()
    {
        failureLevelAdviseUI.gameObject.SetActive(false);
        failureMenuUtilitiesVar.gameObject.SetActive(false);

        nameOftheCurrentScene = SceneManager.GetActiveScene().name; //get the name of the current active scene.
        Debug.Log(nameOftheCurrentScene);
        if(nameOftheCurrentScene == nameFirstLevelScene) //if the scene is Level1
        {
            valueTimeForCountdown = 359.00f; //set the start value of the timer to 6 minutes.
            levelNumber = 1;
            DataPersistence.instanceDataPersistence.levelAvancement = 1; //add the persistence of the level avancemenent 1.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json.
        }

        else if(nameOftheCurrentScene == nameSecondLevelScene) //if the scene is Level2
        {
            valueTimeForCountdown = 719.00f; //set the start value of the timer to 12 minutes.
            levelNumber = 2;
            DataPersistence.instanceDataPersistence.levelAvancement = 2; //add the persistence of the level avancemenent 2.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json.
        }

        else if(nameOftheCurrentScene == nameThirdLevelScene) // if the scene is Level3
        {
            valueTimeForCountdown = 719.00f; //set the start value of the timer to 12 minutes. 
            levelNumber = 3;
            DataPersistence.instanceDataPersistence.levelAvancement = 3; //add the persistence of the level avancement 3.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json format extension. 
        }
    }
    private void Update()
    {
        if(ausiliarAddingTimeGameManager.gameObject.activeSelf == true) //ausiliar gameobject used for add time to the gamemanager.
        {
            valueTimeForCountdown = 89.0f; //set the value of the timer to 1.30 minutes. 
            ausiliarAddingTimeGameManager.gameObject.SetActive(false);
        }

        //ausiliar condition used for the first frame. 
        if (introductionChapterVideosource.isPlaying)
        {
            ausiliarSecondFrameVideo = true;
        }
        //condition that verify if the introduction's video is ended.If the value is true,all the GameButtons of the menu will back active, and the intro will disabilited.

        if ((ausiliarSecondFrameVideo == true) && (!introductionChapterVideosource.isPlaying) && (ausiliarIntroductionChapters != true)) //if the video has finished the reproduction
        {
            Debug.Log("The video is ended successfully"); 
            introductionChapterVideosource.gameObject.SetActive(false);
            gameButtons.gameObject.SetActive(true);
            ausiliarIntroductionChapters = true;
        }


        if ((ausiliarSecondFrameVideo == true) && (!introductionChapterVideosource.isPlaying)) //if the loading sub-scene is complete, this part of script make time(used for the timer) pass. 
        {
            gameButtons.gameObject.SetActive(true);  //buttons of the scene are visible.
            countdownTextUI.gameObject.SetActive(true);   //the timer is actived.

            if ((valueTimeForCountdown > 0) && (ausiliarG03TimerStop.gameObject.activeSelf == false)) //if the time(in deltatime value) is major than zero
            {
                valueTimeForCountdown = (valueTimeForCountdown - Time.deltaTime);  //the time will slide.
            }
            else if (valueTimeForCountdown < 0) //if the value is less than 0, the value of the time is setted to zero(0).
            {
                valueTimeForCountdown = 0.00f; //the time will set to zero.
            }
            DisplayTimeCountdownFunction(valueTimeForCountdown); //call of the function

        }


    }


    //this function is used for translate the value of the time(in deltatime format) in minutes and seconds.After that, the values of the minutes and the seconds will change in a string format(for be viewed in the display by a text(countdownTextUI)).
    private void DisplayTimeCountdownFunction(float currentTimeValueDisplay)
    {
        if (currentTimeValueDisplay <= 0) //if the value od the time in deltatime is less than zero
        {
            currentTimeValueDisplay = 0.00f; //variable of the time is setted to zero. 
        }

        float minutesOfCountdown = Mathf.FloorToInt(currentTimeValueDisplay / 60); //translation of the deltatime in minutes.
        float secondsOfCountdown = Mathf.FloorToInt(currentTimeValueDisplay % 60); //translation of the deltatime in seconds.

        countdownTextUI.text = string.Format("{0:00}:{1:00}", minutesOfCountdown, secondsOfCountdown);   //change of the format from int value to string(for be viewed in the countdown text).
        if ((minutesOfCountdown == 0) && (secondsOfCountdown <= 10)) //if the timer is arrived to 10 seconds or less
        {
            countdownTextUI.color = Color.red; //the color of the timer text will be setted to red.
            if ((secondsOfCountdown == 0) && (isGameAlreadyEnded != true)) //if the timer is arrived to 0 seconds remaining
            {
                isGameAlreadyEnded = true;  //the boolean value that verify that the timer is expired(scaduto) will changed to tue.
                failureLevelAdviseUI.gameObject.SetActive(true);
                Debug.Log("il tempo è scaduto, riprova!");
                ausiliarGO1Look.gameObject.SetActive(true); //ausiliar gameobject used in the look script.
                ausiliarGO2Move.gameObject.SetActive(true); //ausiliar gameobject used in the movement script.

                //failure menu gameobjects are sected to active.
                failureMenuUtilitiesVar.gameObject.SetActive(true);
            }
        }
        else if ((minutesOfCountdown >= 0) && (secondsOfCountdown > 10)) //if the timer is over ten seconds of time remaining
        {
            countdownTextUI.color = Color.white; // set the color to white
        }

        if ((valueTimeForCountdown > 0) && (ausiliarG03TimerStop.gameObject.activeInHierarchy == true)) //if the level is passed successfully
        {
            countdownTextUI.color = Color.green; //the color of the timer text will be setted to green.
        }
    }

    public void OnClickEnterButtonMainFunction()
    {
        counterClickerButtonAusiliarVar.gameObject.SetActive(true); //ausiliar gameobject for the clicking is sected to true.
    }

    public void OnExitButtonCLick()
    {
        DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction();
        SceneManager.LoadScene(0);
    }
}
