using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class NativeAdsScript : MonoBehaviour
{


    public Shader unlitShader;
    public string adsID = "";
    public Text text;
    public Text error;

    private CustomNativeTemplateAd customNativeTemplateAd;
    private AdLoader adLoader;

    private GameObject headline;
    private GameObject icon;
    public Image testImage;

    private GameObject exit = null;
    private GameObject interstials;
    private bool appInstallAdLoaded;
    private bool isReadyToShow;
    private NativeAppInstallAd appInstallAd;
    private NativeContentAd contentAd;
    private GameObject go;

    public void setAdID(string adID) {
        
        appInstallAdLoaded = false;
        string id = adID;
        adLoader = new AdLoader.Builder(id)
            .ForNativeAppInstallAd()
            .Build();

        adLoader.OnNativeAppInstallAdLoaded += this.HandleNativeAppInstallAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        RequestNativeAd();
    }

    public void RequestNativeAd()
    {
        isReadyToShow = false;
        adLoader.LoadAd(new AdRequest.Builder().Build());
        if (text != null)
            text.text = "AdLoadings";
        transform.parent.gameObject.SetActive(false);
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("Native ad failed to load: " + args.Message);
        if (text != null)

            text.text = "Native ad failed to load: " + args.Message;

    }


    private void HandleNativeContentAdLoaded(object sender, NativeContentAdEventArgs args)
    {
        MonoBehaviour.print("Content ad loaded." + Screen.width + " " + Screen.height + " " + Screen.orientation.ToString());
        this.contentAd = args.nativeAd;

    }


    private void Awake()
    {

        string id = (adsID == null || adsID == "" ? "ca-app-pub-6416530778400331/1695690401" : adsID);
        adLoader = new AdLoader.Builder(id)
            .ForNativeAppInstallAd()
            .Build();

        adLoader.OnNativeAppInstallAdLoaded += this.HandleNativeAppInstallAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        RequestNativeAd();

        createDialog();
        
    }

    // Use this for initialization
    void Start()
    {

        //Image img2 = this.transform.parent.gameObject.GetComponent<Image>();
        //img2.sprite = Resources.Load<Sprite>("Viguys/x");
        //transform.gameObject.GetComponent<RectTransform>().sizeDelta = this.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta;

    }


    Vector3 getLocalScale_(Vector3 size, Vector3 source)
    {
        Vector3 target = ScreenToWorldSize2D(size);
        return new Vector3(target.x / source.x, target.y / source.y);
    }

    Vector3 ScreenToWorldSize2D(Vector3 size)
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

        float w = size.x * (width / Screen.width);
        float h = size.y * (height / Screen.height);

        return new Vector3(w, h);
    }

    void createDialog()
    {



    }

    private void HandleNativeAppInstallAdLoaded(object sender, NativeAppInstallAdEventArgs args)
    {
        MonoBehaviour.print("App install ad loaded.");
        this.appInstallAd = args.nativeAd;
        this.appInstallAdLoaded = true;
        if (text != null)
            text.text = "App install ad loaded.";
        transform.parent.gameObject.SetActive(true);
    }

    public bool isReady()
    {
        return isReadyToShow;
    }


    public void displayAds()
    {
        if (isReadyToShow)
        {
            try
            {
                Texture2D img = appInstallAd.GetImageTextures()[0];
                if ((img.width < img.height))
                {
                    if (Screen.orientation.Equals(ScreenOrientation.Portrait) | Screen.orientation.Equals(ScreenOrientation.PortraitUpsideDown))
                    {
                        go = Instantiate(Resources.Load("Viguys/PPDialog")) as GameObject;
                        if (error != null)
                            error.text = "displayAd PP Dialog " + img.width + " " + img.height;
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Viguys/PLDialog")) as GameObject;
                        if (error != null)
                            error.text = "displayAd PL Dialog " + img.width + " " + img.height;
                    }
                }
                else
                {
                    if (Screen.orientation.Equals(ScreenOrientation.Portrait) | Screen.orientation.Equals(ScreenOrientation.PortraitUpsideDown))
                    {
                        go = Instantiate(Resources.Load("Viguys/LPDialog")) as GameObject;
                        if (error != null)
                            error.text = "displayAd LP Dialog " + img.width + " " + img.height;
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Viguys/LLDialog")) as GameObject;
                        if (error != null)
                            error.text = "displayAd LL Dialog " + img.width + " " + img.height;
                    }
                }


                
                go.transform.parent = transform.parent.parent;
                go.transform.localScale = Vector3.one;
                go.GetComponent<TestScript>().appInstallAd = appInstallAd;
                //				if (error != null)
                //					error.text = "Sao for what " + img.width+" "+img.height;

                if (error != null)
                    error.text = "displayAd " + img.width + " " + img.height;
            }
            catch (Exception e)
            {
                if (error != null)
                    error.text = "displayAd " + e.Message;
            }

            isReadyToShow = false;
            RequestNativeAd();
        }
        else
        {

            error.text = "Ads is not ready";
            RequestNativeAd();
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (this.appInstallAdLoaded)
        {
            if (icon != null)
            {
                icon.transform.parent = null;
            }
            if (this.headline != null)
                this.headline.transform.parent = null;
            this.appInstallAdLoaded = false;
            this.isReadyToShow = true;
            string headlineText = this.appInstallAd.GetHeadlineText();
            Texture2D iconTexture = this.appInstallAd.GetIconTexture();
            Texture2D img = appInstallAd.GetImageTextures()[0];
            //string callToActionText = this.appInstallAd.GetCallToActionText ();
            //Texture2D adchoices = appInstallAd.GetAdChoicesLogoTexture ();
            //string bodyText = this.appInstallAd.GetBodyText ();
            //string price = this.appInstallAd.GetPrice ();
            try
            {
                if (testImage != null)
                {
                    testImage.sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f), 100);
                    testImage.SetNativeSize();
                }
                Image img2 = this.transform.parent.gameObject.GetComponent<Image>();
                img2.sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), new Vector2(0.5f, 0.5f), 100);
                img2.transform.rotation = Quaternion.Euler(0, 0, 180);
                img2.transform.localScale = new Vector3(-1, 1, 1);
                img2.SetNativeSize();
                transform.gameObject.GetComponent<RectTransform>().sizeDelta = this.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta;
                //				if (text != null)
                //					text.text = "Updated";
            }
            catch (Exception e)
            {
                if (error != null)
                    error.text = e.Message + " " + e.Source;
            }


        }

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (go != null)
                go.GetComponent<TestScript>().exit();

        }

    }




    Texture2D CalculateTexture(int h, int w, float r, float cx, float cy, Texture2D sourceTex)
    {
        Color[] c = sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
        Texture2D b = new Texture2D(h, w);
        for (int i = 0; i < (h * w); i++)
        {
            int y = Mathf.FloorToInt(((float)i) / ((float)w));
            int x = Mathf.FloorToInt(((float)i - ((float)(y * w))));
            if (r * r >= (x - cx) * (x - cx) + (y - cy) * (y - cy))
            {
                b.SetPixel(x, y, c[i]);
            }
            else
            {
                b.SetPixel(x, y, Color.clear);
            }
        }
        b.Apply();
        return b;
    }
}
