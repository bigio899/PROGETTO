using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickButton : MonoBehaviour
{
    public RewardedAdsButton rewardedAdsButton; //reference of rewarded button script.
    public IntersitialAdsButton interstitialAdsButton; //reference of intersitial button script.
    public void BackMenuFunction()
    {
        IntersitialAdsButton interstitialAdsButton = GameObject.Find("Advertisement").GetComponent<IntersitialAdsButton>(); //reference of intersitial button script.
        interstitialAdsButton.ShowAd();
        GameObject.Find("backMenuVerifier").SetActive(false);
    }

}
