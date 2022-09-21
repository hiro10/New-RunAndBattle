using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{

    // 取得時のエフェクトプレハブ
    [SerializeField] GameObject effectParticle = null;

    // アイテムのレンダラー
    [SerializeField] Renderer itemRenderer = null;

    // エフェクト実行フラグ
    bool isEffect = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
