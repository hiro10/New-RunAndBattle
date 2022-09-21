using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerAttacke_Sword : MonoBehaviour
{
    // アニメーター
    Animator animator;

    // アニメーション中フラグ
    public bool isAttack ;

    // 攻撃ボタンを押したかどうか
    //TODO:攻撃状態になったらに変更
    public bool OnAttackButton;

    PlayerController playerController;

    // スキル用
    [SerializeField] GameObject objButton;	// Image＋Buttonのオブジェクトをアサイン
    [SerializeField] Text lblText;			// 残り時間を表示するTextオブジェクトをアサイン
    Image imgButton;
    Button btnButton;
    // 何秒でボタンが再アクティブになるか
    const float SKILLCOUNT = 5;
    float countTime;

    // 攻撃ボタン用
    // 何秒でボタンが再アクティブになるか
    const int ATTACKCOUNT = 2;
    int attackTime;

    // スキルタイマー
    float skillTimer;

    // 攻撃タイマー
    float attackTimer;

    private void Awake()
    {
        imgButton = objButton.GetComponent<Image>();
        btnButton = objButton.GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;

        OnAttackButton = false;

        animator = GetComponent<Animator>();

        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        SkillTime(SKILLCOUNT);
        ChangeMoveMode();



    Debug.Log("Attack" + isAttack);
    }

    /// <summary>
    /// 攻撃ボタン押し込み時
    /// </summary>
    public void AttackOnButton()
    {
        
        attackTime = ATTACKCOUNT;
        OnAttackButton = true;
        
       
        if (isAttack == false)
        {
            animator.SetTrigger("Attack");

            animator.SetInteger("AttackType", 0);

            isAttack = true;

            Debug.Log("攻撃");
           

        }
    }

    /// <summary>
    /// スキルボタン押下時
    /// </summary>
    public void SkillOnButton()
    {
        // カウントタイムのリセット（スキルのリチャージタイム）
        countTime = SKILLCOUNT;
        attackTime = ATTACKCOUNT;
        // interactableをfalseにすることでボタンを押せなくする
        btnButton.interactable = false;

        OnAttackButton = true;
        animator.SetTrigger("Attack");

        animator.SetInteger("AttackType", 1);

        isAttack = true;

        Debug.Log("スキル");
    }

    private void ChangeMoveMode()
    {
        attackTimer += Time.deltaTime;
        // 毎秒処理
        if (attackTimer > 1f)
        {
            attackTimer = 0f;
            if (attackTime > 0)
            {

                // スキルタイムの減少
                attackTime--;
                if(attackTime<=0)
                {
                    playerController.ChangeMoveState();
                }

            }
        }
    }

    private void SkillTime(float _skillTime)
    {
        skillTimer += Time.deltaTime;
        // 毎秒処理
        if (skillTimer > 1f)
        {
            skillTimer = 0f;
            if (countTime > 0)
            {
               
                // スキルタイムの減少
                countTime--;
                // 残り時間を表示
                lblText.text = countTime.ToString();
                // 
                imgButton.fillAmount = 1- (float)countTime / (float)_skillTime;
                
            }
            else
            {
                lblText.text = ("Skill");
                btnButton.interactable = true;
            }
        }
    }

    /// <summary>
    /// 攻撃アニメーション終了コールイベント
    /// </summary>
    private void  Anim_AttackEnd()
    {
       
        Debug.Log("End");
       
        isAttack = false;
        OnAttackButton = false;

    }

   
}
