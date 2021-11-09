using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;

public class YodoOne : MonoBehaviour
{
    public static YodoOne yodoOne;
    public bool isCOPPA;

    private void Awake()
    {
        //definindo 
        if (yodoOne == null)
        {
            yodoOne = this;
        }
        else if (yodoOne != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    public void StartYodo()
    {
        Yodo1U3dMas.SetCOPPA(isCOPPA);
        Yodo1U3dMas.InitializeSdk();
        SetDelegates();
        StartCoroutine(ShowBanner());
    }
    public IEnumerator ShowBanner()
    {
        //esperar um tempo para iniciar
        yield return new WaitForSeconds(1.0f);

        if (Yodo1U3dMas.IsBannerAdLoaded())
        {
            //alinhando
            //detectar se está no menu ou gamepaly
            if (!GameManager._gameManager._gamePlay)
            {
                int align = Yodo1U3dBannerAlign.BannerTop | Yodo1U3dBannerAlign.BannerLeft;
                Yodo1U3dMas.ShowBannerAd(align);
            }
            else
            {
                int align = Yodo1U3dBannerAlign.BannerBottom;
                Yodo1U3dMas.ShowBannerAd(align);
            }
        }
        else
        {
            StartCoroutine(ShowBanner());
        }
    }

    public void ShowInterstitial()
    {
        if (Yodo1U3dMas.IsInterstitialAdLoaded())
        {
            Yodo1U3dMas.ShowInterstitialAd();
            SetDelegates();
        }
    }
    public void DismissBanner()
    {
        Yodo1U3dMas.DismissBannerAd();
    }
    public void SetDelegates()
    {
        //Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        //{
        //    Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

        //    if (success)
        //    {
        //        StartCoroutine(ShowBanner());
        //    }
        //    else
        //    {

        //    }
        //});
        //banner
        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });

        //interstitial
        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }
        });
    }


}
