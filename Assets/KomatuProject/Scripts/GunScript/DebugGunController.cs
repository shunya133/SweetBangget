using UnityEngine;
using UnityEngine.InputSystem;
public class DebugGunController : MonoBehaviour
{
    public float speed = 40.0f;
    private InputAction rotationAction;
    private Vector2 moveinput;
    private Vector2 movezInput;
    
    private GunController01 GunController01;
    private GunController02 GunController02;
    [SerializeField]
    private int selectedPlayer = 0;
    public Material defaultMaterial;
    public Material hilightMaterial;
    
    void Start()
    {
        rotationAction = InputSystem.actions.FindAction("Rotate");
        //GunController01 = Player01.GetComponent<GunController01>();
        //GunController02 = Player02.GetComponent<GunController02>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedPlayer = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedPlayer = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedPlayer = 3;
        //キーボードの入力番号に応じて回転とマテリアルの変更処理を行う
        if (selectedPlayer == 1)
        {
            if (this.gameObject.name == "Player01")
            {
                RotateObject();
                GetComponent<Renderer>().material = hilightMaterial;
                //Debug.Log("Changematerial");
            }
            else if (this.gameObject.name == "TitlePlayer01")
            {
                TitleRotateObject();
                GetComponent<Renderer>().material = hilightMaterial;

            }
            else if (selectedPlayer == 1)
                GetComponent<Renderer>().material = defaultMaterial;
        }

        if (selectedPlayer == 2)
        {
            if (this.gameObject.name == "Player02")
            {
                RotateObject();
                GetComponent<Renderer>().material = hilightMaterial;
            }
            else if (this.gameObject.name == "TitlePlayer02")
            {
                TitleRotateObject();
                GetComponent<Renderer>().material = hilightMaterial;
            }
            else GetComponent<Renderer>().material = defaultMaterial;
        }


        if (selectedPlayer == 3)
        {
            if (this.gameObject.name == "Player03")
            {
                RotateObject();
                GetComponent<Renderer>().material = hilightMaterial;
            }
            else if (this.gameObject.name == "TitlePlayer03")
            {
                TitleRotateObject();
                GetComponent<Renderer>().material = hilightMaterial;
            }
            else GetComponent<Renderer>().material = defaultMaterial;


        }
    }

    public void RotateObject() {
        Vector3 move = new Vector3(-moveinput.y, moveinput.x, 0);
        this.transform.Rotate(move * speed * Time.deltaTime);
        Vector3 rot = transform.localEulerAngles;
        rot.z = 0;
        this.transform.localEulerAngles = rot;
    }
    //Title時の銃の回転操作
    public void TitleRotateObject() {
        Vector3 titlemove = new Vector3(-moveinput.y, 0, moveinput.x);
        this.transform.Rotate(titlemove * speed/2 * Time.deltaTime);
        Vector3 titlerot = transform.eulerAngles;
        titlerot.y = 0;
        this.transform.eulerAngles = titlerot;
    
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveinput = context.ReadValue<Vector2>();
    }
    
}
