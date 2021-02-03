using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
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

        SelectButtonCreate(gameObject);
        SelectObj = gameObject;
        for (int i = 0; i < SelectObj.transform.childCount; i++)
        {
            selects[i] = SelectObj.transform.GetChild(i).gameObject;
            selectButton[i] = selects[i].GetComponent<Button>();
        }
    }

    //オブジェクトに選択肢を子オブジェクト化する
    void SelectButtonCreate(GameObject obj)
    {
        for (int i = 0; i < GameManager.Instance.SelectNum; i++)
        {
            var o = new GameObject($"Button{i}");
            o.gameObject.AddComponent<CanvasRenderer>();
            o.gameObject.AddComponent<GridLayoutGroup>();
            o.gameObject.AddComponent<Image>();
            o.gameObject.AddComponent<Button>();
            o.transform.parent = obj.transform;

        }
    }

    void Update()
    {
        
    }
}
