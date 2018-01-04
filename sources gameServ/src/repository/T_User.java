package repository;

import gameServ.gameServ;
import it.gotoandplay.smartfoxserver.data.User;
import it.gotoandplay.smartfoxserver.db.DbManager;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;





public class T_User {

	private int id;
	private String login;
	private String pass;
	private String mail;
	private int ServId;
	private int position;

	private Set<T_Character> characters = new HashSet<T_Character>();

	public void setId(int id) {
		this.id = id;
	}

	public int getId() {
		return id;
	}

	public void setLogin(String login) {
		this.login = login;
	}

	public String getLogin() {
		return login;
	}

	public void setPass(String pass) {
		this.pass = pass;
	}

	public String getPass() {
		return pass;
	}

	public void setMail(String mail) {
		this.mail = mail;
	}

	public String getMail() {
		return mail;
	}

	public void setServId(int servId) {
		ServId = servId;
	}

	public int getServId() {
		return ServId;
	}

	public void setCharacters(Set<T_Character> characters) {
		this.characters = characters;
	}

	public Set<T_Character> getCharacters() {
		return characters;
	}

	public Set<T_Character> loadCharacters() throws SQLException {
		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT id, class, lvl, name, way FROM characters WHERE USERS_id='"
						+ id + "' ");

		ResultSet result = sql.executeQuery();
		Set<T_Character> chars = new HashSet<T_Character>();
		
		if (result.first()) {
			do {
				T_Character ch = new T_Character();
				ch.setId(result.getInt("id"));
				ch.setKlasa(result.getInt("class"));
				ch.setLvl(result.getInt("lvl"));
				ch.setName(result.getString("name"));
				ch.setWay(result.getInt("way"));				
				chars.add(ch);
			} while (result.next());
		}
		characters = chars;
		return chars;
	}

	static public boolean CheckPassword(String login, String pass,
			DbManager db, User u) throws SQLException {

		PreparedStatement sql = gameServ.getObj().manager.conn
				.prepareStatement("SELECT * FROM users WHERE login='" + login
						+ "' AND pass='" + pass + "' ");

		ResultSet result = sql.executeQuery();
		if (result.first()) {
			T_User user = new T_User();
			user.setId(result.getInt("id"));
			user.setLogin(result.getString("login"));
			user.setPass(result.getString("pass"));
			user.setMail(result.getString("mail"));
			user.setServId(u.getUserId());
			gameServ.getObj().manager.tabela.addPlayer(user);
			return true;
		} else {
			return false;
		}

	}

	public void AddCharacter(T_Character ch) {
		characters.add(ch);
	}

	public void setPosition(int position) {
		this.position = position;
	}

	public int getPosition() {
		return position;
	}
	
	public boolean hasCharacter(int charId){
		for(Iterator<T_Character> iter = characters.iterator(); iter.hasNext(); ){
			T_Character ch = iter.next();
			if(ch.getId() == charId){
				return true;
			}
		}
		
		return false;
	}

}
