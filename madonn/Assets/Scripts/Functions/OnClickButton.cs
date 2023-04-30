using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickButton : MonoBehaviour
{

    public void BackMenuFunction()
    {
        GameObject.Find("LoadingSubScene").SetActive(true);
        IntersitialAdsButton interstitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>(); //reference of intersitial button script.
        GameObject.Find("backMenuVerifier").SetActive(false);
        interstitialAdsButton.ShowAd();
        interstitialAdsButton.LoadAd();
    }

    public void RewardAdsFunction()
    {
        GameObject.Find("LoadingSubScene").SetActive(true);
        RewardedAdsButton rewardAdsFunction = GameObject.Find("Advertisement").GetComponent<RewardedAdsButton>();
        GameObject.Find("FailureMenu").SetActive(false);
        rewardAdsFunction.ShowAd();
        rewardAdsFunction.LoadAd();
    }

    public void NextLevel()
    {
        GameObject.Find("LoadingSubScene").SetActive(true);
        IntersitialAdsButton intersitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>();
        GameObject.Find("nextLevelVerifier").SetActive(false);
        intersitialAdsButton.ShowAd();
        intersitialAdsButton.LoadAd();
    }

}
