using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    Player role = new Player();
    Text txtName = null;
    Text txtLevel = null;
    Slider sliderHP = null;
    Slider sliderMP = null;
    Slider sliderEXP = null;
    Text txtHP = null;
    Text txtMP = null;
    Text txtEXP = null;
    GameObject AttrPanel = null;
    GameObject objAttrValue = null;
    GameObject EnemyPanel = null;
    GameObject objEnemy = null;

    T GetText<T>(string strName)
    {
        GameObject obj = GameObject.Find(strName);
	    if (obj != null)
        {
            return obj.GetComponent<T>();
        }
        return default(T);
    }
         
	// Use this for initialization
	void Start () {
        role.Init("李铁柱", 1);

	    txtName = GetText<Text>("Name"); 
        txtLevel = GetText<Text>("Level");
        txtHP = GetText<Text>("Canvas/Hp/HpNum"); 
        txtMP = GetText<Text>("Mp/MpNum"); 
        txtEXP = GetText<Text>("Exp/ExpNum");
        sliderHP = GetText<Slider>("Canvas/Hp"); 
        sliderMP = GetText<Slider>("Mp"); 
        sliderEXP = GetText<Slider>("Exp"); 
        AttrPanel = GameObject.Find("Attr");
        EnemyPanel = GameObject.Find("Enemy");
        objAttrValue = GameObject.Find("Attr/Attr");
        objEnemy = GameObject.Find("Enemy/Enemy");

        if (objAttrValue != null)
        {
            objAttrValue.SetActive(false);
        }

        txtName.text = role.strName;
        txtLevel.text = "LV." + role.Level;

        txtHP.text = role.HP + "/" + role.HPMax;
        sliderHP.value = role.HP * 1f / role.HPMax;

        txtMP.text = role.MP + "/" + role.MPMax;
        sliderMP.value = role.MP * 1f / role.MPMax;

        txtEXP.text = role.EXP + "/" + role.EXPMax;
        sliderEXP.value = role.EXP * 1f / role.EXPMax;

        for (int i=0; i<8; i++)
        {
            GameObject obj = GameObject.Instantiate(objAttrValue);
            obj.transform.parent = objAttrValue.transform.parent;
            obj.SetActive(true);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 80-i*20);
        }
	}

    public float fLastUpdateTime = 0f;
	// Update is called once per frame
	void Update () {

        if (Time.time < fLastUpdateTime || role == null)
        {
            return;
        }
        fLastUpdateTime = Time.time; 
        role.EXP += 10;
        if (role.EXP > role.EXPMax)
        {
            int nLevelExp = role.EXP - role.EXPMax;
            role.Level++;
            role.RefreshAttr();
            role.EXP = nLevelExp;
        }

        txtName.text = role.strName;
        txtLevel.text = "LV." + role.Level;

        txtHP.text = role.HP + "/" + role.HPMax;
        sliderHP.value = role.HP * 1f / role.HPMax;

        txtMP.text = role.MP + "/" + role.MPMax;
        sliderMP.value = role.MP * 1f / role.MPMax;

        txtEXP.text = role.EXP + "/" + role.EXPMax;
        sliderEXP.value = role.EXP * 1f / role.EXPMax;	
	}
}

class Player
{
    public string strName = "";
    public int Level = 0;
    public int HP = 0;
    public int HPMax = 0;
    public int MP = 0;
    public int MPMax = 0;
    public int EXP = 0;
    public int EXPMax = 0;

    public void Init(string name, int nLevel)
    {
        strName = name;
        Level = nLevel;

        RefreshAttr();
    }

    public void RefreshAttr()
    {
        HPMax = (Level * Level) * 50 + 50;
        HP = HPMax;
        MPMax = (Level * 1) * 20 + 80;
        MP = MPMax;
        EXPMax = (Level * Level) * 50 + 100;
        EXP = 0;
    }
}
