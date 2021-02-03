using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cts = new CancellationTokenSource();//cancelする人(送信側)
        //var ct = cts.Token;//cancelを受け取る側(受信側)

        //Debug.Log("キャンセルリクエスト" + ct.IsCancellationRequested);
        //cts.Cancel();
        //Debug.Log("キャンセルリクエスト" + ct.IsCancellationRequested);
        StartCoroutine(WaitClickAsync(cts));
        StartCoroutine(DoAsyne(cts.Token));
    }
 
    private IEnumerator WaitClickAsync(CancellationTokenSource cts)
    {
        Debug.Log("入力待機");
        while (!Input.GetMouseButtonDown(0)) yield return null;
        cts.Cancel();
        Debug.Log("入力終了");
    }
    private IEnumerator DoAsyne(CancellationToken ct)
    {
        Debug.Log("開始");
        while (!ct.IsCancellationRequested) yield return null;
        Debug.Log("完了");
    }
}
