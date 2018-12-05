using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.PostProcessing;
public class VignetteEditor : MonoBehaviour {

    
    GameObject gameManager;
    public PostProcessVolume ppvol;
    public PostProcessProfile ppProfile;
    public PostProcessEffectSettings ppEffectsSettings;
    Vignette outSetting;
    float deaths;
    float vignetteTimer;
    
    // Use this for initialization
    void Start () {
        vignetteTimer = 1;
        ppvol = GetComponent<PostProcessVolume>();
        ppProfile = ppvol.profile;        
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
        ppProfile.TryGetSettings<Vignette>(out outSetting);
        deaths = gameManager.GetComponent<GameManager>().deaths;
        StartCoroutine(Fade());        
    }

    IEnumerator Fade()
    {        
            outSetting.intensity.value = Mathf.Lerp(outSetting.intensity.value,deaths/4, 4*Time.deltaTime);
            yield return null;        
    }
}
