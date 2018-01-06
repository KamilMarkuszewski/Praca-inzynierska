using UnityEngine;
using System.Collections;
using Enumerations;
using DragAble;

public class depoChest: MonoBehaviour
{
	int item_id = 6;
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
		int start = 16 + 20;


		if(quick[start]==null){
			//wstaw naszyjnik 

		}
		
		if(quick[start+1]==null){
			//wstaw helm
			quick[start+1] = AllItems.items[9];
			
		}
		
		if(quick[start+2]==null){
			
		}
		
		if(quick[start+3]==null){
			//wstaw bron
			quick[start+3] = AllItems.items[11];
			
		}
		
		if(quick[start+4]==null){
			//wstaw armor
			quick[start+4] = AllItems.items[2];
			
		}
		
		if(quick[start+5]==null){
			//wstaw tarcze
			quick[start+5] = AllItems.items[10];
			
		}
		
		if(quick[start+6]==null){
			//wstaw ring

			
		}
		
		if(quick[start+7]==null){
			//wstaw buty

			
		}
		
		DragAble.DragAbleTableActions.saveItems(quick, 16, 20, 9, start+6, start);
		
		
	}


}
