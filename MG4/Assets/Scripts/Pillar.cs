using JetBrains.Annotations;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    //Imports
    public LayerMask layerMask;

    //Usual Variables
    private bool _notYetScored = true;
    private float PillarSpeed = -3f;
    
    //Unique Variables
    public delegate void OnScore();
    public event OnScore OnScoreNotify;
    private Rigidbody2D _rb;

    private void Start()
    {


        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(PillarSpeed,_rb.velocity.y);


    }


    void Update()
    {

        ScoreDetector();


    }



    private void ScoreDetector()
    {

        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 20f, layerMask);

        if (hit.collider != null && _notYetScored)
        {

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                _notYetScored = false;
                OnScoreNotify();
            }

        }

        Debug.DrawRay(rayOrigin, rayDirection * 6f, Color.red);

    }




}
