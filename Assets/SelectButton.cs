using UnityEngine;
using UnityEngine.UI;

public class SelectButton : Singleton<SelectButton>
{
    [SerializeField] GameObject SelectObj = null;
    private Button[] selectButton = null;
    [SerializeField] Sprite selectImg = null;

    TextAsset asset;
    //下2つの配列はボタンが表示されたら使う予定だった。
    Text[] text;
    string[] unit;

    void Start()
    {
        asset = Resources.Load<TextAsset>("SelectString");
        string stringNum = asset.text;
        unit = stringNum.Split('\n');
    }

    /// <summary>
    /// ボタンをselectButton[]の中にいれて管理するスクリプトだったがエラーが出た。
    /// </summary>
    public void SelectStart()
    {
        SelectButtonCreate(gameObject);
        SelectObj = gameObject;
        for (int i = 0; i < SelectObj.transform.childCount; i++)
        {
            Debug.Log("始まった");
            selectButton[i] = SelectObj.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    //テキスト内のコマンド文がから数値を抽出してその数分のボタンオブジェクトを作るメソッド
    void SelectButtonCreate(GameObject obj)
    {
        for (int i = 0; i < GameManager.Instance.SelectNum; i++)
        {
            var o = new GameObject($"Button{i}");
            o.gameObject.AddComponent<CanvasRenderer>();
            o.gameObject.AddComponent<VerticalLayoutGroup>();
            o.gameObject.AddComponent<LayoutElement>();
            o.gameObject.AddComponent<Image>();
            o.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(selectImg.name);
            o.gameObject.AddComponent<Button>();
            o.gameObject.GetComponent<Button>().onClick.AddListener(OnclickEvent);
            o.gameObject.AddComponent<ContentSizeFitter>();
            o.transform.SetParent(obj.transform);
            o.layer = 5;

            var t = new GameObject($"Text");
            t.gameObject.AddComponent<CanvasRenderer>();
            t.gameObject.AddComponent<Text>();
            t.gameObject.GetComponent<Text>().font = Resources.Load<Font>("komorebi-gothic-P");
            t.transform.SetParent(o.transform);
            t.layer = 5;

        }
        GameManager.Instance.onSelect = false;
    }

    /// <summary>
    /// ボタンがclickされたときに使うメソッドだった
    /// </summary>
    public void OnclickEvent()
    {
        string t = gameObject.name;
        Debug.Log(t);
    }
}
