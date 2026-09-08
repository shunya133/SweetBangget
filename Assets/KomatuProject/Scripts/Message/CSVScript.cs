using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

//使い方 Coroutineを使うので困ったらご相談ください
//using System.Collections;
// public CSVScript csvScript;
//public CSVScriptObj;
//csvScript = GameObject.Find("CSVScript").GetComponent<CSVScript>();
//yield return StartCoroutine(csvScript.StoryPlay(0)); 一個目の会話
//yield return StartCoroutine(csvScript.StoryPlay(1)); 二個目の会話
//yield return StartCoroutine(csvScript.StoryPlay(2)); 三個目の会話

public class CSVScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MessageScript messageScript;
    private int[] topicNo; //どのシーンの会話か
    private string[] characterName; //キャラクター名
    private string[] sentence; //セリフ
    private string[] characterFace; //キャラクターの表情
    private float[] second; //セリフの表示時間
    private bool[] majoTrue; //魔女がいるかどうか
    private string[] animationName; //どのシーンの会話か

    public TextAsset csvFile;
    List<string[]> csvList = new List<string[]>();
    private int height;

    //話題数-1の数だけ増やす
    private int topic1;
    private int topic2;

    private int[] topicLoadNumber;
    private int[] topicLastNumber;

    void Awake()
    {
        if (messageScript == null)
        {
            // Debug.LogError("CSVScript: MessageScript component was not found on the MessageScript object.");
            enabled = false;
            return;
        }

        // csvFile = Resources.Load<TextAsset>("Data/StoryData");
        if (csvFile == null)
        {
            // Debug.LogError($"CSVScript: TextAsset 'Data/StoryData' was not found in a Resources folder.");
            //今はcsvデータがないのでエラーが出ます
            enabled = false;
            return;
        }

        StringReader reader = new StringReader(csvFile.text);

        csvList.Clear();
        height = 0;
        
        //CSVの長さを計算
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvList.Add(line.Split(','));
            height++;
        }
        int dataLen = height - 1;

        //CSVの長さだけ配列を作りデータを格納
        topicNo = new int[dataLen];
        characterName = new string[dataLen];
        sentence = new string[dataLen];
        characterFace = new string[dataLen];
        second = new float[dataLen];
        majoTrue = new bool[dataLen];
        animationName = new string[dataLen];

        for (int i = 0; i < dataLen; i++)
        {
            topicNo[i] = int.Parse(csvList[i + 1][0]);
            characterName[i] = csvList[i + 1][1];
            sentence[i] = csvList[i + 1][2];
            characterFace[i] = csvList[i + 1][3];
            second[i] = float.Parse(csvList[i + 1][4]);
            majoTrue[i] = bool.Parse(csvList[i + 1][5]);
            animationName[i] = csvList[i + 1][6];

            if (topicNo[i] == 0)
            {
                topic1++;
            }
            
            if (topicNo[i] == 0 || topicNo[i] == 1)
            {
                topic2++;
            }
        }

        topicLoadNumber = new int[3] { 0, topic1, topic2 };
        topicLastNumber = new int[3] { topic1 - 1, topic2 - 1, height - 2 };
    }

    void Start()
    {
       // StartCoroutine(Test());
    }


    private IEnumerator Test()
    {
        yield return StartCoroutine(StoryPlay(2));
        // yield return new WaitForSeconds(1f); // 1秒待機
        // yield return StartCoroutine(StoryPlay(1));
        // yield return new WaitForSeconds(1f); // 1秒待機
        // yield return StartCoroutine(StoryPlay(2));
    }

    public IEnumerator StoryPlay(int topicNumber)
    {
        int topicIndex = topicLoadNumber[topicNumber];
        int lastIndex = topicLastNumber[topicNumber];

        for (int i = topicIndex; i <= lastIndex; i++)
        {
            if(i==lastIndex) //最後の文章
            {
                yield return StartCoroutine(messageScript.SendMessage(characterName[i], sentence[i], characterFace[i], second[i], majoTrue[i], animationName[i], 2));
                break;
            }
            if(i==topicIndex) //最初の文章
            {
                yield return StartCoroutine(messageScript.SendMessage(characterName[i], sentence[i], characterFace[i], second[i], majoTrue[i], animationName[i], 0));
                continue;
            }
            yield return StartCoroutine(messageScript.SendMessage(characterName[i], sentence[i], characterFace[i], second[i], majoTrue[i], animationName[i], 1));
            // Debug.Log("CSVScript: Sent message " + i + " to MessageScript.");
        }
        yield return null;
    }

}
