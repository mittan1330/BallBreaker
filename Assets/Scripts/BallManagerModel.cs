using UnityEngine;
using System;

public class BallManagerModel : MonoBehaviour
{
    float speedHead;
    float speedSide;

    public event Action<GameObject> OnDestroyBlock;
    public event Action OnGameOver;
    public event Action<int> SetComboCount;

    // Start is called before the first frame update
    void Start()
    {
        speedHead = Const.ballSpeed;
        speedSide = Const.ballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speedSide, speedHead, 0f) * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Side")
        {
            speedSide = -speedSide;
        }
        if (collision.gameObject.tag == "Head")
        {
            speedHead = -speedHead;
            SetComboCount?.Invoke(0);
        }
        if (collision.gameObject.tag == "GameOver")
        {
            OnGameOver?.Invoke();
        }
        if (collision.gameObject.tag == "Block")
        {
            speedHead = -speedHead;
            OnDestroyBlock?.Invoke(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    public void EndMovePlayer()
    {
        speedHead = 0;
        speedSide = 0;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}