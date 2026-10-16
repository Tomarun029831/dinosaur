using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Dinosaur : MonoBehaviour
{
    public bool isDeath = false;
    public LayerMask StageLayer;
    Rigidbody2D rbody2D;

    [SerializeField] private int jumpForce = 300;

    // しゃがみ
    [Header("Components")]
    [SerializeField] private CapsuleCollider2D  playerCollider;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite crouchSprite;

    private bool isCrouching = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            return;
        }

        var upArrowKey = current.upArrowKey;

        if (GroundChk() && !isDeath) {
            if (upArrowKey.wasPressedThisFrame)
            {
                Jump();
            }
        }

        // 下矢印キーの入力を取得
        bool crouchInput = Keyboard.current != null &&
            Keyboard.current.downArrowKey.isPressed;

        if (crouchInput && !isCrouching && GroundChk())
        {
            Crouch(true);
        }
        else if (!crouchInput && isCrouching)
        {
            Crouch(false);
        }
    }

    public void Jump(){
        rbody2D.AddForce(Vector2.up * jumpForce);
    }

    public void Crouch(bool crouch)
    {
        isCrouching = crouch;

        // 当たり判定を切り替え
        playerCollider.enabled = !crouch; 

        // しゃがみ状態に応じてスプライトを切り替える
        spriteRenderer.sprite = crouch ? crouchSprite : normalSprite;
    }

    bool GroundChk(){
		Vector3 startposition = transform.position - transform.up * 0.5f;						// Playerの中心を始点とする
		Vector3 endposition = transform.position - transform.up * 1.2f;	// Playerの足元を終点とする

		// Debug用に始点と終点を表示する
		Debug.DrawLine(startposition, endposition, Color.red);

		// Physics2D.Linecastを使い、ベクトルとStageLayerが接触していたらTrueを返す
		return Physics2D.Linecast(startposition, endposition, StageLayer);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("すり抜けた");
        isDeath = true;
    }
}
