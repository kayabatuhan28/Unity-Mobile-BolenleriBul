using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElmasCekmeKontroller : MonoBehaviour
{
    [SerializeField]
    Transform elmasTutucuTransform;

    bool elmasTutuldumu;

    KancaHareketController kancaHareketController;

    MadenciAnimasyonKontroller madenciAnimasyonKontroller;

    GameManager gameManager;

    SoundManager soundManager;

    private void Awake()
    {
        kancaHareketController = Object.FindObjectOfType<KancaHareketController>();
        madenciAnimasyonKontroller = GetComponentInParent<MadenciAnimasyonKontroller>();
        gameManager = Object.FindObjectOfType<GameManager>();
        soundManager = Object.FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BuyukElmas" || target.tag == "OrtaElmas" || target.tag == "KucukElmas")
        {
            elmasTutuldumu = true;

            target.transform.parent = elmasTutucuTransform; // isabet anýnda carpýlan nesneyi kancaya degdigi yerden yapistirir.
            target.transform.position = elmasTutucuTransform.position; // yapistirilan nesnenin pozisyonunu kancanýn ortasýndaki deðiskene setler

            kancaHareketController.KancaYukariDonsun();

            madenciAnimasyonKontroller.IpSarmaAnimasyonu();

            if(target.tag == "BuyukElmas" || target.tag == "OrtaElmas")
            {
                soundManager.BuyukElmasSesiCikar();
            }
            else
            {
                soundManager.KucukElmasSesiCikar();
            }

            soundManager.ElmasCekmeSesiCikar(true);

        }

        if(target.tag == "GelenElmas")
        {
            if (elmasTutuldumu)
            {
                elmasTutuldumu = false;
                Transform objChild = elmasTutucuTransform.GetChild(0); // Gelen elmasý bulur

                //tutulan elmas bu component kancada oldugu icin 2 tane child alarak transforma oturan elmasa erisip onunda text componentindeki texti inte cevirir
                int elmasDegeri = int.Parse(objChild.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
                gameManager.SonucuKontrolEt(elmasDegeri);
                soundManager.ElmasCekmeSesiCikar(false);
                //Kancaya gelen elmasýn kaybolup hýzýn eski haline setlendigi kýsým
                kancaHareketController.inmeHizi = 4f;
                objChild.parent = null;
                objChild.gameObject.SetActive(false);

                madenciAnimasyonKontroller.DurmaAnimasyonu();

               
            }
        }

    }

}
