using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class IntersitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    [SerializeField] private GameObject loadingSubScene;  //loading screen gameobject. 

    private string stringActualScene;
    private int numberActualScene;

    private bool isPlay = false;

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
        stringActualScene = SceneManager.GetActiveScene().name; //get the name of the scene in esecution. 
        if (stringActualScene == "SampleScene")
        {
            numberActualScene = 0; //menu.
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

        loadingSubScene.gameObject.SetActive(true);
        if (isPlay == true)
        {
            SceneManager.LoadScene(1); //load the level 1 scene.
            isPlay = false;
        }
        else if (isPlay == false)
        {
            SceneManager.LoadScene(numberActualScene + DataPersistence.instanceDataPersistence.levelAvancement);
        }
    }

    //function that starts id the player click on the play button. 
    public void PlayVerifier()
    {
        isPlay = true; //the value assigned is positive.
    }
}