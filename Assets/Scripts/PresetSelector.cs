using UnityEngine;

public class PresetSelector : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Transform[] presetTransform;

    void Awake() {
		float worldScreenHeight = mainCamera.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight * mainCamera.aspect;
		Vector3 initPos = new Vector3(0,worldScreenHeight,0);
		Vector3 initScale = new Vector3(worldScreenWidth, worldScreenHeight, 1);
		foreach(Transform t in presetTransform){
			t.position=initPos;
			t.localScale=initScale;
		}
	}

    void Update() { }
}
