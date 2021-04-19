using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//今後の課題：選択肢を動的に増やすことが出来るようにする。シェーダーについての理解を深める。
//Gameオブジェの子オブジェクトをbuttonListに取得する
public class TextLoad : MonoBehaviour
{
	private string[] unit;
	private string stringNum;
	Text uiText;
	[SerializeField] AudioClip nextButtonSfx;
	[SerializeField] GameObject buttons;
	bool onSelect = false;


	public List<string> backImgName = new List<string> { };
	public List<string> charaImgName = new List<string> { };

	private AudioSource audio;
	private int count = 0;


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
		stringNum = asset.text;
		unit = stringNum.Split('\n');
		for (int i = 0; i < unit.Length; i++)
		{
			if (unit[i].Contains("@") || unit[i].Contains("!"))
			{
				if (unit[i].Contains("@"))//画像を判定する。背景画像は全て「@ + Number」の形式で保存する
				{

					if (Regex.Match(unit[i], "[0-9]").Success)
					{
						backImgName.Add(unit[i]);
					}
					else if (Regex.Match(unit[i], "[a-z]").Success)
					{
						charaImgName.Add(unit[i]);
					}
				}
				else if (Regex.Match(unit[i],"[!]").Success)
				{
					GameManager.Instance.SelectNum = int.Parse(unit[i].Replace("!",""));
				}
			}
		}
		audio = this.gameObject.GetComponent<AudioSource>();
		SetNextLine();
	}

	void Update()
	{
		//ボタンを作成、表示する条件式
		if (onSelect)
		{
			SelectButton.Instance.SelectStart();
			onSelect = false;
		}


		//表示するものが無いならこの条件式に入る
		if (count == unit.Length && Input.GetKeyDown(KeyCode.Return) || count == unit.Length && Input.GetMouseButtonDown(0))
		{
			Debug.Log("呼ばれた");
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


	/// <summary>
	/// 次の文字をセットするメソッド
	/// </summary>
	//背景、人物、選択肢を条件式で処理を行う。
	//条件式に当てはまらなかったらシーンに表示される
	void SetNextLine()
	{
		currentText = unit[currentLine];

		if (currentText == string.Empty) currentLine++;
		//背景のコマンド文か確かめる
		if (Regex.Match(currentText, "[0-9]").Success && Regex.Match(currentText, "[^!]").Success)
		{
			GameManager.Instance.backImg = Resources.Load<Image>(backImgName[backKeepNum]);
			backKeepNum++;
			currentLine++;
			currentText = unit[currentLine];
		}
		//選択肢のコマンド文か確かめる
		if (Regex.Match(currentText, "[!]").Success && Regex.Match(currentText,"[0-9]").Success)
		{
			//選択肢
			onSelect = true;
			currentText = unit[currentLine];		}
		//キャラクターのコマンド文か確かめる
		if (Regex.Match(currentText, "[a-z]").Success)
		{
			GameManager.Instance.charaImg.sprite = Resources.Load<Sprite>(charaImgName[charaKeepNum]);

			charaKeepNum++;
			currentLine++;
			currentText = unit[currentLine];
		}

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine++;
		count++;
		lastUpdateCharacter = -1;
	}
}
