using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool onlyPlayer = false;
    [SerializeField] private GameObject otherPortal;
    private float timer = 0.5f;

    private float moveTime = 0.75f;
    private GameObject player;
    private SpriteRenderer[] childSprites;

    private void Start()
    {
        if(otherPortal.GetComponent<Portal>().GetOnlyPlayerStatus() == true)
        {
            onlyPlayer = true;
        }
        else
        {
            onlyPlayer = false;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        childSprites = player.gameObject.GetComponentsInChildren<SpriteRenderer>();
        
    }

    private void Update()
    {
        if(timer < 0.5f)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(onlyPlayer)
        {
            if(other.tag != "Player")
            {
                return;
            }
        }
        if (timer >= 0.5f && player.GetComponent<PlayerController>().portalEligible == true)
        {
            otherPortal.GetComponent<Portal>().Pause();
            StartCoroutine(MoveToPosition(other.transform, otherPortal.transform.position, moveTime));
            //other.transform.position = otherPortal.transform.position;
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector2 positon, float timeToMove)
    {
        foreach (SpriteRenderer spriteRenderer in childSprites)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
        player.GetComponent<PlayerController>().portalEligible = false;
        Vector2 currentPostion = player.transform.position;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        
        float movement = 0f;
        while (movement < 1)
        {
            movement += Time.deltaTime / timeToMove;
            player.transform.position = Vector2.Lerp(currentPostion, positon, movement);
            yield return null;   
        }
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        foreach(SpriteRenderer spriteRenderer in childSprites)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<PlayerController>().portalEligible = true;
    }

    public void Pause()
    {
        timer = 0;
    }

    public bool GetOnlyPlayerStatus()
    {
        return onlyPlayer;
    }
}