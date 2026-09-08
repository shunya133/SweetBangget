using UnityEngine;

public class PlayerPieceController03 : MonoBehaviour
{
    public static PlayerPieceController03 Instance; //static型にして、シーン上に１つしか存在しないように
    private BoxCollider boxCollider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;// PlayerPieceController.Instanceでいつでも参照できるように
        }
        else
        {
            Invoke(nameof(DestroyInstance), 1.0f);

        }
    }
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    
    public void OnTriggerEnter(Collider other)
    {
        if (boxCollider.isTrigger == true)
        {
            if (other.CompareTag("PlayerSelectPanel"))
            {
                if (other.gameObject.name == "Panel_Doctor01") GManager.instance.Player03GunType = "GunType01";

                else if (other.gameObject.name == "Panel_Doctor02") GManager.instance.Player03GunType = "GunType02";

                else if (other.gameObject.name == "Panel_Doctor03") GManager.instance.Player03GunType = "GunType03";
            }

            if (other.CompareTag("PlayerSelectPanel"))
            {
                boxCollider.isTrigger = false;
            }
        }
    }
    private void DestroyInstance()
    {
        Destroy(Instance.gameObject);
        Instance = this;
    }
}
