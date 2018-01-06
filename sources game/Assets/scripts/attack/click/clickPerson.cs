using UnityEngine;
using System.Collections;

public class clickPerson : MonoBehaviour
{

    public int id = -1;
	public bool attacked = false;
	public float attacked_time = 0.0f;
	public float attacked_time_passed = 0.0f;
	public bool died = false;
	public int addDmg = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		id = id;

		if(attacked == true && attacked_time_passed < attacked_time && attacked_time_passed < Time.time){

			attacked_time_passed = Time.time + 1.0f;
			attack();
			if(attacked_time_passed + 5.0f > attacked_time){
				attacked = false; 
			}
		}
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
			//Objects.contextMenu.Make(Input.mousePosition, id, this);
			attack();
		}
    }

    public static void stop() {
        GameObject.Find("localPlayer").SendMessage("stopFollow");
    }

	public void attack(){
		if(!died)
		{
			DatabaseAccess.Battle.sendAtack(NetworkController.GetClient(), id, addDmg);
			if(Objects.myLocalPlayer.mySettings.autofollow){
				GameObject.Find("localPlayer").SendMessage("setTargetPos", transform.position);
			}else{
				stop();
			}
			if(Objects.myLocalPlayer.mySettings.autoattack){
				attacked = true;
				attacked_time = Time.time + 20.0f;
				attacked_time_passed = Time.time;
			}		
		}
		if(died){
			stop();
			attacked_time_passed = 1.0f;
			attacked_time = 0.0f;
			attacked = false;
		}
	}
}
