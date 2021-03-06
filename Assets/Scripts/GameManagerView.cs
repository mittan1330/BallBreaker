using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManagerView : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text remainBlockText;
    // PostProcessに関する変数
    [SerializeField] private PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;

    [SerializeField] private Text highScoreText;
    [SerializeField] private Text[] gameStatusText;
    [SerializeField] private Animator gameOverTextAnimator;
    [SerializeField] private Animator gameOverButtonAnimator;

    // C# Action
    public event Action OnInputRight;
    public event Action OnInputLeft;
    public event Action OnInputUp;
    public event Action OnInputDown;
    public event Action OnClickRestartButton;

    private void Start()
    {
        // ポストプロセスに関する初期化を実施
        // TODO: 明らかにその都度生成しているために重くなっている印象
        chromaticAberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        chromaticAberration.enabled.Override(true);
        // リスタートボタンボタンが押されたときの処理を実行
        restartButton.onClick.AddListener(() => OnClickRestartButton?.Invoke());
    }

    private void Update()
    {
        // 各種ゲームに関わるInputの処理が行われた際に実行する処理
        if (Input.GetKey(KeyCode.RightArrow))
        {
            OnInputRight?.Invoke();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnInputLeft?.Invoke();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnInputUp?.Invoke();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            OnInputDown?.Invoke();
        }
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "SCORE " + String.Format("{0:D4}", (int)score);
    }

    public void SetTimerText(float timer)
    {
        timerText.text = String.Format("{0:D3}", (int)timer);
    }

    public void SetRemainBlockText(int remainBlock)
    {
        remainBlockText.text = String.Format("{0:D2}", (int)remainBlock) + " / 66";
    }

    public void ApplyPostProcessChromaticAberration(float value)
    {
        chromaticAberration.intensity.Override(value);
        postProcessVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, chromaticAberration);
    }

    public void SetHighScoreText(int highScore)
    {
        highScoreText.text = "HIGH SCORE " + highScore.ToString();
    }

    public void SetGameStatusText(string outPutText)
    {
        for(int i = 0; i < gameStatusText.Length; i++)
        {
            gameStatusText[i].text = outPutText;
        }
    }

    public void StartGameOverAnimator(string animTriggerName)
    {
        gameOverTextAnimator.SetTrigger(animTriggerName);
        gameOverButtonAnimator.SetTrigger(animTriggerName);
    }

}
