package gameServ;

import it.gotoandplay.smartfoxserver.lib.ActionscriptObject;

import java.nio.channels.SocketChannel;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.Set;

import repository.*;

import org.json.JSONException;
import org.json.JSONObject;

public class responseCreator {

	public static void putChars(Set<T_Character> lista,
			ActionscriptObject db_array) {
		int i = 0;
		for (Iterator<T_Character> iter = lista.iterator(); iter.hasNext(); i++) {
			ActionscriptObject as_obj = new ActionscriptObject();
			T_Character actual = iter.next();
			as_obj.put("id", Integer.toString(actual.getId()));
			as_obj.put("name", actual.getName());
			as_obj.put("klasa", Integer.toString(actual.getKlasa()));
			as_obj.put("lvl", Integer.toString(actual.getLvl()));
			as_obj.put("way", Integer.toString(actual.getWay()));
			db_array.put(i, as_obj);
		}

	}

	public static void putCharacter(T_Character ch, ActionscriptObject db_array) {

		db_array.put("id", Integer.toString(ch.getId()));
		db_array.put("userId", Integer.toString(ch.getUserId()));

		db_array.put("name", ch.getName());
		db_array.put("klasa", Integer.toString(ch.getKlasa()));
		db_array.put("level", Integer.toString(ch.getLvl()));
		db_array.put("way", Integer.toString(ch.getWay()));
		db_array.put("exp", Integer.toString(ch.getExp()));

		db_array.put("necklace", Integer.toString(ch.getEq().getNecklace()));
		db_array.put("helmet", Integer.toString(ch.getEq().getHelmet()));
		db_array.put("wings", Integer.toString(ch.getEq().getWings()));
		db_array.put("leftH", Integer.toString(ch.getEq().getLeftH()));
		db_array.put("armor", Integer.toString(ch.getEq().getArmor()));
		db_array.put("rightH", Integer.toString(ch.getEq().getRightH()));
		db_array.put("ring1", Integer.toString(ch.getEq().getRing1()));
		db_array.put("ring2", Integer.toString(ch.getEq().getRing2()));
		db_array.put("boots", Integer.toString(ch.getEq().getBoots()));

		for (int i = 0; i < 16; i++) {
			int slot[] = ch.getQuickpanel().getSlot();
			int slot_nr[] = ch.getQuickpanel().getSlot_number();

			db_array.put("qslot" + (i + 1), Integer.toString(slot[i]));
			db_array.put("qslot" + (i + 1) + "_number",
					Integer.toString(slot_nr[i]));
		}

		for (int i = 0; i < 20; i++) {
			int slot[] = ch.getBackpack().getSlot();
			int slot_nr[] = ch.getBackpack().getSlot_number();

			db_array.put("slot" + (i + 1), Integer.toString(slot[i]));
			db_array.put("slot" + (i + 1) + "_number",
					Integer.toString(slot_nr[i]));
		}

		db_array.put("corX", Float.toString(ch.getTempdata().getCor_x()));
		db_array.put("corY", Float.toString(ch.getTempdata().getCor_y()));
		db_array.put("corZ", Float.toString(ch.getTempdata().getCor_z()));

		db_array.put("hp", Integer.toString(ch.getTempdata().getHp()));
		db_array.put("mp", Integer.toString(ch.getTempdata().getMp()));
		db_array.put("world", Integer.toString(ch.getTempdata().getWorld()));

		db_array.put("life", Integer.toString(ch.getStats().getLife()));
		db_array.put("atk", Integer.toString(ch.getStats().getAtk()));
		db_array.put("def", Integer.toString(ch.getStats().getDef()));
		db_array.put("dex", Integer.toString(ch.getStats().getDex()));
		db_array.put("wis", Integer.toString(ch.getStats().getWis()));

		db_array.put("skull", Integer.toString(ch.getStats().getSkull()));
		db_array.put("killed_overall",
				Integer.toString(ch.getStats().getKilled_overall()));
		db_array.put("killed_last_week",
				Integer.toString(ch.getStats().getKilled_last_week()));
		db_array.put("skull_time",
				Integer.toString(ch.getStats().getSkull_time()));

		// db_array.put("todo", Integer.toString(ch.getSpellbook().get ));

	}

	public static boolean saveCharacterDisconnect(T_Character myChar,
			JSONObject jso) throws JSONException, SQLException {

		T_Character.saveCharacter(myChar);
		return true;
	}

	public static boolean saveCharacter(int chId, JSONObject jso,
			T_Character myChar) throws JSONException, SQLException {

		/*
		 * // stats int life = jso.getInt("life"); int atk = jso.getInt("atk");
		 * int def = jso.getInt("def"); int dex = jso.getInt("dex"); int wis =
		 * jso.getInt("wis");
		 * 
		 * int skull = jso.getInt("skull"); int killed_overall =
		 * jso.getInt("killed_overall"); int killed_last_week =
		 * jso.getInt("killed_last_week"); int skull_time =
		 * jso.getInt("skull_time");
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * T_Eq e = myChar.getEq();
		 * 
		 * T_Spellbook spell =myChar.getSpellbook(); T_Stats s =
		 * myChar.getStats();
		 * 
		 * 
		 * s.setCharacterId(chId); s.setAtk(atk); s.setDef(def); s.setDex(dex);
		 * s.setKilled_last_week(killed_last_week);
		 * s.setKilled_overall(killed_overall); s.setLife(life);
		 * s.setSkull(skull_time); s.setSkull_time(skull_time); s.setWis(wis);
		 * ch.setStats(s);
		 * 
		 * spell.setCharacterId(chId); ch.setSpellbook(spell);
		 */

		T_Character.saveCharacter(myChar);
		return true;
	}

	public static boolean savePeriodCharacter(T_Character myChar, JSONObject jso)
			throws JSONException, SQLException {

		// temp
		int world = jso.getInt("world");
		int hp = jso.getInt("hp");
		int mp = jso.getInt("mp");

		float cor_x = (float) (jso.getInt("corX") / 100.0);
		float cor_y = (float) (jso.getInt("corY") / 100.0);
		float cor_z = (float) (jso.getInt("corZ") / 100.0);

		// int exp = jso.getInt("exp");
		// int level = jso.getInt("level");

		// myChar.setLvl(level);
		// myChar.setExp(exp);

		T_Tempdata tmp = myChar.getTempdata();
		tmp.setCor_x(cor_x);
		tmp.setCor_y(cor_y);
		tmp.setCor_z(cor_z);
		tmp.setHp(hp);
		tmp.setMp(mp);
		tmp.setWorld(world);

		return false;
	}

	public static boolean savePeriodCharacterStats(T_Character myChar,
			JSONObject jso) throws JSONException, SQLException {

		int life = jso.getInt("life");
		int atk = jso.getInt("atk");
		int def = jso.getInt("def");
		int dex = jso.getInt("dex");
		int wis = jso.getInt("wis");
		T_Stats s = myChar.getStats();
		s.setAtk(atk);
		s.setDef(def);
		s.setDex(dex);
		s.setLife(life);
		s.setWis(wis);

		return false;
	}

	public static boolean savePeriodCharacterItems(T_Character myChar,
			JSONObject jso) throws JSONException, SQLException {

		boolean q = jso.getBoolean("quick");
		boolean e = jso.getBoolean("eq");
		boolean b = jso.getBoolean("backpack");

		if (q) {
			// quickpanel
			int qslot[] = new int[16];
			int qslot_number[] = new int[16];

			for (int i = 0; i < 16; i++) {
				qslot[i] = jso.getInt("qslot" + (i + 1));
				qslot_number[i] = jso.getInt("qslot" + (i + 1) + "_number");
			}

			T_Quickpanel qu = myChar.getQuickpanel();
			qu.setSlot(qslot);
			qu.setSlot_number(qslot_number);
		}

		if (e) {

			int necklace = jso.getInt("necklace");
			int helmet = jso.getInt("helmet");
			int wings = jso.getInt("wings");

			int leftH = jso.getInt("leftH");
			int armor = jso.getInt("armor");
			int rightH = jso.getInt("rightH");

			int ring1 = jso.getInt("ring1");
			int ring2 = jso.getInt("ring2");
			int boots = jso.getInt("boots");

			T_Eq eq = myChar.getEq();
			eq.setArmor(armor);
			eq.setBoots(boots);
			eq.setHelmet(helmet);
			eq.setLeftH(leftH);
			eq.setNecklace(necklace);
			eq.setRightH(rightH);
			eq.setRing1(ring1);
			eq.setRing2(ring2);
			eq.setWings(wings);

		}

		if (b) {
			// backpack
			int slot[] = new int[20];
			int slot_number[] = new int[20];

			for (int i = 0; i < 20; i++) {
				slot[i] = jso.getInt("slot" + (i + 1));
				slot_number[i] = jso.getInt("slot" + (i + 1) + "_number");
			}
			T_Backpack ba = myChar.getBackpack();
			ba.setSlot(slot);
			ba.setSlot_number(slot_number);
		}

		return false;
	}

	public static boolean saveCharacterWay(T_Character myChar, JSONObject jso)
			throws JSONException, SQLException {

		int way = jso.getInt("way");
		myChar.setWay(way);
		myChar.saveWay();
		return false;
	}

	public static int AtkCharacterId(T_Character myChar, JSONObject jso,
			T_Character hisChar, ActionscriptObject db_array, int addDmg)
			throws JSONException, SQLException {

		Float myX = myChar.getTempdata().getCor_x();
		Float myZ = myChar.getTempdata().getCor_z();

		Float hisX = hisChar.getTempdata().getCor_x();
		Float hisZ = hisChar.getTempdata().getCor_z();

		hisX -= myX;
		hisZ -= myZ;

		float odl = hisX * hisX + hisZ * hisZ;
		odl = (float) Math.sqrt(odl);
		if (odl < 3.0) {
			int dmg = 5 + myChar.getStats().getAtk() + myChar.getTempdata().getItemsAtk()
					- (int)(hisChar.getTempdata().getItemsDef() + (hisChar.getStats().getDef()) * 0.8) + addDmg;
			if (dmg < 0) {
				dmg = 0;
			}

			db_array.put("id", Integer.toString(myChar.getId()));
			db_array.put("attackedId", Integer.toString(hisChar.getId()));
			db_array.put("dmg", Integer.toString(dmg));

			return dmg;
		}
		return -1;
	}

	public static int giveExpKill(T_Character killer, T_Character victim,
			ActionscriptObject db_array) throws JSONException, SQLException {

		int exp = giveExp(killer.getLvl(), victim.getLvl()) + killer.getExp();
		int lvl = getLvl(exp);
		killer.setExp(exp);
		killer.setLvl(lvl);

		db_array.put("id", Integer.toString(killer.getId()));
		db_array.put("exp", Integer.toString(exp));
		db_array.put("lvl", Integer.toString(lvl));

		return exp;
	}

	static int getLvl(int exp) {
		int lvl = (int) Math.sqrt(exp);
		return lvl;
	}

	static int giveExp(int myLvl, int hisLvl) {
		int roznica = (hisLvl - myLvl);
		if (roznica < 1)
			roznica = 0;
		int exp = 5 + 10 * roznica + hisLvl;
		return exp;
	}

}
