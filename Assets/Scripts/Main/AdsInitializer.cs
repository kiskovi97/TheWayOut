using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace TheWayOut.Main
{
    public class AdsInitializer : MonoBehaviour
    {
        private BannerView bannerView;
        private RewardedAd rewardedAd;

        private static AdsInitializer Instance { get; set; }

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
                if (bannerView != null)
                    bannerView.Destroy();
            }
        }

        public void InitializeAds()
        {
            MobileAds.Initialize(OnInitializationComplete);
        }

        private void OnInitializationComplete(InitializationStatus obj)
        {
            Debug.Log("Unity Ads initialization complete.");
            this.RequestBanner();
        }

        private void RequestBanner()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-5964647989848848/9613823452";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-5964647989848848/9613823452";
#else
            string adUnitId = "unexpected_platform";
#endif

#if !UNITY_EDITOR
            // Create a 320x50 banner at the top of the screen.
            this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
            // Called when an ad request has successfully loaded.
            this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerView.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerView.OnAdClosed += this.HandleOnAdClosed;


            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            this.bannerView.LoadAd(request);
#endif

        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.LoadAdError);
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        public static void LoadAd(Action<bool> onFinshed)
        {
            Instance._LoadAdd(onFinshed);
        }

        private List<Action<bool>> onFinished = new List<Action<bool>>();

        private void _LoadAdd(Action<bool> onFinished)
        {
            this.onFinished.Add(onFinished);
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-5964647989848848/7971786332";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-5964647989848848/7971786332";
#else
            string adUnitId = "unexpected_platform";
#endif


#if !UNITY_EDITOR
            this.rewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request failed to load.
            this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad request failed to show.
            this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(request);
#else
            SendOneTimemessage(true);
#endif
        }

        private void HandleRewardedAdClosed(object sender, EventArgs e)
        {
            SendOneTimemessage(false);
        }

        private void HandleUserEarnedReward(object sender, Reward e)
        {
            SendOneTimemessage(true);
        }

        private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
        {
            Debug.LogWarning("HandleRewardedAdFailedToShow: " + e.AdError);
            SendOneTimemessage(false);
        }

        private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Debug.LogWarning("HandleRewardedAdFailedToLoad: " + e.LoadAdError);
            SendOneTimemessage(false);
        }

        private void SendOneTimemessage(bool value)
        {
            foreach(var action in onFinished)
            {
                action?.Invoke(value);
            }
            onFinished.Clear();
        }
    }
}
