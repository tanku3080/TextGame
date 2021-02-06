using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class GameManager : Singleton<GameManager>
{
    //select場面のflag
    public bool onSelect = false;
    [SerializeField] GameObject uiObj = null;
    public List<string> backImgName = null;
    public List<string> charaImgName = null;
    public Image backImg = null;
    public Image charaImg = null;
    public int textNum = 0;
    public int SelectNum { get; set; }//ナンバー

    bool buttonFlag = false;

    void Start()
    {
        uiObj = GameObject.Find("Canvas");
        backImg = uiObj.transform.GetChild(0).GetComponent<Image>();
        charaImg = uiObj.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if (onSelect) SelectPop();

        if (buttonFlag)
        {
            SelectButton.Instance.SelectStart();
            buttonFlag = false;
        }
    }
    /// <summary>
    /// 長いので省略できたらする
    /// </summary>
    void SelectPop()
    {
        onSelect = false;
    }

    /// <summary>
    /// Resources.Loadで表示するために仕分けする
    /// </summary>
    /// <param name="stg">オリジナル</param>
    /// <param name="img">背景、キャラを使うか？</param>
    /// <param name="selector">選択肢を使うか</param>
    public void ImageSet(string stg,bool img = false,bool selector = false)
    {
        if (img)
        {
            //キャラ = string,背景 = int
            //List化すればよかった？
            if (Regex.Match(stg,"[0-9]").Success)
            {
                backImgName.Add(stg);
                Debug.Log("int" + stg);
            }
            else if(Regex.Match(stg,"[a-z]").Success)
            {
                charaImgName.Add(stg);
                Debug.Log("string" + stg);
            }
        }

        if (selector)
        {
            //2
            string n = stg.Replace("!", null);
            SelectNum = int.Parse(n);
            Debug.Log("数" + SelectNum);
            buttonFlag = true;
            selector = false;
        }
    }

    public void ImageStart(Image img)
    {

    }
}
