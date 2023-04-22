using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntersitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    [SerializeField] private GameObject loadingSubScene;  //loading screen gameobject. 

    private string stringActualScene;
    private int numberActualScene;

    private bool isPlayButton = false; //this bool is used for verify in Menu that the button clicked is button.
    private bool backMenuVerifier = false; //this bool is used for verify in levels that the button clicked is "BackToMenuButton".
    private bool nextLevelVerifier = false; //this bool is used for verify in levels that the button clicked is "NextLevel".

    //GAMEOBJECT TO DISACTIVE END LEVEL
    [SerializeField] private GameObject endLevel;
    [SerializeField] private GameObject gamebuttonText;
    [SerializeField] private GameObject textAdvisePassedLevel;
    [SerializeField] private Image keyIconImage; //variable where's comtained the icon of the key.
    [SerializeField] private GameObject clickerButton;
    [SerializeField] private GameObject backMenuFromPlaying;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject batteryIcon;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }

    //function that is called after the end of the advertisment.
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
    {
        Destroy(this.gameObject);
        loadingSubScene.gameObject.SetActive(true);
        stringActualScene = SceneManager.GetActiveScene().name; //get the name of the scene in esecution. 
        if (stringActualScene == "SampleScene")
        {
            numberActualScene = 0; //menu.
            if (isPlayButton == true)
            {
                SceneManager.LoadScene(1); //load the level 1 scene.
                isPlayButton = false;
            }
            else if (isPlayButton == false)
            {
                SceneManager.LoadScene(numberActualScene + DataPersistence.instanceDataPersistence.levelAvancement);
            }
        }

        else if (stringActualScene == "Level1")
        {
            numberActualScene = 1; //level1.
        }

        else if (stringActualScene == "Level2") 
        {
            numberActualScene = 2; //level2.
        }

        else if(stringActualScene == "Level3")
        {
            numberActualScene = 3; //level3.
        }

        if (backMenuVerifier == true)
        {
            loadingSubScene.gameObject.SetActive(true);
            SceneManager.LoadScene(0);
        }
        else if (nextLevelVerifier == true)
        {
            loadingSubScene.gameObject.SetActive(true);
            DataPersistence.instanceDataPersistence.levelAvancement = (numberActualScene + 1);
            SceneManager.LoadScene(numberActualScene + 1);
        }
 
    }

    //function that starts id the player click on the play button. 
    public void PlayVerifier()
    {
        isPlayButton = true; //the value assigned is positive.
    }

    //(LEVELS)function that starts if the player click on the "BackToMenuButton".
    public void BackMenuVerifier()
    {
        clickerButton.gameObject.SetActive(false);
        timer.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        batteryIcon.gameObject.SetActive(false);
        backMenuFromPlaying.gameObject.SetActive(false);
        keyIconImage.gameObject.SetActive(false);
        endLevel.gameObject.SetActive(false);
        gamebuttonText.gameObject.SetActive(false);
        textAdvisePassedLevel.gameObject.SetActive(false);
        backMenuVerifier = true; // the value assigned is changed to true.
    }

    //(LEVELS)function that starts if the player click on the "NextlevelButton".
    public void NextLevelVerifier()
    {
        clickerButton.gameObject.SetActive(false);
        timer.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        batteryIcon.gameObject.SetActive(false);
        backMenuFromPlaying.gameObject.SetActive(false);
        keyIconImage.gameObject.SetActive(false);
        endLevel.gameObject.SetActive(false);
        gamebuttonText.gameObject.SetActive(false);
        textAdvisePassedLevel.gameObject.SetActive(false);
        nextLevelVerifier = true; //the value assigned is changed to true.
    }
}