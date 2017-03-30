using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    List<string> lMonsterName = new List<string>();

    Player role = new Player();
    Monster target = null;
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

    Text txtTargetName = null;
    Text txtTargetLevel = null;
    Slider sliderTargetHP = null;
    Text txtTargetHP = null;

    System.Random rand = new System.Random();

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
        lMonsterName.Add("小地精");
        lMonsterName.Add("大地精");
        lMonsterName.Add("地精祭祀");
        lMonsterName.Add("鬓刺猪");
        lMonsterName.Add("剑齿虎");
        lMonsterName.Add("猛犸象");
        lMonsterName.Add("火精灵");
        lMonsterName.Add("熔岩蜥蜴");
        lMonsterName.Add("飞羽");
        lMonsterName.Add("黑石兽人");
        lMonsterName.Add("魅魔");

        role.Init("李铁柱", 1);

	    txtName = GetText<Text>("Role/Name");
        txtLevel = GetText<Text>("Role/Level");
        txtHP = GetText<Text>("Role/Hp/HpNum");
        txtMP = GetText<Text>("Role/Mp/MpNum"); 
        txtEXP = GetText<Text>("Exp/ExpNum");
        sliderHP = GetText<Slider>("Role/Hp");
        sliderMP = GetText<Slider>("Role/Mp"); 
        sliderEXP = GetText<Slider>("Exp"); 
        AttrPanel = GameObject.Find("Attr");
        EnemyPanel = GameObject.Find("Enemy");
        objAttrValue = GameObject.Find("Attr/Attr");
        objEnemy = GameObject.Find("Enemy/Enemy");

        txtTargetName = GetText<Text>("Target/Name");
        txtTargetLevel = GetText<Text>("Target/Level");
        txtTargetHP = GetText<Text>("Target/Hp/HpNum");
        sliderTargetHP = GetText<Slider>("Target/Hp");

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

        Vector2 rPos = objAttrValue.GetComponent<RectTransform>().anchoredPosition;
        for (int i=0; i<8; i++)
        {
            GameObject obj = GameObject.Instantiate(objAttrValue, objAttrValue.transform.parent);
            //obj.transform.parent = objAttrValue.transform.parent;
            obj.SetActive(true);
            obj.GetComponent<RectTransform>().anchoredPosition = rPos;
            rPos.y -= 60f;
        }
	}

    public float fLastUpdateTime = 0f;
	// Update is called once per frame
	void Update () {

        if (Time.time < fLastUpdateTime || role == null)
        {
            return;
        }
        fLastUpdateTime = Time.time + 0.1f;
        if (target == null || target.HP <= 0)
        {
            target = new Monster();
            target.Init(lMonsterName[rand.Next()%lMonsterName.Count], role.Level + (rand.Next()%3-1));
        }

        role.Attack(target);

        if (target.HP <= 0)
        {
            role.EXP += target.AddEXP;
        }

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

        txtTargetName.text = target.strName;
        txtTargetLevel.text = "LV." + target.Level;

        txtTargetHP.text = target.HP + "/" + target.HPMax;
        sliderTargetHP.value = target.HP * 1f / target.HPMax;
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

    public float fAttackSpeed = 1.5f;
    public float fCritRate = 0.2f;
    public float fCritDamageRate = 2f;
    public float fAttackTime = 0;

    System.Random rand = new System.Random();

    public void Init(string name, int nLevel)
    {
        strName = name;
        Level = nLevel;

        RefreshAttr();
    }

    public void Attack(Monster target)
    {
        if (Time.time < fAttackTime)
        {
            return;
        }
        fAttackTime = Time.time + fAttackSpeed;

        int Damage = (int)Mathf.Pow(Level+5, 2);

        if (rand.NextDouble() <= fCritRate)
        {
            Damage = (int)(Damage *fCritDamageRate);
        }
        target.HP -= Damage;
        if (target.HP < 0)
        {
            target.HP = 0;
        }
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

class Monster
{
    public string strName = "";
    public int Level = 0;
    public int HP = 0;
    public int HPMax = 0;
    public int AddEXP = 0;

    public void Init(string name, int nLevel)
    {
        strName = name;
        Level = nLevel;

        RefreshAttr();
    }

    public void RefreshAttr()
    {
        HPMax = (Level * Level) * 30 + 30;
        HP = HPMax;
        AddEXP = (int)Mathf.Sqrt((Level * Level) * 50 + 500);
    }
}
