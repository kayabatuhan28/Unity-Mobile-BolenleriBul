using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadenciAnimasyonKontroller : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void DurmaAnimasyonu()
    {
        anim.Play("Durma");
    }

    public void IpSarmaAnimasyonu()
    {
        anim.Play("ipSarma");
    }

   
}
