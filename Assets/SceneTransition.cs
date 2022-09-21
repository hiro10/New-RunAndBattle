using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移用
/// </summary>
public class SceneTransition : MonoBehaviour
{
    private GameManagerData gameManagerData;

    private void Start()
    {
        gameManagerData = FindObjectOfType<GameManager>().GetGameManagerData();
    }

    public void GoToOtherScene(string stage)
    {
        // 次にシーンデータをGameManagerに保存
        gameManagerData.SetNextSceneName(stage);

        // キャラクター選択シーンへ
        SceneManager.LoadScene("SelectCharacter");
    }

    public void GameStart()
    {
        //　MyGameManagerDataに保存されている次のシーンに移動する
        SceneManager.LoadScene(gameManagerData.GetNextSceneName());
    }
}
