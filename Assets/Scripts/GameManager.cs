using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // シーン切り替え・リロード用

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Dinosaur dinosaur;
    private float survivalTime = 0f;
    private bool isPlaying = true;
    private bool isDeadState = false; // 死亡状態フラグ

    void Start()
    {
        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlaying)
        {
            survivalTime += Time.deltaTime;
        }
        
        // 死亡状態かつエンターキーが押されたらシーンをリロード
        if (isDeadState && Input.GetKeyDown(KeyCode.Return))
        {
            ReloadScene();
        }
    }

    private void OnEnable()
    {
        // dinosaur.OnDied += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        // dinosaur.OnDied -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        isPlaying = false;
        isDeadState = true; // エンターキー入力を受け付ける状態にする

        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            // リザルト表示にリトライの案内を追加
            resultText.text = "生存時間: " + survivalTime.ToString("F1") + " 秒\n[Enter]キーでリトライ";
        }
        
        Debug.Log("リザルト表示完了。エンターキーでリトライ。");
    }

    // シーンを最初からやり直すメソッド
    private void ReloadScene()
    {
        // 現在アクティブなシーンの名前を取得して再読み込みする
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
