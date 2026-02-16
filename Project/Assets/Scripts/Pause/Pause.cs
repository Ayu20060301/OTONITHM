using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField]
     GameObject pausePanel; //ポーズパネル
    [SerializeField] 
     Image[] buttonImage;  //ボタン画像
    [SerializeField]
     Reset reset; //「Reset」スクリプトを格納する変数
    [SerializeField]
    TextMeshProUGUI timeText; 
    int selectIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GManager.instance.isPause = false;
        selectIndex = 0;
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ESCキーを押したらポーズ中にする
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            

            if(!GManager.instance.start)
            {
                return;
            }

            GManager.instance.isPause = !GManager.instance.isPause;
            pausePanel.SetActive(GManager.instance.isPause);
        }

        if (!GManager.instance.isPause) return; //ポーズ中のみ操作

        //左キーを押した場合
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(selectIndex > 0)
            {
                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT); //選択音
                selectIndex--;
            }
        }
        //右キーを押した場合
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(selectIndex < buttonImage.Length - 1)
            {
                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT); //選択音
                selectIndex++;
            }
        }

        //Enterキー
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.DECISION); //決定音
            ButtonUpdate();
        }

        //ボタンの見た目更新
        HighLightButton();
    }

    //ボタンの見た目
    void HighLightButton()
    {
        for(int i = 0; i < buttonImage.Length; i++)
        {
            buttonImage[i].color = (i == selectIndex) ? Color.gray : Color.white;
        }
    }

    //ボタンの更新
    void ButtonUpdate()
    {
        switch(selectIndex)
        {
            case 0: //続ける
                pausePanel.SetActive(false); //ポーズ画面を閉じる
                StartCoroutine(CountDown());
               
                break;
            case 1: //リトライ
                SceneController.instance.SceneChange("GameScene");
                reset.ResetUI();
                break;
            case 2: //セレクトに戻る
                SceneController.instance.SceneChange("SelectScene");
                reset.ResetUI();
                break;
        }
    }

    //カウントダウン
    private IEnumerator CountDown()
    {
        float countDown = 3.0f;

        // 判定を止める
        GManager.instance.isPause = true;
        GManager.instance.start = false;

        countDown -= Time.deltaTime;

        while (countDown > 0)
        {
            timeText.text = Mathf.Ceil(countDown).ToString(); //整数にして表示（3,2,1）
            yield return new WaitForSeconds(1f);              //1秒待機
            countDown -= 1f;
        }
        //最後に "Start!" を表示
        timeText.text = "Start!";
        yield return new WaitForSeconds(1f);

        //表示を消す
        timeText.text = "";

        GManager.instance.isPause = false;
        GManager.instance.start = true;

    }
}
