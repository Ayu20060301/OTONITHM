using System.Collections;
using UnityEngine;
using TMPro;

public class StartText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_startText; //開始する時に表示させるテキスト
    [SerializeField]
    private GameObject m_operationText; //操作
    //スタート    
     void Start()
    {
        StartCoroutine(StartTextIn());
    }

     IEnumerator StartTextIn()
    {
        GManager.instance.start = false;
        m_startText.text = "Ready?";
        yield return new WaitForSeconds(1.0f);
        m_startText.text = " ";
        yield return new WaitForSeconds(2.0f);
        m_operationText.SetActive(false);
        GManager.instance.start = true;
    }
}
