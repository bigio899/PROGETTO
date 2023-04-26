using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsGameObject : MonoBehaviour
{
    //declaration gameobjects's variables
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;

    //variable used for the animation of the player at the main menu UI.
    public GameObject theNPC;

    //variable that's used for the background music.
    [SerializeField] private GameObject musicGameObject;

    //variable that is used to contain the loading subscene.
    [SerializeField] private GameObject loadingSubScene;  //loading screen gameobject. 

    private void Start()
    {
        DataPersistence.instanceDataPersistence.LoadLevelAvancementFunction();
    }
    //function that is performed when the player click the Option button. 
    public void OnClickOptionButton()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    //function that is performed when the player click the Back To Menu button from the option setting.
    public void OnClickBackToMenu()
    {
        mainMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }

    //function that is performed when the player click the Play button.
    public void ChangeSceneFromMenuToLevelOne()
    {
        musicGameObject.gameObject.SetActive(false);
        DataPersistence.instanceDataPersistence.levelAvancement = 1;
        DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction();
    }

    //function that is performed when the player click the Resume button.
    public void ChangeSceneFromMenuToLevelAvancement()
    {
        musicGameObject.gameObject.SetActive(false);
        DataPersistence.instanceDataPersistence.SaveLevelAvancementFunction();
    }
}