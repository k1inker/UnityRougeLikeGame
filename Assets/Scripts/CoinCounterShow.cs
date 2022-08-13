using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounterShow : MonoBehaviour
{
    private bool showed = false;
    CanvasGroup canvasGroup = null;
    float t = 0f;
    float min = 0f;
    float max = 1f;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0f;
    }

    //private void FixedUpdate()
    //{
    //    if (!showed) 
    //    {
    //        canvasGroup.alpha = Mathf.Lerp(min, max, t);

    //        t += 0.5f * Time.deltaTime;

    //        if (max == 0 && t > 1f)
    //            showed = true;

    //        if (t > 1.0f)
    //        {
    //            float temp = max;
    //            max = min;
    //            min = temp;
    //            t = 0.0f;
    //        }
    //        Debug.Log(t);
    //    }
    //}

    public void coinShow(float minim, float maxim)
    {
        canvasGroup.alpha = Mathf.Lerp(minim, maxim, t);

        t += 0.5f * Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = maxim;
            maxim = minim;
            minim = temp;
            t = 0.0f;
        }
        if (maxim == 0 || t > 1f)
            showed = true;
    }
}
