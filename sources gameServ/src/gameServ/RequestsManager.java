package gameServ;

import java.nio.channels.SocketChannel;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.LinkedList;

import java.util.Set;

import it.gotoandplay.smartfoxserver.data.User;
import it.gotoandplay.smartfoxserver.db.DbManager;
import it.gotoandplay.smartfoxserver.lib.ActionscriptObject;

import org.json.JSONException;
import org.json.JSONObject;
import repository.*;

public class RequestsManager {
	public data tabela;
	public DbManager db;
	public Connection conn;

	public RequestsManager() {
		tabela = new data();
		db = gameServ.getObj().db;

	}

	public void handleRequest(String cmd, JSONObject jso, User u, int fromRoom) {

		gameServ.ThrowTrace("Zapytanie");
		try {
			if (cmd.equals("getAuth")) {
				conn = db.getConnection();
				getAuth(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("getChars")) {
				conn = db.getConnection();
				getChars(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("loadCharacter")) {
				conn = db.getConnection();
				getLoadCharacter(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("createCharacter")) {
				conn = db.getConnection();
				getCreateCharacter(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("saveCharacter")) {
				conn = db.getConnection();
				getSaveCharacter(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("savePeriodCharacter")) {
				getSaveCharacterPeriod(cmd, jso, u, fromRoom);
			}
			if (cmd.equals("disconnect")) {
				conn = db.getConnection();
				getSaveCharacterDisc(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("saveCharacterStats")) {
				getSaveCharacterStats(cmd, jso, u, fromRoom);
			}
			if (cmd.equals("saveCharacterWay")) {
				conn = db.getConnection();
				getSaveCharacterWay(cmd, jso, u, fromRoom);
				close();
			}
			if (cmd.equals("saveCharacterItems")) {
				getSaveCharacterItems(cmd, jso, u, fromRoom);
			}
			if (cmd.equals("atkCharacter")) {
				getAtkCharacterId(cmd, jso, u, fromRoom);
			}
			if (cmd.equals("characterDied")) {
				getCharacterDied(cmd, jso, u, fromRoom);
			}
			if (cmd.equals("saveCharacterItemsStats")) {
				saveCharacterItemsStats(cmd, jso, u, fromRoom);
			}

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			gameServ.ThrowTrace(e.getMessage() + e.toString());
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			gameServ.ThrowTrace(e.getMessage() + e.toString());
		}

	}

	private void close() {
		try {
			conn.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public void getAuth(String cmd, JSONObject jso, User u, int fromRoom)
			throws JSONException, SQLException {
		gameServ.ThrowTrace("StartAuth");
		String login = jso.getString("login");
		String pass = jso.getString("pass");

		LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
		recipients.add(u.getChannel());
		ActionscriptObject response = new ActionscriptObject();
		if (T_User.CheckPassword(login, pass, db, u)) {
			response.put("_cmd", "OkAuth");
			gameServ.ThrowTrace("Ok Auth");
			gameServ.getObj().sendResponse(response, fromRoom, u, recipients);

		} else {
			response.put("_cmd", "NoAuth");
			gameServ.ThrowTrace("No Auth");
			gameServ.getObj().sendResponse(response, fromRoom, u, recipients);
		}
	}

	public void getChars(String cmd, JSONObject jso, User u, int fromRoom)
			throws JSONException, SQLException {
		gameServ.ThrowTrace("StartChars");
		T_User me = tabela.getUserByServId(u.getUserId());
		Set<T_Character> lista = me.loadCharacters();

		LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
		recipients.add(u.getChannel());

		ActionscriptObject response = new ActionscriptObject();
		ActionscriptObject db_array = new ActionscriptObject();

		if (lista.isEmpty()) {
			gameServ.ThrowTrace("NoChars");
			response.put("_cmd", "NoChars");

		} else {
			responseCreator.putChars(lista, db_array);
			response.put("db", db_array);
			gameServ.ThrowTrace("Chars");
			response.put("_cmd", "Chars");

		}
		gameServ.getObj().sendResponse(response, fromRoom, u, recipients);
	}

	public void getCreateCharacter(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {
		gameServ.ThrowTrace("StartCreateCharacter");
		String newNick = jso.getString("newNick");
		int newKlasaId = jso.getInt("newKlasaId");
		T_User me = tabela.getUserByServId(u.getUserId());
		boolean ret = T_Character.createCharacter(newNick, newKlasaId, me);

		LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
		recipients.add(u.getChannel());

		ActionscriptObject response = new ActionscriptObject();

		if (ret == false) {
			gameServ.ThrowTrace("createCharacterErrorNick");
			response.put("_cmd", "createCharacterErrorNick");
		} else {
			gameServ.ThrowTrace("createCharacterOk");
			response.put("_cmd", "createCharacterOk");
		}

		gameServ.getObj().sendResponse(response, fromRoom, u, recipients);
	}

	public void getLoadCharacter(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {
		gameServ.ThrowTrace("StartLoadCharacter");
		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("characterId");
		if (me.hasCharacter(chId)) {

			LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
			recipients.add(u.getChannel());

			ActionscriptObject response = new ActionscriptObject();
			ActionscriptObject db_array = new ActionscriptObject();

			
			T_Character mine = T_Character.loadCharacter(chId);
			mine.setSocket(u.getChannel());
			tabela.addChar(mine);
			responseCreator.putCharacter(mine, db_array);
			response.put("db", db_array);
			response.put("_cmd", "Character");
			gameServ.ThrowTrace("Character");

			gameServ.getObj().sendResponse(response, fromRoom, u, recipients);
		}

	}

	public void getSaveCharacter(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {
		gameServ.ThrowTrace("StartSaveCharacter");
		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {

			if (responseCreator.saveCharacter(chId, jso,
					tabela.getCharById(chId))) {
				gameServ.ThrowTrace("saveCharacter");
			}
		}

	}

	public void getSaveCharacterPeriod(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {

			T_Character myChar = tabela.getCharById(chId);
			myChar.setSocket(u.getChannel());
			responseCreator.savePeriodCharacter(myChar, jso);
			gameServ.ThrowTrace("savePeriodCharacter");

			
			LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
			recipients.add(u.getChannel());
			ActionscriptObject response = new ActionscriptObject();
			ActionscriptObject db_array = new ActionscriptObject();
			response.put("db", db_array);
			response.put("_cmd", "getSaveCharacterPeriod");
			gameServ.getObj().sendResponse(response, fromRoom, u, recipients);
		}
	}

	public void getSaveCharacterStats(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {

			T_Character myChar = tabela.getCharById(chId);
			responseCreator.savePeriodCharacterStats(myChar, jso);
			gameServ.ThrowTrace("savePeriodCharacterStats");

		}
	}

	public void getSaveCharacterItems(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		
		if (me.hasCharacter(chId)) {

			T_Character myChar = tabela.getCharById(chId);
			responseCreator.savePeriodCharacterItems(myChar, jso);
			gameServ.ThrowTrace("savePeriodCharacterItems");
			
			LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
			recipients.add(u.getChannel());
			ActionscriptObject response = new ActionscriptObject();
			ActionscriptObject db_array = new ActionscriptObject();
			int control = jso.getInt("control");
			db_array.put("control", Integer.toString(control));
			response.put("db", db_array);
			response.put("_cmd", "savePeriodCharacterItems");
			gameServ.getObj().sendResponse(response, fromRoom, u, recipients);
		}
	}

	public void getSaveCharacterWay(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {

			T_Character myChar = tabela.getCharById(chId);
			responseCreator.saveCharacterWay(myChar, jso);
			gameServ.ThrowTrace("savePeriodCharacter");
		}
	}

	public void getAtkCharacterId(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {
		gameServ.ThrowTrace("start AtkCharacterId");
		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {
			int atkChId = jso.getInt("atkCharacterId");
			T_Character hisChar = tabela.getCharById(atkChId);
			T_Character myChar = tabela.getCharById(chId);
			int userId = hisChar.getUserId();
			userId = tabela.getUserId(userId).getServId();

			LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();
			recipients.add(u.getChannel());
			recipients.add(hisChar.getSocket());
			ActionscriptObject response = new ActionscriptObject();
			ActionscriptObject db_array = new ActionscriptObject();
			int addDmg = jso.getInt("addDmg");
			int dmg = responseCreator.AtkCharacterId(myChar, jso, hisChar,
					db_array, addDmg);
			if (dmg > -1) {

				response.put("db", db_array);
				response.put("_cmd", "AtkCharacter");
				gameServ.ThrowTrace("AtkCharacterId");
				gameServ.getObj().sendResponse(response, fromRoom, u,
						recipients);
			}

		}
	}

	public void getSaveCharacterDisc(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		T_Character myChar = tabela.getCharByUserId(me.getId());
		ActionscriptObject response = new ActionscriptObject();
		responseCreator.saveCharacterDisconnect(myChar, jso);
		gameServ.ThrowTrace("saveCharacter Disconnect");
	}

	public void getCharacterDied(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {
			int killerId = jso.getInt("killerId");
			T_Character myChar = tabela.getCharById(chId);

			LinkedList<SocketChannel> recipients = new LinkedList<SocketChannel>();

			ActionscriptObject response = new ActionscriptObject();
			ActionscriptObject db_array = new ActionscriptObject();

			T_Character killer = tabela.getCharById(killerId);
			recipients.add(killer.getSocket());

			int exp = responseCreator.giveExpKill(killer, myChar, db_array);
			if (exp > 0) {
				response.put("db", db_array);
				response.put("_cmd", "characterDied");
				gameServ.ThrowTrace("characterDied");

				gameServ.getObj().sendResponse(response, fromRoom, u,
						recipients);
			}
		}

	}
	
	public void saveCharacterItemsStats(String cmd, JSONObject jso, User u,
			int fromRoom) throws JSONException, SQLException {

		T_User me = tabela.getUserByServId(u.getUserId());
		int chId = jso.getInt("id");
		if (me.hasCharacter(chId)) {

			int atk = jso.getInt("atk");
			int def = jso.getInt("def");
			T_Character myChar = tabela.getCharById(chId);
			myChar.getTempdata().setItemsAtk(atk);
			myChar.getTempdata().setItemsDef(def);
			

		}

	}
	
	

}
