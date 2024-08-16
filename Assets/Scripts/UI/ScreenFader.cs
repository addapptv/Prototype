using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    public GameObject screenMask;
    public bool dimScreen;
    public float dimAlpha = 0.6f;

    private void OnEnable()
    {
        GameEventsManager.instance.saveEvents.OnLoadGame += ScreenFadeOutIn;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.saveEvents.OnLoadGame -= ScreenFadeOutIn;

    }

    /*private void Update()
    {
            DimScreen();
    }

    public void DimScreen()
    {
        if (dimScreen)
        {
            ScreenMask.SetActive(true);
            Color maskColor = ScreenMask.GetComponent<RawImage>().color;
            maskColor.a = dimAlpha;
            ScreenMask.GetComponent<RawImage>().color = maskColor;
        }
        else
        {
            ScreenMask.SetActive(false);
        }

*//*            ScreenMask.GetComponent<RawImage>().CrossFadeAlpha(dimAlpha,0.03f,false);*//*
    }*/

    public void ScreenFadeOutIn()
    {
        StartCoroutine(FadeOutIn());
    }

    public void ScreenFadeOut()
    {
        screenMask.GetComponent<Animation>().Play("ScreenMaskFadeOut");
    }

    public void ScreenFadeIn()
    {
        screenMask.GetComponent<Animation>().Play("ScreenMaskFadeIn");
    }

    public void DimScreen()
    {

    }

    IEnumerator FadeOutIn()
    {
        screenMask.SetActive(true);
        screenMask.GetComponent<Animation>().Play("ScreenMaskFadeOut");
        yield return new WaitForSeconds(1);
        screenMask.GetComponent<Animation>().Play("ScreenMaskFadeIn");
        yield return new WaitForSeconds(3);
        screenMask.SetActive(false);
    }

/*    IEnumerator DimDown()
    {
        ScreenMask.SetActive(true);

        yield return new WaitForSeconds(1);
        ScreenMask.GetComponent<Animation>().Play("ScreenMaskFadeIn");
        yield return new WaitForSeconds(3);
        ScreenMask.SetActive(false);
    }*/

}



