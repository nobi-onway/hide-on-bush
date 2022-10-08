using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region Variable
        #region movement
            [SerializeField] private float speed;            
            [SerializeField] private float waitTime;
            [SerializeField] private Collider2D visible;
            [SerializeField] private Collider2D player;
            [SerializeField] private bool isChasing = false;
            [SerializeField] private AudioSource chasingAudio;
            private float lastTime;
            private bool isMovingRight;
            private bool isMovingUP;
            private bool isHorizontal;
        #endregion

        #region destination
            [SerializeField] private Transform[] points;
            [SerializeField] private int curIndex = 0;
        #endregion

        #region animation
            private Animator enemyAnim;
        #endregion
    #endregion
    // private void OnValidate() 
    // {
    //     enemyAnim = GetComponentInChildren<Animator>(); 
    // }         

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        enemyAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!isChasing) Patrolling();     

        if (isChasing) Chasing();

        PlayAnimation();
    }

    private void UpdateDestination() 
    {
        if (curIndex < points.Length - 1) curIndex++;
        else 
        {
            System.Array.Reverse(points);
            curIndex = 0;
        }
    }

    public void Chasing() 
    {
        isChasing = true;

        if(!chasingAudio.isPlaying && visible.GetComponent<Collider2D>().isActiveAndEnabled) chasingAudio.Play();

        visible.GetComponent<Collider2D>().enabled = false;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Patrolling()
    { 
        transform.position = Vector2.MoveTowards(transform.position, points[curIndex].position, speed * Time.deltaTime);  

        if (transform.position != points[curIndex].position) return;

        if(Time.time - lastTime < waitTime) return; 
        
        UpdateDestination();    

        lastTime = Time.time;
    }

    private void PlayAnimation()
    {
        // Tìm hướng
        Vector3 direction = (isChasing)? player.transform.position - transform.position : points[curIndex].position - transform.position;

        isHorizontal = (Vector3.Angle(direction, Vector3.right) % 180 == 0)? true : false;

        isMovingRight = (Vector3.Angle(direction, Vector3.right) == 0)? true : false;

        isMovingUP = (Vector3.Angle(direction, Vector3.up) == 0)? true : false;      

        enemyAnim.SetBool("isHorizontal", isHorizontal);

        enemyAnim.SetBool("isMovingRight", isMovingRight);
           
        enemyAnim.SetBool("isMovingUp", isMovingUP);    

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; 

        visible.gameObject.transform.eulerAngles = Vector3.forward * angle;   

        if(isChasing) visible.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.6f);
    }

}
