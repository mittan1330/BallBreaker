using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerModel : MonoBehaviour
{
    /// <summary> プレイヤーの壁が上向きに移動できる上限を代入する変数 </summary>
    private float maxWeight = 7.0f;
    /// <summary> プレイヤーの壁が下向きに移動できる上限を代入する変数 </summary>
    private float maxHeightDown = -4.3f;
    /// <summary> プレイヤーの壁が横方向に移動できる上限を代入する変数 </summary>
    private float maxHeightUp = 0.5f;

    /// <summary>
	/// 右へ移動した際の処理
	/// </summary>
    public void OnMovePlayerRight()
    {
        // ゲームプレイが可能かどうかを判定して処理を切り上げる
        if (Const.isPlay == false) return;
        // 移動可能かを判定して移動処理
        if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x <= maxWeight)
        {
            this.transform.position += new Vector3(Const.wallSpeed * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
	/// 左へ移動した際の処理
	/// </summary>
    public void OnMovePlayerLeft()
    {
        // ゲームプレイが可能かどうかを判定して処理を切り上げる
        if (Const.isPlay == false) return;
        // 移動可能かを判定して移動処理
        if (this.transform.position.x >= -maxWeight)
        {
            this.transform.position += new Vector3(-Const.wallSpeed * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
	/// 上へ移動した際の処理
	/// </summary>
    public void OnMovePlayerUp()
    {
        // ゲームプレイが可能かどうかを判定して処理を切り上げる
        if (Const.isPlay == false) return;
        // 移動可能かを判定して移動処理
        if (this.transform.position.y <= maxHeightUp)
        {
            this.transform.position += new Vector3(0, Const.wallSpeed * Time.deltaTime, 0);
        }
    }

    /// <summary>
	/// 下へ移動した際の処理
	/// </summary>
    public void OnMovePlayerDown()
    {
        // ゲームプレイが可能かどうかを判定して処理を切り上げる
        if (Const.isPlay == false) return;
        // 移動可能かを判定して移動処理
        if (this.transform.position.y >= maxHeightDown)
        {
            this.transform.position += new Vector3(0, -Const.wallSpeed * Time.deltaTime, 0);
        }
    }
}
