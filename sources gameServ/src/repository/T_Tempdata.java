package repository;

import gameServ.gameServ;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class T_Tempdata {

	private int id;
	private int characterId;

	private float cor_x;
	private float cor_y;
	private float cor_z;

	private int hp;
	private int mp;
	private int world;
	
	private int itemsAtk;
	private int itemsDef;

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

	public void setCor_x(float cor_x) {
		this.cor_x = cor_x;
	}

	public float getCor_x() {
		return cor_x;
	}

	public void setCor_y(float cor_y) {
		this.cor_y = cor_y;
	}

	public float getCor_y() {
		return cor_y;
	}

	public void setCor_z(float cor_z) {
		this.cor_z = cor_z;
	}

	public float getCor_z() {
		return cor_z;
	}

	public void setHp(int hp) {
		this.hp = hp;
	}

	public int getHp() {
		return hp;
	}

	public void setMp(int mp) {
		this.mp = mp;
	}

	public int getMp() {
		return mp;
	}

	public void setWorld(int world) {
		this.world = world;
	}

	public int getWorld() {
		return world;
	}

	public static void createTempdata(int charId) throws SQLException {

		String zapytanie = "INSERT INTO tempdata (CHARACTERS_ID) VALUES ('"
				+ charId + "')";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();
	}

	public static T_Tempdata loadTempdata(int charId) throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM tempdata WHERE CHARACTERS_id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();

		if (result.first()) {
			T_Tempdata td = new T_Tempdata();

			td.setCharacterId(charId);
			td.setCor_x(result.getInt("cor_x"));
			td.setCor_y(result.getInt("cor_y"));
			td.setCor_z(result.getInt("cor_z"));

			td.setHp(result.getInt("hp"));
			td.setMp(result.getInt("mp"));
			td.setWorld(result.getInt("world"));

			return td;
		}
		return null;
	}

	public static void saveTempdata(T_Tempdata t) throws SQLException {
		int chId = t.getCharacterId();

		int hp = t.getHp();
		int mp = t.getMp();
		int world = t.getWorld();

		float corX = t.getCor_x();
		float corY = t.getCor_y();
		float corZ = t.getCor_z();

		String zapytanie = "UPDATE tempdata SET hp='" + hp + "', mp='" + mp
				+ "', world='" + world + "', cor_x='" + corX + "', cor_y='"
				+ corY + "', cor_z='" + corZ + "' WHERE id ='" + chId + "' ";

		PreparedStatement sql2 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql2.executeUpdate();

	}

	public void setItemsAtk(int itemsAtk) {
		this.itemsAtk = itemsAtk;
	}

	public int getItemsAtk() {
		return itemsAtk;
	}

	public void setItemsDef(int itemsDef) {
		this.itemsDef = itemsDef;
	}

	public int getItemsDef() {
		return itemsDef;
	}

}
