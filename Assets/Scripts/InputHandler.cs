using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // インスペクターで恐竜オブジェクトのDinosaurスクリプトをセット
    [SerializeField] private Dinosaur dinosaur;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dinosaur != null)
            {
                // 恐竜に「Jump」の指示を送る
                dinosaur.Jump();
            }   
        }
    }
}