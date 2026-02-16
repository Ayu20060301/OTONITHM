using UnityEngine;
using Effekseer;

public class Effect : MonoBehaviour
{
    [SerializeField] private EffekseerEffectAsset effectAsset; // インスペクタから設定

    // 他のスクリプトから呼び出せるように public にする
    public EffekseerHandle PlayEffect(Vector3 pos, Quaternion rot)
    {
        if (effectAsset == null)
        {
            Debug.LogError("EffekseerEffectAsset が設定されていません！");
            return default;
        }

        // エフェクト再生
        EffekseerHandle handle = EffekseerSystem.PlayEffect(effectAsset, pos);

        // 回転も適用
        handle.SetRotation(rot);

        return handle;
    }


}
