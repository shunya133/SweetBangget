using UnityEngine;

public class TitleCameramanager : MonoBehaviour
{
    private GameObject titleCamera;
    private Animator anim;
    void Start()
    {
        this.titleCamera = GameObject.Find("Main Camera");
        this.anim = titleCamera.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimateCameraAnim() { //NƒL[‚ğ‰Ÿ‚µ‚½ÛAGmanager‚©‚çŠÖ”‚ªŒÄ‚Î‚ê‚é
        anim.SetBool("b_CameraMove", true);
    }
}
