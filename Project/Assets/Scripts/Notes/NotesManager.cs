using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

//Jsonファイルの情報
[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;

}

//ノーツ一つ分の情報
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

/// <summary>
/// ノーツオブジェクトを管理するクラス
/// </summary>
public class NotesManager : MonoBehaviour
{

    public int noteNum; //総ノーツ数
    private string m_songName; //曲名を入れる変数

    public List<int> LaneNum = new List<int>(); //何番のレーンにノーツが落ちてくるか
    public List<int> NoteType = new List<int>(); //何ノーツか
    public List<float> NotesTime = new List<float>(); //ノーツが判定線と重なる時間
    public List<GameObject> NoteObj = new List<GameObject>(); //ノーツオブジェクト

    private float m_notesSpeed = 6.0f; //ノーツスピード
    [SerializeField] 
     private GameObject m_noteObj; //ノーツオブジェクトを格納する変数
    [SerializeField] 
     private SongDataBase m_dataBase; //データベースを格納する変数

    private void OnEnable()
    {
        m_notesSpeed = GManager.instance.noteSpeed * 2; //ノーツスピード
        noteNum = 0;
        m_songName = m_dataBase.songData[GManager.instance.songID].songName; //JSONファイル名と一致させる
        Load(m_songName);
    }

    /// <summary>
    /// ロード
    /// </summary>
    /// <param name="songName">JSONファイル名</param>
    private void Load(string songName)
    {
        //Jsonファイルの読み込み
        TextAsset textAsset = Resources.Load<TextAsset>(songName);
        string inputString = textAsset.text;
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length; //総ノーツ数の指定
        GManager.instance.maxScore = noteNum * 5; //スコアの最大値の設定

        for(int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f + GManager.instance.timingOffset * 0.001f;

            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float laneWidth = 2.125f; //各レーンの横幅
            int laneCount = 4; //レーンの数
            float baseX = -((laneCount - 2)- (float)0.5f) * laneWidth;

            float x = baseX + inputJson.notes[i].block * laneWidth;
            float z = NotesTime[i] * m_notesSpeed;
            //ノーツの生成
            NoteObj.Add(Instantiate(m_noteObj, new Vector3(x, 0.65f, z), Quaternion.identity));

        }
    }
}
