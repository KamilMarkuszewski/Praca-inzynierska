package gameServ;

import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.Vector;

import org.hibernate.Criteria;
import org.hibernate.Session;
import org.hibernate.criterion.Example;

import repository.T_Character;
import repository.T_User;

public class data {
	public Vector<T_User> users;
	public Vector<T_Character> characters;
	public Vector<Integer> charactersInGame;

	public data() {
		users = new Vector<T_User>();
		characters = new Vector<T_Character>();
	}

	public void addChar(T_Character character) {
		characters.add(character);
	}

	public void addPlayer(T_User user) {
		users.add(user);
	}

	public T_User getUserId(int id) {
		T_User tmp;
		for (Iterator<T_User> iter = users.iterator(); iter.hasNext();) {
			tmp = iter.next();
			if (tmp.getId() == id) {
				return tmp;
			}
		}
		return null;
	}

	public T_User getUserByServId(int servId) {
		T_User tmp = users.elementAt(servId);
		tmp.setPosition(servId);
		if (tmp.getServId() != servId) {
			int i = 0;
			for (Iterator<T_User> iter = users.iterator(); iter.hasNext();) {
				tmp = iter.next();
				if (tmp.getServId() == servId) {
					tmp.setPosition(servId);
					break;
				}
				i++;
			}
		}
		return tmp;
	}

	public T_Character getCharacterById(T_User actUser, int characterId) {
		Set<T_Character> ch = actUser.getCharacters();
		int i = 0;
		for (Iterator<T_Character> iter = ch.iterator(); iter.hasNext(); i++) {
			T_Character actual = iter.next();
			if (actual.getId() == characterId) {
				return actual;

			}
		}
		return null;
	}

	public T_Character getCharById(int characterId) {
		for (Iterator<T_Character> iter = characters.iterator(); iter.hasNext();) {
			T_Character tmp = iter.next();
			if (tmp.getId() == characterId) {
				return tmp;
			}
		}
		return null;
	}

	public T_Character getCharByUserId(int userId) {
		for (Iterator<T_Character> iter = characters.iterator(); iter.hasNext();) {
			T_Character tmp = iter.next();
			if (tmp.getUserId() == userId) {
				return tmp;
			}
		}
		return null;
	}

}
