using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = false;
    private bool ausiliar = false;
    private string _gameId;

    public RewardedAdsButton rewardedAdsButton; //reference of rewarded button script.
    public IntersitialAdsButton interstitialAdsButton; //reference of intersitial button script.


    void Awake()
    {
        rewardedAdsButton = GameObject.Find("Advertisement").GetComponent<RewardedAdsButton>();
        interstitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>();
        InitializeAds(); //function that initialize the advertisement tool.
    }
    private void Update()
    {
        if (ausiliar == true)
        {
            ausiliar = !ausiliar;
            rewardedAdsButton.LoadAd();
            interstitialAdsButton.LoadAd();
        }
    }
    //function that initialize the advertisement tool.
    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId; 
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        ausiliar = true;
    }

    
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}