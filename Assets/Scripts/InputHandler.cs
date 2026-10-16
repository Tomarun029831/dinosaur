using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // インスペクターで恐竜オブジェクトのDinosaurスクリプトをセット
    [SerializeField] private Dinosaur dinosaur;

    void Update()
    {
        var current = Keyboard.current;
        if (current == null) return;

        var spaceKey = current.spaceKey;
        if (spaceKey.wasPressedThisFrame)
        {
            dinosaur.Jump();
        }
    }
}
