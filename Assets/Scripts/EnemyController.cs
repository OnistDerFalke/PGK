using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float distance;
    public float moveSpeed;
    public float jumpHeightToKill;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer rend;
    
    private float rootX;
    private int dir;
        
    void Awake()
    {
        rootX = transform.position.x;
    }

    void Update()
    {
        //Debug.Log("E: " + transform.position.y);
        HandleFlip();
        HandleMovement();
    }

    public void KillEnemy()
    {
        StartCoroutine(KillAfterDeath());
    }

    private void HandleMovement()
    {
        if(transform.position.x > rootX+distance) 
        {
            if(dir == 1) 
                distance *= -1;
            dir = -1;
        }
        else if(transform.position.x < rootX+distance) 
        {
            if(dir == -1) 
                distance *= -1;
            dir = 1;
        }
    
        transform.Translate(dir * moveSpeed * Time.deltaTime, 0, 0, Space.World);
    }

    private void HandleFlip()
    {
        if(dir == 1) 
            rend.flipX = true;
        else if(dir == -1) 
            rend.flipX = false;
    }

    private IEnumerator KillAfterDeath()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Debug.Log("Enemy is killed!");
    }
}