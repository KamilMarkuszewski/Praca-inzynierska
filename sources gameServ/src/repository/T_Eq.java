package repository;

import gameServ.gameServ;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class T_Eq {

	private int id;
	private int characterId;

	private int necklace;
	private int helmet;
	private int wings;

	private int leftH;
	private int armor;
	private int rightH;

	private int ring1;
	private int ring2;
	private int boots;

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

	public void setNecklace(int necklace) {
		this.necklace = necklace;
	}

	public int getNecklace() {
		return necklace;
	}

	public void setHelmet(int helmet) {
		this.helmet = helmet;
	}

	public int getHelmet() {
		return helmet;
	}

	public void setWings(int wings) {
		this.wings = wings;
	}

	public int getWings() {
		return wings;
	}

	public void setLeftH(int leftH) {
		this.leftH = leftH;
	}

	public int getLeftH() {
		return leftH;
	}

	public void setArmor(int armor) {
		this.armor = armor;
	}

	public int getArmor() {
		return armor;
	}

	public void setRightH(int rightH) {
		this.rightH = rightH;
	}

	public int getRightH() {
		return rightH;
	}

	public void setRing1(int ring1) {
		this.ring1 = ring1;
	}

	public int getRing1() {
		return ring1;
	}

	public void setRing2(int ring2) {
		this.ring2 = ring2;
	}

	public int getRing2() {
		return ring2;
	}

	public void setBoots(int boots) {
		this.boots = boots;
	}

	public int getBoots() {
		return boots;
	}

	public static void createEq(int charId) throws SQLException {
		String zapytanie = "INSERT INTO eq (CHARACTERS_ID, leftH) VALUES ('" + charId
				+ "', '1' )";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();
	}

	public static T_Eq loadEq(int charId) throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM eq WHERE CHARACTERS_id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();

		if (result.first()) {
			T_Eq eq = new T_Eq();

			eq.setNecklace(result.getInt("necklace"));
			eq.setHelmet(result.getInt("helmet"));
			eq.setWings(result.getInt("wings"));

			eq.setLeftH(result.getInt("leftH"));
			eq.setArmor(result.getInt("armor"));
			eq.setRightH(result.getInt("rightH"));

			eq.setRing1(result.getInt("ring1"));
			eq.setBoots(result.getInt("boots"));
			eq.setRing2(result.getInt("ring2"));

			eq.setCharacterId(charId);

			return eq;
		}
		return null;
	}

	public static void saveEq(T_Eq e) throws SQLException {
		
		int chId = e.getCharacterId();
		
		int local_necklace = e.getNecklace();
		int local_helmet = e.getHelmet();
		int local_wings=e.getWings();

		int local_leftH = e.getLeftH();
		int local_armor = e.getArmor();
		int local_rightH = e.getRightH();
		
		int local_ring1 = e.getRing1();
		int local_ring2 = e.getRing2();
		int local_boots = e.getBoots();


		String zapytanie = "UPDATE eq SET necklace='"+local_necklace+"', helmet='"+local_helmet+"', wings='"+local_wings+"', leftH='"+local_leftH+"', armor='"+local_armor+"', rightH='"+local_rightH+"', ring1='"+local_ring1+"', ring2='"+local_ring2+"', boots='"+local_boots+"'  WHERE id ='" +chId+ "' ";

		PreparedStatement sql2 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql2.executeUpdate();      
	}
}
