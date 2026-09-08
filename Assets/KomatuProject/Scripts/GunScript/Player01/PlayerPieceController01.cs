using UnityEngine;

public class PlayerPieceController01 : MonoBehaviour
{
    public static PlayerPieceController01 Instance; //static型にして、シーン上に１つしか存在しないように
    private BoxCollider boxCollider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;// PlayerPieceController.Instanceでいつでも参照できるように
        }
        else {
            Invoke(nameof(DestroyInstance), 1.0f);
            
        }
    }
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (boxCollider.isTrigger == true) {

            if (other.CompareTag("PlayerSelectPanel") ) {
                if (other.gameObject.name == "Panel_Doctor01") GManager.instance.Player01GunType = "GunType01";

                else if (other.gameObject.name == "Panel_Doctor02") GManager.instance.Player01GunType = "GunType02";

                else if (other.gameObject.name == "Panel_Doctor03") GManager.instance.Player01GunType = "GunType03";
            }

            if (other.CompareTag("PlayerSelectPanel"))
            {
                boxCollider.isTrigger = false;
            }
        }
    }
    private void DestroyInstance() {
        Destroy(Instance.gameObject);
        Instance = this;
    }
}
