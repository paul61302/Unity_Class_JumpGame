using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 設定海豚跳的力量
    public float Power;
    public Vector3 BeganPosition;
    public Vector3 EndPosition;
    [Header("移動速度")]
    public float Speed;
    bool isTrun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 按下鍵盤空白鍵
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Power);
        }
        // 手指在手機螢幕點擊
        if (Input.touchCount > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Power);
        }
        #region 手指在手機螢幕上滑動
        // 如果有一隻手指接觸到手機螢幕
        /*if(Input.touchCount == 1)
        {
            // 手指頭剛接觸螢幕
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                // 記錄到碰到螢幕的座標位置
                BeganPosition = Input.touches[0].position;
            }
            // 手指頭剛離開螢幕
            if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[0].phase == TouchPhase.Canceled)
            {
                // 記錄到碰到螢幕的座標位置
                EndPosition = Input.touches[0].position;
                // 手指往水平滑動
                if(Mathf.Abs(BeganPosition.x- EndPosition.x) > Mathf.Abs(BeganPosition.y - EndPosition.y))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, Power);
                }
                // 手指垂直
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, Power);
                }
            }
        }
        */
        #endregion
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.GetComponent<Collider2D>().tag == "Border")
        {
            isTrun = !isTrun;
            GetComponent<SpriteRenderer>().flipX = isTrun;
            Speed = Speed * -1f;
        }
    }
}
