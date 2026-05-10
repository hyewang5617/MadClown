using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TMP_Text talkText;
    public GameObject scanObject;
    public bool isAction;
    /*public TalkManager talkManager;
    public int talkIndex;*/
 
 
    public void Action(GameObject scanObj)
    {
        if(isAction) // 실행중 아닐때 ->대화창 없애기 
        {
            isAction=false;

        }
        else //실행중 -> 대화창 띄우기 
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "TIRAMISU CAKE~~~~ ";
            //스캔한 오브젝트의 id와 isNPC정보를 가져와야 하기 때문에 objData script가 필요
            //ObjData objData = scanObject.GetComponent<ObjData>();
            //objData의 id와 NPC인지 정보를 매개변수로 넘김 
            //Talk(objData.id,objData.isNPC);
        }

        talkPanel.SetActive(isAction); //대화창 활성화 상태에 따라 대화창 활성화 변경*/
    }
    
    /*//실제 대사들을 UI에 출력하는 함수     
    void Talk(int id, bool isNPC){

		//talkManager에 생성한 대사를 반환하는 함수를 이용하여 대사 한줄을 입력받음 
        string talkData = talkManager.GetTalk(id, talkIndex);

		//입력받은 대사를 이용하여 출력 
        if(isNPC){ 
            UITalkText.text = talkData;
        }else{
            UITalkText.text = talkData;
        }
    }*/
}
