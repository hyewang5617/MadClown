using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
    	//id = 1000 : Talia
        talkData.Add(1000, new string[]{"티라미수 케잌 내놔!!!!!!","빨리 내놓으라고!!!"});
        //id = 100 : 탈리아가 갇혀있는 Prision
        talkData.Add(100,new string[]{"동색 열쇠이다."});
        //id = 200 : 다음 스테이지로 넘어갈 수 있는 문 
        talkData.Add(200,new string[]{"은색 열쇠이다."});
        talkData.Add(300,new string[]{"금색 열쇠이다."});
        talkData.Add(400,new string[]{"무언가를 부술 수 있는 망치이다."});
        talkData.Add(500,new string[]{"열쇠로 잠긴 문이다."});
        talkData.Add(600,new string[]{"낡은 나무판자로 덮혀진 문이다."});
    }

    public string GetTalk(int id, int talkIndex) //Object의 id , string배열의 index
    {
        return talkData[id][talkIndex]; //해당 아이디의 해당
    }
}
