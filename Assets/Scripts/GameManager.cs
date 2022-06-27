using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // List özelliklerine eriþmek için
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform[] elmasYerleri;

    [SerializeField]
    List<GameObject> elmaslarPrefabs;

    [SerializeField]
    Transform elmaslariTutanTransform;

    [SerializeField]
    private TextMeshProUGUI soruText;

    [SerializeField]
    List<int> sayilarListesi;

    [SerializeField]
    private TextMeshProUGUI geriSaymaTxt;

    [SerializeField]
    private Image sliderImg;

    [SerializeField]
    TextMeshProUGUI puanTxt;

    [SerializeField]
    GameObject yildizImg;

   

    [SerializeField]
    GameObject birinciYildizImg, ikinciYildizImg, ucuncuYildizImg;

    [SerializeField]
    GameObject bitisPanel, panelbackground, scoreleaf, scoreleaf2, scoreleaf3, stagebackground, stagelight, stagetxt, stagecleartxt, nextstageBtn;

    DeletePrefabs deletePrefabs;

    int sorulacakSayi;

    List<int> dogruCevaplar = new List<int>();
    List<int> yanlisCevaplar = new List<int>();
    List<int> bolenlerListesi = new List<int>();

    int geriSayac = 45;

    float skor = 25f;

    float toplamSkor = 0f;

    int kalanhak = 3;

    SoundManager soundManager;

   

    public bool oyunBittimi;

    [SerializeField]
    Sprite bosleaf;

    public bool gecti;

    [SerializeField]
    Sprite gribackground, stagefail, btnTrue;

    private void Awake()
    {
        soundManager = Object.FindObjectOfType<SoundManager>();
        deletePrefabs = Object.FindObjectOfType<DeletePrefabs>();
       
    }

    void ElmaslariYerlestir()
    {
        elmaslarPrefabs = elmaslarPrefabs.OrderBy(i => Random.value).ToList(); // ElmaslarPrefabs List içeriðini karýþtýrýr

        for (int i = 0; i < elmasYerleri.Length; i++)
        {
            GameObject elmas = Instantiate(elmaslarPrefabs[i]) as GameObject;
            elmas.GetComponentInChildren<TextMeshProUGUI>().text = bolenlerListesi[i].ToString();
            elmas.transform.parent = elmaslariTutanTransform; 
            elmas.transform.position = elmasYerleri[i].position;
        }

    }

    private void Start()
    {
        oyunBittimi = false;
        MadenciyeSoruSor();
        BolenleriBul();

        ElmaslariYerlestir();

        StartCoroutine(GeriSaymaRoutine());

        puanTxt.text = toplamSkor.ToString();

        HaklariGoster();
        
    }

    void MadenciyeSoruSor()
    {
        sayilarListesi = sayilarListesi.OrderBy(i => Random.value).ToList();

        sorulacakSayi = sayilarListesi[0];

        soruText.text = sayilarListesi[0]+ " sayýsýnýn bölenlerini bul.";
    }

    void BolenleriBul()
    {
        for (int  i = 2;  i < sorulacakSayi;  i++)
        {
            if(sorulacakSayi % i == 0)
            {
                dogruCevaplar.Add(i);
            }
            else
            {
                yanlisCevaplar.Add(i);
            }
        }

        dogruCevaplar = dogruCevaplar.OrderBy(i => Random.value).ToList();
        yanlisCevaplar = yanlisCevaplar.OrderBy(i => Random.value).ToList();

        for (int i = 0; i < 4; i++)
        {
            bolenlerListesi.Add(dogruCevaplar[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            bolenlerListesi.Add(yanlisCevaplar[i]);
        }

        bolenlerListesi = bolenlerListesi.OrderBy(i => Random.value).ToList(); // bölen ve bölmeyen sayilarin rastgele transform iþlemi

    }

    public void SonucuKontrolEt(int gelenDeger)
    {
        if (dogruCevaplar.Contains(gelenDeger)) // dogrucevaplar listin icinde gelendeger var ise true 
        {
            PuaniGoster();
            YildiziCikar();
            
        }
        else
        {
            //Gelen deger yanlis ise
            kalanhak--;
            HaklariGoster();
        }
    }

    IEnumerator GeriSaymaRoutine()
    {
        yield return new WaitForSeconds(1f);
        geriSayac--;
        geriSaymaTxt.text = geriSayac.ToString();
      
        StartCoroutine("GeriSaymaRoutine");
        
        if(geriSayac <= 10)
        {
            soundManager.SureBitiyorSesiCikar(true);
        }

        if(geriSayac <= 0)
        {
            soundManager.BackGroundSesiCikar(false);
            StopCoroutine("GeriSaymaRoutine");
            soundManager.SureBitiyorSesiCikar(false);
            oyunBittimi = true;
            StartCoroutine("OyunBittiRoutine");
           
            //oyun bitti

        }

    }

    public void PuaniGoster()
    {
        toplamSkor += skor;
        sliderImg.fillAmount = (float) toplamSkor / 100f;

        puanTxt.text = toplamSkor.ToString();

        if(toplamSkor >= 100)
        {
            gecti = true;
            soundManager.BackGroundSesiCikar(false);
            StopCoroutine("GeriSaymaRoutine");
            geriSaymaTxt.text = "";          
            StartCoroutine("OyunBittiRoutine");

            //Oyun bitti
        }
    }

    void YildiziCikar()
    {
        yildizImg.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
        yildizImg.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        Invoke("YildiziGonder", 1.2f); 
    }

    void YildiziGonder()
    {
        yildizImg.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack);
        yildizImg.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
    }

    void HaklariGoster()
    {
        if(kalanhak == 3)
        {
            birinciYildizImg.SetActive(true);
            ikinciYildizImg.SetActive(true);
            ucuncuYildizImg.SetActive(true);

            
        }
        else if(kalanhak == 2)
        {
            birinciYildizImg.SetActive(true);
            ikinciYildizImg.SetActive(true);
            ucuncuYildizImg.SetActive(false);

            scoreleaf3.GetComponent<Image>().sprite = bosleaf;
        }
        else if(kalanhak == 1)
        {
            birinciYildizImg.SetActive(true);
            ikinciYildizImg.SetActive(false);
            ucuncuYildizImg.SetActive(false);

            scoreleaf3.GetComponent<Image>().sprite = bosleaf;
            scoreleaf2.GetComponent<Image>().sprite = bosleaf;

        }
        else if (kalanhak == 0)
        {
            oyunBittimi = true;
            StartCoroutine("OyunBittiRoutine");
            birinciYildizImg.SetActive(false);
            ikinciYildizImg.SetActive(false);
            ucuncuYildizImg.SetActive(false);

            scoreleaf3.GetComponent<Image>().sprite = bosleaf;
            scoreleaf2.GetComponent<Image>().sprite = bosleaf;
            scoreleaf.GetComponent<Image>().sprite = bosleaf;


        }

    }

    IEnumerator OyunBittiRoutine()
    {

        if(gecti == true)
        {
            stagelight.GetComponent<Image>().sprite = gribackground;
            stagecleartxt.GetComponent<Image>().sprite = stagefail;
            nextstageBtn.GetComponent<Image>().sprite = btnTrue;
            StopCoroutine("GeriSaymaRoutine");
            soundManager.SureBitiyorSesiCikar(false);
        }

        deletePrefabs.YokEt();
        yield return new WaitForSeconds(0.1f);
        bitisPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        bitisPanel.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.5f);
        panelbackground.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        panelbackground.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.5f);
        scoreleaf.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        scoreleaf.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        scoreleaf2.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        scoreleaf2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        scoreleaf3.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        scoreleaf3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.5f);
        panelbackground.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        panelbackground.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.5f);
        stagebackground.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        stagebackground.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        stagelight.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        stagelight.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.5f);
        stagetxt.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        stagetxt.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        stagecleartxt.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        stagecleartxt.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
        yield return new WaitForSeconds(0.5f);
        nextstageBtn.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutCirc);
        nextstageBtn.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutCirc);
       
    }



    public void nextStageBtn()
    {
        SceneManager.LoadScene("GameScene");
    }


}
