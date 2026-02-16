using System;
using UnityEngine;
using TMPro;
using System.Collections;
public class Judge : MonoBehaviour
{

    //変数の宣言
    [SerializeField] 
     private NotesManager m_notesManager; //スクリプト「NotesManager」を格納する変数
    [SerializeField] 
     private Effect m_effect; //エフェクト
    [SerializeField] 
     private TextMeshProUGUI m_comboText; //コンボテキスト
    [SerializeField] 
     private GameObject[] m_objectText; //判定オブジェクト(perfect,great,good,miss)
    [SerializeField] 
     private GameObject m_scoreText; //スコアテキスト
    [SerializeField] 
     private GameObject m_perfectText;  //perfectテキスト
    [SerializeField] 
     private GameObject m_greatText;  //greatテキスト
    [SerializeField] 
     private GameObject m_goodText; //goodテキスト
    [SerializeField] 
     private GameObject m_missText; //missテキスト 
    [SerializeField] 
     private GameObject m_levelText; //Levelテキスト
    [SerializeField]  
     private SongDataBase m_dataBase; //データベースを格納する変数
    [SerializeField]
    private TextMeshProUGUI m_finishText; //終了時に表示させるテキストを格納する変数

    private float m_endTime = 0.0f; //曲の終了時間

    // ポーズ時間補正
    private float totalPauseTime = 0f;
    private float pauseStartTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //最後のノーツ時間を保存
        if (m_notesManager.NotesTime.Count > 0)
        {
            m_endTime = m_notesManager.NotesTime[m_notesManager.NotesTime.Count - 1];
        }

        //レベル名とカラーをUIに反映
        m_levelText.GetComponent<TextMeshPro>().text = m_dataBase.songData[GManager.instance.songID].levelName;
        m_levelText.GetComponent<TextMeshPro>().color = m_dataBase.songData[GManager.instance.songID].ImageColor;
    }

    //毎フレームの更新処理
    // Update is called once per frame
    void Update()
    {

        if (!GManager.instance.start) return;



        // ポーズ中は判定を止める
        if (GManager.instance.isPause)
        {
            if (pauseStartTime == 0f)
                pauseStartTime = Time.time;
            return;
        }
        else
        {
            if (pauseStartTime > 0f)
            {
                totalPauseTime += Time.time - pauseStartTime;
                pauseStartTime = 0f;
            }
        }

        // 判定基準時間の補正
        float correctedTime = Time.time - totalPauseTime - GManager.instance.startTime;


            //キー入力
            CheckKey(KeyCode.D, 0,correctedTime); //左端　
            CheckKey(KeyCode.F, 1,correctedTime); //左中
            CheckKey(KeyCode.J, 2,correctedTime); //右中
            CheckKey(KeyCode.K, 3,correctedTime); //右端

            //叩くノーツがない場合
            if(m_notesManager.NotesTime.Count == 0)
            {

              

                //Missがなかった場合Full Combo
                if(GManager.instance.miss == 0)
                {
                    m_finishText.text = "Full Combo";
                    m_finishText.color = Color.yellow;
                }

                //MissもGreatもGoodもなかった場合All Perfect
                if(GManager.instance.great  == 0&& GManager.instance.good == 0 && GManager.instance.miss == 0)
                {
                    m_finishText.text = "All Perfect";
                    m_finishText.color = Color.yellow;
                }

                    StartCoroutine("SceneResult");
            }

            

            //本来ノーツをたたくべき時間から入力がなかった場合
            if ( m_notesManager.NotesTime.Count > 0 && correctedTime > m_notesManager.NotesTime[0])
            {

               

                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.MISS);
                Debug.Log("Miss"); //ログ
                GManager.instance.miss++; //miss数の加算
                GManager.instance.combo = 0; //コンボ数を初期化
                Message(3);
                DeleteData(0); //ノーツの削除
            }
        
    }

  
    //キー入力
    void CheckKey(KeyCode key, int num, float correctedTime)
    {
        if (!Input.GetKeyDown(key)) return;

        if (m_notesManager.LaneNum.Count > 0 && m_notesManager.LaneNum[0] == num)
            JudgeMent(Mathf.Abs(correctedTime - m_notesManager.NotesTime[0]), 0);
        else if (m_notesManager.LaneNum.Count > 1 && m_notesManager.LaneNum[1] == num)
            JudgeMent(Mathf.Abs(correctedTime - m_notesManager.NotesTime[1]), 1);
    }

    //判定処理
    void JudgeMent(float timeLag,int numOffset)
    {

        //本来ノーツを叩くべき時間と実際にノーツを叩いた時間の誤差が0.1秒以下だったら
        if (timeLag <= 0.10 + GManager.instance.timingOffset * 0.1f)
        {
           
            Debug.Log("Perfect"); //ログ
            GManager.instance.ratioScore += 5.0f; //スコアの加算
            GManager.instance.perfect++; //perfect数の加算
            GManager.instance.combo++; //コンボ数の加算
            Message(0);
            Effect();
            DeleteData(numOffset); //ノーツの削除
        }
        //本来ノーツを叩くべき時間と実際にノーツを叩いた時間の誤差が0.15秒以下だったら
        else if (timeLag <= 0.12 + GManager.instance.timingOffset * 0.1f)
        {
          
            Debug.Log("Great"); //ログ
            GManager.instance.ratioScore += 3.0f; //スコアの加算
            GManager.instance.great++; //great数の加算
            GManager.instance.combo++; //コンボ数の加算
            Message(1);
            Effect();
            DeleteData(numOffset); //ノーツの削除
        }
        //本来ノーツを叩くべき時間と実際にノーツを叩いた時間の誤差が0.20秒以下だった場合
        else if(timeLag <= 0.14 + GManager.instance.timingOffset * 0.1f)
        {
           
            Debug.Log("Good"); //ログ
            GManager.instance.ratioScore += 1.0f; //スコアの加算
            GManager.instance.good++; //good数の加算
            GManager.instance.combo++; //コンボ数の加算
            Message(2);
            Effect();
            DeleteData(numOffset); //ノーツの削除
        }
    }
    //引数の絶対値を返す変数
    float GetABS(float num)
    {
        if(num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }

    //すでにたたいたノーツを削除する関数
    void DeleteData(int numOffset)
    {
        /*
        //ノーツのオブジェクトを削除
        if(m_notesManager.NoteObj.Count > numOffset)
        {
            Destroy(m_notesManager.NoteObj[numOffset]);
            m_notesManager.NoteObj.RemoveAt(numOffset);
        }
        */

        m_notesManager.NotesTime.RemoveAt(numOffset);
        m_notesManager.LaneNum.RemoveAt(numOffset);
        m_notesManager.NoteType.RemoveAt(numOffset);

        // スコア計算
        GManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GManager.instance.ratioScore / GManager.instance.maxScore * 1000000) / 1000000);

        //UIの更新
        m_comboText.text = GManager.instance.combo.ToString(); //コンボ
        m_scoreText.GetComponent<TextMeshPro>().text = GManager.instance.score.ToString(); //スコア
        m_perfectText.GetComponent<TextMeshPro>().text = GManager.instance.perfect.ToString(); //perfect
     　 m_greatText.GetComponent<TextMeshPro>().text = GManager.instance.great.ToString(); //great
        m_goodText.GetComponent<TextMeshPro>().text = GManager.instance.good.ToString(); //good
        m_missText.GetComponent<TextMeshPro>().text = GManager.instance.miss.ToString(); //miss
    }

    //メッセージの表示
    void Message(int index)
    {
        if (GManager.instance.isPause) return;

        Instantiate(m_objectText[index], new Vector3(m_notesManager.LaneNum[0] - 1.5f, 2.0f, -0.9f), Quaternion.identity);
    }

    void Effect()
    {
        // ノーツのレーン位置にエフェクトを出す
        m_effect.PlayEffect(
            new Vector3(m_notesManager.LaneNum[0] - 1.5f, 2.0f, -0.5f),
            Quaternion.identity
        );
    }

    //リザルト
    IEnumerator SceneResult()
    {
       
        //10秒後にリザルトシーンに遷移する
        yield return new WaitForSeconds(10.0f);
        SceneController.instance.SceneChange("ResultScene");
    }
}
