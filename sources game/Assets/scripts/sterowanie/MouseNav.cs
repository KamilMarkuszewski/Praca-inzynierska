using UnityEngine;
using System.Collections;

public class MouseNav : MonoBehaviour {
	public Transform clickPrefab;
	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public bool attachToCenterOfMass = false;

	private SpringJoint springJoint ;
	UnityEngine.Object clickObj;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!Input.GetMouseButtonDown (0))	return;
		if(Objects.myData.Controllable == false)	return;
		
		float _x = Input.mousePosition.x;
		float _y = Input.mousePosition.y;
		
		bool ret = Objects.mapMain.contextMenu.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y));
		Objects.contextMenu.doDestroy();
		
		if(_x < 200 && !ret) {
			if(_x > 10 && _x <160 && _y < Screen.height - 10 && _y > Screen.height - 150){
				Camera miniCamera = FindMiniCamera();
				RaycastHit hit = new RaycastHit();
				TerrainCollider col = GameObject.Find("Terrain").GetComponent(typeof(TerrainCollider)) as TerrainCollider;
				
				if (col.Raycast(miniCamera.ScreenPointToRay(Input.mousePosition),  out hit, 1000.0f)){
					clickObj = GameObject.Find("click(Clone)");
					if(clickObj !=null ) Destroy (clickObj);
					
					SendMessage("setTargetPos", hit.point);
					clickObj = Instantiate(clickPrefab, new Vector3(hit.point.x, hit.point.y+0.1f,hit.point.z), new Quaternion(0,0,0,1));

					return;
				} 	
				
			}
			return;
		}else{
			if(_y< 40 && _x > Screen.width/2 + 100 - 600/2 && _x < Screen.width/2 + 100 +600/2) return;
			
			Camera mainCamera = FindCamera();
			
			// We need to actually hit an object
			RaycastHit hit = new RaycastHit();
			TerrainCollider col = GameObject.Find("Terrain").GetComponent(typeof(TerrainCollider)) as TerrainCollider;
			if (col.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),  out hit, 1000.0f)){
				clickObj = GameObject.Find("click(Clone)");
				if(clickObj !=null ) Destroy (clickObj);
				
				SendMessage("setTargetPos", hit.point);
				clickObj = Instantiate(clickPrefab, new Vector3(hit.point.x, hit.point.y+0.1f,hit.point.z), new Quaternion(0,0,0,1));

				return;
			} 	
			
			if (!hit.rigidbody || hit.rigidbody.isKinematic){
				Debug.Log("DRUGI!");

				
				return;
			}
		}

			
		
	}
	Camera FindMiniCamera ()
	{
		Camera [] tab = Camera.allCameras;
		for(int i=0; i<tab.Length; i++){
				if(tab[i]!=Camera.main) return tab[i];
		}
		return null;
	}
	
	
	Camera FindCamera ()
	{
		if (camera)
			return camera;
		else
			return Camera.main;
	}
	
	
}
