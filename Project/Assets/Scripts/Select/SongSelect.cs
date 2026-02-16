using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SongSelect : MonoBehaviour
{
    [SerializeField] SongDataBase dataBase; //SongDataBaseを格納する変数

    int selectIndex; //選択インデックス
    AudioSource audio;
    AudioClip clip;
    string songName;
    string clipName;

    [SerializeField] Image arrowImage; //選択矢印
    [SerializeField] RectTransform[] songPositions; //各曲の位置
    [SerializeField] GameObject SettingPanel; //設定用のパネル
    [SerializeField] SettingManager settingManager; //スクリプト「SettingManager」を格納する変数
    [SerializeField] Text saveText; 

    bool isSettingOpen = false; //設定フラグ


    private void Start()
    {
        selectIndex = GManager.instance.selectIndex;
        audio = GetComponent<AudioSource>();
        clipName = "FREEDOM-DiVE↓";
        clip = (AudioClip)Resources.Load("Musics/" + clipName);
        songName = dataBase.songData[selectIndex].songName;
        audio.PlayOneShot(clip);
        SongUpdateAll();
    }

    private void Update()
    {
    
        //設定画面を開いている場合のみの処理
        if(isSettingOpen)
        {
            //設定画面を閉じる
            if(isSettingOpen && Input.GetKeyDown(KeyCode.Escape))
            {

                StartCoroutine("SaveText");
                settingManager.SaveSettings();
                SettingPanel.SetActive(false);
                isSettingOpen = false;
            }
            return;
        }

        
        //下矢印キーを押した場合
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(selectIndex > 0)
            {
                selectIndex--;
                SongUpdateAll();
            }
        }
        //上矢印キーを押した場合
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(selectIndex < dataBase.songData.Length - 1)
            {
                selectIndex++;
                SongUpdateAll();
            }
        }

        
        //Enterキーを押すとGamerSceneに遷移する
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SongStart();
        }

        //Tabキーを押すと設定画面が開く
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SettingPanel.SetActive(true);
            isSettingOpen = true;

        }

        //Escキーを押すとタイトルシーンに戻る
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneController.instance.SceneChange("TitleScene");
        }

    }

    IEnumerator SaveText()
    {
        saveText.text = "保存しました";
        yield return new WaitForSeconds(2.0f);

        saveText.text = string.Empty;
    }
    private void SongUpdateAll()
    {
        SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT);
        songName = dataBase.songData[selectIndex].songName;

        //矢印を選択中の曲位置に移動
        if(arrowImage != null && songPositions != null && selectIndex < songPositions.Length)
        {
            arrowImage.rectTransform.position = songPositions[selectIndex].position;
        }
      
    }

    //開始
    public void SongStart()
    {
        SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.DECISION);
        GManager.instance.songID = selectIndex;
        SceneController.instance.SceneChange("GameScene");
    }
}
