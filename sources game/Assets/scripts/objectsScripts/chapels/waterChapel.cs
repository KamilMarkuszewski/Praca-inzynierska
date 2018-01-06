using UnityEngine;
using System.Collections;
using Enumerations;
using DragAble;

public class waterChapel : MonoBehaviour
{
	int item_id = 5;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
			giveSpell();
		}
    }

	void giveSpell(){
		DragAble.DragAble[]  quick = (GameObject.Find("localPlayer").GetComponent(typeof(DragAbleTable)) as DragAbleTable).viewItems;
		for(int i = 0; i< 16; i++){
			if(quick[i] != null){
				if(quick[i] .id == item_id){
					return;
				}
			} 
		}
		for(int i = 0; i< 16; i++){
			if(quick[i]== null){
				quick[i] = AllItems.items[item_id];
				DragAble.DragAbleTableActions.saveItems(quick, 16, 20, 9, i, i);
				return;
			}
		}
		
	}


}
