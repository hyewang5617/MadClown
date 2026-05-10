using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class ClownMoveController : MonoBehaviour
{
    public GameObject player;
    
    private float dX, dY; // 목적지의 x와 y좌표
    private int dNum = 0; // 목적지의 번호
    public int max; // 악당이 갈수있는 최대 선택지
    public int min;

    public float maxPositionX;
    public float minPositionX;
    
    public float nearStairX; // 현재 위치에서 가장 가까운 계단위치

    private int pastDirection; // 바로 직전 갔던 목적지
    public  int findDCount;
    public bool findPlayerOn;

    public bool followPlayerOn;
    public bool gotoPlayer;
    public bool isLadderOn;

    public float speed;

    public float firstFloor;
    public float secondFloor;
    public float underFloor;

    public float underLadder;
    public float firstLadderLeft;
    public float firstLadderRight;
    
    void Awake()
    {
        findDCount = 1;
        pastDirection = 1;
        findPlayerOn = false;
        followPlayerOn = false;
        gotoPlayer = false;
        isLadderOn = false;
        StartCoroutine("findDirecton");
    }

    public void StartFindDirection()
    {
        StartCoroutine("findDirecton");
    }
    
    void Update()
    {
        if (followPlayerOn)
        {
            StopCoroutine(findDirecton());
            followPlayerOn = false;
            gotoPlayer = true;
        }
        
    }
    private void FixedUpdate()
    {
        if (gotoPlayer)
        {
            Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    IEnumerator findDirecton()
    {
        if (findPlayerOn)
        {
            dNum = 100;
        }
        else
        {
            do {
                dNum = Random.Range(min, max+1);
                //dNum = 4; // 목적지
            } while (dNum == pastDirection);
        }
        

        pastDirection = dNum;
        
        Debug.Log("목적지는 " + dNum);
        
        switch (dNum)
        {
            case 1:
                dX = -13.76f;
                dY = secondFloor; 
                // 2층방 옆
                break;
            case 2:
                dX = -43.02f;
                dY = underFloor;
                // 지하실
                break;
            case 3:
                dX = -43.21f;
                dY = firstFloor;
                // 1층 왼쪽 방안
                break;
            case 4:
                dX = -2.66f;
                dY = firstFloor;
                // 맵의 전체 중앙
                break;
            case 5:
                dX = -25.52f;
                dY = firstFloor;
                break;
            case 6:
                dX = -24.88f;
                dY = secondFloor;
                break;
            case 7:
                dX = 0f;
                dY = secondFloor;
                break;
            case 8:
                dX = 18.96f;
                dY = secondFloor;
                break;
            case 9:
                dX = 19.45f;
                dY = firstFloor;
                break;
            default:
                dX = player.transform.position.x;
                
                if (player.transform.position.y < -5)
                {
                    dY = underFloor;
                } 
                else if (player.transform.position.y < 3)
                {
                    dY = firstFloor;
                }
                else
                {
                    dY = secondFloor;
                }
                break;
        }
        Vector2 target = new Vector2(dX, dY); // 2층방 옆
        
        if (gotoPlayer)
            yield break;
        
        while(!(transform.position.y.Equals(dY))) // 올라가야 하는 상황
        {
            Vector2 ladderPosition;
            Vector2 ladderUpDownPosition;
            if (transform.position.y < dY)
            {
                Debug.Log("층수가 다릅니다.");
                if (transform.position.y.Equals(underFloor)) // 지하
                {
                    ladderPosition = new Vector2(underLadder, underFloor);
                    while (transform.position.x != underLadder)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, ladderPosition, speed * Time.deltaTime);
                        yield return new WaitForSecondsRealtime(Time.deltaTime);
                        if (gotoPlayer)
                            yield break;
                    }
                    Debug.Log("사다리에 도착했습니다.");
                    ladderUpDownPosition = new Vector2(underLadder, firstFloor);
                    isLadderOn = true;
                    while (transform.position.y != firstFloor)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, ladderUpDownPosition, speed * Time.deltaTime);
                        yield return new WaitForSecondsRealtime(Time.deltaTime);
                        if (gotoPlayer)
                            yield break;
                    }
                    isLadderOn = false;
                    Debug.Log("1층에 도착했습니다.");
                }
                else // 1층
                {
                    if (transform.position.x <= (firstLadderLeft + firstLadderRight) / 2) // 1층 왼쪽 사다리타기
                    {
                        ladderPosition = new Vector2(firstLadderLeft, firstFloor);
                        while (transform.position.x != firstLadderLeft)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        Debug.Log("사다리에 도착했습니다.");
                        isLadderOn = true;
                        ladderUpDownPosition = new Vector2(firstLadderLeft, secondFloor);
                        while (transform.position.y != secondFloor)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderUpDownPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        isLadderOn = false;
                        Debug.Log("2층에 도착했습니다.");
                    }
                    else // 1층 오른쪽 사다리타기
                    {
                        ladderPosition = new Vector2(firstLadderRight, firstFloor);
                        while (transform.position.x != firstLadderRight)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        Debug.Log("사다리에 도착했습니다.");
                        isLadderOn = true;
                        ladderUpDownPosition = new Vector2(firstLadderRight, secondFloor);
                        while (transform.position.y != secondFloor)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderUpDownPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        isLadderOn = false;
                        Debug.Log("2층에 도착했습니다.");
                    }
                }
            }
            else if (transform.position.y > dY) // 내려가야 하는 상황
            {
                Debug.Log("층수가 다릅니다.");
                if (transform.position.y.Equals(firstFloor)) // 1층
                {
                    ladderPosition = new Vector2(underLadder, firstFloor);
                    while (transform.position.x != underLadder)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, ladderPosition, speed * Time.deltaTime);
                        yield return new WaitForSecondsRealtime(Time.deltaTime);
                        if (gotoPlayer)
                            yield break;
                    }
                    Debug.Log("사다리에 도착했습니다.");
                    isLadderOn = true;
                    ladderUpDownPosition = new Vector2(underLadder, underFloor);
                    while (transform.position.y != underFloor)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, ladderUpDownPosition, speed * Time.deltaTime);
                        yield return new WaitForSecondsRealtime(Time.deltaTime);
                        if (gotoPlayer)
                            yield break;
                    }
                    isLadderOn = false;
                    Debug.Log("지하에 도착했습니다.");
                }
                else // 1층
                {
                    if (transform.position.x <= (firstLadderLeft + firstLadderRight) / 2) // 2층 왼쪽 사다리타기
                    {
                        ladderPosition = new Vector2(firstLadderLeft, secondFloor);
                        while (transform.position.x != firstLadderLeft)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        Debug.Log("사다리에 도착했습니다.");
                        isLadderOn = true;
                        ladderUpDownPosition = new Vector2(firstLadderLeft, firstFloor);
                        while (transform.position.y != firstFloor)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderUpDownPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        isLadderOn = false;
                        Debug.Log("1층에 도착했습니다.");
                    }
                    else // 1층 오른쪽 사다리타기
                    {
                        ladderPosition = new Vector2(firstLadderRight, secondFloor);
                        while (transform.position.x != firstLadderRight)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        Debug.Log("사다리에 도착했습니다.");
                        isLadderOn = true;
                        ladderUpDownPosition = new Vector2(firstLadderRight, firstFloor);
                        while (transform.position.y != firstFloor)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, ladderUpDownPosition, speed * Time.deltaTime);
                            yield return new WaitForSecondsRealtime(Time.deltaTime);
                            if (gotoPlayer)
                                yield break;
                        }
                        isLadderOn = false;
                        Debug.Log("1층에 도착했습니다.");
                    }
                }
            }
        }
        Debug.Log("층수가 같습니다!");
        
        while (transform.position.x != dX)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            if (gotoPlayer)
                yield break;
        }
        
        Debug.Log("도착했습니다.");
        
        yield return new WaitForSeconds(1f);

        if (findDCount < 2)
        {
            findDCount++;
            findPlayerOn = false;
        }
        else
        {
            findDCount = 0;
            findPlayerOn = true;
        }
        if (gotoPlayer)
            yield break;
        
        StartCoroutine(findDirecton());
    }
}
