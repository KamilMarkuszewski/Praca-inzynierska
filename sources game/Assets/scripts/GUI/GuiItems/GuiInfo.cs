using UnityEngine;
using System.Collections;

public class GuiInfo
{


    Info but1;
    Info but2;
    Info but3;
    Info but4;
    Info but5;
    Info but6;
    Info but7;
    Info but8;

    Info but9;
    Info but10;
    Info but11;

    Info but12;
    Info but13;
    Info but14;
    Info but15;
    Info but16;

    public void makeDesc()
    {
        but1 = new Info(1);
        string s1 = "Ekwipunek postaci";
        but1.setString(0, s1);
        but1.setCor(Objects.buttonsMap.Menu1);
        but1.setSzer(s1.Length);

        but2 = new Info(1);
        string s2 = "Plecak postaci";
        but2.setString(0, s2);
        but2.setCor(Objects.buttonsMap.Menu2);
        but2.setSzer(s2.Length);

        but3 = new Info(1);
        string s3 = "Znajomi";
        but3.setString(0, s3);
        but3.setCor(Objects.buttonsMap.Menu3);
        but3.setSzer(s3.Length);

        but4 = new Info(1);
        string s4 = "Bitwa";
        but4.setString(0, s4);
        but4.setCor(Objects.buttonsMap.Menu4);
        but4.setSzer(s4.Length);

        but5 = new Info(1);
        string s5 = "Statystyki postaci";
        but5.setString(0, s5);
        but5.setCor(Objects.buttonsMap.Menu5);
        but5.setSzer(s5.Length);

        but6 = new Info(1);
        string s6 = "Umiejetnosci i magia postaci";
        but6.setString(0, s6);
        but6.setCor(Objects.buttonsMap.Menu6);
        but6.setSzer(s6.Length);

        but7 = new Info(1);
        string s7 = "Opcje gry";
        but7.setString(0, s7);
        but7.setCor(Objects.buttonsMap.Menu7);
        but7.setSzer(s7.Length);

        but8 = new Info(1);
        string s8 = "Wylogowanie";
        but8.setString(0, s8);
        but8.setCor(Objects.buttonsMap.Menu8);
        but8.setSzer(s8.Length);

        but9 = new Info(1);
        string s9 = "Punkty zdrowia: " + Objects.myData.hp + " / " + Objects.myData.max_hp;
        but9.setString(0, s9);
        but9.setCor(Objects.buttonsMap.HpBar);
        but9.setSzer(s9.Length);

        but10 = new Info(1);
        string s10 = "Punkty many: " + Objects.myData.mp + " / " + Objects.myData.max_mp;
        but10.setString(0, s10);
        but10.setCor(Objects.buttonsMap.ManaBar);
        but10.setSzer(s10.Length);

        but11 = new Info(3);
        string s11a = "Minimapa";
        string s11b = "Uzyj przycisku + po prawej by przyblizyc";
        string s11c = "Uzyj przycisku - po prawej by oddalic";
        but11.setString(0, s11a);
        but11.setString(1, s11b);
        but11.setString(2, s11c);
        but11.setCor(new Rect(Objects.mapMain.minimapRect.x, Objects.odstep, Objects.mapMain.minimapRect.width, Objects.mapMain.minimapRect.height));
        but11.setSzer(s11b.Length);

        but12 = new Info(1);
        string s12 = "Przyblizanie minimapy";
        but12.setString(0, s12);
        but12.setCor(Objects.buttonsMap.Up1);
        but12.setSzer(s12.Length);

        but13 = new Info(1);
        string s13 = "Oddalanie minimapy";
        but13.setString(0, s13);
        but13.setCor(Objects.buttonsMap.Up2);
        but13.setSzer(s13.Length);

        but14 = new Info(1);
        string s14 = "Otwieranie panelu chatu";
        but14.setString(0, s14);
        but14.setCor(Objects.buttonsMap.Up3);
        but14.setSzer(s14.Length);

        but15 = new Info(1);
        string s15 = "Automatycznie podazaj za celem";
        but15.setString(0, s15);
        but15.setCor(Objects.buttonsMap.Up4);
        but15.setSzer(s15.Length);

        but16 = new Info(1);
        string s16 = "Automatycznie atakuj";
        but16.setString(0, s16);
        but16.setCor(Objects.buttonsMap.Up5);
        but16.setSzer(s16.Length);

    }

    public void checkDesc()
    {

        string s9 = "Punkty zdrowia: " + Objects.myData.hp + " / " + Objects.myData.max_hp;
        but9.setString(0, s9);
        but9.setSzer(s9.Length);
        string s10 = "Punkty many: " + Objects.myData.mp + " / " + Objects.myData.max_mp;
        but10.setString(0, s10);
        but10.setSzer(s10.Length);


        but1.checkDraw();
        but2.checkDraw();
        but3.checkDraw();
        but4.checkDraw();
        but5.checkDraw();
        but6.checkDraw();
        but7.checkDraw();
        but8.checkDraw();

        but9.checkDraw();
        but10.checkDraw();
        but11.checkDraw();

        but12.checkDraw();
        but13.checkDraw();
        but14.checkDraw();
        but15.checkDraw();
        but16.checkDraw();
    }

}
