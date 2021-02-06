using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

//今後の課題：選択肢を動的に増やすことが出来るようにする。シェーダーについての理解を深める。
//Gameオブジェの子オブジェクトをbuttonListに取得する
public class TextLoad : MonoBehaviour
{
	private string[] unit;
	private Image backImgs = null;
	private Image charaImgs = null;
    Text uiText;
	[SerializeField] AudioClip nextButtonSfx;
	[SerializeField] GameObject buttons;
	
    private AudioSource audio;
	private int count = 0;
	//Transform wipeObj = null;

	[SerializeField]
	[Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
	private int currentLine = 0;
	private int lastUpdateCharacter = -1;
	private int backKeepNum = 0;
	private int charaKeepNum = 0;
	bool SelectFlag { get { return GameManager.Instance.onSelect; } set {; } }
	TextAsset asset;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText
	{
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		uiText = this.gameObject.GetComponent<Text>();

		asset = Resources.Load<TextAsset>("Text");
		string stringNum = asset.text;
		unit = stringNum.Split('\n');
        for (int i = 0; i < unit.Length; i++)
        {
            if (unit[i].Contains("@") || unit[i].Contains("!"))
            {
				if (unit[i].Contains("@"))//画像を判定する。背景画像は全て「@ + Number」の形式で保存する
				{
					GameManager.Instance.ImageSet(unit[i], true);
				}
				else if (unit[i].Contains("!"))
				{
					GameManager.Instance.ImageSet(unit[i], false, true);
				}
			}
		}
		//?
		List<string> a = unit.ToList();
		a.AddRange(unit);
		foreach (var item in a)
		{
			if (item == null)
			{
				a.Remove(item);
			}
		}
		audio = this.gameObject.GetComponent<AudioSource>();
		SetNextLine();
	}

	void Update()
	{

		if (count == unit.Length && Input.GetKeyDown(KeyCode.Return) || count == unit.Length && Input.GetMouseButtonDown(0))
		{
			Debug.Log("呼ばれた");
			GameManager.Instance.textNum++;
			SelectFlag = true;
			return;
		}
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if (IsCompleteDisplayText)
		{
			if (currentLine < unit.Length && Input.GetMouseButtonDown(0) || currentLine < unit.Length && Input.GetKeyDown(KeyCode.Return))
			{
				audio.PlayOneShot(nextButtonSfx);
				SetNextLine();
			}
		}
		else
		{
			// 完了してないなら文字をすべて表示する
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
			{
				timeUntilDisplay = 0;
			}
		}

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if (displayCharacterCount != lastUpdateCharacter)
		{
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}


	void SetNextLine()
	{
		currentText = unit[currentLine];
		//消す
        if (Regex.Match(currentText,"[1-9]").Success)
        {
			backImgs = Resources.Load<Image>(GameManager.Instance.backImgName[backKeepNum]);
			backKeepNum++;
			currentLine += 2;
        }
        else if (Regex.Match(currentText,"[a-z]").Success)
        {
			charaImgs = Resources.Load<Image>(GameManager.Instance.backImgName[charaKeepNum]);
			charaKeepNum++;
			currentLine += 2;
		}
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine++;
		count++;
		lastUpdateCharacter = -1;
	}
}
