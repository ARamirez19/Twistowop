using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectable : MonoBehaviour
{
    public string audioEventID = "";
    private GameObject goal;
    private float moveTime = 0.75f;
    private float opacityChange = 1.0f;
    private SpriteRenderer[] childSprites;

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Goal");
        childSprites = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Compare(other.tag, "Player") == 0)
        {
            if (audioEventID != "")
                AkSoundEngine.PostEvent(audioEventID, gameObject);
            LevelManager.GetInstance().CurrentCollectableCount++;
            StartCoroutine(MoveToGoal(this.transform, goal.transform.position, moveTime));
        }
    }

    private IEnumerator MoveToGoal(Transform transform, Vector2 position, float timeToMove)
    {
        
        Vector2 curretPosition = transform.position;
        float time = 0f;
        while(time < 1)
        {
            time += Time.deltaTime / timeToMove;
            opacityChange -= Time.deltaTime;
            foreach (SpriteRenderer spriteRenderer in childSprites)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, opacityChange);
            }
            transform.position = Vector2.Lerp(curretPosition, position, time);
            yield return null;
        }
        if (LevelManager.GetInstance().CurrentCollectableCount == LevelManager.GetInstance().CollectableAmount)
        {
            LevelManager.GetInstance().SetGoalActive();
        }
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}
