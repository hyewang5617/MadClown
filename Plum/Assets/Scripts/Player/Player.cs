using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public ClownMoveController clown;
    public GameManager manager;

    [Header("# 플레이어")]
    [SerializeField] public float normalSpeed, runSpeed;    //노말은 걷는속도
    public float speed;
    public Vector2 inputVec;
    public bool isRunning = true;
    public bool isladder = false;
    public bool isHiding = false;

    [Header("# 스테미나")]
    public Image StaminaBar;
    public float Stamina, MaxStamina;
    public float RunCost;
    public float ChargeRate;    //스태미나 차는 퍼센트

    [Header("# 아이템")]
    public GameObject[] items;  // 아이템 리스트
    public GameObject putItem;
    bool isHaving = false;
    public string havingItem;

    [Header("# 땅")]
    public GameObject ladderUp;
    public GameObject ladderDown;
    public GameObject ladder;
    public bool isFloor2;
    

    private Coroutine recharge;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.DrawRay(rigid.position, Vector2.down,new Color(1,0,0));
        if(!isladder){
            rigid.gravityScale = 0;
            inputVec.x = Input.GetAxisRaw("Horizontal");
        }
        else{
            inputVec.y = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0;
            Vector3 myPos = transform.position;
            if(Input.GetKey(KeyCode.UpArrow)){
                if( myPos.y >= -3.4f){
                    
                    if(isFloor2){
                        if(myPos.y >= 5.89f){
                            inputVec.y =0;
                            isladder = false;
                        }
                        
                    }
                    else{
                        myPos.y = -3.4f;
                        inputVec.y =0;
                        isladder = false;
                    }
                    
                }
                /*else if(myPos.y >= 3 ){
                    ladderUp.GetComponent<BoxCollider2D>().enabled = false;
                }*/
            }


            
        }


        //아이템 버리기
        if(Input.GetKey(KeyCode.X)){
            if(isHaving == true){
                switch(havingItem){
                    case "key0":
                        putItem = items[0];
                        putItem.transform.position = transform.position; //나중에 바꾸기
                        putItem.SetActive(true);
                        break;
                    case "key1":
                        putItem = items[1];
                        putItem.transform.position = transform.position; //나중에 바꾸기
                        putItem.SetActive(true);
                        break;
                    case "key2":
                        putItem = items[2];
                        putItem.transform.position = transform.position; //나중에 바꾸기
                        putItem.SetActive(true);
                        break;
                    case "hammer":
                        putItem = items[3];
                        putItem.transform.position = transform.position; //나중에 바꾸기
                        putItem.SetActive(true);
                        break;
                }
            }
            havingItem = null;
            isHaving = false;
        }

        if(!(Input.GetKey(KeyCode.LeftControl)) && isHiding){
            isHiding = false;
            sprite.color = new Color(1,1,1,1);
        }
        
        

            
        
        
    }

    void FixedUpdate()
    {
        
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        //사다리
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector3.down, 1,LayerMask.GetMask("ladder"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if(hit.collider.name == "ladderDown"){
                if(Input.GetKey(KeyCode.UpArrow)){
                    inputVec.x =0;
                    isladder = true;
                }
                else if(Input.GetKey(KeyCode.DownArrow)){
                    isladder = false;
                }
                    

            }

            if(hit.collider.name == "ladderUp"){
                if(Input.GetKey(KeyCode.DownArrow)){
                    Debug.Log("아래 방향키");
                    inputVec.x =0;
                    isladder = true;
                }
            }

            if(hit.collider.name == "ladderDown2"){
                isFloor2 = true;
                if(Input.GetKey(KeyCode.UpArrow)){
                    inputVec.x =0;
                    isladder = true;
                }
                else if(Input.GetKey(KeyCode.DownArrow)){
                    isladder = false;
                    isFloor2 = false;
                }
            }

            
                
        }


        //달리기
        if(Stamina == 100){
            isRunning = true;
        }

        if(Input.GetKey(KeyCode.LeftShift) && isRunning){
            
            speed = runSpeed;
            Stamina -= RunCost*Time.deltaTime;
            if(Stamina <= 0){
                isRunning = false;
                Stamina =0;
                
            }
                
            StaminaBar.fillAmount = Stamina / MaxStamina;
            if(recharge != null)
                StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
            
        }
        else    
            speed = normalSpeed;
        

        
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while(Stamina <MaxStamina){
            Stamina += ChargeRate / 10f;
            if(Stamina > MaxStamina){
                Stamina = MaxStamina;
                isRunning = true;
            }
                
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(.01f);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //사다리
        /*if(collision.CompareTag("ladder")){
            inputVec.x =0;
            isladder = true;
            
            Vector3 myPos = transform.position;
            if(myPos.y >= 3){
                ladderGate.GetComponent<BoxCollider2D>().enabled = false;
            }
            

        }*/



        //아이템
        if(Input.GetKey(KeyCode.Z)){
            if(collision.CompareTag("Item")){
                if(isHaving){
                        switch(havingItem){
                            case "key0":
                                putItem = items[0];
                                putItem.transform.position = transform.position; //나중에 바꾸기
                                putItem.SetActive(true);
                                break;
                            case "key1":
                                putItem = items[1];
                                putItem.transform.position = transform.position; //나중에 바꾸기
                                putItem.SetActive(true);
                                break;
                            case "key2":
                                putItem = items[2];
                                putItem.transform.position = transform.position; //나중에 바꾸기
                                putItem.SetActive(true);
                                break;
                            case "hammer":
                                putItem = items[3];
                                putItem.transform.position = transform.position; //나중에 바꾸기
                                putItem.SetActive(true);
                                break;
                        }
                }
                collision.gameObject.SetActive(false);
                havingItem = collision.gameObject.name;
                Debug.Log(havingItem);
                isHaving = true;
            }
        
        /*if(collision.CompareTag("Item")){

            switch(collision.gameObject.name){
                case "key1":
                    IdleKey1 = true;
                    break;
                case "key2":
                    IdleKey2 = true;
                    break;
            }

                    
            if(Input.GetKey(KeyCode.Z)){
                if(isHaving){
                    switch(havingItem){
                        case "key1":
                            putItem = items[0];
                            putItem.transform.position = transform.position; //나중에 바꾸기
                            putItem.SetActive(true);
                            break;
                        case "key2":
                            putItem = items[1];
                            putItem.transform.position = transform.position; //나중에 바꾸기
                            putItem.SetActive(true);
                            break;
                    }
                }
                collision.gameObject.SetActive(false);
                havingItem = collision.gameObject.name;
                Debug.Log(havingItem);
                isHaving = true;
            }*/
                
        }


        //부쉬에 숨기
        if(Input.GetKey(KeyCode.LeftControl)){
            Debug.Log("부쉬");
            if(collision.CompareTag("Bush")){
                isHiding = true;
                sprite.color = new Color(1,1,0,1);
            }


        }
        if(collision.CompareTag("Gone")){
            if(Input.GetKeyDown("c")){
                manager.Action(collision.gameObject);
            }
        }

    }

    /*void OnTriggerStay2D(Collider2D collision){
        if(collision.CompareTag("ladderGate")){
            Debug.Log("계단 트리거 감지");
            ladderUp.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }*/

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Monster")){
            if(clown.gotoPlayer){
                playerDie();
            }else if(!(isHiding)){
                playerDie();
            }
        }
    }

    public void playerDie(){
        // 죽었을 때
        Debug.Log("으아아아아!");
    }

    void OnTriggerExit2D(Collider2D collision){
        /*
        if(collision.CompareTag("Item")){

            switch(collision.gameObject.name){
                case "key1":
                    IdleKey1 = false;
                    break;
                case "key2":
                    IdleKey2 = false;
                    break;
            }
        }*/

        if(collision.CompareTag("Bush")){
            isHiding = false;
            sprite.color = new Color(1,1,1,1);

        }

    }

    void OnCollisionStay2D(Collision2D collision){
        
        if(collision.gameObject.name == "door0"){
            if(Input.GetKey(KeyCode.C) && havingItem == "key0"){
                IsOpenDoor(collision);
                havingItem = null;
                isHaving = false;
            }
        }
        if(collision.gameObject.name == "door1"){
            if(Input.GetKey(KeyCode.C) && havingItem == "key1"){
                IsOpenDoor(collision);
                havingItem = null;
                isHaving = false;
            }
        }
        if(collision.gameObject.name == "door2"){
            if(Input.GetKey(KeyCode.C) && havingItem == "key2"){
                IsOpenDoor(collision);
                havingItem = null;
                isHaving = false;
            }
        }

        if(collision.gameObject.name == "door3"){
            if(Input.GetKeyDown(KeyCode.C) && havingItem == "hammer"){
                if(Input.GetKeyDown(KeyCode.C) && havingItem == "hammer"){
                    IsOpenDoor(collision);
                    havingItem = null;
                    isHaving = false;
                }
                
            }
        }

        
        /*//사다리
        Debug.Log(collision.gameObject);
        if(collision.gameObject == ladderDown){
            if(Input.GetKey(KeyCode.UpArrow)){
                inputVec.x =0;
                isladder = true;
            }
            
        }*/
    }

    void IsOpenDoor(Collision2D collision){
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
