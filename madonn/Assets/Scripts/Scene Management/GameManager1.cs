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
    private static string nameFourthLevelScene = "Level4"; //variable that is used to compare the "nameofthecurrentscene" to the fourth level scene.
    private static string nameFifthLevelScene = "Level5"; //variable that is used to compare the "nameofthecurrentscene" to the fifth level scene.
    private static string nameSixthLevelScene = "Level6"; //variable that is used to compare the "nameofthecurrentscene" to the sixth level scene.
    private static string nameSeventhLevelScene = "Level7"; 

    //this variable is used for take the count of the click of the main button.
    [SerializeField] private GameObject counterClickerButtonAusiliarVar;

    //ausiliar gameobjects.
    [SerializeField] private GameObject ausiliarGO1Look; //gameobject used for block the camera movement. 
    [SerializeField] private GameObject ausiliarGO2Move;  //gameobject used for block the player movement.
    [SerializeField] private GameObject ausiliarG03TimerStop; //gameobject used to verify that the player have ended the level and the timer must be stopped.
    [SerializeField] private GameObject ausiliarAddingTimeGameManager; //gameobject used for add time to the gamemanager.
    //declaration variables.
    private int levelNumber = 0; //this variable is used to identify the level and load the scene of the relative level if the time ends. 

    //declaration gameobjects's variables 
    [SerializeField] private GameObject loadingSubScene;  //loading screen gameobject.
    [SerializeField] private GameObject gameButtons; //buttons in the menu gameobjects.
    public GameObject pauseButton;

    //variables that are used for the countdown.
    [SerializeField] private TextMeshProUGUI countdownTextUI; //countdown timer text.
    [SerializeField] private TextMeshProUGUI failureLevelAdviseUI; //end countdown timer text advise.
    [SerializeField] private float valueTimeForCountdown;  //variable that contain the value in seconds of the timer and it is used for slides the time how the reality.

    //Chapters Introductions
    [SerializeField] private VideoPlayer introductionChapterVideosource; // Chapter 1-2-3 Introduction Video texted with sound.
    private bool ausiliarIntroductionChapters = false; //ausiliar variable used for the coroutine.
    private bool ausiliarSecondFrameVideo = false; //ausiliar variable used for do the condition of the introduction form the second frame after the playing of the video.  

    //reawrd another time with ads variable utilities.
    [SerializeField] private GameObject failureMenuUtilitiesVar;

    //Tips GameObjects.
    [SerializeField] private GameObject[] tipsGameObjects;

    //ausiliar
    private bool ausiliar = false;
    private bool ausiliarZeroSeconds = false; 

    // Start is called before the first frame update.
    void Start()
    {
        failureLevelAdviseUI.gameObject.SetActive(false); //failure menu text disabled
        failureMenuUtilitiesVar.gameObject.SetActive(false); //failure menu disabled

        nameOftheCurrentScene = SceneManager.GetActiveScene().name; //get the name of the current active scene.
        Debug.Log(nameOftheCurrentScene);
        if (nameOftheCurrentScene == nameFirstLevelScene) //if the scene is Level1
        {

            valueTimeForCountdown = 359.00f; //set the start value of the timer to 6 minutes.
            levelNumber = 1;
            DataPersistence.instanceDataPersistence.levelAvancement = 1; //add the persistence of the level avancemenent 1.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json.
            CallLoadingFunctionAdvertisement();
        }

        else if (nameOftheCurrentScene == nameSecondLevelScene) //if the scene is Level2
        {
            valueTimeForCountdown = 539.00f; //set the start value of the timer to 9 minutes.
            levelNumber = 2;
            DataPersistence.instanceDataPersistence.levelAvancement = 2; //add the persistence of the level avancemenent 2.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json.
            CallLoadingFunctionAdvertisement();
        }

        else if (nameOftheCurrentScene == nameThirdLevelScene) // if the scene is Level3
        {
            valueTimeForCountdown = 599.00f; //set the start value of the timer to 10 minutes. 
            levelNumber = 3;
            DataPersistence.instanceDataPersistence.levelAvancement = 3; //add the persistence of the level avancement 3.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json format extension. 
            CallLoadingFunctionAdvertisement();
        }
        else if (nameOftheCurrentScene == nameFourthLevelScene) // if the scene is level4
        {
            valueTimeForCountdown = 299.00f; //set the start value of the timer to 5 minutes.
            levelNumber = 4;
            DataPersistence.instanceDataPersistence.levelAvancement = 4; //add the persistence of the level avancement 4.
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json format extension. 
            CallLoadingFunctionAdvertisement();
        }
        else if (nameOftheCurrentScene == nameFifthLevelScene) // if the scene is Level5
        {
            valueTimeForCountdown = 419.00f; //set the start value of the timer to 7 minutes.
            levelNumber = 5;
            DataPersistence.instanceDataPersistence.levelAvancement = 5;
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json format extension. 
            CallLoadingFunctionAdvertisement();
        }
        else if (nameOftheCurrentScene == nameSixthLevelScene) //if the scene is level6
        {
            valueTimeForCountdown = 429.00f; //set the start value of the timer to 7 minutes.
            levelNumber = 6;
            DataPersistence.instanceDataPersistence.levelAvancement = 6;
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json format extension. 
            CallLoadingFunctionAdvertisement();

        }
        else if ( nameOftheCurrentScene == nameSeventhLevelScene) //if the scene is level7
        {
            valueTimeForCountdown = 419.00f; //set the start value of the timer to 7 minutes.
            levelNumber = 7;
            DataPersistence.instanceDataPersistence.levelAvancement = 7;
            DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction(); //save this value in json format extension. 
            CallLoadingFunctionAdvertisement();
        }    

    }

    //Update is called once a frame. +
    private void Update()
    {
        //if the player has watched the rewarded ad, he has more time for finish the level(89 seconds).
        if (ausiliarAddingTimeGameManager.gameObject.activeSelf == false) //ausiliar gameobject used for add time to the gamemanager.
        {
            CallLoadingFunctionAdvertisement();
            ausiliarZeroSeconds = false;
            valueTimeForCountdown = 89.0f; //set the value of the timer to 1.30 minutes. 
            ausiliarAddingTimeGameManager.gameObject.SetActive(true);
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
            if (ausiliar == false) //only the first frame esecute these lines of code.
            {
                ausiliar = true;
                gameButtons.gameObject.SetActive(true);  //buttons of the scene are visible.
                countdownTextUI.gameObject.SetActive(true);   //the timer is actived.
                ausiliarZeroSeconds = false;
            }

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
            if ((secondsOfCountdown == 0) && (ausiliarZeroSeconds == false)) //if the timer is arrived to 0 seconds remaining
            {
                ausiliarZeroSeconds = true;

                failureLevelAdviseUI.gameObject.SetActive(true);
                ausiliarGO1Look.gameObject.SetActive(true); //ausiliar gameobject used in the look script.
                ausiliarGO2Move.gameObject.SetActive(true); //ausiliar gameobject used in the movement script.

                //failure menu gameobjects are sected to active.
                failureMenuUtilitiesVar.gameObject.SetActive(true);
            }
        }
        else if ((minutesOfCountdown >= 0) && (secondsOfCountdown > 10)) //if the timer is over ten seconds of time remaining
        {
            countdownTextUI.color = Color.white; // set the color to white

            //this conditions show on screen the tips that help the player to finish the levels
            if (levelNumber == 1) //TIPS LEVEL1
            {
                //Visualizing all the tips
                if ((minutesOfCountdown == 5) && (secondsOfCountdown == 16)) //tip1
                {
                    tipsGameObjects[0].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[0])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 4) && (secondsOfCountdown == 31)) //tip2
                {
                    tipsGameObjects[1].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[1])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 3) && (secondsOfCountdown == 31)) //tip3
                {
                    tipsGameObjects[2].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[2])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 2) && (secondsOfCountdown == 01)) //tip4
                {
                    tipsGameObjects[3].gameObject.SetActive(true); //text active 
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[3])); //15 seconds of visualization
                }
            }
            else if (levelNumber == 2) //TIPS LEVEL2
            {
                //Visualizing all the tips
                if ((minutesOfCountdown == 8) && (secondsOfCountdown == 11)) //tip1
                {
                    tipsGameObjects[0].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[0])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 5) && (secondsOfCountdown == 31)) //tip2
                {
                    tipsGameObjects[1].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[1])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 2) && (secondsOfCountdown == 31)) //tip3
                {
                    tipsGameObjects[2].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[2])); //15 seconds of visualization
                }
            }
            else if (levelNumber == 3) //TIPS LEVEL3
            {
                //Visualizing all the tips
                if ((minutesOfCountdown == 9) && (secondsOfCountdown == 41)) //tip1
                {
                    tipsGameObjects[0].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[0])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 7) && (secondsOfCountdown == 41)) //tip2
                {
                    tipsGameObjects[1].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[1])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 5) && (secondsOfCountdown == 01)) //tip3
                {
                    tipsGameObjects[2].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[2])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 2) && (secondsOfCountdown == 31)) //tip4
                {
                    tipsGameObjects[3].gameObject.SetActive(true); //text active 
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[3])); //15 seconds of visualization
                }
            }
            else if (levelNumber == 4) //TIPS LEVEL4
            {
                //Visualizing all the tips
                if ((minutesOfCountdown == 4) && (secondsOfCountdown == 41)) //tip1
                {
                    tipsGameObjects[0].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[0])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 3) && (secondsOfCountdown == 01)) //tip2
                {
                    tipsGameObjects[1].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[1])); //15 seconds of visualization
                }
            }
            else if (levelNumber == 5)
            {
                //Visualizing all the tips
                if ((minutesOfCountdown == 6) && (secondsOfCountdown == 31)) //tip1
                {
                    tipsGameObjects[0].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[0])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 4) && (secondsOfCountdown == 51)) //tip2
                {
                    tipsGameObjects[1].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[1])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 3) && (secondsOfCountdown == 21)) //tip3
                {
                    tipsGameObjects[2].gameObject.SetActive(true); //text active
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[2])); //15 seconds of visualization
                }
                else if ((minutesOfCountdown == 2) && (secondsOfCountdown == 11)) //tip4
                {
                    tipsGameObjects[3].gameObject.SetActive(true); //text active 
                    StartCoroutine(VisualizingTipsCoroutine(tipsGameObjects[3])); //15 seconds of visualization
                }
            }    

             if ((valueTimeForCountdown > 0) && (ausiliarG03TimerStop.gameObject.activeInHierarchy == true)) //if the level is passed successfully
             {
                 countdownTextUI.color = Color.green; //the color of the timer text will be setted to green.
             }
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

    private IEnumerator VisualizingTipsCoroutine(GameObject tip)
    {
        yield return new WaitForSeconds(15.0f);
        tip.gameObject.SetActive(false); //the tip text is disabled and not visible at the player.
    }

    //Function called when the player stop the game and open the pause menu.
    public void StopEsecutionGameFunction()
    {
        ausiliarGO1Look.gameObject.SetActive(true); //block of the movement input from the player.
        ausiliarGO2Move.gameObject.SetActive(true); //block of the looking visual input from the player.
        ausiliarG03TimerStop.gameObject.SetActive(true); //block of the timer value.
        pauseButton.gameObject.SetActive(false);
    }

    //Function called when the player resume the game from the pause.
    public void ResumeEsecutionGameFunction()
    {
        ausiliarGO1Look.gameObject.SetActive(false); //sblock of the movement input from the player.
        ausiliarGO2Move.gameObject.SetActive(false); //sblock of the looking visual input from the player.
        ausiliarG03TimerStop.gameObject.SetActive(false); //sblock of the timer value.
        pauseButton.gameObject.SetActive(true);
    }

    private void CallLoadingFunctionAdvertisement()
    {
        IntersitialAdsButton interstitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>();
        interstitialAdsButton.LoadAd();

        RewardedAdsButton rewardedAdsButton = GameObject.Find("Advertisement").GetComponent<RewardedAdsButton>();
        rewardedAdsButton.LoadAd();
    }
}
