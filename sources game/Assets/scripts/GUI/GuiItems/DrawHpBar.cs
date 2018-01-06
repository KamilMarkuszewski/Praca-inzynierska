using UnityEngine;
using System.Collections;

public class DrawHpBar
{
    public void paski()
    {
        GUI.Box(Objects.buttonsMap.HpBar, "", Objects.colorTextures.black);
        GUI.Box(Objects.buttonsMap.ManaBar, "", Objects.colorTextures.black);
        GUI.Box(new Rect(Objects.buttonsMap.HpBar.x, Objects.buttonsMap.HpBar.y, (Objects.buttonsMap.HpBar.width * Objects.myData.hp / Objects.myData.max_hp), Objects.buttonsMap.HpBar.height), "", Objects.colorTextures.red);
        GUI.Box(new Rect(Objects.buttonsMap.ManaBar.x, Objects.buttonsMap.ManaBar.y, (Objects.buttonsMap.ManaBar.width * Objects.myData.mp / Objects.myData.max_mp), Objects.buttonsMap.ManaBar.height), "", Objects.colorTextures.blue);
    }



}
