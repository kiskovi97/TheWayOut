using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace TheWayOut.Main
{
    public class AdsShower : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
        [SerializeField] string _androidAdUnitIdSpecial = "Rewarded_Android";
        [SerializeField] string _iOsAdUnitIdSpecial = "Rewarded_iOS";
        string _adUnitId;
        string _adUnitBigId;
        private static List<Action<bool>> OnFinishedListeners = new List<Action<bool>>();

        public static void LoadAdSpecial(Action<bool> OnFinished)
        {
            if (Instance != null)
            {
                if (OnFinished != null)
                    OnFinishedListeners.Add(OnFinished);
                Instance.LoadSpecialAdd();
            }
        }

        private static AdsShower Instance { get; set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                // Get the Ad Unit ID for the current platform:
                _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                    ? _iOsAdUnitId
                    : _androidAdUnitId;
                _adUnitBigId = (Application.platform == RuntimePlatform.IPhonePlayer)
                    ? _iOsAdUnitIdSpecial
                    : _androidAdUnitIdSpecial;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public static void LoadAd(Action<bool> OnFinished)
        {
            if (Instance != null)
            {
                if (OnFinished != null)
                    OnFinishedListeners.Add(OnFinished);
                Instance.LoadAd();
            }
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        private void LoadSpecialAdd()
        {
            Debug.Log("Loading Ad: " + _adUnitBigId);
            Advertisement.Load(_adUnitBigId, this);
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
            Debug.Log($"Unity Ads Loaded: {adUnitId}");
            ShowAd();
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
            SendOneTimeMessage(false);
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
            SendOneTimeMessage(false);
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            Debug.Log($"OnUnityAdsShowStart: {adUnitId}");
        }
        public void OnUnityAdsShowClick(string adUnitId)
        {
            Debug.Log($"OnUnityAdsShowClick: {adUnitId}");
        }
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log($"OnUnityAdsShowComplete: {adUnitId} {showCompletionState}");
            SendOneTimeMessage(true);
        }

        private static void SendOneTimeMessage(bool success)
        {
            foreach (var finished in OnFinishedListeners)
            {
                finished?.Invoke(success);
            }
            OnFinishedListeners.Clear();
        }
    }
}
