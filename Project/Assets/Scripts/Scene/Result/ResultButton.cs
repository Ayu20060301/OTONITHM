using UnityEngine;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour
{
    [SerializeField] Image[] buttonImage;
    [SerializeField] Reset reset;

    public void UpdateAll()
    {
        //左キーを押した場合
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(GManager.instance.selectIndex > 0)
            {
                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT);
                GManager.instance.selectIndex--;
            }         
        }

        //右キーを押した場合
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GManager.instance.selectIndex < buttonImage.Length - 1)
            {
                SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.SELECT);
                GManager.instance.selectIndex++;
            }
        }
        //Enterキーを押した場合
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SoundEffectManager.instance.PlaySE(SoundEffectManager.SoundType.DECISION); //決定音
            ButtonUpdate();
        }

        //見た目
        HighLightButton();
    }

    //ボタンの見た目
    public void HighLightButton()
    {
        for(int i = 0; i < buttonImage.Length; i++)
        {
            buttonImage[i].color = (i == GManager.instance.selectIndex) ? Color.gray : Color.white;
        }
    }

    //ボタンの更新
    public void ButtonUpdate()
    {
        switch (GManager.instance.selectIndex)
        {
            case 0: //Retry
                SceneController.instance.SceneChange("GameScene");
                reset.ResetUI();
                break;
            case 1: //Next
                SceneController.instance.SceneChange("SelectScene");
                reset.ResetUI();
                break;
        }
    }
}
