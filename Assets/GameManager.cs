using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // この世界に一つだけ
    private static GameManager gameManager;
    // ゲーム全体で管理するデータ
    [SerializeField] private GameManagerData gameManagerData = null;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Awake()
    {
        if(gameManager==null)
        {
            gameManager = this;
            // 他のシーンに遷移しても壊されないように
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// GameManagerDataを返す
    /// </summary>
    /// <returns></returns>
    public GameManagerData GetGameManagerData()
    {
        return gameManagerData;
    }
}
