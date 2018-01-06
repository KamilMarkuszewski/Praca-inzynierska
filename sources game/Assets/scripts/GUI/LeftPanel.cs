using UnityEngine;
using System.Collections;



public class LeftPanel : MonoBehaviour
{

    private int firstWindowUp;
    private int firstWindowDown;
    private int secondWindowUp;
    private int secondWindowDown;
    private int numberOfWindows;

    private bool eq_on = false;
    private bool items_on = false;
    private bool friends_on = false;
    private bool battle_on = false;
    private bool staty_on = false;
    private bool magia_on = false;
    private bool options_on = false;

    private int countWindows = 0;
    private bool fullscreenStatus;
    public float fps = 15.0f;


    void Start() {
        Objects.CreateInWorld();
    }

    public void CallUpdate()
    {
        OnGUI();
    }


    void Update() {

        Objects.dragAbleTable.checkClick();
    }

    void OnGUI()
    {
        ControllConnection.isInWorld(); 
        checkFullscreen();
        Objects.drawMain.mapa();
        mapa_buttons();
        Objects.drawHpBar.paski();
        zakladki();
        Objects.drawMain.pasekQuick();
        Objects.guiInfo.checkDesc();
        Objects.dragAbleTable.checkClick();
    }


    public void zakladki()
    {

        doMax2Windows();
        GUI.BeginGroup(new Rect(10, 140 + 20 + 20 + 5, 180, 84));
        if (GUI.Button(new Rect(1, 0, 37, 37), Objects.guiTextures.txtEq) && Event.current.button != 1)
        {
            if (eq_on)
            {
                eq_on = !eq_on;
                Objects.mapMain.eqRectVisible = false;
            }
            else if (!eq_on && countWindows < 2)
            {
                eq_on = true;
                Objects.mapMain.eqRectVisible = true;
            }
        }
        if (GUI.Button(new Rect(48, 0, 37, 37), Objects.guiTextures.txtItems) && Event.current.button != 1)
        {
            if (items_on)
            {
                items_on = !items_on;
                Objects.mapMain.backpackRectVisible = false;
            }
            else if (!items_on && countWindows < 2)
            {
                items_on = true;
                Objects.mapMain.backpackRectVisible = true;
            }
        }
        if (GUI.Button(new Rect(95, 0, 37, 37), Objects.guiTextures.txtFriends) && Event.current.button != 1)
        {
            if (friends_on)
            {
                friends_on = !friends_on;
            }
            else if (!friends_on && countWindows < 2)
            {
                friends_on = true;
            }
        }
        if (GUI.Button(new Rect(142, 0, 37, 37), Objects.guiTextures.txtBattle) && Event.current.button != 1)
        {
            if (battle_on)
            {
                battle_on = !battle_on;
            }
            else if (!battle_on && countWindows < 2)
            {
                battle_on = true;
            }
        }
        if (GUI.Button(new Rect(1, 47, 37, 37), Objects.guiTextures.txtChar) && Event.current.button != 1)
        {
            if (staty_on)
            {
                staty_on = !staty_on;
            }
            else if (!staty_on && countWindows < 2)
            {
                staty_on = true;
            }
        }
        if (GUI.Button(new Rect(48, 47, 37, 37), Objects.guiTextures.txtMagic) && Event.current.button != 1)
        {
            if (magia_on)
            {
                magia_on = !magia_on;
            }
            else if (!magia_on && countWindows < 2)
            {
                magia_on = true;
            }
        }
        if (GUI.Button(new Rect(95, 47, 37, 37), Objects.guiTextures.txtOptions) && Event.current.button != 1)
        {
            if (options_on)
            {
                options_on = !options_on;
            }
            else if (!options_on && countWindows < 2)
            {
                options_on = true;
            }
        }
        if (GUI.Button(new Rect(142, 47, 37, 37), Objects.guiTextures.txtLogout) && Event.current.button != 1)
        {
            if (true)
            {
                ControllConnection.backToChose();
            }
        }
        GUI.EndGroup();


        firstWindowUp = 190 + 90;
        numberOfWindows = 0;
        checkWindows();
    }
    public void doMax2Windows()
    {
        countWindows = 0;
        if (eq_on) countWindows++;
        if (items_on) countWindows++;
        if (friends_on) countWindows++;
        if (battle_on) countWindows++;
        if (staty_on) countWindows++;
        if (magia_on) countWindows++;
        if (options_on) countWindows++;

    }








    public void checkWindows()
    {
        if (eq_on) EqWindow();
        if (items_on) ItemsWindow();
        if (friends_on) FriendsWindow();
        if (battle_on) BattleWindow();
        if (staty_on) StatyWindow();
        if (magia_on) MagiaWindow();
        if (options_on) OptionsWindow();
    }


    public void StatyWindow()
    {
        int myHight = 170 + 20 + 10;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }
        if (numberOfWindows == 1)
        {
            actUp = secondWindowUp;

        }
        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);
        guiStatsWindow.draw();

        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            staty_on = false;
        }

        GUI.EndGroup();
        numberOfWindows++;
    }

    public void MagiaWindow()
    {
        int myHight = 200 + 20 + 10;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }
        if (numberOfWindows == 1)
        {
            actUp = secondWindowUp;

        }
        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);


        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            magia_on = false;
        }

        GUI.EndGroup();
        numberOfWindows++;
    }

    public void EqWindow()
    {
        int myHight = 154 + 20 + 10;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }

        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);
        GUI.DrawTexture(new Rect(30, 20, 120, 154), Objects.guiTextures.txtEqPanel);
        Objects.mapMain.eqRect = new Rect(10 + 30, actUp + 20, 120, 154);

        Objects.dragAbleTable.setEqRect();
        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            eq_on = false;
            Objects.mapMain.eqRectVisible = false;
        }


        GUI.EndGroup();
        numberOfWindows++;
    }

    public void ItemsWindow()
    {
        int myHight = 200 + 20 + 10;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }
        if (numberOfWindows == 1)
        {
            actUp = secondWindowUp;

        }
        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);
        GUI.DrawTexture(new Rect(0, 20, 180, 200), Objects.guiTextures.txtItemsPanel);

        Objects.mapMain.backpackRect = new Rect(10, actUp + 20, 180, 200);
        Objects.dragAbleTable.setBackpackRect();
        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            items_on = false;
            Objects.mapMain.backpackRectVisible = false;
        }

        GUI.EndGroup();
        numberOfWindows++;
    }

    public void FriendsWindow()
    {
        int myHight = 200;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }
        if (numberOfWindows == 1)
        {
            actUp = secondWindowUp;

        }
        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);
        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            friends_on = false;
        }

        GUI.EndGroup();
        numberOfWindows++;
    }

    public void BattleWindow()
    {
        int myHight = 200;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }
        if (numberOfWindows == 1)
        {
            actUp = secondWindowUp;

        }
        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);
        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            battle_on = false;
        }


        GUI.EndGroup();
        numberOfWindows++;
    }

    public void OptionsWindow()
    {
        int myHight = 154 + 20 + 10;
        int actUp = 0;
        if (numberOfWindows == 0)
        {
            actUp = firstWindowUp;
            secondWindowUp = actUp + 10 + myHight;
        }
        if (numberOfWindows == 1)
        {
            actUp = secondWindowUp;

        }
        GUI.BeginGroup(new Rect(10, actUp, 180, myHight));

        GUI.Box(new Rect(0, 0, 180, myHight), "", Objects.colorTextures.gray);

        fullscreenStatus = GUI.Toggle(new Rect(10, 20, 150, 25), fullscreenStatus, " Tryb pelnoekranowy");
        Objects.myLocalPlayer.mySettings.fullscreenStatus = fullscreenStatus;

        GUI.Label(new Rect(10, myHight - 10 - 25, 150, 25), "FPS: " + fps);
        if (GUI.Button(new Rect(160, 0, 20, 20), Objects.guiTextures.txtExit))
        {
            options_on = false;
        }


        GUI.EndGroup();
        numberOfWindows++;
    }





    void checkFullscreen()
    {
        Screen.fullScreen = Objects.myLocalPlayer.mySettings.fullscreenStatus;
    }



    public void mapa_buttons()
    {
        if (GUI.Button(Objects.buttonsMap.Up1, Objects.guiTextures.txtPlus) && Event.current.button != 1)
        {
            Objects.drawMain.camPlus();
        }
        if (GUI.Button(Objects.buttonsMap.Up2, Objects.guiTextures.txtMinus) && Event.current.button != 1)
        {
            Objects.drawMain.camMinus();
        }
        if (GUI.Button(Objects.buttonsMap.Up3, Objects.guiTextures.txtChat) && Event.current.button != 1)
        {

        }
        if (Objects.myLocalPlayer.mySettings.autofollow)
        {
            if (GUI.Button(Objects.buttonsMap.Up4, Objects.guiTextures.txtFollow) && Event.current.button != 1)
            {
                Objects.myLocalPlayer.mySettings.autofollow = !Objects.myLocalPlayer.mySettings.autofollow;
            }
        }
        else {
            if (GUI.Button(Objects.buttonsMap.Up4, Objects.guiTextures.txtFollow_off) && Event.current.button != 1)
            {
                Objects.myLocalPlayer.mySettings.autofollow = !Objects.myLocalPlayer.mySettings.autofollow;
            }
        }
        if (Objects.myLocalPlayer.mySettings.autoattack)
        {
            if (GUI.Button(Objects.buttonsMap.Up5, Objects.guiTextures.txtAttack) && Event.current.button != 1)
            {
                Objects.myLocalPlayer.mySettings.autoattack = !Objects.myLocalPlayer.mySettings.autoattack;
            }
        }
        else
        {
            if (GUI.Button(Objects.buttonsMap.Up5, Objects.guiTextures.txtAttack_off) && Event.current.button != 1)
            {
                Objects.myLocalPlayer.mySettings.autoattack = !Objects.myLocalPlayer.mySettings.autoattack;
            }
        }

    }



}


