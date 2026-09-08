using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

//会話文を表示するためのコード

public class MessageScript : MonoBehaviour
{
    public TMP_Text messageText; //セリフを表示するテキストメッシュ
    public TMP_Text characterNameText; //キャラクター名を表示するテキストメッシュ
    public Image messageWindow; //キャラクター名を表示するテキストメッシュのオブジェクト
    public GameObject messageUI; //メッセージウィンドウやテキストメッシュなど全部入ってるキャンバス

    public GameObject[] hideUI; //MessageUI表示中には表示しないUIがあればInspectorで指定してください
    private AudioSource audioSource;

    public GameObject[] tachieObject; //立ち絵を表示するImageObject

    private GameObject[,] tachie; //立ち絵を表示するImageObject
    private Image[,] tachieImage; //立ち絵を表示するImageObjectのImageコンポーネント
    private bool moveFlag; //立ち絵を動かすかどうかのフラグ

    private Sprite[,] tachieSpriteArray; //立ち絵の画像を格納する配列
    private string[] faceArray = { "Normal", "Smile", "Surprised", "Thinking", "Damaged" }; //ファイル名と連動させて変更可
    private string[] characterArrayE = { "Doctor1", "Doctor2", "Doctor3", "Witch" }; //ファイル名と連動させて変更可
    private string[] characterArrayJ = { "チョイス", "ルック", "コロン", "リチュ", "全員", "効果音" }; //表示用名前変更可
    public AudioClip[] voiceArray; //喋り声の音声ファイル
    public AudioClip bombAudio; //喋り声の音声ファイル
    public GameObject BombObject; //爆発エフェクトのオブジェクト

    private float fadeDuration = 0.5f; //フェードアウトの時間
    private float fadeBlack = 0.5f; //透明度

    private int[] before;
    private bool beforeMajoTrue;
    private string beforeCharacterName;
    
    private Coroutine wiggleCoroutine;
    private GameObject wiggleObject;
    private Vector3 wiggleStartPos;
    private Vector3 wiggleStartRot;

    Animator animator; //アニメーションを制御するAnimatorコンポーネント

    //魔女右、左博士三最前表示 魔女なしは博士三人表示
    //番号、キャラ名、内容、表情、秒数、立ち位置、アニメーション
    //spriteはtextureTypeをspriteに、spriteModeをsingleに
    //魔女の魔はフォントにありません

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        voiceArray = new AudioClip[4] { Resources.Load<AudioClip>("Voice1"), Resources.Load<AudioClip>("Voice2"), Resources.Load<AudioClip>("Voice3"), Resources.Load<AudioClip>("Voice4") };
        animator = BombObject.GetComponent<Animator>();
        // Debug.Log(HasBoolParam(animator, "BombFlag") ? "BombFlag parameter exists." : "BombFlag parameter does not exist.");
        tachie = new GameObject[2,4] { { GameObject.Find("LeftLeftCharacter"), GameObject.Find("CenterLeftCharacter"), GameObject.Find("RightLeftCharacter"), GameObject.Find("Witch") }, { GameObject.Find("LeftCharacter"), GameObject.Find("CenterCharacter"), GameObject.Find("RightCharacter"), null } };
        tachieImage = new Image[2,4] { { tachie[0,0].GetComponent<Image>(), tachie[0,1].GetComponent<Image>(), tachie[0,2].GetComponent<Image>(), tachie[0,3].GetComponent<Image>() }, { tachie[1,0].GetComponent<Image>(), tachie[1,1].GetComponent<Image>(), tachie[1,2].GetComponent<Image>(), null } };
        tachieSpriteArray = new Sprite[4,5];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                string path = characterArrayE[i] + "/" + faceArray[j];
                if (Resources.Load<Sprite>(path) != null)
                {
                    tachieSpriteArray[i, j] = Resources.Load<Sprite>(path);
                }
            }
        }

        before = new int[4] { 0, 0, 0, 0 };

        messageUI.SetActive(false);

        // StartCoroutine(TestText());
    }

    private bool HasBoolParam(Animator a, string paramName)
{
    foreach (var p in a.parameters)
    {
        if (p.type == AnimatorControllerParameterType.Bool && p.name == paramName)
        {
            return true;
        }
    }
    return false;
}

    private void StartWiggle(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        StopWiggle();
        wiggleObject = obj;
        wiggleStartPos = obj.transform.position;
        wiggleStartRot = obj.transform.rotation.eulerAngles;
        moveFlag = true;
        wiggleCoroutine = StartCoroutine(Wiggle(obj, wiggleStartPos, wiggleStartRot));
    }

    private void StopWiggle()
    {
        moveFlag = false;

        if (wiggleCoroutine != null)
        {
            StopCoroutine(wiggleCoroutine);
            wiggleCoroutine = null;
        }

        if (wiggleObject != null)
        {
            wiggleObject.transform.position = wiggleStartPos;
            wiggleObject.transform.rotation = Quaternion.Euler(wiggleStartRot);
            wiggleObject = null;
        }
    }

    private IEnumerator Wiggle(GameObject obj, Vector3 startPos, Vector3 startRot)
    {
        float wiggleAmount = 0.5f; // 揺れの大きさ
        float wiggleSpeed = 5f; // 揺れの速さ

        while (moveFlag)
        {
            float offsetX = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
            float offsetY = Mathf.Cos(Time.time * wiggleSpeed) * wiggleAmount;
            float offsetRot = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount * 10f; // 回転の揺れ

            obj.transform.position = startPos + new Vector3(offsetX, offsetY, 0);
            obj.transform.rotation = Quaternion.Euler(startRot + new Vector3(0, 0, offsetRot));

            yield return null;
        }

        obj.transform.position = startPos; // 元の位置に戻す
        obj.transform.rotation = Quaternion.Euler(startRot); // 元の回転に戻す
        yield return null;
    }

    public IEnumerator TestText()
    {
        yield return StartCoroutine(SendMessage("博士1", "テストテストテストテストテストテストテストテスト", "Normal", 1, true, null, 0));
        yield return StartCoroutine(SendMessage("博士2", "2テストテストテストテストテストテストテストテスト", "Smile", 1, true, null, 1));
        yield return StartCoroutine(SendMessage("博士3", "3テストテストテストテストテストテストテストテスト", "Thinking", 1, true, null, 2));
    }

    //FaceNum("表情名");の形で表情を指定のintで返します 
    private int FaceNum(string face)
    {
        int faceNum = 0;
        for(int i = 0; i < faceArray.Length; i++)
        {
            if(face == faceArray[i])
            {
                faceNum = i;
                break;
            }
        }
        return faceNum;
    }

    //Character("キャラ名");の形でキャラクターを指定のintで返します
    private int CharacterNum(string character)
    {
        int characterNum = 0;
        for(int i = 0; i < characterArrayJ.Length; i++)
        {
            if(character == characterArrayJ[i])
            {
                characterNum = i;
                break;
            }
        }
        return characterNum;
    }

    private Sprite TachieSpriteSelector(string character, string face)
    {
        int characterNum = CharacterNum(character);
        int faceNum = FaceNum(face);
        return tachieSpriteArray[characterNum, faceNum];
    }

    private GameObject TachieSelector(string character, bool MajoTrue)
    {
        int characterNum = CharacterNum(character);
        if(MajoTrue)
        {
            return tachie[0, characterNum];
        }
        return tachie[1, characterNum];
    }

    private Image TachieImageSelector(string character, bool MajoTrue)
    {
        int characterNum = CharacterNum(character);
        if(MajoTrue)
        {
            return tachieImage[0, characterNum];
        }
        return tachieImage[1, characterNum];
    }

    private void FrontTachie(GameObject[] objects, GameObject changeObject)
    {
        if (objects == null || changeObject == null)
        {
            return;
        }

        // まず対象以外を一番後ろにし、最後に changeObject を一番前にする
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null && objects[i] != changeObject)
            {
                objects[i].transform.SetAsFirstSibling();
            }
        }

        if (changeObject != null)
        {
            changeObject.transform.SetAsLastSibling();
        }

    }
    
    //メッセージを表示する間隔を指定するコルーチン
    private IEnumerator WaitMessage(float time)
    {
        yield return new WaitForSeconds(time);
    }

    //メッセージウィンドウの開閉
    private void OpenMessage()
    {
        messageUI.SetActive(true);
        for(int i = 0; i < hideUI.Length; i++)
        {
            hideUI[i].SetActive(false);
        }
    }

    public void CloseMessage()
    {
        messageUI.SetActive(false);
        for(int i = 0; i < hideUI.Length; i++)
        {
            hideUI[i].SetActive(true);
        }
    }

    //メッセージ表示
    public IEnumerator SendMessage(string characterName, string sentence, string characterFace, float second, bool majoTrue, string animationName, int messageIndex)
    {
        // Debug.Log("MessageScript: Sending message. Character: " + characterName + ", Sentence: " + sentence + ", Face: " + characterFace + ", Duration: " + second + ", MajoTrue: " + majoTrue + ", Animation: " + animationName + ", MessageIndex: " + messageIndex);
        OpenMessage();
        messageText.maxVisibleCharacters = 0;
        int Length = sentence.Length + 1;
        messageText.text = sentence;
        characterNameText.text = characterName;
        if(characterName == "効果音")
        {
            characterNameText.text = "";
        }

        bool FaceChanged = false;
        int number;
        int anotherNumber;
        Image[] nowImage;
        Image[] anotherImage;

        int characterNum = CharacterNum(characterName);
        int faceNum = FaceNum(characterFace);

        Image changeImage = null;
        GameObject changeObject = null;

        if(characterNum < 4)
        {
            changeImage = TachieImageSelector(characterName, majoTrue);
            changeObject = TachieSelector(characterName, majoTrue);
        }

        Image beforeImage = TachieImageSelector(beforeCharacterName, beforeMajoTrue);
        GameObject[] nowImageObject;

        if(characterNum < 4)
        {
            if(faceNum != before[characterNum]) //表情が前回と異なる場合、フラグを立てる
            {
                FaceChanged = true;
            }
            else
            {
                FaceChanged = false;
            }
        }

        if (majoTrue) //今動かす立ち絵が四人[0]か三人[1]かを決める
        {
            number = 0;
            anotherNumber = 1;
            nowImage = new Image[] { tachieImage[number,0], tachieImage[number,1], tachieImage[number,2], tachieImage[number,3] };
            nowImageObject = new GameObject[] { tachie[number,0], tachie[number,1], tachie[number,2], tachie[number,3] };
            anotherImage = new Image[] { tachieImage[anotherNumber,0], tachieImage[anotherNumber,1], tachieImage[anotherNumber,2] };
        }
        else
        {
            number = 1;
            anotherNumber = 0;
            nowImage = new Image[] { tachieImage[number,0], tachieImage[number,1], tachieImage[number,2] };
            nowImageObject = new GameObject[] { tachie[number,0], tachie[number,1], tachie[number,2] };
            anotherImage = new Image[] { tachieImage[anotherNumber,0], tachieImage[anotherNumber,1], tachieImage[anotherNumber,2], tachieImage[anotherNumber,3] };
        }

        if(messageIndex == 0)
        {
            beforeMajoTrue = !majoTrue; //最初の文章の時は前回の魔女の有無を逆に
            for(int i = 0; i < nowImage.Length; i++)
            {
                nowImage[i].color = new Color(1f, 1f, 1f, 0f);
            }
            for(int i = 0; i < anotherImage.Length; i++)
            {
                anotherImage[i].color = new Color(1f, 1f, 1f, 0f);
            }
            messageWindow.color = new Color(1f, 1f, 1f, 0f);
            characterNameText.color = new Color(1f, 1f, 1f, 0f);
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0f);
            yield return StartCoroutine(FadeIn(new Graphic[] { messageWindow, characterNameText, messageText }, -1));
        }

        if(beforeMajoTrue != majoTrue) //魔女の有無が前回と変わった場合
        {
            if(messageIndex != 0) //最初の文章の時はフェードアウトしない
            {
                //全員フェードアウト
                yield return StartCoroutine(FadeOut(anotherImage));
                StopWiggle();
            }
            
            tachieObject[anotherNumber].SetActive(false);
            tachieObject[number].SetActive(true);

            before[characterNum] = faceNum; //表情を更新

            for(int i = 0; i < nowImage.Length; i++)//表情を引き継ぐ
            {
                tachieImage[number, i].sprite = tachieSpriteArray[i, before[i]];
            }

            FrontTachie(nowImageObject, changeObject);
            StartWiggle(changeObject);

            //全員フェードイン
            yield return StartCoroutine(FadeIn(nowImage, characterNum));
        }
        else if(characterNum < 4) //配置は変わらない場合
        {
            // Debug.Log("MessageScript: No change in MajoTrue. FaceChanged: " + FaceChanged + ", changeImage == beforeImage: " + changeImage + beforeImage);
            switch(FaceChanged, changeImage != beforeImage) //表情が前回と異なるか、キャラクターが前回と異なるか
            {
                case (true, true):
                    // yield return StartCoroutine(FadeOut(new Graphic[] { changeImage }));
                    StopWiggle();
                    FrontTachie(nowImageObject, changeObject);
                    StartWiggle(changeObject);
                    changeImage.sprite = TachieSpriteSelector(characterName, characterFace);
                    yield return StartCoroutine(FadeInOne(changeImage, beforeImage));
                    break;

                case (true, false):
                    changeImage.sprite = TachieSpriteSelector(characterName, characterFace);
                    break;

                case (false, true):
                    StopWiggle();
                    FrontTachie(nowImageObject, changeObject);
                    StartWiggle(changeObject);
                    yield return StartCoroutine(FadeInOne(changeImage, beforeImage));
                    break;

                case (false, false):
                    break;
            }
        }
        else if(characterName == "全員")
        {
            StopWiggle();
            FrontTachie(nowImageObject, changeObject);
            StartWiggle(changeObject);
            for(int i = 0; i < 4; i++)//表情を引き継ぐ
            {
                tachieImage[number, i].sprite = tachieSpriteArray[i, 1];
            }
            yield return StartCoroutine(AllFadeIn(nowImage));
        }
        else if(characterName == "効果音")
        {
            StopWiggle();
            yield return StartCoroutine(FadeInOne(null, beforeImage));
    //         Debug.Log("AnimatorObj=" + animator.gameObject.name
    // + " active=" + animator.gameObject.activeInHierarchy
    // + " enabled=" + animator.enabled
    // + " initialized=" + animator.isInitialized
    // + " controller=" + (animator.runtimeAnimatorController == null ? "NULL" : animator.runtimeAnimatorController.name));
            animator.SetTrigger("BombTrigger");
            audioSource.PlayOneShot(bombAudio);

        }

        //セリフの文字を一文字ずつ表示し、音を鳴らす
        for (int i = 0; i < Length; i++)
        {
            messageText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.05f);
            if(characterNum < 4)
            {
                audioSource.PlayOneShot(voiceArray[characterNum]);
            }
            else if(characterName == "全員")
            {
                audioSource.PlayOneShot(voiceArray[0]);
                audioSource.PlayOneShot(voiceArray[1]);
                audioSource.PlayOneShot(voiceArray[2]);
                audioSource.PlayOneShot(voiceArray[3]);
            }
        }
        yield return WaitMessage(second);

        if(characterNum < 4)
        {
            before[characterNum] = faceNum; //前回の表情を更新
            beforeCharacterName = characterName; //前回のキャラクター名を更新
        }
        beforeMajoTrue = majoTrue; //前回の魔女の有無

        if(messageIndex == 2)
        {
            yield return StartCoroutine(FadeOut(nowImage));
            StopWiggle();
            tachieObject[number].SetActive(false);
            yield return StartCoroutine(FadeOut(new Graphic[] { messageWindow, characterNameText, messageText}));

            for(int i = 0; i < before.Length; i++)
            {
                before[i] = 0;
            }

            CloseMessage();
        }
    }
    private IEnumerator FadeOut(Graphic[] img)
    {
        Color[] color = new Color[img.Length];
        for (int i = 0; i < img.Length; i++)
        {
            color[i] = new Color(img[i].color.r, img[i].color.g, img[i].color.b, img[i].color.a);
        }
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            for (int i = 0; i < img.Length; i++)
            {
                img[i].color = new Color(img[i].color.r, img[i].color.g, img[i].color.b, color[i].a * alpha);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeIn(Graphic[] img, int nowCharacterNum) //全員フェードイン
    {
        float elapsedTime = 0f;

        if(nowCharacterNum < 0)
        {
            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                for (int i = 0; i < img.Length; i++)
                {
                    img[i].color = new Color(img[i].color.r, img[i].color.g, img[i].color.b, alpha);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            for (int i = 0; i < img.Length; i++)
            {
                if(i == nowCharacterNum)
                {
                    img[i].color = new Color(1f , 1f, 1f, alpha);
                }
                else
                {
                    img[i].color = new Color(fadeBlack * alpha, fadeBlack * alpha, fadeBlack * alpha, alpha);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInOne(Image nowimg, Image beforeimg) //人数変更なし、表情変更
    {

        Color nowColor = Color.clear;
        if(nowimg != null)
        {
            nowColor = new Color(nowimg.color.r, nowimg.color.g, nowimg.color.b, nowimg.color.a);
        }
        Color beforeColor = new Color(beforeimg.color.r, beforeimg.color.g, beforeimg.color.b, beforeimg.color.a);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            if(nowimg != null)
            {
                nowimg.color = new Color(nowColor.r + (1f - nowColor.r) * alpha, nowColor.g + (1f - nowColor.g) * alpha, nowColor.b + (1f - nowColor.b) * alpha, nowColor.a + (1f - nowColor.a) * alpha);
            }
            if(nowimg != beforeimg)
            {
                beforeimg.color = new Color(beforeColor.r - (1f - fadeBlack) * alpha, beforeColor.g - (1f - fadeBlack) * alpha, beforeColor.b - (1f - fadeBlack) * alpha, beforeColor.a);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator AllFadeIn(Graphic[] img)
    {
        float elapsedTime = 0f;
        Color[] color = new Color[img.Length];
        for(int i = 0; i < img.Length; i++)
        {
            color[i] = new Color(img[i].color.r, img[i].color.g, img[i].color.b, img[i].color.a);
        }
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            for (int i = 0; i < img.Length; i++)
            {
                img[i].color = new Color(color[i].r + (1f - color[i].r) * alpha, color[i].g + (1f - color[i].g) * alpha, color[i].b + (1f - color[i].b) * alpha, color[i].a + (1f - color[i].a) * alpha);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
