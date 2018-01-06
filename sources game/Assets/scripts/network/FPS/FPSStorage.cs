using UnityEngine;
using System.Collections;

public class FPSStorage : MonoBehaviour {
	
	private float fps = 15.0f;
	private LeftPanel panel;
	public float GetCurrentFPS() {
		return fps;
	}
	
	void Start(){
		panel = GameObject.Find("localPlayer").GetComponent(typeof(LeftPanel)) as LeftPanel;
	}
	public void FPSChanged(float fps) {
		this.fps = fps;
	}
	
    void OnGUI() {
		panel.fps = fps;
	}
}
