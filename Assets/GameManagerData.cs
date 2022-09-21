using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="GameManagerData",menuName = "GameManagerData")]
public class GameManagerData : ScriptableObject
{
    // 次のシーン名
    [SerializeField] private string nextSceneName;

    // 選択キャラプレハブ
    [SerializeField] private GameObject character;

    /// <summary>
    /// データの初期化
    /// </summary>
    private void OnEnable()
    {
        // タイトルシーンの時だけリセット
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // シーン名を空に
            nextSceneName = "";
            // 選択キャラがないもない状態に 
            character = null;
        }
    }

    /// <summary>
    /// 次にシーン名のセット
    /// </summary>
    /// <param name="_nextSceneNeme"> 次のシーンの名前 </param>
    public void SetNextSceneName(string _nextSceneNeme)
    {
        this.nextSceneName = _nextSceneNeme;
    }

    /// <summary>
    /// 次のシーン名を取得する
    /// </summary>
    /// <returns></returns>
    public string GetNextSceneName()
    {
        return nextSceneName;
    }

    /// <summary>
    /// 使用キャラクターのセット
    /// </summary>
    /// <param name="_character"> 選んだ使用キャラ </param>
    public void SetCharacter(GameObject _character)
    {
        this.character = _character;
    }

    /// <summary>
    /// 選んだ使用キャラクターの取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetCharacter()
    {
        return character;
    }
}
