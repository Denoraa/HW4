using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{



    private Animator _Animator;

    public static PlayerAnimation instance;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.jNotify += Animations;
        }
    }

    private void OnDisable()    
    {
       
        PlayerController.Instance.jNotify -= Animations;


    }



    private void Animations(string name)
    {

        switch(name){


            case "JumpAnim":

                if (_Animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJumpAnimation"))
                {

                    _Animator.ResetTrigger("Jump");
                    
                }

                _Animator.SetTrigger("Jump");


                break;






        }
        






    }




}

