using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElmasController : MonoBehaviour
{
    KancaHareketController kancaHareketController;

    public float cekilmeHizi;

    private void Awake()
    {
        kancaHareketController = Object.FindObjectOfType<KancaHareketController>();
    }

   

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Kanca"))
        {
            kancaHareketController.inmeHizi = cekilmeHizi;
        }       

    }
}
