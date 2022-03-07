using UnityEngine;
using System;

public class BallManagerModel : MonoBehaviour
{
    /// <summary> ボールの縦向きの速度を代入する変数 </summary>
    private float speedHead;
    /// <summary> ボールの横向きの速度を代入する変数 </summary>
    private float speedSide;

    // C# Action
    public event Action<GameObject> OnDestroyBlock;
    public event Action OnGameOver;
    public event Action SetComboCount;

    // Start is called before the first frame update
    private void Start()
    {
        // ボールの進む速さをConstから取得してそれぞれの速度へ代入する
        speedHead = Const.ballSpeed;
        speedSide = Const.ballSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        // ボールを移動させる処理
        transform.position += new Vector3(speedSide, speedHead, 0f) * Time.deltaTime;
    }

    // ボールが各オブジェクトへ当たった際の処理を記載。
    // TODO:将来的にViewへ移植
    private void OnCollisionEnter(Collision collision)
    {
        // 横の壁へ当たった際の処理
        if (collision.gameObject.tag == Const.GameTags.Side.ToString())
        {
            speedSide = -speedSide;
        }
        // 天井の壁へ当たった際の処理
        if (collision.gameObject.tag == Const.GameTags.Head.ToString())
        {
            speedHead = -speedHead;
            SetComboCount?.Invoke();
        }
        // 床へ当たった際の処理
        if (collision.gameObject.tag == Const.GameTags.GameOver.ToString())
        {
            OnGameOver?.Invoke();
        }
        // ブロックへ当たった際の処理
        if (collision.gameObject.tag == Const.GameTags.Block.ToString())
        {
            speedHead = -speedHead;
            OnDestroyBlock?.Invoke(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    // ゲームが終了してPlayerが移動しないように指定する関数
    public void EndMovePlayer()
    {
        speedHead = 0;
        speedSide = 0;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}