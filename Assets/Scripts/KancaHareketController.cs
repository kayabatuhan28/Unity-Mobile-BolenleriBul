using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KancaHareketController : MonoBehaviour
{
    [SerializeField]
    float min_Z = -50f, max_Z = 50f;

    [SerializeField]
    float donusHizi = 5f;

    float donusAcisi;
    bool sagaDonsunmu;
    bool donebilirmi;

    bool kancaBtnBasildi;

    [SerializeField]
    public float inmeHizi = 3f;
    float baslangicInmeHizi;

    [SerializeField]
    float min_Y = -2.5f;

    float baslangicY;

    bool asagiGitsinmi;

    ipRenderer ipRendererClass;

    SoundManager soundManager;

    GameManager gameManager;

    private void Awake()
    {
        ipRendererClass = GetComponent<ipRenderer>();
        soundManager = Object.FindObjectOfType<SoundManager>();
        gameManager = Object.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        baslangicY = transform.position.y;
        baslangicInmeHizi = inmeHizi;

        donebilirmi = true;
    }

    private void Update()
    {
        Rotate();
        KancaFirlat();
        KancaHareketi();
    }

    void Rotate()
    {
        if (gameManager.oyunBittimi)
        {
            return;
        }

        if (!donebilirmi)
        {
            return;
        }

        if (sagaDonsunmu)
        {
            donusAcisi += donusHizi * Time.deltaTime;
        }
        else
        {
            donusAcisi -= donusHizi * Time.deltaTime;
        }

        transform.rotation = Quaternion.AngleAxis(donusAcisi, Vector3.forward); 


        if(donusAcisi >= max_Z)
        {
            sagaDonsunmu = false;
        }
        else if(donusAcisi <= min_Z)
        {
            sagaDonsunmu = true;
        }
    }

    void KancaFirlat()
    {
        
        if (kancaBtnBasildi && !gameManager.oyunBittimi)
        {
            if (donebilirmi)
            {
                //soundManager.KancaSesiCikar(true);
                donebilirmi = false;
                asagiGitsinmi = true;
            }
        }
    }

    public void KancaBasildi()
    {
        kancaBtnBasildi = true;
    }

    void KancaHareketi()
    {
        if (gameManager.oyunBittimi)
        {
            return;
        }

        if (donebilirmi) 
        {
            return;
            
        }            

        if (!donebilirmi)
        {
            Vector3 temp = transform.position;

            if (asagiGitsinmi)
            {
                temp -= transform.up * Time.deltaTime * inmeHizi; // up y deðeridir.
            }
            else
            {
                temp += transform.up * Time.deltaTime * inmeHizi;
            }

            transform.position = temp;

            if(temp.y <= min_Y) // alandan çýkmamasý için kancanýn
            {
                asagiGitsinmi = false;
                //soundManager.KancaSesiCikar(false);
            }

            if(temp.y >= baslangicY) // yukarý doðru geri dönerken karakterin pozisyonuna gelince durmasý icin
            {
                donebilirmi = true;
                //inmeHizi = baslangicInmeHizi;
                kancaBtnBasildi = false;
                ipRendererClass.ipGosterFNC(temp,false);
            }

            ipRendererClass.ipGosterFNC(temp, true);

        }


    }

    public void KancaYukariDonsun()
    {
        asagiGitsinmi = false;
    }

}
