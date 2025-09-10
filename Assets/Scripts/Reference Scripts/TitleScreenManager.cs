using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 
NOTE: If you would like to follow along with the video in the courseware, you must first
download DOTween into your project from the Unity Asset store. This is a free asset used to
create lightweight, script based animations. You can find it here:
https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676 
 */

//using DG.Tweening;

//public class TitleScreenManager : MonoBehaviour
//{
//    public CanvasGroup buttonsGroup;
//    public CanvasGroup quitCheckGroup;
//    public Image overlayImage;
//    public int loadLevelID;
//    public float fadeTime;



//    // Start is called before the first frame update



//    public void ShowQuitCheck()
//    {

//        quitCheckGroup.DOFade(1, fadeTime);
//        buttonsGroup.interactable = false;
//        buttonsGroup.DOFade(0, fadeTime);
//    }

//    public void HideQuitCheck()
//    {
//        quitCheckGroup.DOFade(0, fadeTime);
//        buttonsGroup.interactable = true;
//        buttonsGroup.DOFade(1, fadeTime);
//    }

//    public void QuitGame()
//    {
//        StartCoroutine("ExitGame");
//    }

//    IEnumerator ExitGame()
//    {
//        overlayImage.DOFade(1, 1);
//        yield return new WaitForSeconds(1.0f);
//        Application.Quit();
//    }


//    public void PlayGame()
//    {
//        StartCoroutine("LoadSceneAsync");
//    }

//    IEnumerator LoadSceneAsync()
//    {

//        overlayImage.DOFade(1,0.5f);
//        yield return new WaitForSeconds(0.5f);

//        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadLevelID);

//        // Wait until the asynchronous scene fully loads
//        while (!asyncLoad.isDone)
//        {
//            yield return null;
//        }
//    }


//}
