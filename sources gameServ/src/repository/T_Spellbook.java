package repository;

import gameServ.gameServ;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class T_Spellbook {

	private int id;
	private int characterId;

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

	public static void createSpellbook(int charId) throws SQLException {
		String zapytanie = "INSERT INTO spellbook (CHARACTERS_ID) VALUES ('"
				+ charId + "')";

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement(zapytanie);

		sql.executeUpdate();
	}

	public static T_Spellbook loadSpellbook(int charId) throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM spellbook WHERE CHARACTERS_id='"
						+ charId + "' ");

		ResultSet result = sql.executeQuery();

		if (result.first()) {
			T_Spellbook sp = new T_Spellbook();

			sp.setCharacterId(charId);

			return sp;
		}
		return null;
	}
	
	public static void  saveSpellbook(T_Spellbook ch) throws SQLException {

	}
}
