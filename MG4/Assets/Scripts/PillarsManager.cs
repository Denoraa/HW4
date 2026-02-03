using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
public class PillarsManager : MonoSingleton<PillarsManager>
{

    //Imports
    public GameObject Pillar;

    //Usual Variables
    private float SpaceBtwPillars = 8f;
    private bool OnTurn = false;

    //Unique Variables
    private Pillar PillarScorer;
    public delegate void OnScoreing();
    public event OnScoreing OnScoreingNotify;
    public event OnScoreing UpdateScoreUINotify;


    private void Update()
    {



        StartRolling();

      


    }

    private void StartRolling()
    {

        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameON)
        {
            if (OnTurn == false)
            {

                StartCoroutine("Rolling");

            }

        }
      

    }
    private IEnumerator Rolling()
    {
        OnTurn = true;
       
        //Pillar Positions
        float uRandY = UnityEngine.Random.Range(6.5f,3f);
        SpaceBtwPillars = UnityEngine.Random.Range(7f, 9f);
        float dConstY = uRandY - SpaceBtwPillars;
        Vector3 uPillarPos = new Vector3(PlayerController.Instance.transform.position.x + 5f, uRandY, 0);
        Vector3 dPillarPos = new Vector3(PlayerController.Instance.transform.position.x + 5f, dConstY, 0);
        GameObject UpperPillar = Instantiate(Pillar, uPillarPos, Quaternion.identity);
        GameObject LowerPillar = Instantiate(Pillar, dPillarPos, Quaternion.Euler(0,0,180));
        PillarScorer = UpperPillar.GetComponent<Pillar>();
       
        //Pillar Detection Event
        PillarScorer.OnScoreNotify += ScoreGet;


         yield return new WaitForSeconds(3f);


        PillarScorer.OnScoreNotify -= ScoreGet;

        Destroy(UpperPillar);
        Destroy(LowerPillar);

        OnTurn = false;

    } 
    
    private void ScoreGet()
    {
        AudioManager.Instance.PlaySFX(false);
        OnScoreingNotify?.Invoke();
        UpdateScoreUINotify?.Invoke();


    }


}
