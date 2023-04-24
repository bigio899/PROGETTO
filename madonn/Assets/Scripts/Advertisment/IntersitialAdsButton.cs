using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntersitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    private string stringActualScene;
    private int numberActualScene;

    private GameObject backMenu;
    private GameObject nextLevelVerifier;
    //ausiliar
    private int ausiliar = 0;

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
        Debug.Log("Loaded Intersitial Ad");
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
        stringActualScene = SceneManager.GetActiveScene().name; //get the name of the scene in esecution. 
        if (stringActualScene == "SampleScene")
        {
            numberActualScene = 0; //menu.
            if (GameObject.Find("isPlayButton").activeInHierarchy == true)
            {
                GameObject.Find("isPlayButton").SetActive(false);
                SceneManager.LoadScene(1); //load the level 1 scene.
            }
            else if (GameObject.Find("isResumeButton") == true)
            {
                GameObject.Find("isResumeButton").SetActive(false);
                SceneManager.LoadScene(DataPersistence.instanceDataPersistence.levelAvancement);
            }

            ausiliar = 1;
            backMenu = GameObject.Find("backMenuVerifier");
            nextLevelVerifier = GameObject.Find("nextLevelVerifier");
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

        if ((backMenu.activeSelf == false) && (stringActualScene != "SampleScene"))
        {
            backMenu.gameObject.SetActive(true);
            SceneManager.LoadScene(0);
        }

        else if ((nextLevelVerifier.activeSelf == false) && (stringActualScene != "SampleScene"))
        {
            Debug.Log("nextLevelVerifier called correctly.");
            nextLevelVerifier.SetActive(true);
            DataPersistence.instanceDataPersistence.levelAvancement = (numberActualScene + 1);
            SceneManager.LoadScene(numberActualScene + 1);
        }
 
    }

}