using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPresenter : MonoBehaviour
{
    // ViewとModelを繋ぐために変数として宣言
    [SerializeField] private GameManagerModel gameManagerModel;
    [SerializeField] private PlayerManagerModel playerManagerModel;
    [SerializeField] private BallManagerModel ballManagerModel;
    [SerializeField] private GameManagerView gameManagerView;

    // Start is called before the first frame update
    private void Start()
    {
        // Model → View
        // Modelの値の変更を監視する
        gameManagerModel.OnChangeScore += gameManagerView.SetScoreText;
        gameManagerModel.OnChangeTime += gameManagerView.SetTimerText;
        gameManagerModel.OnChangePostProcessValue += gameManagerView.ApplyPostProcessChromaticAberration;
        gameManagerModel.OnSetGameOverText += gameManagerView.SetGameStatusText;
        gameManagerModel.OnSetUiAnimation += gameManagerView.StartGameOverAnimator;
        gameManagerModel.OnChangeRemainBlock += gameManagerView.SetRemainBlockText;
        gameManagerModel.OnChangeHighScore += gameManagerView.SetHighScoreText;

        // View → Model
        // Viewの右矢印が押されているかを監視する
        gameManagerView.OnClickRestartButton += gameManagerModel.OnGameRestart;

        // View → PlayerManagerModel
        gameManagerView.OnInputRight += playerManagerModel.OnMovePlayerRight;
        gameManagerView.OnInputLeft += playerManagerModel.OnMovePlayerLeft;
        gameManagerView.OnInputUp += playerManagerModel.OnMovePlayerUp;
        gameManagerView.OnInputDown += playerManagerModel.OnMovePlayerDown;


        //PlayerManagerModel → ballManagerModel
        gameManagerModel.OnGameEnd += ballManagerModel.EndMovePlayer;


        //ballManagerModel → PlayerManagerModel
        ballManagerModel.OnGameOver += gameManagerModel.GameOver;
        ballManagerModel.SetComboCount += gameManagerModel.SetComboCount;
        ballManagerModel.OnDestroyBlock += gameManagerModel.DestroyBlock;
    }
}
