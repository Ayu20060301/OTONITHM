using UnityEngine;

public class Reset : MonoBehaviour
{

    //スコアなどのリセット
   public void ResetUI()
    {
        GManager.instance.score = 0;
        GManager.instance.maxScore = 0;
        GManager.instance.ratioScore = 0;
        GManager.instance.perfect = 0;
        GManager.instance.great = 0;
        GManager.instance.good = 0;
        GManager.instance.miss = 0;
        GManager.instance.combo = 0;
    }
}
