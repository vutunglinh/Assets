using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
//using TMPro;

public class TestScript : MonoBehaviour
{

    public NativeAppInstallAd appInstallAd;


    private GameObject choices;
    private GameObject boundIcon;
    private GameObject icon;
    private GameObject headline;
    private GameObject body;
    private GameObject bigImage;
    private GameObject callToAction;
    public Text text;

    public int type = 0;

    // Use this for initialization

    void Awake()
    {
        //Image interstialsBG = transform.Find("InterstialsBG").gameObject.GetComponent<Image>();

        //Debug.Log("" + interstialsBG.transform.rotation.z);
    }

    void Start()
    {
        
        if (appInstallAd != null)
        {
            try
            {

                Sprite iconB = Resources.Load<Sprite>("Viguys/1c");
                if (this.text != null) this.text.text = "ICON B";
                bool isPortraitScreen = Screen.orientation.Equals(ScreenOrientation.Portrait) | Screen.orientation.Equals(ScreenOrientation.PortraitUpsideDown);

                Image iconBG = transform.Find("IconBG").gameObject.GetComponent<Image>();
                Image parentInterstials = transform.Find("InterstialsBG").gameObject.GetComponent<Image>();
                Image interstialsBG = transform.Find("Content").gameObject.GetComponent<Image>();
                Text name = transform.Find("Name").gameObject.GetComponent<Text>();
                Text slogan = transform.Find("Slogan").gameObject.GetComponent<Text>();
                Button play = transform.Find("Play").gameObject.GetComponent<Button>();
                Image adChoicesBG = transform.Find("AdChoicesBG").gameObject.GetComponent<Image>();
                if (this.text != null) this.text.text = "Get Component";

                string headlineText = this.appInstallAd.GetHeadlineText();
                Texture2D iconTexture = this.appInstallAd.GetIconTexture();
                Texture2D img = appInstallAd.GetImageTextures()[0];
                string callToActionText = this.appInstallAd.GetCallToActionText();

                Texture2D adchoices = null;
                try
                {
                    adchoices = appInstallAd.GetAdChoicesLogoTexture();
                }
                catch (Exception e)
                {
                    adchoices = null;
                }

                string bodyText = null;
                try
                {
                    bodyText = this.appInstallAd.GetBodyText();
                }
                catch (Exception e)
                {
                    bodyText = null;
                }

                string store = null;

                try
                {
                    store = this.appInstallAd.GetStore();
                }
                catch (Exception e)
                {
                    store = null;
                }

                //string price = this.appInstallAd.GetPrice();


                float bounderInc = 20;
                Shader unlit = Resources.Load<Shader>("Viguys/Unlit-Alpha");
                Texture2D sprite = Resources.Load<Texture2D>("Viguys/x");
                icon = GameObject.CreatePrimitive(PrimitiveType.Quad);
                icon.transform.position = new Vector3(iconBG.transform.position.x, iconBG.transform.position.y, -1);
                Material material = new Material(unlit);
                icon.GetComponent<Renderer>().material = material;
                sprite = iconTexture;
                material.mainTexture = sprite;
                standardlize(new Vector3(iconBG.rectTransform.sizeDelta.x - bounderInc, iconBG.rectTransform.sizeDelta.y - bounderInc), icon.GetComponent<Renderer>().bounds.size, sprite.width < sprite.height, isPortraitScreen, icon);


                appInstallAd.RegisterIconImageGameObject(icon);

                if (this.text != null) this.text.text = "ICON DONE";

                boundIcon = new GameObject();
                boundIcon.AddComponent<SpriteRenderer>().sprite = iconB;
                if (this.text != null) this.text.text = "BOUND SPRITE 1";
                boundIcon.transform.localScale = getLocalScale_(new Vector3(iconBG.rectTransform.sizeDelta.x, iconBG.rectTransform.sizeDelta.y), iconB.bounds.size);
                if (this.text != null) this.text.text = "BOUND SPRITE DONE";



                bigImage = GameObject.CreatePrimitive(PrimitiveType.Quad);
                bigImage.transform.position = new Vector3(interstialsBG.transform.position.x, interstialsBG.transform.position.y, -1);
                material = new Material(unlit);
                bigImage.GetComponent<Renderer>().material = material;
                sprite = img;
                material.mainTexture = sprite;

                float bounderIN = 0;

                
                switch (type) {
                    case 0: // LL
                        
                        break;
                    case 1: // LP
                        break;
                    case 2: // PP
                        break;
                    case 3: // PL
                        break;
                }

                float width = interstialsBG.rectTransform.sizeDelta.x;
                float height = interstialsBG.rectTransform.sizeDelta.y;
                //float ratioI = img.width / img.height;
                //float ratioB = interstialsBG.rectTransform.sizeDelta.x / interstialsBG.rectTransform.sizeDelta.y;
                //if (ratioI < ratioB)
                //{
                //    width = height * ratioI;
                //}
                //else height = width / ratioI;
                //float scaleX = width / interstialsBG.rectTransform.sizeDelta.x;
                //float scaleY = height / interstialsBG.rectTransform.sizeDelta.y;

                if (interstialsBG.transform.rotation.z > 0)
                    standardlize(new Vector3(width, height), bigImage.GetComponent<Renderer>().bounds.size, sprite.width < sprite.height, isPortraitScreen, bigImage);

                else
                    standardlize(new Vector3(width, height), bigImage.GetComponent<Renderer>().bounds.size, sprite.width < sprite.height, isPortraitScreen, bigImage);
                //interstialsBG.transform.localScale = new Vector3(scaleX * interstialsBG.transform.localScale.x, scaleY * interstialsBG.transform.localScale.y);
                //parentInterstials.transform.localScale = new Vector3(scaleX * parentInterstials.transform.localScale.x, scaleY * parentInterstials.transform.localScale.y);

                List <GameObject> list = new List<GameObject>();
                list.Add(bigImage.gameObject);
                appInstallAd.RegisterImageGameObjects(list);



                if (adchoices != null)
                {
                    choices = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    choices.transform.position = new Vector3(adChoicesBG.transform.position.x, adChoicesBG.transform.position.y, -1);
                    material = new Material(unlit);
                    choices.GetComponent<Renderer>().material = material;
                    sprite = adchoices;
                    material.mainTexture = sprite;
                    adChoicesBG.rectTransform.sizeDelta = new Vector2(adchoices.width, adchoices.height);
                    standardlize(new Vector3(adChoicesBG.rectTransform.sizeDelta.x, adChoicesBG.rectTransform.sizeDelta.y), choices.GetComponent<Renderer>().bounds.size, sprite.width < sprite.height, isPortraitScreen, choices);
                    appInstallAd.RegisterAdChoicesLogoGameObject(choices);
                }





                if (this.text != null) this.text.text = "LOGO ACHOICES DONE";

                name.text = headlineText;


                try
                {
                    slogan.text = (bodyText == null || bodyText == "") ? "In " + store : bodyText;
                }
                catch (Exception e)
                {
                }


                Text text = transform.Find("Play").Find("Text").GetComponent<Text>();
                text.text = callToActionText;

                callToAction = new GameObject();
                callToAction.AddComponent<TextMesh>();
                callToAction.GetComponent<TextMesh>().characterSize = 0.25f;
                callToAction.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
                callToAction.GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, 0);
                callToAction.GetComponent<TextMesh>().text = callToActionText;//"Nhi";
                callToAction.AddComponent<BoxCollider>().size = ScreenToWorldSize2D(new Vector3(play.GetComponent<RectTransform>().sizeDelta.x, play.GetComponent<RectTransform>().sizeDelta.y));
                callToAction.transform.position = play.transform.position;
                appInstallAd.RegisterCallToActionGameObject(callToAction);

                if (text != null) text.text = width+" "+height;
            }
            catch (Exception e)
            {
                if (text != null) text.text = e.Message;
            }


        }


    }

    Vector3 getLocalScale_(Vector3 size, Vector3 source)
    {
        Vector3 target = ScreenToWorldSize2D(size);



        return new Vector3(target.x / source.x, target.y / source.y);
    }

    void standardlize(Vector3 size, Vector3 source, bool isPortraitImage, bool isPotraitScreen, GameObject obj, bool isAdmob = true)
    {
        Vector3 target = ScreenToWorldSize2D(size);

        float height = target.y;
        float width = target.x; // basically height * screen aspect ratio
        float unitWidth = source.x;
        float unitHeight = source.y;

        obj.transform.localScale = new Vector3(-width / unitWidth, height / unitHeight, 1);

        obj.transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    Vector3 ScreenToWorldSize2D(Vector3 size)
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio

        float w = size.x * (width / Screen.width);
        float h = size.y * (height / Screen.height);

        return new Vector3(w, h);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void exit()
    {
        if (gameObject)
        transform.gameObject.SetActive(false);
        if (choices)
        Destroy(choices);
        if (icon)
        Destroy(icon);
        if (headline)
        Destroy(headline);
        if (body)
        Destroy(body);
        if (bigImage)
        Destroy(bigImage);
        if (callToAction)
        Destroy(callToAction);
        if (boundIcon)
        Destroy(boundIcon);
    }
}
