using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour
{
    private GameManagerData gameManagerData;
    [SerializeField]private GameObject gameStartButton;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        // 世界に一つだけのGameManagerからGameManagerDataを取得する
        gameManagerData = FindObjectOfType<GameManager>().GetGameManagerData();
        // ゲームスタートボタンを取得する
        //gameStartButton = transform.parent.Find("ButtonPanel/GameStart").gameObject;
        // ゲームスターとボタンを無効にする
        gameStartButton.SetActive(false);
    }

    /// <summary>
    /// キャラクターを選択したときに実行したキャラクターデータをGamaManagerDataにセット
    /// </summary>
    /// <param name="character"> 選択したキャラクター </param>
    public void OnSelectCharacter(GameObject _character)
    {
        // ボタンの選択状態を解除して選択したボタンのハイライト表示を可能にするために実行
        EventSystem.current.SetSelectedGameObject(null);

        // GameDataManagerDataにキャラクターデータをセット
        gameManagerData.SetCharacter(_character);

        // ゲームスタートボタンを有効にする
        gameStartButton.SetActive(true);
    }

    /// <summary>
    /// キャラクターを選択したとき背景をonにする
    /// </summary>
    /// <param name="buttonNumber"></param>
    public void SwitchButtonBackground(int buttonNumber)
    {
        // このオブジェクトの中にある子オブジェクトの数を計る
        for (int i = 0; i < transform.childCount; i++) 
        {
            // キャラクターが選択されたら表示
            if(i==buttonNumber-1)
            {
                transform.GetChild(i).Find("Background").gameObject.SetActive(true);
            }
            // されなければ非表示
            else
            {
                transform.GetChild(i).Find("Background").gameObject.SetActive(false);
            }
        }
    }

}
