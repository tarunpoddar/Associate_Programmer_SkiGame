/*
NOTE: If you would like to follow along with the video in the courseware, you must first
download DOTween into your project from the Unity Asset store. This is a free asset used to
create lightweight, script based animations. You can find it here:
https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676 
 */

using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public CanvasGroup buttonsGroup;
    public CanvasGroup quitCheckGroup;
    public Image overlayImage;
    public CanvasGroup LoadingText;
    public CanvasGroup NFSText;
    public int loadLevelID;
    public float fadeTime;

    private void Start()
    {
        // Start with the overlay and quit check fully opaque
        //overlayImage.DOFade(1, 0);
        quitCheckGroup.DOFade(0, 0);
        buttonsGroup.DOFade(0, 0);
        LoadingText.DOFade(0, 0);
        NFSText.DOFade(0, 0);
        // Fade the overlay out to reveal the title screen
        //overlayImage.DOFade(0, fadeTime);
        // Fade in the buttons
        buttonsGroup.DOFade(1, fadeTime).SetDelay(fadeTime);
        NFSText.DOFade(1, fadeTime).SetDelay(fadeTime);
        buttonsGroup.interactable = true;
    }

    public void ShowQuitCheck()
    {
        quitCheckGroup.DOFade(1, fadeTime); 
        buttonsGroup.interactable = false;
        buttonsGroup.DOFade(0, fadeTime);
        NFSText.DOFade(0, fadeTime);
        NFSText.interactable = false;
    }

    public void HideQuitCheck()
    {
        quitCheckGroup.DOFade(0, fadeTime);
        buttonsGroup.interactable = true;
        buttonsGroup.DOFade(1, fadeTime);
        NFSText.DOFade(1, fadeTime);
        NFSText.interactable = true;
    }

    public void QuitGame()
    {
        StartCoroutine("ExitGame");
    }

    IEnumerator ExitGame()
    {
        overlayImage.DOFade(1, 1);
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PlayGame()
    {
        NFSText.DOFade(0, fadeTime);
        buttonsGroup.interactable = false;
        LoadingText.DOFade(1, 0.1f);
        StartCoroutine("LoadSceneAsync");
    }

    IEnumerator LoadSceneAsync()
    {
        overlayImage.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        
        SceneManager.LoadScene("Level1_Scene");

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadLevelID);

        //// Wait until the asynchronous scene fully loads
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}
    }
}
