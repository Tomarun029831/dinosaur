using UnityEngine;

public class PresetSelector : MonoBehaviour
{
	// https://www.irasutoya.com/2017/02/blog-post_597.html
	// https://www.irasutoya.com/2017/11/blog-post_551.html
	// https://www.irasutoya.com/2016/01/blog-post_656.html

	[SerializeField] private Camera mainCamera;
	[SerializeField] private Transform[] presetTransform;
	private System.Collections.Generic.List<Transform> availablePreset = new System.Collections.Generic.List<Transform>(6);
	[SerializeField] private Transform[] birdTransform;
	private System.Collections.Generic.List<Transform> availableBird = new System.Collections.Generic.List<Transform>(5);

    void Awake() {
		float worldScreenHeight = mainCamera.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight * mainCamera.aspect;
		Vector3 initPos = new Vector3(0,worldScreenHeight,0);
		Vector3 initScale = new Vector3(worldScreenWidth, worldScreenHeight, 1);
		foreach(Transform t in presetTransform){
			t.position=initPos;
			t.localScale=initScale;
			t.gameObject.SetActive(false);
			availablePreset.Add(t);
		}
		foreach(Transform t in birdTransform){
			t.gameObject.SetActive(false);
			availableBird.Add(t);
		}
	}

	// INFO: ==== API of PresetSelector ====
	public Transform borrowNextLand(){
		// select preset
		int iPreset = Random.Range(0, availablePreset.Count);
		Transform selectedPreset = availablePreset[iPreset];
		availablePreset.RemoveAt(iPreset);
		selectedPreset.gameObject.SetActive(true);
		// select cactus
		int iCactus = Random.Range(0, selectedPreset.childCount);
		Transform selectedCactus = selectedPreset.GetChild(iCactus).transform;
		// swap cactus with bird
		if(availableBird.Count==0) return selectedPreset;
		Transform selectedBird = availableBird[0];
		availableBird.RemoveAt(0);
		selectedBird.SetParent(selectedPreset);
		selectedBird.position = selectedCactus.position;
		selectedBird.gameObject.SetActive(true);
		selectedCactus.gameObject.SetActive(false);

		return selectedPreset;
	}

	public void returnLand(Transform preset){
		for (int i = preset.childCount - 1; i >= 0; i--) {
            Transform c = preset.GetChild(i);
			if(c.name.StartsWith("bird")){
				c.SetParent(null);
				c.gameObject.SetActive(false);
				availableBird.Add(c);
			} else {
				c.gameObject.SetActive(true);
			}
		}
		preset.gameObject.SetActive(false);
		availablePreset.Add(preset);
	}
}
