using UnityEngine;
using TMPro;
using System.Collections;

public class Result : MonoBehaviour
{

    [SerializeField]
     private TextMeshProUGUI m_scoreText; //Score
    [SerializeField] 
     private TextMeshProUGUI m_perfectText;  //perfect
    [SerializeField] 
     private TextMeshProUGUI m_greatText; //great
    [SerializeField] 
     private TextMeshProUGUI m_goodText; //good
    [SerializeField] 
     private TextMeshProUGUI m_missText; //miss
    [SerializeField] 
     private TextMeshProUGUI m_rankText; //Rank
    [SerializeField] 
     private ResultButton m_button;  //スクリプト「ResultButton」を格納する変数
    [SerializeField]
    private TextMeshProUGUI m_levelText; //レベル
    private bool m_isButtonActive = false; //ボタン操作が必要かどうか

    [SerializeField]
    private SongDataBase m_dataBase; //データベースを格納する変数

    private void Start()
    {
        m_levelText.text = m_dataBase.songData[GManager.instance.songID].levelName;
        m_levelText.color = m_dataBase.songData[GManager.instance.songID].ImageColor;
    }

    private void OnEnable()
    {
        m_rankText.text = string.Empty;

        //スコアをカウントアップ
        StartCoroutine(CountUpInt(m_scoreText, 0, GManager.instance.score, 1.5f));

        //次に判定(perfect、great、good、miss)を表示
        StartCoroutine(ShowJudge());

        //次にランクを表示
        StartCoroutine(ShowRank());

        //最後にボタン
        StartCoroutine(ShowButton());
    }
    private void Update()
    {
       if(m_isButtonActive)
        {
            m_button.UpdateAll();
        }
    }
    //カウントアップ
    IEnumerator CountUpInt(TextMeshProUGUI text,int from,int to,float duration)
    {
        float elpsed = 0.0f;
        while(elpsed < duration)
        {
            elpsed += Time.deltaTime;
            float t = Mathf.Clamp01(elpsed / duration);
            int value = Mathf.RoundToInt(Mathf.Lerp(from, to, t));
            text.text = value.ToString();
            yield return null;
        }
        text.text = to.ToString();
    }

    //判定
    IEnumerator ShowJudge()
    {
        yield return new WaitForSeconds(2.0f);

        m_perfectText.text = GManager.instance.perfect.ToString();  
        m_greatText.text = GManager.instance.great.ToString();  
        m_goodText.text = GManager.instance.good.ToString();
        m_missText.text = GManager.instance.miss.ToString();
    }

    //ランク
    IEnumerator ShowRank()
    {
        yield return new WaitForSeconds(3.0f);

        //---スコアに応じてランクを表示
        if (GManager.instance.score >= 900000)
        {
            m_rankText.text = "S";
            m_rankText.color = Color.yellow;
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SOUND_RANK_S);
        }
        else if (GManager.instance.score >= 800000 && GManager.instance.score < 900000)
        {
            m_rankText.text = "A";
            m_rankText.color = Color.red;
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SOUND_RANK_A);
        }
        else if (GManager.instance.score >= 700000 && GManager.instance.score < 800000)
        {
            m_rankText.text = "B";
            m_rankText.color = Color.blue;
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SOUND_RANK_B);
        }
        else if(GManager.instance.score >= 600000 && GManager.instance.score < 700000)
        {
            m_rankText.text = "C";
            m_rankText.color = Color.orange;
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SOUND_RANK_C);
        }
        else
        {
            m_rankText.text = "D";
            m_rankText.color = Color.gray;
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SOUND_RANK_D);
        }
    }

    //ボタン
    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(3.0f);
        m_isButtonActive = true;
    }
   
}
