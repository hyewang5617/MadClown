using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClownSensePlayer : MonoBehaviour
{
    public ClownMoveController clown;
    public Player player;

    private bool isSensing;
    void Awake()
    {
        isSensing = true;
    }

    void OnIsSensing()
    {
        clown.gotoPlayer = false;
        clown.StartFindDirection();
        isSensing = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isSensing && !(player.isHiding) && !(clown.isLadderOn))
        {
            Debug.Log("플레이어 감지됨!");
            isSensing = false;
            Invoke("OnIsSensing", 3f);
            clown.followPlayerOn = true;
        }
    }
}
