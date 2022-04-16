﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TheWayOut.Main;

public class BannerAdExample : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    string _androidAdUnitId = "Banner_Android";
    string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    bool isLoaded = false;

    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    private void Update()
    {
        if (Advertisement.isInitialized && !isLoaded)
        {
            LoadBanner();
        }
    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        if (!Advertisement.Banner.isLoaded)
        {
            isLoaded = true;
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(_adUnitId, options);
        }
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        AdsShower.LoadAd(null);
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        // Clean up the listeners:
        HideBannerAd();
    }
}