using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject uiObj = null;

    //キャラクターと背景用の変数
    public Image backImg = null;
    public Image charaImg = null;
    /// <summary>
    /// 選択肢の数が入っている
    /// </summary>
    public int SelectNum { get; set; }//ナンバー
    public int s = 0;

    void Start()
    {
        uiObj = GameObject.Find("Canvas");
        backImg = uiObj.transform.GetChild(0).GetComponent<Image>();
        charaImg = uiObj.transform.GetChild(1).GetComponent<Image>();
    }
}
