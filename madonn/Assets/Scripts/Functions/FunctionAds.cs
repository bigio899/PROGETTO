using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FunctionAds : MonoBehaviour
{
    //GAMEOBJECT TO DISACTIVE END LEVEL
    [SerializeField] public GameObject endLevel;
    [SerializeField] public GameObject gamebuttonText;
    [SerializeField] public GameObject textAdvisePassedLevel;
    [SerializeField] public Image keyIconImage; //variable where's comtained the icon of the key.
    [SerializeField] public GameObject clickerButton;
    [SerializeField] public GameObject backMenuFromPlaying;
    [SerializeField] public GameObject timer;
    [SerializeField] public GameObject batteryIcon;

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
        batteryIcon.gameObject.SetActive(false);
        backMenuFromPlaying.gameObject.SetActive(false);
        keyIconImage.gameObject.SetActive(false);
        endLevel.gameObject.SetActive(false);
        gamebuttonText.gameObject.SetActive(false);
        textAdvisePassedLevel.gameObject.SetActive(false);

        loadingSubScene.gameObject.SetActive(true);
        CallLoadingFunctionAdvertisement();
    }

    //(LEVELS)function that starts if the player click on the "NextlevelButton".
    public void NextLevel()
    {
        clickerButton.gameObject.SetActive(false);
        timer.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        batteryIcon.gameObject.SetActive(false);
        backMenuFromPlaying.gameObject.SetActive(false);
        keyIconImage.gameObject.SetActive(false);
        endLevel.gameObject.SetActive(false);
        gamebuttonText.gameObject.SetActive(false);
        textAdvisePassedLevel.gameObject.SetActive(false);

        loadingSubScene.gameObject.SetActive(true);
        CallLoadingFunctionAdvertisement();
    }

    private void CallLoadingFunctionAdvertisement()
    {
        IntersitialAdsButton interstitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>();
        interstitialAdsButton.LoadAd();

        RewardedAdsButton rewardedAdsButton = GameObject.Find("Advertisement").GetComponent<RewardedAdsButton>();
        rewardedAdsButton.LoadAd();
    }
}
