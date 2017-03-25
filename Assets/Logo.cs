using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {

    Color colorStart = Color.red;
    Color colorEnd = Color.green;

    Color colorStart1 = Color.yellow;
    Color colorEnd1 = Color.red;
    float duration = 10.0f;

    float minimum = 0.0f;
    float maximum = 200.0f;

    UnityEngine.UI.Image imgLogo = null;
    UnityEngine.UI.Text txtLogo = null;
	// Use this for initialization

    void Start()
    {
        GameObject objLogo = GameObject.Find("imgLogo");
        if (objLogo != null)
        {
            imgLogo = objLogo.GetComponent<UnityEngine.UI.Image>();
        }
        GameObject objText = GameObject.Find("txtShadow");
        if (objText != null)
        {
            txtLogo = objText.GetComponent<UnityEngine.UI.Text>();
        }
        StartCoroutine(wait1());
        StartCoroutine(wait2());
    }

    IEnumerator wait1()
    {
        imgLogo.CrossFadeColor(colorEnd, 5, false, true);//RGBA(0,0,0,0)
        yield return new WaitForSeconds(5);
        imgLogo.CrossFadeColor(colorStart, 5, false, true);//RGBA(0,0,0,1)
    }

    IEnumerator wait2()
    {
        yield return new WaitForSeconds(10);
        imgLogo.CrossFadeAlpha(0, 5f, false);
        yield return new WaitForSeconds(5);
        imgLogo.CrossFadeAlpha(1, 5f, false);
    }

	
	// Update is called once per frame
	void Update () {
        //var lerp = Mathf.PingPong(Time.time, duration) / duration;
        //imgLogo.defaultMaterial.color = Color.Lerp(colorStart, colorEnd, lerp);
        //imgLogo.CrossFadeColor(colorEnd, duration, true, true, true);
	}
}
