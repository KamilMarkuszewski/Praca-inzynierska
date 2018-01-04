package gameServ;

import java.nio.channels.SocketChannel;
import java.sql.Connection;
import java.util.*;

import it.gotoandplay.smartfoxserver.db.*;
import it.gotoandplay.smartfoxserver.data.*;
import it.gotoandplay.smartfoxserver.exceptions.*;
import it.gotoandplay.smartfoxserver.extensions.*;
import it.gotoandplay.smartfoxserver.lib.ActionscriptObject;
import it.gotoandplay.smartfoxserver.events.InternalEventObject;

import org.json.JSONObject; 
import it.gotoandplay.smartfoxserver.extensions.AbstractExtension;
import org.json.*;

import repository.T_User;


public class gameServ extends AbstractExtension
{
	static gameServ me;
	public RequestsManager manager;
    public ExtensionHelper helper;
    public DbManager db;
    public Zone currZone;

	
	public void init()
	{
        // Get Helper
        helper = ExtensionHelper.instance();
        
        // Get the current zone
        currZone = helper.getZone(this.getOwnerZone());
        
        // Get database manager
        db = currZone.dbManager;

		me = this;
		trace("Extension initialized");	
		manager = new RequestsManager();
	}
	
	public static void ThrowTrace(String str){
		me.trace(str);
	}
	
	
	public static gameServ getObj(){
		return me;
	}

	public void destroy()
	{
        db = null;

		trace("Extension destroyed");
	}

	public void handleRequest(String cmd, JSONObject jso, User u, int fromRoom)
	{
		T_User t;
		trace("handleRequest");
		manager.handleRequest(cmd, jso, u, fromRoom);
	}
	
	
	public void handleRequest(String cmd)
	{
		trace("EMPTY REQUEST!" + cmd);
		
	}
	
	
	
	
	
	
	// UNUSED
	public void handleRequest(String cmd, ActionscriptObject ao, User u, int fromRoom)
	{
		trace("AO REQUEST");
		// Your code here, handles XML-based requests
	}
	
	public void handleRequest(String cmd, String params[], User u, int fromRoom)
	{
		trace("String REQUEST");
		// Your code here, handles String-based requests
	}

	public void handleInternalEvent(InternalEventObject ieo)
	{
		trace("InternalEvent IEO" + ieo.getEventName());
		if(ieo.getEventName().equals(InternalEventObject.EVENT_USER_LOST)){
			User u = (User) ieo.getObject("user");

			manager.handleRequest("disconnect", null, u, 0);
		}
		
		// Your code here, handles server events
	}
	
	public Object handleInternalRequest(Object param)
	{
		trace("InternalEvent");
		// (Optional) Your code here, handles an internal request
		return param;
	}
}
