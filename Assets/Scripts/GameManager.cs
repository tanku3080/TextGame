using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class GameManager : Singleton<GameManager>
{
    //select場面のflag
    public bool onSelect;
    [SerializeField] GameObject uiObj = null;

    //キャラクターと背景用の変数
    public Image backImg = null;
    public Image charaImg = null;
    /// <summary>
    /// 選択肢の数を入れる構造体
    /// </summary>
    public int SelectNum { get; set; }//ナンバー
    public int s = 0;

    void Start()
    {
        uiObj = GameObject.Find("Canvas");
        backImg = uiObj.transform.GetChild(0).GetComponent<Image>();
        charaImg = uiObj.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {

        if (onSelect)
        {
            SelectButton.Instance.SelectStart();
            onSelect = false;
        }
    }

}
