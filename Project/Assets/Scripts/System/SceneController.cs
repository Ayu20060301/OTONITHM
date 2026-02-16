using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを管理するクラス
/// SingletonMonoBehaviourを継承している
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    public BaseScene currentScene;

    /// <summary>
    /// 初期化処理
    /// フレームレートを60に設定
    /// </summary>
    private void Awake()
    {
        //60フレーム
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// シーンチェンジ
    /// </summary>
    /// <param name="シーンの名前">シーンの名前</param>
    public void SceneChange(string _SceneName,float fadeTime = 1.0f)
    {
        SceneManager.LoadScene(_SceneName);
    }

   

}
