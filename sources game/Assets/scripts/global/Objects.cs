using UnityEngine;
using System.Collections;
using pixelMaps;

public class Objects : MonoBehaviour
{

    public static int odstep = 10;
    public static PlayerSpawnController playerSpawnController;
    public static NetworkController networkController;

    public static MyData myData;
    public static MouseNav mouseNav;
    public static LeftPanel leftPanel;
    public static DrawMain drawMain;
    public static DrawHpBar drawHpBar;
    public static GuiInfo guiInfo;
    public static DragAbleTable dragAbleTable;
    public static ContextMenu contextMenu;


    public static ColorTextures colorTextures;
    public static GuiTextures guiTextures;
    public static ItemsTextures itemsTextures;

    public static MapMain mapMain;
    public static ButtonsMap buttonsMap;
    public static LocalPlayerData.MyPlayer myLocalPlayer;

    public static guiChoose guiChooseObj;
    public static soundsRepository sounds;


    static public void saveGuiChoose()
    {
        guiChooseObj = GameObject.Find("Main Camera").GetComponent(typeof(guiChoose)) as guiChoose;
    }

    static public void CreateInWorld()
    {
        mapMain = new MapMain();
        buttonsMap = new ButtonsMap();
        drawMain = new DrawMain();
        drawHpBar = new DrawHpBar();
        guiInfo = new GuiInfo();
        guiInfo.makeDesc();
        contextMenu = GameObject.Find("localPlayer").GetComponent(typeof(ContextMenu)) as ContextMenu;
    }

    static public void CreateInChoose()
    {
        colorTextures = new ColorTextures();
        colorTextures.makeTexture();
        if (myLocalPlayer == null)
        {
            myLocalPlayer = new LocalPlayerData.MyPlayer();
        }
    }


    // Use this for initialization
    void Start()
    {
        CreateInChoose();
        playerSpawnController = GameObject.Find("NetworkController").GetComponent(typeof(PlayerSpawnController)) as PlayerSpawnController;
        networkController = GameObject.Find("NetworkController").GetComponent(typeof(NetworkController)) as NetworkController;

        myData = GameObject.Find("localPlayer").GetComponent(typeof(MyData)) as MyData;
        mouseNav = GameObject.Find("localPlayer").GetComponent(typeof(MouseNav)) as MouseNav;
        leftPanel = GameObject.Find("localPlayer").GetComponent(typeof(LeftPanel)) as LeftPanel;
        guiTextures = GameObject.Find("Repository").GetComponent(typeof(GuiTextures)) as GuiTextures;
        itemsTextures = GameObject.Find("Repository").GetComponent(typeof(ItemsTextures)) as ItemsTextures;
        dragAbleTable = GameObject.Find("localPlayer").GetComponent(typeof(DragAbleTable)) as DragAbleTable;
        sounds = GameObject.Find("Repository").GetComponent(typeof(soundsRepository)) as soundsRepository;
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerSpawnController) playerSpawnController = GameObject.Find("NetworkController").GetComponent(typeof(PlayerSpawnController)) as PlayerSpawnController;
        if (!networkController) networkController = GameObject.Find("NetworkController").GetComponent(typeof(NetworkController)) as NetworkController;

        if (!myData) myData = GameObject.Find("localPlayer").GetComponent(typeof(MyData)) as MyData;
        if (!mouseNav) mouseNav = GameObject.Find("localPlayer").GetComponent(typeof(MouseNav)) as MouseNav;
        if (!leftPanel) leftPanel = GameObject.Find("localPlayer").GetComponent(typeof(LeftPanel)) as LeftPanel;
    }
}
