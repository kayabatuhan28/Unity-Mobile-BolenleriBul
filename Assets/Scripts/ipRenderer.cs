using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ipRenderer : MonoBehaviour
{
    LineRenderer lineRenderer;

    float cizgiGenisligi = 0.05f;

    [SerializeField]
    Transform baslangicTransform;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; 
        lineRenderer.startWidth = cizgiGenisligi;
    }

    //Kanca icin linerenderer
    public void ipGosterFNC(Vector3 hedefPos,bool enableRenderer)
    {
        if (enableRenderer)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.positionCount = 2; // baslangic ve bitis icin
           
        }
        else
        {
            lineRenderer.positionCount = 0;

            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }


        if (enableRenderer)
        {
            Vector3 temp = baslangicTransform.position;
            temp.z = -3f;

            baslangicTransform.position = temp;

            //bitis noktasý icin
            temp = hedefPos;
            temp.z = 0f;
            hedefPos = temp;

            lineRenderer.SetPosition(0, baslangicTransform.position);
            lineRenderer.SetPosition(1, hedefPos);
        }


    }

}
