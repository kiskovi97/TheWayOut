using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace TheWayOut.Main
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iOSGameId;
        [SerializeField] bool _testMode = true;

        private string _gameId;

        private static AdsInitializer Instance { get; set; }

        private static List<Action<bool>> OnFinishedListeners = new List<Action<bool>>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeAds();
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

        public void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
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
}
