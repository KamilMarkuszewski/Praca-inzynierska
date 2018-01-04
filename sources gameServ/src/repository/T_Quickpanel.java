package repository;

import gameServ.gameServ;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class T_Quickpanel {

	private int id;
	private int characterId;

	private int[] slot;
	private int[] slot_number;

	public T_Quickpanel() {
		setSlot(new int[16]);
		setSlot_number(new int[16]);
	}

	public void setSlot(int[] slot) {
		this.slot = slot;
	}

	public int[] getSlot() {
		return slot;
	}

	public void setSlot_number(int[] slot_number) {
		this.slot_number = slot_number;
	}

	public int[] getSlot_number() {
		return slot_number;
	}

	public void setId(int id) {
		this.id = id;
	}

	public int getId() {
		return id;
	}

	public void setCharacterId(int characterId) {
		this.characterId = characterId;
	}

	public int getCharacterId() {
		return characterId;
	}

	public static void createQuickpanel(int charId) throws SQLException {
		String zapytanie = "INSERT INTO quickpanel (CHARACTERS_ID) VALUES ('"
				+ charId + "')";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();
	}

	public static T_Quickpanel loadQuickpanel(int charId) throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM quickpanel WHERE CHARACTERS_id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();

		if (result.first()) {
			T_Quickpanel q = new T_Quickpanel();

			int tablica[] = new int[16];
			int tablica_nr[] = new int[16];

			for (int i = 0; i < 16; i++) {
				tablica[i] = result.getInt("slot" + (i + 1));
				tablica_nr[i] = result.getInt("slot" + (i + 1) + "_number");
			}
			q.setSlot(tablica);
			q.setSlot_number(tablica_nr);
			q.setCharacterId(charId);

			return q;
		}
		return null;
	}

	public static void saveQuickpanel(T_Quickpanel q) throws SQLException {
		int localCharId = q.getCharacterId();

		int slot[] = q.getSlot();
		int slot_number[] = q.getSlot_number();

		String zapytanie = "UPDATE quickpanel SET slot1='" + slot[0]
				+ "', slot2='" + slot[1] + "', slot3='" + slot[2]
				+ "', slot4='" + slot[3] + "', slot5='" + slot[4]
				+ "' WHERE id ='" + localCharId + "'  ";
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot6='" + slot[5] + "', slot7='"
				+ slot[6] + "', slot8='" + slot[7] + "', slot9='" + slot[8]
				+ "', slot10='" + slot[9] + "' WHERE id ='" + localCharId
				+ "' ";
		PreparedStatement sql2 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql2.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot11='" + slot[10] + "', slot12='"
				+ slot[11] + "', slot13='" + slot[12] + "', slot14='"
				+ slot[13] + "', slot15='" + slot[14] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql3 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql3.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot16='" + slot[15]
				+ "' WHERE id ='" + localCharId + "' ";
		PreparedStatement sql4 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql4.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot1_number='" + slot_number[0]
				+ "', slot2_number='" + slot_number[1] + "', slot3_number='"
				+ slot_number[2] + "', slot4_number='" + slot_number[3]
				+ "', slot5_number='" + slot_number[4] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql5 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql5.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot6_number='" + slot_number[5]
				+ "', slot7_number='" + slot_number[6] + "', slot8_number='"
				+ slot_number[7] + "', slot9_number='" + slot_number[8]
				+ "', slot10_number='" + slot_number[9] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql6 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql6.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot11_number='" + slot_number[10]
				+ "', slot12_number='" + slot_number[11] + "', slot13_number='"
				+ slot_number[12] + "', slot14_number='" + slot_number[13]
				+ "', slot15_number='" + slot_number[14] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql7 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql7.executeUpdate();

		zapytanie = "UPDATE quickpanel SET slot16_number='" + slot_number[15]
				+ "' WHERE id ='" + localCharId + "' ";
		PreparedStatement sql8 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql8.executeUpdate();
	}

}
