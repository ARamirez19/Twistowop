using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRider : BaseController
{
    [Header("WallRider")]
    [SerializeField] private float speed = 10f;

    protected override void ExtraStart()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y);
        AttachToNormal();
    }

    private void Update()
    {
        MoveEnemy();
    }

    public void MovementCallback()
    {
        TurnAround();
    }

    private void MoveEnemy()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void TurnAround()
    {
        transform.Rotate(new Vector2(0f, 180f));
        speed *= -1;
    }

    private void AttachToNormal()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.up, 5f);
        if (hitInfo.collider != null)
        {
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector2.up, hitInfo.normal);
            Quaternion newRotation = surfaceRotation * Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector2.up);
            transform.rotation = newRotation;
            transform.position = hitInfo.point;
        }
        else
        {
            Debug.LogError("WallRider failed to find wall to attach to");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            levelManager.RestartLevel();
        }
    }
}
