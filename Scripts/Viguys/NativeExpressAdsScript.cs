using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeExpressAdsScript : MonoBehaviour
{
    public string adID;
    public Shader unlitShader;

    private NativeAppInstallAd appInstallAd;
    private AdLoader adLoader;
    private bool appInstallAdLoaded = false;
    private bool isReadyToShow = false;
    private bool canShow = false;

    private GameObject iconObj;
    private GameObject adChoicesObj;
    private GameObject headLineObj;
    private GameObject priceObj;
    private GameObject storeObj;
    private GameObject calToActionObj;

    public Text text;

    public Text log;

    public static NativeExpressAdsScript nativeExpressAds;

    public void setAdID(string adID)
    {
        Hide();
        appInstallAdLoaded = false;
        string id = adID;
        adLoader = new AdLoader.Builder(id)
            .ForNativeAppInstallAd()
            .Build();

        adLoader.OnNativeAppInstallAdLoaded += this.HandleNativeAppInstallAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        RequestNativeAd();
    }

    public void Show()
    {

        if (text != null) text.text = "Show " + canShow + " " + isReadyToShow + " " + appInstallAd;
        canShow = true;
        if (isReadyToShow)
        {
            gameObject.SetActive(true);
            if (iconObj != null)
                iconObj.SetActive(true);
            if (adChoicesObj != null)
                adChoicesObj.SetActive(true);
            if (headLineObj != null)
                headLineObj.SetActive(true);
            if (priceObj != null)
                priceObj.SetActive(true);
            if (storeObj != null)
                storeObj.SetActive(true);
            if (calToActionObj != null)
                calToActionObj.SetActive(true);
        }
    }

    public void Hide()
    {
        canShow = false;
        if (gameObject)
            gameObject.SetActive(false);
        if (iconObj != null)
            iconObj.SetActive(false);
        if (adChoicesObj != null)
            adChoicesObj.SetActive(false);
        if (headLineObj != null)
            headLineObj.SetActive(false);
        if (priceObj != null)
            priceObj.SetActive(false);
        if (storeObj != null)
            storeObj.SetActive(false);
        if (calToActionObj != null)
            calToActionObj.SetActive(false);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        if (iconObj != null)
            Destroy(iconObj);
        if (adChoicesObj != null)
            Destroy(adChoicesObj);
        if (headLineObj != null)
            Destroy(headLineObj);
        if (priceObj != null)
            Destroy(priceObj);
        if (storeObj != null)
            Destroy(storeObj);
        if (calToActionObj != null)
            Destroy(calToActionObj);
        nativeExpressAds = null;
    }

    public void RequestNativeAd()
    {
        adLoader.LoadAd(new AdRequest.Builder().Build());
    }

    private void Awake()
    {


        if (nativeExpressAds)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            nativeExpressAds = this;
            string id = (adID == null || adID == "" ? "ca-app-pub-6416530778400331/1695690401" : adID);
            adLoader = new AdLoader.Builder(id)
                .ForNativeAppInstallAd()
                .Build();

            adLoader.OnNativeAppInstallAdLoaded += this.HandleNativeAppInstallAdLoaded;
            adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
            RequestNativeAd();
            Hide();
            if (text != null) text.text = "Awake";
        }
    }

    private void HandleNativeAppInstallAdLoaded(object sender, NativeAppInstallAdEventArgs args)
    {
        if (text != null) text.text = ("App install ad loaded.");
        this.appInstallAd = args.nativeAd;
        this.appInstallAdLoaded = true;

        //Update();
        gameObject.SetActive(true);
        if (text != null) text.text = "App install ad loaded. " + canShow + " " + isReadyToShow + " " + appInstallAd;
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (text != null) text.text = ("Native ad failed to load: " + args.Message);
    }

    private void Start()
    {
        //Image icon = transform.Find("Icon").gameObject.GetComponent<Image>();
        //Image choices = transform.Find("Adchoice").gameObject.GetComponent<Image>();
        //Text name = transform.Find("Name").gameObject.GetComponent<Text>();
        //Text priceT = transform.Find("Price").gameObject.GetComponent<Text>();
        //Image storeI = transform.Find("Store").gameObject.GetComponent<Image>();
        //Button play = transform.Find("Button").gameObject.GetComponent<Button>();
    }

    void Update()
    {
        if (appInstallAdLoaded)
        {
            appInstallAdLoaded = false;
            // update o day
            isReadyToShow = true;
            try
            {
                if (iconObj != null)
                    DestroyImmediate(iconObj);
                if (adChoicesObj != null)
                    DestroyImmediate(adChoicesObj);
                if (headLineObj != null)
                    DestroyImmediate(headLineObj);
                if (priceObj != null)
                    DestroyImmediate(priceObj);
                if (storeObj != null)
                    DestroyImmediate(storeObj);
                if (calToActionObj != null)
                    DestroyImmediate(calToActionObj);
                if (log != null) log.text = "Destroy done";

                string headlineText = this.appInstallAd.GetHeadlineText();
                if (log != null) log.text = "Fetch headline done";
                Texture2D iconTexture = this.appInstallAd.GetIconTexture();
                if (log != null) log.text = "Fetch iconTexture done";
                Texture2D img = appInstallAd.GetImageTextures()[0];
                if (log != null) log.text = "Fetch ImageTextures done";
                string callToActionText = this.appInstallAd.GetCallToActionText();
                if (log != null) log.text = "Fetch CallToAction done";
                Texture2D adchoices = null;
                try
                {
                    adchoices = appInstallAd.GetAdChoicesLogoTexture();
                }
                catch (Exception e)
                {
                    adchoices = null;
                }

                if (log != null) log.text = "Fetch AdChoices done";
                string bodyText = null;
                try
                {
                    bodyText = this.appInstallAd.GetBodyText();
                }
                catch (Exception e)
                {
                    bodyText = null;
                }

                if (log != null) log.text = "Fetch BodyText done";
                string price = null;
                try
                {
                    price = this.appInstallAd.GetPrice();
                }
                catch (Exception e)
                {
                    price = null;
                }

                if (log != null) log.text = "Fetch Price done";
                string store = null;
                try
                {
                    store = this.appInstallAd.GetStore();
                }
                catch (Exception e)
                {
                    store = null;
                }



                if (log != null) log.text = "Fetch Store done";

                Vector3 localS = transform.localScale;

                Image icon = transform.Find("Icon").gameObject.GetComponent<Image>();
                Image choices = transform.Find("Adchoice").gameObject.GetComponent<Image>();
                Text name = transform.Find("Name").gameObject.GetComponent<Text>();
                Text priceT = transform.Find("Price").gameObject.GetComponent<Text>();
                Image storeI = transform.Find("Store").gameObject.GetComponent<Image>();
                Button play = transform.Find("Button").gameObject.GetComponent<Button>();

                if (log != null) log.text = "Find Component done";

                iconObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                iconObj.transform.position = new Vector3(icon.transform.position.x, icon.transform.position.y, -1);

                Material material = new Material(unlitShader);
                iconObj.GetComponent<Renderer>().material = material;
                material.mainTexture = iconTexture;
                standardlize(new Vector3(icon.rectTransform.sizeDelta.x, icon.rectTransform.sizeDelta.y), iconObj.GetComponent<Renderer>().bounds.size, iconObj, localS);
                appInstallAd.RegisterIconImageGameObject(iconObj);

                if (log != null) log.text = "Icon done";

                name.text = headlineText;
                headLineObj = new GameObject();
                TextMesh mesh = headLineObj.AddComponent<TextMesh>();
                mesh.characterSize = 0.25f;
                mesh.anchor = TextAnchor.MiddleCenter;
                mesh.color = new Color(1f, 1f, 1f, 0);
                mesh.text = callToActionText;//"Nhi";
                headLineObj.AddComponent<BoxCollider>().size = ScreenToWorldSize2D(new Vector3(name.rectTransform.sizeDelta.x, name.rectTransform.sizeDelta.y));
                headLineObj.transform.localScale = localS;
                headLineObj.transform.position = new Vector3(name.transform.position.x, name.transform.position.y, -1);
                appInstallAd.RegisterHeadlineTextGameObject(headLineObj);

                if (log != null) log.text = "HeadLine done";
                if (price != null)
                {
                    priceT.text = price;
                    priceObj = new GameObject();
                    mesh = priceObj.AddComponent<TextMesh>();
                    mesh.characterSize = 0.25f;
                    mesh.anchor = TextAnchor.MiddleCenter;
                    mesh.color = new Color(1f, 1f, 1f, 0);
                    mesh.text = price;//"Nhi";
                    priceObj.AddComponent<BoxCollider>().size = ScreenToWorldSize2D(new Vector3(priceT.rectTransform.sizeDelta.x, priceT.rectTransform.sizeDelta.y));
                    priceObj.transform.localScale = localS;
                    priceObj.transform.position = new Vector3(priceT.transform.position.x, priceT.transform.position.y, -1); ;
                    appInstallAd.RegisterPriceGameObject(priceObj);
                }
                else
                {
                    priceT.text = "";
                }

                if (log != null) log.text = "Price done";

                if (store != null)
                {
                    if (store == "Google Play")
                    {
                        storeI.sprite = Resources.Load<Sprite>("Viguys/appstore2");
                    }
                    else storeI.sprite = Resources.Load<Sprite>("Viguys/appstore1");
                    storeObj = new GameObject();
                    mesh = storeObj.AddComponent<TextMesh>();
                    mesh.characterSize = 0.25f;
                    mesh.anchor = TextAnchor.MiddleCenter;
                    mesh.color = new Color(1f, 1f, 1f, 0);
                    mesh.text = store;//"Nhi";
                    storeObj.AddComponent<BoxCollider>().size = ScreenToWorldSize2D(new Vector3(storeI.rectTransform.sizeDelta.x, storeI.rectTransform.sizeDelta.y));
                    storeObj.transform.localScale = localS;
                    storeObj.transform.position = new Vector3(storeI.transform.position.x, storeI.transform.position.y, -1); ;

                    appInstallAd.RegisterStoreGameObject(storeObj);
                }

                if (log != null) log.text = "Store done";

                play.transform.Find("Text").gameObject.GetComponent<Text>().text = callToActionText;

                calToActionObj = new GameObject();
                mesh = calToActionObj.AddComponent<TextMesh>();
                mesh.characterSize = 0.25f;
                mesh.anchor = TextAnchor.MiddleCenter;
                mesh.color = new Color(1f, 1f, 1f, 0f);
                mesh.text = callToActionText;//"Nhi";

                calToActionObj.AddComponent<BoxCollider>().size = ScreenToWorldSize2D(new Vector3(play.GetComponent<RectTransform>().sizeDelta.x, play.GetComponent<RectTransform>().sizeDelta.y));
                calToActionObj.transform.localScale = localS;
                calToActionObj.transform.position = new Vector3(play.transform.position.x, play.transform.position.y, -1);// play.transform.position;
                appInstallAd.RegisterCallToActionGameObject(calToActionObj);
                if (log != null) log.text = "CallAction done";

                if (adchoices != null)
                {
                    adChoicesObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    adChoicesObj.transform.position = new Vector3(choices.transform.position.x, choices.transform.position.y, -1);// choices.transform.position;

                    material = new Material(unlitShader);
                    adChoicesObj.GetComponent<Renderer>().material = material;
                    material.mainTexture = adchoices;

                    standardlize(new Vector3(choices.rectTransform.sizeDelta.x, choices.rectTransform.sizeDelta.y), adChoicesObj.GetComponent<Renderer>().bounds.size, adChoicesObj, localS);
                    appInstallAd.RegisterAdChoicesLogoGameObject(adChoicesObj);
                }



                if (log != null) log.text = "Adchoice done";

                if (canShow) Show(); else Hide();
                if (log != null) log.text = "Finish done";
            }
            catch (Exception e)
            {
                if (text != null) text.text = "" + e.Message;
            }



        }
    }

    private void standardlize(Vector3 size, Vector3 source, GameObject obj, Vector3 totalScale)
    {
        Vector3 target = ScreenToWorldSize2D(size);
        float height = target.y;
        float width = target.x; // basically height * screen aspect ratio
        float unitWidth = source.x;
        float unitHeight = source.y;

        if (text != null)
            text.text = ("standard" + unitHeight + " " + unitWidth + " " + width + " " + height + " " + totalScale.x + " " + totalScale.y);
        //obj.transform.localScale = new Vector3(-width / unitWidth , height / unitHeight , 1);
        obj.transform.localScale = new Vector3(-width / unitWidth * totalScale.x, height / unitHeight * totalScale.y, 1);
        obj.transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    private Vector3 ScreenToWorldSize2D(Vector3 size)
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

        float w = size.x * (width / Screen.width);
        float h = size.y * (height / Screen.height);

        return new Vector3(w, h);
    }
}
