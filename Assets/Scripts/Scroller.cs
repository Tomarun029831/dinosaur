using Unity.VisualScripting;
using UnityEngine;

public class Scroller : MonoBehaviour
{

    [SerializeField] private Transform[] BG;

    [SerializeField] private PresetSelector presetselector;

    public float scrollSpeed = 0.05f;
    public bool isStop = false;
    const int ImageNumber = 3;
    const int ImageWidth = 16;
    float leftOffset, rightOffset;
    Transform[] borrowed_Preset = new Transform[3];
    // Transform borrowed_Preset;


    void Start()
    {
        leftOffset = -1 * ImageWidth;
        rightOffset = 2 * ImageWidth;

        // borrowed_Preset = presetselector.borrowNextLand();
        // borrowed_Preset.position = new Vector3(64, 0, 0);

        borrowed_Preset[0] = presetselector.borrowNextLand();
        borrowed_Preset[1] = presetselector.borrowNextLand();
        borrowed_Preset[2] = presetselector.borrowNextLand();

        borrowed_Preset[0].position = new Vector3(32, 0, 0);
        borrowed_Preset[1].position = new Vector3(48, 0, 0);
        borrowed_Preset[2].position = new Vector3(64, 0, 0);
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

        /* プリセットスクロール */
        int i;
        for (i=0; i<3; i++) {
            borrowed_Preset[i].Translate(new Vector3(-1 * scrollSpeed, 0, 0));
            if (borrowed_Preset[i].transform.position.x <= leftOffset)
            {                
                borrowed_Preset[i].position = new Vector3(32, 0, 0);
                presetselector.returnLand(borrowed_Preset[i]);
                borrowed_Preset[i] = presetselector.borrowNextLand();
                borrowed_Preset[i].position = new Vector3(32, 0, 0);
            }
        }
            // borrowed_Preset.Translate(new Vector3(-1 * scrollSpeed, 0, 0));
            // if (borrowed_Preset.transform.position.x <= leftOffset)
            // {
            //     presetselector.returnLand(borrowed_Preset);
            //     borrowed_Preset = presetselector.borrowNextLand();
            //     borrowed_Preset.position = new Vector3(16, 0, 0);
            // }
        // foreach ( Transform t in borrowed_Preset )
        // { 
        //     t.Translate(new Vector3(-1 * scrollSpeed, 0, 0));
        // }
    }
}
