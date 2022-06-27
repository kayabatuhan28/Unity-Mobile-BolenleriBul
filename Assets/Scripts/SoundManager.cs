using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource background_FX,buyukElmas_FX, kucukElmas_FX, kanca_FX, elmasCekme_FX, oyuncuGulme_FX, oyunSonu_FX, sureBitiyor_FX;

    public void BuyukElmasSesiCikar()
    {
        buyukElmas_FX.Play();
    }

    public void KucukElmasSesiCikar()
    {
        kucukElmas_FX.Play();
    }

    public void KancaSesiCikar(bool sesCiksinmi)
    {
        if (sesCiksinmi)
        {
            if (!kanca_FX.isPlaying) // Kanca ses cýkarmýyorsa
            {
                kanca_FX.Play();
            }           
        }
        else
        {
            
         kanca_FX.Stop();
           
        }

    }

    public void ElmasCekmeSesiCikar(bool sesCiksinmi)
    {
        if (sesCiksinmi)
        {
            if (!elmasCekme_FX.isPlaying)
            {
                elmasCekme_FX.Play();
            }
            
        }
        else
        {
            if (elmasCekme_FX.isPlaying)
            {
                elmasCekme_FX.Stop();
            }
        }
    }

    public void BackGroundSesiCikar(bool sesCiksinmi)
    {
        if (sesCiksinmi)
        {
            if (!background_FX.isPlaying) 
            {
                background_FX.Play();
            }

        }
        else
        {
            if (background_FX.isPlaying)
            {
                background_FX.Stop();
            }
        }
    }

    public void SureBitiyorSesiCikar(bool sesCiksinmi)
    {
        if (sesCiksinmi)
        {
            if (!sureBitiyor_FX.isPlaying) 
            {
                sureBitiyor_FX.Play();
            }          
        }
        else
        {
            if (sureBitiyor_FX.isPlaying)
            {
                sureBitiyor_FX.Stop();
            }
        }

    }

    public void OyunBittiSesiCikar()
    {
        oyunSonu_FX.Play();
    }

    public void OyuncuGulmeSesiCikar()
    {
        oyuncuGulme_FX.Play();
    }




}
