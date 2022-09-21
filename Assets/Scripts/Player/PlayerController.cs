using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerController : MonoBehaviour
{
    
    public class Status
    {
        // HP
        public int Hp = 10;

        // 攻撃力
        public int Power = 100;
    }
    // 基本プレイヤーステータス
    [SerializeField] Status DefaultStatus = new Status();

    // 現在ステータス
    public Status CurrentStatus = new Status();

    // HPUI用スライダー
    // Hpバー用
    public Slider slider;

    // ボタン
    [SerializeField] GameObject attackButton;
    //[SerializeField] GameObject putButton;

    // 武器
    [SerializeField] GameObject Weapon;
    public CharacterController controller;

    public Transform cam;

    public float speed = 6.0f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    // アニメーター
    Animator animator = null;

    // ジョイスティック
    [SerializeField] VariableJoystick joystick;

    // ジョイスティック用のx,y
    float x = 0;
    float y = 0;

    PlayerAttacke_Sword playerAttacke_Sword;

    // 死亡したかどうか
    public bool Isdead => CurrentState == State.Dead;


    // Playerの状態
    public enum State
    {
        // 移動状態
        Moving = 0,

        // 戦闘状態
        Battle,

        // 死亡
        Dead
    }

    private State _currentState = State.Moving;

    private State CurrentState
    {
        get => _currentState;
        set
        {
            //// 死亡したらステートの変更はしない
            //if(IsDedad)
            //{
            //    return
            //}
            _currentState = value;
        }
    }

    private void Start()
    {
        // プレイヤーステータスの初期化
        InitPlayer();

        // Hpゲージ用スライダーの取得
        slider = GameObject.Find("PlayerHpGauge").GetComponent<Slider>();

        // 武器を表示しない
        Weapon.SetActive(false);
        // Animatorを取得し保管.
        animator = GetComponent<Animator>();

        playerAttacke_Sword = GetComponent<PlayerAttacke_Sword>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (CurrentState)
        {
            case State.Moving:
                {
                    Move();
                    break;
                }
            case State.Battle:
                {
                    Battle();
                    break;
                }
        }

        // Hpゲージに反映
        slider.value = (float)CurrentStatus.Hp / (float)DefaultStatus.Hp;

       

    }

    private void Move()
    {
        // 
        Weapon.SetActive(false);
        //putButton.SetActive(true);
        //attackButton.SetActive(false);

        // 待機モーションの設定
        animator.SetFloat("Blend", 0f, 0.1f, Time.deltaTime);

        // ジョイスティック用
        x = joystick.Horizontal;
        y = joystick.Vertical;
        float horizontal = x;//Input.GetAxisRaw("Horizontal");
        float vertical = y;//Input.GetAxisRaw("Vertical");


        Vector3 direction = new Vector3(horizontal, 0f, vertical);//.normalized;

        // 入力が0.2以上なら
        if (direction.magnitude >= 0.2f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Input.GetButtonDown Jump");
            }

            // 歩きと走りの切り替え
            // TODO::もうちょっとどうにかできそう
            if (horizontal > 0.5f || vertical > 0.5f || horizontal < -0.5f || vertical < -0.5f)
            {
                animator.SetFloat("Blend", 2);
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
            else
            {
                animator.SetFloat("Blend", 1);
                controller.Move(moveDirection.normalized * Time.deltaTime);
            }

        }

    }

    private void Battle()
    {

        Weapon.SetActive(true);
       // putButton.SetActive(false);
        //attackButton.SetActive(true);

        x = joystick.Horizontal;
        y = joystick.Vertical;
        float horizontal = x;//Input.GetAxisRaw("Horizontal");
        float vertical = y;//Input.GetAxisRaw("Vertical");


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }

    /// <summary>
    /// 攻撃状態への変更
    /// </summary>
    public void ChangeBattleState()
    {
        _currentState = State.Battle;

    }

    /// <summary>
    /// 移動状態への変更
    /// </summary>
    public void ChangeMoveState()
    {
        _currentState = State.Moving;
    }

    //キャラクターコントローラーの衝突処理
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Sphereにぶつかれば、パーティクルを発生させる
        if (hit.gameObject.name == "Item")
        {
            Debug.Log("ぶつかった");
            hit.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// プレイヤーステータスの初期化
    /// </summary>
    private void InitPlayer()
    {
        CurrentStatus.Hp = DefaultStatus.Hp;
        CurrentStatus.Power = DefaultStatus.Power;
    }
}
