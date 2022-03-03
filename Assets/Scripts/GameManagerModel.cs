using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManagerModel : MonoBehaviour
{
    /// <summary> ゲームのスコアを代入する変数 </summary>
    int score;
    /// <summary> ゲームの時間を代入する変数 </summary>
    [SerializeField] float timer = 100;
    /// <summary> ゲームに残ったブロックの数を代入する変数 </summary>
    int remainBlock = 0;
    /// <summary> ゲームでブロックを破壊でコンボした回数を代入する変数 </summary>
    int comboCount = 0;

    //TODO:以下の部分はModelにあるのはまずそう
    /// <summary> ブロックを破壊した際のパーティクルを代入する変数 </summary>
    [SerializeField] GameObject destroyBlockParticle;
    /// <summary> パーティクルを生成する親にあたるTransformを指定する変数 </summary>
    [SerializeField] Transform particlePosition;
    /// <summary> ゲームオーバーのパーティクルを代入する変数 </summary>
    [SerializeField] GameObject gameOverParticle;
    /// <summary> ボールへ追従するパーティクルを代入する変数 </summary>
    [SerializeField] GameObject ballParticle;
    /// <summary> ボールのゲームオブジェクトを代入する変数 </summary>
    [SerializeField] GameObject ball;


    // PostProcessに関する制御変数
    /// <summary> ボールのゲームオブジェクトを代入する変数 </summary>
    public bool isBreakBlock = false;
    /// <summary> ボールのゲームオブジェクトを代入する変数 </summary>
    public bool isBreakBlockMax = false;
    /// <summary>PostProcessの値を指定する変数 </summary>
    float postProcessValue = 0;

    // C# Action
    public event Action<int> OnChangeScore;
    public event Action<int> OnChangeHighScore;
    public event Action<int> OnChangeRemainBlock;
    public event Action<float> OnChangeTime;
    public event Action<float> OnChangePostProcessValue;
    public event Action<string> OnSetGameOverText;
    public event Action<string> OnSetUiAnimation;
    public event Action OnGameEnd;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // ブロックが破壊された際にPostProcess演出の値を計算して反映用の関数を実行
        if (isBreakBlock)
        {
            if (isBreakBlockMax == true)
            {
                postProcessValue += Time.deltaTime * 6;
                if (postProcessValue >= 0.68f) isBreakBlockMax = false;
            }
            else
            {
                postProcessValue -= Time.deltaTime * 3;
                if (postProcessValue <= 0) isBreakBlock = false;
            }
            OnChangePostProcessValue(postProcessValue);
        }

        if (Const.isPlay == false) return;
        SetTimer(Time.deltaTime);

    }

    /// <summary> 
    /// スコア計算してViewへ反映するロジック
    /// </summary>
    public void SetScore(int addScore)
    {
        score += addScore;
        OnChangeScore?.Invoke(score);
    }

    /// <summary> 
    /// タイマーを計算してViewへ反映するロジック
    /// </summary>
    public void SetTimer(float deltaTime)
    {
        timer -= deltaTime;
        OnChangeTime?.Invoke(timer);
    }

    /// <summary>
	/// ゲームのコンボ回数をリセットする際の処理
	/// </summary>
    public void SetComboCount()
    {
        comboCount = 0;
    }

    /// <summary>
	/// ゲームに残ったブロックの数をViewへ反映する関数
	/// </summary>
    public void SetRemainBlock(int block)
    {
        OnChangeRemainBlock?.Invoke(block);
    }

    /// <summary>
	/// ゲームオーバーになった際の処理
	/// </summary>
    public void IsGameOver(string status)
    {
        OnSetGameOverText?.Invoke(status);
        OnSetUiAnimation?.Invoke("isGameOver");
    }

    /// <summary>
	/// ゲームをリスタートする際の処理
	/// </summary>
    public void OnGameRestart()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
	/// ブロックが破壊された際の処理
	/// </summary>
    public void DestroyBlock(GameObject targetBlock)
    {
        remainBlock--;
        SetRemainBlock(remainBlock);
        comboCount++;
        Instantiate(destroyBlockParticle, targetBlock.transform.position, Quaternion.identity, particlePosition);
        Destroy(targetBlock.gameObject);
        SetScore(50 + comboCount * 30);
        isBreakBlock = true;
        isBreakBlockMax = true;
        if (remainBlock < 0)
        {
            GameEnd("GAME CLEAR");
        }
    }

    /// <summary>
	/// ゲームが終了した際の処理
	/// </summary>
    void GameEnd(string status)
    {
        Instantiate(gameOverParticle, ball.transform.position, Quaternion.identity);
        ballParticle.SetActive(false);
        Const.isPlay = false;
        IsGameOver(status);
        ScoreController.Instance.CheckHighScore(score);
        OnGameEnd?.Invoke();
    }

    /// <summary>
	/// ゲームオーバーの際の処理
	/// </summary>
    public void GameOver()
    {
        GameEnd("GAME OVER");
    }

    /// <summary>
	/// ゲームの初期化
	/// </summary>
    void Initialize()
    {
        score = 0;
        Const.isPlay = true;
        remainBlock = 66;
        OnChangeHighScore?.Invoke(ScoreController.Instance.GetHighScore());
    }
}
