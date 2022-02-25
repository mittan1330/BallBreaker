using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManagerModel : MonoBehaviour
{
    int score;
    [SerializeField] float timer = 100;
    int remainBlock = 0;
    int comboCount = 0;

    [SerializeField] GameObject destroyBlockParticle;
    [SerializeField] Transform particlePosition;
    [SerializeField] GameObject gameOverParticle;
    [SerializeField] GameObject ballParticle;
    [SerializeField] GameObject ball;


    // PostProcessに関する制御変数
    public bool isBreakBlock = false;
    public bool isBreakBlockMax = false;
    float postProcessValue = 0;


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
        score = 0;
        Const.isPlay = true;
        remainBlock = 66;
        OnChangeHighScore?.Invoke(ScoreController.Instance.GetHighScore());
    }

    // Update is called once per frame
    void Update()
    {
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
    /// スコア計算のロジック
    /// </summary>
    public void SetScore(int addScore)
    {
        score += addScore;
        OnChangeScore?.Invoke(score);
    }

    public void SetTimer(float deltaTime)
    {
        timer -= deltaTime;
        OnChangeTime?.Invoke(timer);
    }

    public void SetComboCount(int count)
    {
        comboCount = count;
    }

    public void SetRemainBlock(int block)
    {
        OnChangeRemainBlock?.Invoke(block);
    }

    public void IsGameOver(string status)
    {
        
        OnSetGameOverText?.Invoke(status);
        OnSetUiAnimation?.Invoke("isGameOver");
    }

    public void OnGameRestart()
    {
        SceneManager.LoadScene("Main");
    }

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


    void GameEnd(string status)
    {
        Instantiate(gameOverParticle, ball.transform.position, Quaternion.identity);
        ballParticle.SetActive(false);
        Const.isPlay = false;
        IsGameOver(status);
        ScoreController.Instance.CheckHighScore(score);
        OnGameEnd?.Invoke();
    }

    public void GameOver()
    {
        GameEnd("GAME OVER");
    }
}
