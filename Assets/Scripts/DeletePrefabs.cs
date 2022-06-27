using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePrefabs : MonoBehaviour
{
    [SerializeField]
    GameObject elmaslarObj,madenci;
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void YokEt()
    {
        elmaslarObj.SetActive(false);
        madenci.SetActive(false);
    }

}
