using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public UIManager uiManager;
    public Animator FadeAnimator;
    private SerialReceiver01 serialReceiver01;
    private SerialReceiver02 serialReceiver02;
    private SerialReceiver03 serialReceiver03;
    [SerializeField]
    private GameObject Player01;
    [SerializeField]
    private GameObject Player02;
    private GameObject Player03;
    public UIPointerController pointerController01;
    public UIPointerController02 pointerController02;
    public UIPointerController03 pointerController03;
    
    private void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        GManager.instance.ActivePlayersComp();
        GManager.instance.SetParameter();//Gmanegerで選んだキャラクターに応じたパラメーターをmuzzleに設定
    }
    
    void Start()
    {
        GManager.instance.ActivePlayersComp();
        GManager.instance.SetParameter();//Gmanegerで選んだキャラクターに応じたパラメーターをmuzzleに設定
        serialReceiver01 =  GameObject.Find("SenserReceiver01").GetComponent<SerialReceiver01>();
        serialReceiver02 = GameObject.Find("SenserReceiver02").GetComponent<SerialReceiver02>();
        serialReceiver03 = GameObject.Find("SenserReceiver03").GetComponent<SerialReceiver03>();
        StartGame();
        serialReceiver01.GetComponent<SerialReceiver01>().ChangeMuzzle01();
        serialReceiver02.GetComponent<SerialReceiver02>().ChangeMuzzle02();
        serialReceiver03.GetComponent<SerialReceiver03>().ChangeMuzzle03();
        
        pointerController01.StartObject = GameObject.Find("Player01");
        pointerController02.StartObject = GameObject.Find("Player02");
        pointerController03.StartObject = GameObject.Find("Player03");

       
        
    }

    void Update()
    {
        
    }
    public void StartGame() {
        uiManager.ViewScore();
        uiManager.PlayFadeIn();
    }


}
