package repository;

import gameServ.gameServ;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class T_Backpack {

	private int id;
	private int characterId;

	private int[] slot;
	private int[] slot_number;

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

	public T_Backpack() {
		setSlot(new int[20]);
		setSlot_number(new int[20]);
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

	public static void createBackpack(int charId) throws SQLException {

		String zapytanie = "INSERT INTO backpack (CHARACTERS_ID) VALUES ('"
				+ charId + "')";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();
	}

	public static T_Backpack loadBackpack(int charId) throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM backpack WHERE CHARACTERS_id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();

		if (result.first()) {
			T_Backpack bp = new T_Backpack();

			int tablica[] = new int[20];
			int tablica_nr[] = new int[20];

			for (int i = 0; i < 20; i++) {
				tablica[i] = result.getInt("slot" + (i + 1));
				tablica_nr[i] = result.getInt("slot" + (i + 1) + "_number");
			}
			bp.setSlot(tablica);
			bp.setSlot_number(tablica_nr);
			bp.setCharacterId(charId);

			return bp;
		}

		return null;
	}

	public static void saveBackpack(T_Backpack b) throws SQLException {
		int localCharId = b.getCharacterId();

		int slot[] = b.getSlot();
		int slot_number[] = b.getSlot_number();

		String zapytanie = "UPDATE backpack SET slot1='" + slot[0]
				+ "', slot2='" + slot[1] + "', slot3='" + slot[2]
				+ "', slot4='" + slot[3] + "', slot5='" + slot[4]
				+ "' WHERE id ='" + localCharId + "'  ";
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql.executeUpdate();
		

		zapytanie = "UPDATE backpack SET slot6='" + slot[5] + "', slot7='"
				+ slot[6] + "', slot8='" + slot[7] + "', slot9='" + slot[8]
				+ "', slot10='" + slot[9] + "' WHERE id ='" + localCharId
				+ "' ";
		PreparedStatement sql2 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql2.executeUpdate();

		zapytanie = "UPDATE backpack SET slot11='" + slot[10] + "', slot12='"
				+ slot[11] + "', slot13='" + slot[12] + "', slot14='"
				+ slot[13] + "', slot15='" + slot[14] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql3 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql3.executeUpdate();
		
		zapytanie = "UPDATE backpack SET slot16='" + slot[15] + "', slot17='"
				+ slot[16] + "', slot18='" + slot[17] + "', slot19='"
				+ slot[18] + "', slot20='" + slot[19] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql4 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql4.executeUpdate();
		
		zapytanie = "UPDATE backpack SET slot1_number='" + slot_number[0]
				+ "', slot2_number='" + slot_number[1] + "', slot3_number='"
				+ slot_number[2] + "', slot4_number='" + slot_number[3]
				+ "', slot5_number='" + slot_number[4] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql5 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql5.executeUpdate();
		
		zapytanie = "UPDATE backpack SET slot6_number='" + slot_number[5]
				+ "', slot7_number='" + slot_number[6] + "', slot8_number='"
				+ slot_number[7] + "', slot9_number='" + slot_number[8]
				+ "', slot10_number='" + slot_number[9] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql6 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql6.executeUpdate();

		zapytanie = "UPDATE backpack SET slot11_number='" + slot_number[10]
				+ "', slot12_number='" + slot_number[11] + "', slot13_number='"
				+ slot_number[12] + "', slot14_number='" + slot_number[13]
				+ "', slot15_number='" + slot_number[14] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql7 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql7.executeUpdate();
		
		zapytanie = "UPDATE backpack SET slot16_number='" + slot_number[15]
				+ "', slot17_number='" + slot_number[16] + "', slot18_number='"
				+ slot_number[17] + "', slot19_number='" + slot_number[18]
				+ "', slot20_number='" + slot_number[19] + "' WHERE id ='"
				+ localCharId + "' ";
		PreparedStatement sql8 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);
		sql8.executeUpdate();
		 
	}
}
