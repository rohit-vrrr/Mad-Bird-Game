using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private string playStoreID = "3759193";
    private string interstitialAd = "video";

    bool isTestAd = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Advertisement.Initialize(playStoreID, isTestAd);
    }

    public void PlayInterstitialAd()                                // Showing interstitial Ad
    {
        if(PlayerPrefs.HasKey("AdCount"))                           // Checking for AdCount
        {
            PlayerPrefs.SetInt("AdCount", 
                PlayerPrefs.GetInt("AdCount") + 1);
            if(PlayerPrefs.GetInt("AdCount") == 3)                  // If AdCount is 2, then show Ad
            {
                if(Advertisement.IsReady(interstitialAd))
                {
                    Advertisement.Show(interstitialAd);
                    PlayerPrefs.SetInt("AdCount", 0);               // Resetting AdCount
                }
                else
                {
                    Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("AdCount", 1);                       // Initializing AdCount
        }
    }
}
