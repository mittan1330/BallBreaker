using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerModel : MonoBehaviour
{
    [SerializeField] float maxWeight = 7.0f;
    [SerializeField] float maxHeightDown = -4.3f;
    [SerializeField] float maxHeightUp = 0.5f;

    public void OnMovePlayerRight()
    {
        if (Const.isPlay == false) return;
        if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x <= maxWeight)
        {
            this.transform.position += new Vector3(Const.wallSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void OnMovePlayerLeft()
    {
        if (Const.isPlay == false) return;
        if (this.transform.position.x >= -maxWeight)
        {
            this.transform.position += new Vector3(-Const.wallSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void OnMovePlayerUp()
    {
        if (Const.isPlay == false) return;
        if (this.transform.position.y <= maxHeightUp)
        {
            this.transform.position += new Vector3(0, Const.wallSpeed * Time.deltaTime, 0);
        }
    }

    public void OnMovePlayerDown()
    {
        if (Const.isPlay == false) return;
        if (this.transform.position.y >= maxHeightDown)
        {
            this.transform.position += new Vector3(0, -Const.wallSpeed * Time.deltaTime, 0);
        }
    }
}
