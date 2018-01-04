package repository;

import gameServ.gameServ;

import java.nio.channels.SocketChannel;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashSet;
import java.util.Set;

public class T_Character implements java.io.Serializable {

	/**
	 * 
	 */

	private static final long serialVersionUID = 6279814036616079930L;

	private int id;
	private int userId;
	private int klasa;
	private int exp;
	private int lvl;
	private String name;
	private int way;
	private SocketChannel socket;

	private T_Backpack backpack;
	private T_Eq eq;
	private T_Quickpanel quickpanel;
	private T_Spellbook spellbook;
	private T_Stats stats;
	private T_Tempdata tempdata;

	public void setId(int id) {
		this.id = id;
	}

	public int getId() {
		return id;
	}

	public void setUserId(int userId) {
		this.userId = userId;
	}

	public int getUserId() {
		return userId;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getName() {
		return name;
	}

	public void setWay(int way) {
		this.way = way;
	}

	public int getWay() {
		return way;
	}

	public void setKlasa(int klasa) {
		this.klasa = klasa;
	}

	public int getKlasa() {
		return klasa;
	}

	public void setExp(int exp) {
		this.exp = exp;
	}

	public int getExp() {
		return exp;
	}

	public void setLvl(int lvl) {
		this.lvl = lvl;
	}

	public int getLvl() {
		return lvl;
	}

	public static boolean createCharacter(String newNick, int newKlasaId,
			T_User me) throws SQLException {

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT count(id) FROM characters WHERE name='"
						+ newNick + "' ");

		ResultSet result = sql.executeQuery();
		if (!result.first() || result.getInt("count(id)") > 0) {
			return false;
		} else {

			String zapytanie = "INSERT INTO characters (USERS_id, class, exp, lvl, name) VALUES ('"
					+ me.getId()
					+ "', '"
					+ newKlasaId
					+ "', '1', '1','"
					+ newNick + "')";

			PreparedStatement sql2 = gameServ.getObj().manager.conn
					.prepareStatement(zapytanie);

			sql2.executeUpdate();

			PreparedStatement sql3 = gameServ.getObj().manager.conn
					.prepareStatement("SELECT id FROM characters WHERE name='"
							+ newNick + "' ");
			ResultSet resultId = sql3.executeQuery();
			if (resultId.first()) {
				int charId = resultId.getInt("id");
				T_Backpack.createBackpack(charId);
				T_Eq.createEq(charId);
				T_Quickpanel.createQuickpanel(charId);
				T_Spellbook.createSpellbook(charId);
				T_Stats.createStats(charId);
				T_Tempdata.createTempdata(charId);
			}
		}
		return true;
	}

	public void setBackpack(T_Backpack backpack) {
		this.backpack = backpack;
	}

	public T_Backpack getBackpack() {
		return backpack;
	}

	public void setEq(T_Eq eq) {
		this.eq = eq;
	}

	public T_Eq getEq() {
		return eq;
	}

	public void setQuickpanel(T_Quickpanel quickpanel) {
		this.quickpanel = quickpanel;
	}

	public T_Quickpanel getQuickpanel() {
		return quickpanel;
	}

	public void setSpellbook(T_Spellbook spellbook) {
		this.spellbook = spellbook;
	}

	public T_Spellbook getSpellbook() {
		return spellbook;
	}

	public void setStats(T_Stats stats) {
		this.stats = stats;
	}

	public T_Stats getStats() {
		return stats;
	}

	public void setTempdata(T_Tempdata tempdata) {
		this.tempdata = tempdata;
	}

	public T_Tempdata getTempdata() {
		return tempdata;
	}

	public static T_Character loadCharacter(int charId) throws SQLException {

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM characters WHERE id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();
		if (result.first()) {
			T_Character ch = new T_Character();
			


			ch.setId(result.getInt("id"));
			ch.setUserId(result.getInt("USERS_id"));
			ch.setKlasa(result.getInt("class"));
			ch.setExp(result.getInt("exp"));
			ch.setLvl(result.getInt("lvl"));
			ch.setName(result.getString("name"));
			ch.setWay(result.getInt("way"));

			T_Backpack bp = T_Backpack.loadBackpack(charId);
			T_Eq eq = T_Eq.loadEq(charId);
			T_Quickpanel q = T_Quickpanel.loadQuickpanel(charId);
			T_Spellbook spell = T_Spellbook.loadSpellbook(charId);
			T_Stats st = T_Stats.loadStats(charId);
			T_Tempdata temp = T_Tempdata.loadTempdata(charId);

			ch.setBackpack(bp);
			ch.setEq(eq);
			ch.setQuickpanel(q);
			ch.setSpellbook(spell);
			ch.setStats(st);
			ch.setTempdata(temp);
			return ch;
		} else {
			return null;
		}
	}

	public static void saveCharacter(T_Character ch) throws SQLException {

		int chId = ch.getId();
		int exp = ch.getExp();
		int lvl = ch.getLvl();

		String zapytanie = "UPDATE characters SET lvl='" + lvl + "', exp='"
				+ exp + "'  WHERE id ='" + chId + "' ";

		PreparedStatement sql2 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql2.executeUpdate();
		
		T_Backpack.saveBackpack(ch.getBackpack());
		T_Eq.saveEq(ch.getEq());
		T_Quickpanel.saveQuickpanel(ch.getQuickpanel());
		T_Spellbook.saveSpellbook(ch.getSpellbook());
		T_Stats.saveStats(ch.getStats());
		T_Tempdata.saveTempdata(ch.getTempdata());
		
	}
	
	public void saveWay() throws SQLException {

		int way = this.getWay();
		int chId = this.getId();

		String zapytanie = "UPDATE characters SET way='" + way + "'  WHERE id ='" + chId + "' ";

		PreparedStatement sql2 = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql2.executeUpdate();		
	}

	public void setSocket(SocketChannel socket) {
		this.socket = socket;
	}

	public SocketChannel getSocket() {
		return socket;
	}
}
