using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    public GameObject FadeMask;

    public void ScreenFadeOutIn()
    {
        StartCoroutine(FadeOutIn());
    }

    public void ScreenFadeOut()
    {
        FadeMask.GetComponent<Animation>().Play("FadeOut");
    }

    public void ScreenFadeIn()
    {
        FadeMask.GetComponent<Animation>().Play("FadeIn");
    }


    IEnumerator FadeOutIn()
    {
        FadeMask.SetActive(true);
        FadeMask.GetComponent<Animation>().Play("FadeOut");
        yield return new WaitForSeconds(1);
        FadeMask.GetComponent<Animation>().Play("FadeIn");
        FadeMask.SetActive(false);
    }

}
