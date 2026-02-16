using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
     private Image[] m_buttonImage;

    // Update is called once per frame
    void Update()
    {
        
        //上矢印キー入力
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GManager.instance.selectIndex > 0)
            {
                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT); //選択音
                GManager.instance.selectIndex--;
            }
        }

        //下矢印キー入力
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GManager.instance.selectIndex < m_buttonImage.Length - 1)
            {
                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT); //選択音
                GManager.instance.selectIndex++;
            }
        }
        
        //エンターキーを押した場合
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.DECISION);
            SceneController.instance.SceneChange("SelectScene");
                ButtonUpdate();

        }

        //見た目更新
        HighLightButton();
    }

    
    //ボタンの見た目
    void HighLightButton()
    {
        for (int i = 0; i < m_buttonImage.Length; i++)
        {
            m_buttonImage[i].color = (i == GManager.instance.selectIndex) ? Color.gray : Color.white;
        }
    }

    //ボタンの更新
    void ButtonUpdate()
    {
        switch (GManager.instance.selectIndex)
        {
            case 0: //シーン遷移
                SceneController.instance.SceneChange("SelectScene");
                break;
            case 1: //やめる
                Application.Quit();
                break;
        }
    }
}
