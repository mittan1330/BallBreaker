public class Const
{
    /// <summary> ゲームがプレイ中かを判断する静的変数 </summary>
	// Static変数はConstで使っても良いのでしょうか？
    public static bool isPlay = true;
    /// <summary> ボールの移動の速さを指定する定数 </summary>
    public const float ballSpeed = 5.0f;
    /// <summary> プレイヤーの移動の速さを指定する定数 </summary>
    public const float wallSpeed = 7.0f;

    /// <summary> シーンネームを列挙型で定義する </summary>
    public enum SceneNames
    {
        Main,
    }

    /// <summary> ゲームのタグを列挙型で定義する </summary>
    public enum GameTags
    {
        Side,
        Head,
        GameOver,
        Block,
    }
}
