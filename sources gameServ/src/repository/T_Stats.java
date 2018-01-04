package repository;

import gameServ.RequestsManager;
import gameServ.gameServ;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class T_Stats {

	private int id;
	private int characterId;
	private int life;
	private int atk;
	private int def;
	private int dex;
	private int wis;
	private int skull;
	private int skull_time;
	private int killed_overall;
	private int killed_last_week;

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

	public void setLife(int life) {
		this.life = life;
	}

	public int getLife() {
		return life;
	}

	public void setAtk(int atk) {
		this.atk = atk;
	}

	public int getAtk() {
		return atk;
	}

	public void setDef(int def) {
		this.def = def;
	}

	public int getDef() {
		return def;
	}

	public void setDex(int dex) {
		this.dex = dex;
	}

	public int getDex() {
		return dex;
	}

	public void setWis(int wis) {
		this.wis = wis;
	}

	public int getWis() {
		return wis;
	}

	public void setSkull(int skull) {
		this.skull = skull;
	}

	public int getSkull() {
		return skull;
	}

	public void setSkull_time(int skull_time) {
		this.skull_time = skull_time;
	}

	public int getSkull_time() {
		return skull_time;
	}

	public void setKilled_overall(int killed_overall) {
		this.killed_overall = killed_overall;
	}

	public int getKilled_overall() {
		return killed_overall;
	}

	public void setKilled_last_week(int killed_last_week) {
		this.killed_last_week = killed_last_week;
	}

	public int getKilled_last_week() {
		return killed_last_week;
	}

	public static void createStats(int charId) throws SQLException {
		String zapytanie = "INSERT INTO stats (CHARACTERS_ID) VALUES ('"
				+ charId + "')";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();

	}

	public static T_Stats loadStats(int charId) throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM stats WHERE CHARACTERS_id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();

		if (result.first()) {
			T_Stats st = new T_Stats();

			st.setCharacterId(charId);
			st.setAtk(result.getInt("atk"));
			st.setLife(result.getInt("life"));
			st.setDef(result.getInt("def"));
			st.setDex(result.getInt("dex"));
			st.setWis(result.getInt("wis"));

			st.setSkull(result.getInt("skull"));
			st.setSkull_time(result.getInt("skull_time"));
			st.setKilled_overall(result.getInt("killed_overall"));
			st.setKilled_last_week(result.getInt("killed_last_week"));

			return st;
		}
		return null;
	}

	public static void saveStats(T_Stats s) throws SQLException {
		int chId = s.getCharacterId();

		int local_life = s.getLife();
		int local_atk = s.getAtk();
		int local_def = s.getDef();
		int local_dex = s.getDex();
		int local_wis = s.getWis();
		int localCharId = s.getCharacterId();

		int skull = s.getSkull();
		int skull_time = s.getSkull_time();
		int killed_overall = s.getKilled_overall();
		int killed_last_week = s.getKilled_last_week();

		String zapytanie = "UPDATE stats SET life='" + local_life + "',atk='"
				+ local_atk + "', def='" + local_def + "', dex='" + local_dex
				+ "', wis='" + local_wis + "', skull='" + skull
				+ "', skull_time='" + skull_time + "', killed_overall='"
				+ killed_overall + "', killed_last_week='" + killed_last_week
				+ "' WHERE id ='" + localCharId + "'  ";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();
	}
}
