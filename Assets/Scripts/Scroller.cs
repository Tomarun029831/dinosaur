using UnityEngine;

public class Scroller : MonoBehaviour
{

    [SerializeField] private Transform[] BG;

    public float scrollSpeed = 0.05f;
    public bool isStop = false;
    const int ImageNumber = 3;
    const int ImageWidth = 16;
    float leftOffset, rightOffset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftOffset = -1 * ImageWidth;
        rightOffset = 2 * ImageWidth;
    }

    // Update is called once per frame
    void Update()
    {
        /* スクロール停止 */
        if (isStop)
        {
            return;
        }

        /* 背景スクロール */
        foreach ( Transform t in BG )
        {
            
            t.Translate(new Vector3(-1 * scrollSpeed, 0, 0));
            if (t.transform.position.x <= leftOffset)
                {
                    t.Translate(new Vector3(rightOffset - leftOffset, 0, 0));    // rightOffsetへ飛ばす
                }
        }

        /* 
        TODO: 障害物スクロール

        getNextLand
        障害物を生成
        Translateで障害物をスクロールさせる
        leftOffsetに到達した障害物をkill
         */

    }
}
