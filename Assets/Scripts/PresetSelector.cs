using UnityEngine;

public class PresetSelector : MonoBehaviour
{
	// https://www.irasutoya.com/2017/02/blog-post_597.html
	// https://www.irasutoya.com/2017/11/blog-post_551.html
	// https://www.irasutoya.com/2016/01/blog-post_656.html

	[SerializeField] private Camera mainCamera;
	[SerializeField] private Transform[] presetTransform;
	[SerializeField] private Transform[] birdTransform;

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

	private Transform GetAvailableBird(){
		foreach(Transform b in birdTransform){
			if(b.gameObject.activeInHierarchy) continue;
			return b;
		}
		return null;
	}

	// INFO: ==== API of PresetSelector ====
	public Transform borrowNextLand(){
		// select preset
		const int maxAmountPreset=6;
		int iPreset = Random.Range(0, maxAmountPreset);
		Transform selectedPreset = presetTransform[iPreset];
		// select cactus
		int iCactus = Random.Range(0, selectedPreset.childCount);
		Transform selectedCactus = selectedPreset.GetChild(iCactus).transform;
		// swap cactus with bird
		Transform selectedBird = GetAvailableBird();
		if(selectedBird==null) return selectedPreset;
		selectedBird.SetParent(selectedPreset);
		selectedBird.position = selectedCactus.position;
		selectedBird.gameObject.SetActive(true);
		selectedCactus.gameObject.SetActive(false);

		return selectedPreset;
	}

	public void returnLand(Transform preset){
		foreach(Transform c in preset)
			if(c.name.StartsWith("bird")){
				c.SetParent(null);
				c.gameObject.SetActive(false);
			} else {
				c.gameObject.SetActive(true);
			}
	}
}
