
using UnityEngine;

public class GManager : SingletonMonoBehaviour<GManager>
{
    public int songID; //曲のID
    public float noteSpeed; //ノーツスピード
    public float timingOffset; //オフセット
    public float volume; //ボリューム
    public bool postProcessingEnabled;

    public float ratioScore; //スコアの比率
    public float maxScore; //スコアの最大値
    
    public bool start; //スタートフラグ
    public float startTime; //スタート時間

    public int score; //スコア数を記録
    public int combo; //コンボ数を記録

    public int perfect; //perfect数の記録
    public int great; //great数の記録
    public int good;  //good数の記録
    public int miss; //miss数の記録

    public int selectIndex; //選択インデックス
    public bool isPause; //ポーズ中

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
