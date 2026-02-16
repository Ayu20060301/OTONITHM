using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SongData", menuName = "楽曲データを作成")]

public class SongDatas : ScriptableObject
{
    [SerializeField] public string songID; //ID
    [SerializeField] public string songName; //曲名
    [SerializeField] public Color ImageColor; //カラー
    [SerializeField] public string levelName; //レベル
}
