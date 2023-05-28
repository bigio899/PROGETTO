using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Video;
public class GrabbingObject : MonoBehaviour
{
    //ausiliar GameObject
    [SerializeField] private GameObject counterClickerButtonAusiliarVar; //audsiliar gameobject used for the counter of click of the clickerbutton. 
    [SerializeField] private int ausiliarCoroutineVariable = 0; //change of the scene from level1 to level2.


    //battery variables.
    private int numberOfActualBatteries = 0; //variable that is used to have the count of the actual batteries. 
    private bool isBatteryStarted = false;
    [SerializeField] private GameObject timerAusiliarGOLengthLifeOfBattery;

    //ausiliar variable for identify the drawers
    private int ausiliarVariableForIdentification;
    private int[] ausiliarDrawerVar = { 0, 0, 0, 0 };

    //bunker door animator variable.
    [SerializeField] private Animator firstbunkerDoorAnimationOpening; //variable where's contained the first BunkerDoor Animator, used for working with conditions and parameters.
    [SerializeField] private Animator secondBunkerDoorAnimationOpening; //variable where's contained the second BunkerDoor Animator, used for working with conditions and parameters.
    [SerializeField] private Animator[] DrawersOpeningAndClosingAnimator; //variable where are contained the Drawer Animators, used for working with conditions and parameters.
    [SerializeField] private Animator doorLevel2;

    //lights of the flashlight gameobject variable.
    [SerializeField] private GameObject lightTorchGameObject; //variable where's contained the light of the torch.

    //booleans values
    private bool isKeyGrabbedToThePlayer = false;  //boolean where is contained the information about the grab or not of the key.
    private bool isKeyCoroutineEnded = false;  //boolean where is contained the information about the end or not of the TimeOfViewingKeyText coroutine.
    private bool isKeyMissingCoroutineEnded = false;  //boolean where is contained the information about the end or not of the TimeOfViewingMissingKeyText coroutine.
    private bool isBunkerDoorOpeningCoroutineEnded = false;  //boolean where is contained the information about the end or not of the TimeOfViewingOpeningDoorBunkerText coroutine.
    private bool[] areDrawersOpened = { false, false, false, false }; //booleans where are contained the informations about the drawers,if they are open or not.
    private bool isBatteryTurnedOff = false; //boolean where's contained the information about the grab or not of the battery.
    private bool isBatteryGrabbed = false; //boolean where's contained the information about the grabbing or not of the battery
    private bool hasAlreadyBatteryText = false; //boolean where's contained the information about the text advise "hasAlreadyBatteryGrabbed".
    private bool ausiliarJumpScare = false; // boolean where's contained the information about the Jumpscare status.
    private bool auxJumpscareUpdateMethod = false;

    //door bug variables.
    private int ausiliarVarBunkerDoor = 0; //ausiliar variable that is used for
    private bool areDoorsFixed = false; //variable that is used for resolve a fix of the door. 
    private bool doorFixingBugErrorMissing = false;  //ausiliar variable that is used for fix the bug of missing key at the end of the level. 
    private bool ausiliarFixingOpeningTextConflictWithMissingText = false; //ausiliar variable that is used for fix the bug of sovrapposition between opening text and missing key text.

    //texts and images variables 
    [SerializeField] private TextMeshProUGUI doorBunkerOpeningTextAdvise;  //variable where's contained the text "Complimenti!Hai aperto il bunker!".
    [SerializeField] private TextMeshProUGUI doorBunkerMissingKeyText; //variable where's contained the text "Devi prima trovare la chiave per poter aprire la porta!".
    [SerializeField] private Image keyIconImage; //variable where's comtained the icon of the key.
    [SerializeField] private TextMeshProUGUI keyGrabbedTextAdvise; //variable where's contained the text "Hai appena raccolto una chiave!Ora sta a te capire dove utilizzarla".
    [SerializeField] private TextMeshProUGUI batteryGrabbedTextAdvise; //variable where's contained the text "Hai appena raccolto una batteria per la torcia".
    [SerializeField] private TextMeshProUGUI alreadyHasTheBatteryTextAdvise; //variable where's contained the text "Non puoi raccogliere la batteria,ne hai già una inserita nella torcia!".
    [SerializeField] private TextMeshProUGUI levelPassedTextAdvise; //variable where's contained the text "Hai superato il livello!".

    //music and sounds variables
    [SerializeField] private GameObject bunkerSoundEffect; //gameobject where's allocated the audiosoruce of the bunkersound effect.
    [SerializeField] private GameObject jumpScareSoundEffect; // gameobject where's allocated the audiosource of the jumpscare effect.

    //static boolean values.
    private static bool acceptedTransition = true; //this static variable is sected to true value and is utilised only for active the gameobjects.
    private static bool rejectedTransition = false; //this static variable is sected to false value and is utilised only for active the gameobjects.

    //ausiliar gameobjects.
    [SerializeField] private GameObject ausiliarGO1Look; //gameobject used for block the camera movement. 
    [SerializeField] private GameObject ausiliarGO2Move;  //gameobject used for block the player movement.
    [SerializeField] private GameObject ausiliarGO03TimerStop; //gameobject used to verify that the player have ended the level and the timer must be stopped.

    //level2 variables
    [SerializeField] private GameObject padlockGameObjectTransition;  //this padlock is used for close the door of the mine.

    //gameobjects that contains fixed jostick, clicker button and button.(ALLTHE CANVAS)
    [SerializeField] private GameObject loadingSubScene;  //loading screen gameobject. 
    [SerializeField] private GameObject textAndButtons; //the children "GameButtons&Text".
    [SerializeField] private GameObject pauseSubScene; // the children "pauseSubScene".

    //name of the current scene variable
    private string nameOftheCurrentScene;

    //end level variables gameobjects 
    [SerializeField] private GameObject endLevelGO; //variable that is used for active and disactive the endlevel menu. 

    //Level3 CardElevator Variables.
    private bool hasTheCardForElevator; //this variable is used to know that the player has grabbed the card for the elevator. 
    [SerializeField] private TextMeshProUGUI hasActivedElettricityText;
    private bool hasOpenedElettricity = false;

    //end video first release(1th May 2023)
    private bool ausiliarHole = false;
    private bool ausiliarHole2 = false;

    private bool ausiliarEndLevel4SaveAvancementFunction = false;

    //level5 variables
    private bool hasTheParchment = false; //variable where's allocated the information about the grabbing or not of the parchment.
    private bool ausiliarCollisionWithOldMan = false; //variable that inform the script that the player has collided the first time.
    [SerializeField] private GameObject textAdviseSeniorManDoesntHaveTheParchment; //gameobject where's allocated the text who inform the player that doesn't have the parchment yet.
    [SerializeField] private GameObject textAdviseSeniorManHasntTheParchmentSecondCollision; //gameobject where's allocated the text who inform the player that hasm't the parchment from the second collision and after.
    private bool ausiliarOneTimeCalledCollision =false;
    // Start function is called before the first frame update.(MAIN)
    private void Start()
    {
        levelPassedTextAdvise.gameObject.SetActive(rejectedTransition);
        loadingSubScene.gameObject.SetActive(false); pauseSubScene.gameObject.SetActive(false);
        ausiliarHole = false;
        nameOftheCurrentScene = SceneManager.GetActiveScene().name; //get the name of the current active scene.
    }

    // Update function is called once per frame(MAIN2)
    private void Update()
    {
        if ((nameOftheCurrentScene == "Level1"))
        {
            //active the sound of opening bunker. 
            if ((bunkerSoundEffect.GetComponent<AudioSource>().isPlaying == true) && (auxJumpscareUpdateMethod == false))
            {
                StartCoroutine(JumpScareLevel1());
                auxJumpscareUpdateMethod = true;
            }
            //when the bunker sound opening is ended, start the jumpsacre sound coroutine.
            else if ((areDoorsFixed == true) && (bunkerSoundEffect.GetComponent<AudioSource>().isPlaying == false))
            {
                bunkerSoundEffect.gameObject.SetActive(false); //disactive the sound of the bunker's opening.
                StartCoroutine(JumpScareLevel1()); //start coroutine that set the active status of the jumpscare.
            }
        }

        //condition that active the jumpscare sound.
        if (ausiliarJumpScare == true)
        {
            jumpScareSoundEffect.gameObject.SetActive(true);
        }

        if (isKeyGrabbedToThePlayer == true && (nameOftheCurrentScene != "Level5") && (nameOftheCurrentScene != "Level6") && (nameOftheCurrentScene != "Level7"))
        {
            keyIconImage.gameObject.SetActive(acceptedTransition); //the key icon is sected to active.
        }
        else if ((isKeyGrabbedToThePlayer == false) && (nameOftheCurrentScene != "Level5") && (nameOftheCurrentScene != "Level6") && (nameOftheCurrentScene != "Level7"))
        {
            keyIconImage.gameObject.SetActive(rejectedTransition); //the key icon is sected to disactive.
        }
        
        if (isKeyCoroutineEnded != false) //if the TimeOfViewingKeyText coroutine is ended.
        {
            keyGrabbedTextAdvise.gameObject.SetActive(rejectedTransition); // the text that inform the player whop has grabbed the key is sected to inactive.
            isKeyCoroutineEnded = false;
        }

        if (isKeyMissingCoroutineEnded != false) //if the TimeOfViewingMissingKeyText coroutine is ended.
        {

            doorBunkerMissingKeyText.gameObject.SetActive(rejectedTransition); // the text that inform the player who doesn't have the key is sected to inactive.
            isKeyMissingCoroutineEnded = false;
        }

        if (isBunkerDoorOpeningCoroutineEnded != false) //if the TimeOfViewingOpeningDoorBunkerText coroutine is ended.
        {
            doorBunkerOpeningTextAdvise.gameObject.SetActive(rejectedTransition); // the text that inform the player who's opening the bunker door is sected to inactive.
            isBunkerDoorOpeningCoroutineEnded = false;
        }

        if (isBatteryTurnedOff != false)  //if the LengthLifeOfTheBatteryCoroutine coroutine is ended.
        {
            lightTorchGameObject.gameObject.SetActive(false); //turns off the light
            numberOfActualBatteries--;
            isBatteryTurnedOff = false;
        }

        if (isBatteryGrabbed != false)   //if the TimeOfViewingGrabBatteryText coroutine is ended.
        {
            batteryGrabbedTextAdvise.gameObject.SetActive(false); //the text that inform the player who's grabbed the battery is sected to inactive.
            isBatteryGrabbed = false;
        }

        if (hasAlreadyBatteryText != false)  //if the HasAlreadyTheBatteryCoroutine coroutine is ended.
        {
            alreadyHasTheBatteryTextAdvise.gameObject.SetActive(true); //the text that inform the player who has got already the battery is sected to inactive.
            hasAlreadyBatteryText = false;
        }

        if (isBatteryStarted == true) //if the battery is insert in the torch
        {
            timerAusiliarGOLengthLifeOfBattery.gameObject.SetActive(acceptedTransition); //the ausiliar gameobject is actived for verifiy to "batteryiconscript" that the coroutine is started.
            isBatteryStarted = false;
        }

        if(counterClickerButtonAusiliarVar.gameObject.activeSelf)
        {
            StartCoroutine(TimeOfClicking());
        }
    }
    //function that is called when the player triggerer trigger with another gameobject with a box collider marked like trigger.this function is called from the second frame after the trigger.
    private void OnTriggerStay(Collider other)
    {
        if (counterClickerButtonAusiliarVar.gameObject.activeSelf == true)
        {
            //key grab code part of the script.
            if ((other.gameObject.CompareTag("Key")))   //if the player's approaching at the key.
            {
                other.gameObject.SetActive(rejectedTransition); //disactive the key gameobject.
                Destroy(other.gameObject); //destroy the key gameobject.
                isKeyGrabbedToThePlayer = true; //set true value for this variable(that is used to verify a lot of thing afterwards).
                if (isKeyGrabbedToThePlayer == true)
                {
                    keyGrabbedTextAdvise.gameObject.SetActive(acceptedTransition); // the text that inform the player whop has grabbed the key is sected to active.
                    StartCoroutine(TimeOfViewingKeyText()); //start of 5 second of coroutine. 
                }
            }

            //battery grab code part of the script.
             if (other.gameObject.CompareTag("Battery")) //if the player's approaching at one of the battery and the player doesn't have another of it
            {
                Debug.Log("ready");
                if ((numberOfActualBatteries < 1))
                {
                    Destroy(other.gameObject);
                    numberOfActualBatteries++;
                    StartCoroutine(LengthLifeOfTheBatteryCoroutine());
                    lightTorchGameObject.gameObject.SetActive(true);
                    batteryGrabbedTextAdvise.gameObject.SetActive(true);
                    StartCoroutine(TimeOfViewingGrabBatteryText());
                    Debug.Log("the battery is in the inventory");
                }
                else if (numberOfActualBatteries != 0) //else if the player has already the battery in the flashlight.
                {
                    alreadyHasTheBatteryTextAdvise.gameObject.SetActive(true); //the text that inform the player who has got already the battery is sected to active.
                    StartCoroutine(HasAlreadyTheBatteryCoroutine()); //start of the coroutine of 4 seconds for read the text. 

                }
            }

            //drawers grab code part of the script.
             if (other.gameObject.CompareTag("SingleDrawerGameObject")) //if the player's approaching at one of the drawers
            {
                if ((other.gameObject.name == ("TriggererDrawer1")) && (ausiliarDrawerVar[0] == 0)) //if the player's approaching at the first of the drawers and it isn't already opened
                {
                    ausiliarVariableForIdentification = 0; //ausiliar.
                    Debug.Log("first");
                    OpeningOrClosingParametersMethod(ausiliarVariableForIdentification);
                    SectedOpenOrCloseDrawer(ausiliarVariableForIdentification);

                }
                else if ((other.gameObject.name == ("TriggererDrawer2")) && (ausiliarDrawerVar[1] == 0)) //if the player's approaching at the second of the drawers and it isn't already opened
                {
                    ausiliarVariableForIdentification = 1; //ausiliar.
                    Debug.Log("second");
                    OpeningOrClosingParametersMethod(ausiliarVariableForIdentification);
                    SectedOpenOrCloseDrawer(ausiliarVariableForIdentification);

                }
                else if ((other.gameObject.name == ("TriggererDrawer3")) && (ausiliarDrawerVar[2] == 0)) //if the player's approaching at the third of the drawers and it isn't already opened
                {
                    ausiliarVariableForIdentification = 2; //ausiliar.
                    Debug.Log("third");
                    OpeningOrClosingParametersMethod(ausiliarVariableForIdentification);
                    SectedOpenOrCloseDrawer(ausiliarVariableForIdentification);

                }
                else if ((other.gameObject.name == ("TriggererDrawer4")) && (ausiliarDrawerVar[3] == 0)) //if the player's approaching at the fourth of the drawers and it isn't already opened
                {
                    ausiliarVariableForIdentification = 3; //ausiliar.
                    Debug.Log("fourth");
                    OpeningOrClosingParametersMethod(ausiliarVariableForIdentification);
                    SectedOpenOrCloseDrawer(ausiliarVariableForIdentification);

                }
            }

            if (other.gameObject.CompareTag("CardElevator"))
            {
                Debug.Log("card elevator picked");
                hasTheCardForElevator = true;
                keyGrabbedTextAdvise.gameObject.SetActive(true);
                StartCoroutine(TimeOfViewingKeyText());
                other.gameObject.SetActive(false);
            }

            counterClickerButtonAusiliarVar.gameObject.SetActive(false);
        }
    }

    //function that is called when the triggerer of the player trigger with another gameobject with a tag and a box collider marked like trigger(only for the first frame after the trigger). 
    private void OnTriggerEnter(Collider other)
    {
        if(nameOftheCurrentScene == "Level1") //if the scene is of the first level
        {
            Level1FunctionTriggerer(other); //call the function triggerer of the first level.
        }
        else if(nameOftheCurrentScene == "Level2") //if the scene is of the second level
        {
            Level2FunctionTriggerer(other); //call the function triggerer of the second level.
        }
        else if(nameOftheCurrentScene == "level3") //if the scene is on the third level
        {
            Debug.Log("Level3 call.");
            Level3FunctionTriggerer(other); //call the function triggerer of the third level.
        }
        else if (nameOftheCurrentScene == "Level4")
        {
            Level4FunctionTriggerer(other);
        }     
        else if (nameOftheCurrentScene == "Level5")
        {
            Level5FunctionTriggerer(other);
        }
        else if (nameOftheCurrentScene == "Level6")
        {
            Level6FunctionTriggerer(other);
        }
    }
    //function that verify if the Drawer must be opened or closed,and then do the action(of opening or closing).
    private void OpeningOrClosingParametersMethod(int numberOfDrawer)
    {
        ausiliarVariableForIdentification = numberOfDrawer;
        if (areDrawersOpened[ausiliarVariableForIdentification] == false)
        {
            DrawersOpeningAndClosingAnimator[ausiliarVariableForIdentification].SetBool("IsDrawerOpened", acceptedTransition); //in this line of code,we set the "IsDrawerOpened" parameter(created in the animator controller) to true.
            areDrawersOpened[ausiliarVariableForIdentification] = !areDrawersOpened[ausiliarVariableForIdentification];
        }
        else if (areDrawersOpened[ausiliarVariableForIdentification] == true)
        {
            DrawersOpeningAndClosingAnimator[ausiliarVariableForIdentification].SetBool("CanBeClosedParameter", acceptedTransition); //in this line of code,we set the "CanBeClosedParameter" parameter(created in the animator controller) to true.
            areDrawersOpened[ausiliarVariableForIdentification] = !areDrawersOpened[ausiliarVariableForIdentification];
        }
    }

    //coroutine for don't have bugs for the key with the opening of both the doors.
    private IEnumerator FixingDoorBug()
    {
        yield return (new WaitForSeconds(1.0f));
        ausiliarVarBunkerDoor = 1;
        areDoorsFixed = true;
    }

    //coroutine for don't have bugs for the key with the opening of both the doors.
    private IEnumerator JumpScareLevel1()
    {
        yield return (new WaitForSeconds(3.6f));
        ausiliarJumpScare = true;
        auxJumpscareUpdateMethod = false;
    }

    //this function is used for set open the door.
    private void SectedOpenOrCloseDrawer(int drawernumber)
    {
        ausiliarDrawerVar[drawernumber] = 1;
    }

    //this function is used for get 3 second of waiting before the text is disabled.
    private IEnumerator TimeOfViewingKeyText()
    {
        yield return (new WaitForSeconds(2.5f));  //3 seconds for read the text that inform the player whop has grabbed the key.
        isKeyCoroutineEnded = true;
    }

    //this function is used for get 5 second of waiting before the text is disabled.
    private IEnumerator TimeOfViewingMissingKeyText()
    {
        yield return (new WaitForSeconds(4.0f));  //4 seconds for read the text that inform the player who doesnt't have the key for open the door of the bunker.
        isKeyMissingCoroutineEnded = true;
    }

    //this function is used for get 4 second of waiting before the text is disabled.
    private IEnumerator TimeOfViewingOpeningDoorBunkerText()
    {
        ausiliarFixingOpeningTextConflictWithMissingText = true;
        yield return (new WaitForSeconds(4.0f));  //4 seconds for read the text that inform the player who's opening the door for entry in the bunker.
        isBunkerDoorOpeningCoroutineEnded = true;
        ausiliarFixingOpeningTextConflictWithMissingText = false;
    }

    //this function is used for get 150 second of waiting before the battery turns off.
    private IEnumerator LengthLifeOfTheBatteryCoroutine()
    {
        isBatteryStarted = true; //ausiliar variable used to understand when the coroutine is active. 
        yield return (new WaitForSeconds(180.0f)); //180 seconds for turns off the battery selected.
        isBatteryTurnedOff = true;
        isBatteryStarted = false; //ausiliar variable used to understand when the coroutine is active,who changes the value in false for tell at the code that the coroutine is ended. 
    }

    //this funtion is used for get 5 second of waiting for reading the grab battery text.
    private IEnumerator TimeOfViewingGrabBatteryText()
    {
        yield return (new WaitForSeconds(5.0f)); //5 seconds for read the text that inform the player who has just grabbed the battery for the flashlight.
        isBatteryGrabbed = true;
    }

    //this function is used for get 4 seconds of waiting for reading the "has already the battery" text. 
    private IEnumerator HasAlreadyTheBatteryCoroutine()
    {

        yield return (new WaitForSeconds(4.0f)); //4 seconds for read the text that inform the player who has got already the battery.
        hasAlreadyBatteryText = true;
    }

    //this function is used for mantain active the click only for 0.15 seconds.
    private IEnumerator TimeOfClicking()
    {
        yield return new WaitForSeconds(0.15f);
        counterClickerButtonAusiliarVar.gameObject.SetActive(false);
    }
    //---------------------
    //LEVEL1!
    //+


    //level1 triggerer active function.
    private void Level1FunctionTriggerer(Collider other) 
    {
        if (other.gameObject.CompareTag("BunkerDoor"))  //if the player's approaching at the door
        {
            if ((isKeyGrabbedToThePlayer == true))  //if the player has got the key 
            {
                if ((ausiliarVarBunkerDoor == 1) && (areDoorsFixed == true)) // if the second bunkerdoor is opened
                {
                    bunkerSoundEffect.gameObject.SetActive(true); //sound of the bunker's opening is played.
                    secondBunkerDoorAnimationOpening.SetBool("CanBeOpen", acceptedTransition); //in this line of code,we set the "CanBeOpen" parameter(created in the second animator controller) to true.
                    doorFixingBugErrorMissing = true;
                    ausiliarGO03TimerStop.gameObject.SetActive(true); //ausiliar variable that inform the GameManager that the timer must be stopped.
                }
                else if ((ausiliarVarBunkerDoor == 0) && (areDoorsFixed == false)) //this condition verify if the player is opening the first of the second door of the bunker. 
                {
                    bunkerSoundEffect.gameObject.SetActive(true); //sound of the bunker's opening is played.
                    firstbunkerDoorAnimationOpening.SetBool("CanBeOpen", acceptedTransition); //in this line of code,we set the "CanBeOpen" parameter (created in the first animator controller of the door) to true.
                    StartCoroutine(FixingDoorBug());
                }

                doorBunkerOpeningTextAdvise.gameObject.SetActive(acceptedTransition);  // the text that inform the player who's opening the bunker door is sected to active.
                StartCoroutine(TimeOfViewingOpeningDoorBunkerText()); // start of 4 seconds of coroutine
                isKeyGrabbedToThePlayer = false;
                other.gameObject.SetActive(false);
            }
            else  //if the player doesn't have the key 
            {
                if ((doorFixingBugErrorMissing == false) && (ausiliarFixingOpeningTextConflictWithMissingText == false)) //condition that verify that there aren't other text messages(opening text) and that this text doesn't appeaar at the end of the level1.
                {
                    doorBunkerMissingKeyText.gameObject.SetActive(acceptedTransition);  // the text that inform the player who doesn't have the key to open the door is sected to true.
                    StartCoroutine(TimeOfViewingMissingKeyText()); //start of 5 seconds of coroutine
                }
            }
        }

        //end level trigger level1
        if (other.gameObject.CompareTag("EndLevel"))
        {
            Debug.Log("level passed");
            levelPassedTextAdvise.gameObject.SetActive(true); //level passed advise text.
            ausiliarGO1Look.gameObject.SetActive(acceptedTransition); //block of the movement input from the player.
            ausiliarGO2Move.gameObject.SetActive(acceptedTransition); //block of the looking visual input from the player.
            ausiliarGO03TimerStop.gameObject.SetActive(acceptedTransition); //block of the timer value.
            textAndButtons.gameObject.SetActive(false); // disactive the buttons(invisible).
            endLevelGO.gameObject.SetActive(true); //actove the menu for change the level.
        }
    
    }

    //-------------------------
    //LEVEL2!
    //+


    //level2 triggerer active function.
    private void Level2FunctionTriggerer(Collider other)
    {
        // ENDLEVEL2 verifier that active the endlevelmenu and disactive all the rest.
        if (other.gameObject.CompareTag("EndLevel2Passingtransition")) //if the player arrives in front of the mine
        {
            levelPassedTextAdvise.gameObject.SetActive(true); //level passed advise text.
            padlockGameObjectTransition.gameObject.SetActive(false);
            Destroy(padlockGameObjectTransition); //destroy the padlock of the mine.
            Debug.Log("level passed");
            textAndButtons.gameObject.SetActive(false); // disactive the buttons(invisible).
            endLevelGO.gameObject.SetActive(true); //actove the menu for change the level.
            ausiliarGO1Look.gameObject.SetActive(acceptedTransition); //block of the movement input from the player.
            ausiliarGO2Move.gameObject.SetActive(acceptedTransition); //block of the looking visual input from the player.
            ausiliarGO03TimerStop.gameObject.SetActive(acceptedTransition); //block of the timer value.
        }
        
        if (other.gameObject.CompareTag("ExitFirstHouseLevel2") && (isKeyGrabbedToThePlayer == true)) // if the player open the door of the house of the level2
        {
            Debug.Log("ExitLevel2 Called Tag");
            doorBunkerOpeningTextAdvise.gameObject.SetActive(acceptedTransition);
            StartCoroutine(TimeOfViewingOpeningDoorBunkerText()); // start of 4 seconds of coroutine
            lightTorchGameObject.gameObject.SetActive(true);
            doorLevel2.SetBool("IsActived", true);
            isKeyGrabbedToThePlayer = false;
            other.gameObject.tag = "Untagged";
        }
        
    }


    //-------------------
    //LEVEL3!
    //+

    //level3 triggerer active function.
    private void Level3FunctionTriggerer(Collider other)
    {
        //if the player triggers with the elevator, has the card for use him and the elettricity is been actived(ENDLEVEL3)
        if ((other.gameObject.CompareTag("Elevator")) && (hasTheCardForElevator == true) && (hasOpenedElettricity == true))
        {
            Debug.Log("level Passed Successfully!");
            levelPassedTextAdvise.gameObject.SetActive(true); //level passed advise text.
            textAndButtons.gameObject.SetActive(false); // disactive the buttons(invisible).
            endLevelGO.gameObject.SetActive(true); //actove the menu for change the level.
            ausiliarGO1Look.gameObject.SetActive(acceptedTransition); //block of the movement input from the player.
            ausiliarGO2Move.gameObject.SetActive(acceptedTransition); //block of the looking visual input from the player.
            ausiliarGO03TimerStop.gameObject.SetActive(acceptedTransition); //block of the timer value.
        }
        //if the player triggers with the elevator, but hasn't the card for use him or the elettricity isn't been actived yet
        else if ((other.gameObject.CompareTag("Elevator") && ((hasTheCardForElevator == false) || (hasOpenedElettricity == false))))
        {
            doorBunkerMissingKeyText.gameObject.SetActive(true);
            StartCoroutine(TimeOfViewingMissingKeyText());
        }
        //if the player active the elettricity from the panel
        if (other.gameObject.CompareTag("Elettricity"))
        {
            hasActivedElettricityText.gameObject.SetActive(true);
            StartCoroutine(ElettricityViewingTextAdviseCoroutine());
            hasOpenedElettricity = true;
            counterClickerButtonAusiliarVar.gameObject.SetActive(false);
        }
    }

    //Coroutine for viewing the advise of opening the elettricity of the mine.
    private IEnumerator ElettricityViewingTextAdviseCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        hasActivedElettricityText.gameObject.SetActive(false);
    }

    //----------------------
    //LEVEL4!
    //+

    //level4 triggerer active function.
    private void Level4FunctionTriggerer(Collider other)
    {
        //if the player triggers with the hole(ENDLEVEL4)
        if((other.gameObject.CompareTag("Hole")))
        {
            levelPassedTextAdvise.gameObject.SetActive(true); //level passed advise text.
            textAndButtons.gameObject.SetActive(false); // disactive the buttons(invisible).
            endLevelGO.gameObject.SetActive(true); //actove the menu for change the level.
            ausiliarGO1Look.gameObject.SetActive(acceptedTransition); //block of the movement input from the player.
            ausiliarGO2Move.gameObject.SetActive(acceptedTransition); //block of the looking visual input from the player.
            ausiliarGO03TimerStop.gameObject.SetActive(acceptedTransition); //block of the timer value.

        }
    }

    //-----------------------------------
    //LEVEL5
    //+

    //level5 triggerer active function.
    private void Level5FunctionTriggerer(Collider other)
    {
        if (ausiliarOneTimeCalledCollision == false)
        {
            if (other.gameObject.CompareTag("SeniorMan")) // if the player is near the old man and trigger him
            {
                Debug.Log("SeniorMan is collided with the view of the player.");
                if (hasTheParchment == false) //if the player doesn't have the parchment and collides the first time
                {
                    if (ausiliarCollisionWithOldMan == false) //if the collision happens the first time
                    {
                        textAdviseSeniorManDoesntHaveTheParchment.gameObject.SetActive(true); //activing the visualization of the text. 
                        StartCoroutine(SeniorManAdvice()); //time of viewing the text advice that the player doesn't have the parchment .
                    }
                    else if (ausiliarCollisionWithOldMan == true) //else if the collision happens the second and after times
                    {
                        textAdviseSeniorManHasntTheParchmentSecondCollision.gameObject.SetActive(true); //activing the visualization of the text. 
                        StartCoroutine(SeniorManAdvice()); //time of viewing the text advice that the player has the parchment .
                    }
                }
            }
            ausiliarOneTimeCalledCollision = true;
        }
    }

    private IEnumerator SeniorManAdvice()
    {
        yield return new WaitForSeconds(6.5f);
        if ((hasTheParchment == false) && (ausiliarCollisionWithOldMan == false))
        {
            textAdviseSeniorManDoesntHaveTheParchment.gameObject.SetActive(false);
            ausiliarCollisionWithOldMan = true;
            ausiliarOneTimeCalledCollision = false;
        }
        else if ((hasTheParchment == false) && (ausiliarCollisionWithOldMan == true))
        {
            textAdviseSeniorManHasntTheParchmentSecondCollision.gameObject.SetActive(false);
            ausiliarOneTimeCalledCollision = false;
        }
    }

    //------------------------------------
    //LEVEL6
    //+

    //level6 triggerer active function.
    private void Level6FunctionTriggerer(Collider other)
    {

    }
}
