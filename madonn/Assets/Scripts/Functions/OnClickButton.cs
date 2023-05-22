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
    }

    public void RewardAdsFunction()
    {
        Debug.Log("Rewarded are called.");
        RewardedAdsButton rewardAdsFunction = GameObject.Find("Advertisement").GetComponent<RewardedAdsButton>();
        rewardAdsFunction.ShowAd();
    }

    public void NextLevel()
    {
        GameObject.Find("LoadingSubScene").SetActive(true);
        IntersitialAdsButton intersitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>();
        GameObject.Find("nextLevelVerifier").SetActive(false);
        intersitialAdsButton.ShowAd();
    }

}
