using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // インスペクターで恐竜オブジェクトのDinosaurスクリプトをセット
    [SerializeField] private Dinosaur dinosaur;

    void Update()
    {
		if(Keyboard.current!=null)
			if(Keyboard.current.spaceKey.wasPressedThisFrame)
				dinosaur.Jump();
		if(Touchscreen.current!=null)
			if(Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
				dinosaur.Jump();
    }
}
