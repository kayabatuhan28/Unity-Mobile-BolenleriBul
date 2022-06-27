using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject gameTitle, startBtn, nasilOynanirBtn,Image,panel;
    bool nasilOynanirBasildi;

    void Start()
    {
        StartCoroutine(MenuyuAc());
    }

   
    void Update()
    {
        
    }

    IEnumerator MenuyuAc()
    {
        yield return new WaitForSeconds(0.1f);
        gameTitle.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        gameTitle.GetComponent<RectTransform>().DOScale(1, 0.5f);
        yield return new WaitForSeconds(0.6f);
        startBtn.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        startBtn.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.6f);
        nasilOynanirBtn.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        nasilOynanirBtn.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    public void oyunaBasla()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ImageAc()
    {
        Image.GetComponent<CanvasGroup>().DOFade(1, 1f);
        Invoke("oyunaBasla", 1.1f);
    }

    public void nasilOynanirFNC()
    {
        if (!nasilOynanirBasildi)
        {
            panel.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.OutBack);
            nasilOynanirBasildi = true;
        }
        else
        {
            panel.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.OutBack);
            nasilOynanirBasildi = false;
        }
        
    }


}
