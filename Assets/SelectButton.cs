using UnityEngine;
using UnityEngine.UI;

public class SelectButton : Singleton<SelectButton>
{
    [SerializeField] GameObject SelectObj = null;
    private GameObject[] selects = null;
    private Button[] selectButton = null;
    TextAsset asset;
    Text[] text;
    string[] unit;

    void Start()
    {
        asset = Resources.Load<TextAsset>("SelectString");
        string stringNum = asset.text;
        unit = stringNum.Split('\n');
    }

    public void SelectStart()
    {
        SelectButtonCreate(gameObject);
        SelectObj = gameObject;
        for (int i = 0; i < SelectObj.transform.childCount; i++)
        {
            Debug.Log("始まった");
            //selectButton[i] = SelectObj.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    //オブジェクトに選択肢を子オブジェクト化する
    void SelectButtonCreate(GameObject obj)
    {
        for (int i = 0; i < GameManager.Instance.SelectNum; i++)
        {
            var o = new GameObject($"Button{i}");
            o.gameObject.AddComponent<RectTransform>();
            o.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
            o.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
            o.gameObject.AddComponent<CanvasRenderer>();
            o.gameObject.AddComponent<VerticalLayoutGroup>();
            o.gameObject.AddComponent<LayoutElement>();
            o.gameObject.AddComponent<Image>();
            o.gameObject.AddComponent<Button>();
            o.gameObject.AddComponent<ContentSizeFitter>();
            o.transform.parent = obj.transform;
            o.layer = 5;

            var t = new GameObject($"Text{i}");
            t.gameObject.AddComponent<RectTransform>();
            t.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            t.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            t.gameObject.AddComponent<CanvasRenderer>();
            t.gameObject.AddComponent<Text>();
            t.gameObject.GetComponent<Text>().font = Resources.Load<Font>("komorebi-gothic-P");
            t.transform.parent = o.transform;
            t.layer = 5;

        }
    }

    void Update()
    {
        
    }
}
