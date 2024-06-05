using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CycleController : MonoBehaviour
{
    private static CycleController instance;

    private int index;

    [SerializeField]
    public float cycleLenght = 30.0f;

    bool Key;
    private List<int> collectedInfo = new List<int>();
    private List<int> lastCollectedInfo = new List<int>();

    //private List<string> allInfo;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        index = 0;
        //allInfo = new List<string>();
        //collectedInfo = new List<int>();
        //string infoPath = Application.dataPath + "/resources/InfoCollection.txt";
        //var stream = new StreamReader(infoPath);
        //while (!stream.EndOfStream)
        //{
        //    allInfo.Add(stream.ReadLine());
        //}

        DontDestroyOnLoad(this);

    }

    public void NewCycle()
    {
        lastCollectedInfo = new List<int>();
        Debug.Log("RC before put in lastCI" + lastCollectedInfo.Count);
        for (int i = 0; i < collectedInfo.Count; i++)
        {
            lastCollectedInfo.Add(collectedInfo[i]);
        }
        Debug.Log("RC lastCI" + lastCollectedInfo.Count);
        Debug.Log("RC CI" + collectedInfo.Count);

        collectedInfo.Clear();
        Key = false;
        index++;

        Debug.Log("RC CI after Clear" + collectedInfo.Count);

        SceneManager.LoadScene("MainScenev2");

        Debug.Log("RC lastCI" + lastCollectedInfo.Count);
    }

    public void KeyFound()
    {
        Key = true;
    }

    public bool isKeyFound()
    {
        return Key;
    }

    public void infoFound(int index)
    {
        collectedInfo.Add(index);
    }

    public List<int> getLastInfo()
    {
        return lastCollectedInfo;
    }

    public int getIndex()
    {
        return index;
    }

    public List<int> getInfo()
    {
        return collectedInfo;
    }

}
