using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;
using UnityEngine;

namespace DragAble
{
    class AllItems
    {
        static public DragAble[] items;

        public static void wczytaj()
        {
            items = new DragAble[500];

            int i = 1;


            items[i] = new DragAble(5);
            items[i].txt = Objects.itemsTextures.armor1;

            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 60);

            items[i].inf.setString(0, "Siekiera");
            items[i].inf.setString(1, "Atak: 2");
            items[i].inf.setString(2, "Obrona: 0");
            items[i].inf.setString(3, "jednoreczny");
            items[i].inf.setString(4, "Pierwsza bron dostepna po stworzeniu postaci.");
            items[i].inf.setSzer(50);
            items[i].itemsAttr.def = 0;
            items[i].itemsAttr.atk = 2;
            i++;


            
            items[i] = new DragAble(4);
            items[i].txt = Objects.itemsTextures.armor2;
            items[i].type = ItemyEnums.typyDragAble.armor;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Cwiekowana zbroja");
            items[i].inf.setString(1, "Atak: 0");
            items[i].inf.setString(2, "Obrona: 4");
            items[i].inf.setString(3, "Zdobyta podczas bitwy z orkami.");
            items[i].itemsAttr.def = 4;
            items[i].itemsAttr.atk = 0;
            items[i].inf.setSzer(50);

            i++;


            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor3;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].id = i ;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Siekiera");
            i++;


            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor4;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Siekiera");
            i++;


            items[i] = new DragAble(5);
            items[i].txt = Objects.itemsTextures.armor5;
            items[i].type = ItemyEnums.typyDragAble.magic;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Male uleczenie");
            items[i].inf.setString(1, "Dodaje 5 hp");
            items[i].inf.setString(3, "Pierwszy czar ktory powinien znac kazdy bohater!");
            items[i].inf.setString(4, "Koszt 4 many");
            items[i].inf.setSzer(50);
			items[i].magicAttr.heal = 5;
            i++;

            items[i] = new DragAble(5);
            items[i].txt = Objects.itemsTextures.armor6;
            items[i].type = ItemyEnums.typyDragAble.magic;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Trujacy atak");
            items[i].inf.setString(1, "Dabiera przeciwnikowi 17 hp");
            items[i].inf.setString(3, "Pierwszy czar ofensywny!");
            items[i].inf.setString(4, "Koszt 9 many");
            items[i].magicAttr.dmg = 5;
            items[i].inf.setSzer(50);
            i++;

            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor7;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 60);
            items[i].inf.setString(0, "Siekiera");
            i++;

            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor8;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].id = i ;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Siekiera");
            i++;

            items[i] = new DragAble(4);
            items[i].txt = Objects.itemsTextures.armor9;
            items[i].type = ItemyEnums.typyDragAble.helm;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Zardzewialy helm");
            items[i].inf.setString(1, "Atak: 0");
            items[i].inf.setString(2, "Obrona: 2");
            items[i].inf.setString(3, "Stary zardzewialy helm, ktos znalazl go w rynsztoku.");
            items[i].itemsAttr.def = 2;
            items[i].itemsAttr.atk = 0;
            items[i].inf.setSzer(50);
            i++;

            items[i] = new DragAble(4);
            items[i].txt = Objects.itemsTextures.armor10;
            items[i].type = ItemyEnums.typyDragAble.shield;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Mala tarcza");
            items[i].inf.setString(1, "Atak: 0");
            items[i].inf.setString(2, "Obrona: 2");
            items[i].inf.setString(3, "Kapitan mowi ze tak mala nadaje sie tylko dla niziolka!");
            items[i].itemsAttr.def = 2;
            items[i].itemsAttr.atk = 0;
            items[i].inf.setSzer(50);
            i++;

            items[i] = new DragAble(4);
            items[i].txt = Objects.itemsTextures.armor11;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 60);
            items[i].inf.setString(0, "Berdysz");
            items[i].inf.setString(1, "Atak: 4");
            items[i].inf.setString(2, "Obrona: 0");
            items[i].inf.setString(3, "Solidny stalowy berdysz, kradziony prosto z magazynu strazy.");
            items[i].itemsAttr.def = 0;
            items[i].itemsAttr.atk = 4;
            items[i].inf.setSzer(50);
            i++;

            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor12;
            items[i].type = ItemyEnums.typyDragAble.ring;
            items[i].id = i;
            items[i].cor = new Rect(0, 0, 30, 30);
            items[i].inf.setString(0, "Siekiera");
            i++;
            /*
            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor13;
            items[i].type = ItemyEnums.typyDragAble.magic;
            items[i].typeMagic = ItemyEnums.typyMagicDragAble.self;
            items[i].Name = "Armor";
            items[i].id = i + 1;
            items[i].cor = new Rect(0, 0, 30, 30);
            i++;
            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor14;
            items[i].type = ItemyEnums.typyDragAble.ring;
            items[i].Name = "Armor";
            items[i].id = i + 1;
            items[i].cor = new Rect(0, 0, 30, 30);
            i++;
            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor15;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].Name = "Armor";
            items[i].id = i + 1;
            items[i].cor = new Rect(0, 0, 30, 30);
            i++;
            items[i] = new DragAble(1);
            items[i].txt = Objects.itemsTextures.armor16;
            items[i].type = ItemyEnums.typyDragAble.weapon;
            items[i].Name = "Armor";
            items[i].id = i + 1;
            items[i].cor = new Rect(0, 0, 30, 60);
            i++;
            */
        }
    }
}
