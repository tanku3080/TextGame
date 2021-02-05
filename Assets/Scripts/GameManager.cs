using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
class GameManager : Singleton<GameManager>
{
    public bool onSelect = false;
    [SerializeField] GameObject uiObj = null;
    private Image backImg = null;
    private Image charaImg = null;
    public int textNum = 0;
    public int SelectNum { get; set; }//ナンバー

    //判定
    private static int num = 0;
    private static string strin = null;

    void Start()
    {
        uiObj = GameObject.Find("Canvas");
        backImg = uiObj.transform.GetChild(0).GetComponent<Image>();
        charaImg = uiObj.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if (onSelect) SelectPop();
    }
    /// <summary>
    /// 長いので省略できたらする
    /// </summary>
    void SelectPop()
    {
        onSelect = false;
    }

    /// <summary>
    /// 表示されるものの処理
    /// </summary>
    /// <param name="stg">オリジナル</param>
    /// <param name="img">背景、キャラを使うか？</param>
    /// <param name="selector">選択肢を使うか</param>
    public void ImageOn(string stg,bool img = false,bool selector = false)
    {
        if (img)
        {
            //キャラ = string,背景 = int
            if (Regex.Match(stg,"[0-9]").Success)
            {
                backImg = (Image)Resources.Load(stg);
                Debug.Log("int" + stg);
            }
            else if(Regex.Match(stg,"[a-z]").Success)
            {
                charaImg = (Image)Resources.Load(stg);
                Debug.Log("string" + stg);
            }
        }

        if (selector)
        {
            string n = stg.Replace("!", "");
            SelectNum = int.Parse(n);
            Debug.Log("数" + SelectNum);
        }
    }
}
