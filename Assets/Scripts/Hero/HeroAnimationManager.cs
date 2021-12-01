using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class HeroAnimationManager : MonoBehaviour
{
    #region Variables

        [SerializeField] private AdvancedWalkerController controller;
        [SerializeField] private CharacterNewInput input;
        [SerializeField] private Animator animatorHero;
        [SerializeField] private Animator animatorSwipe;

        [SerializeField] private float movementTolerance = 0.2f;

        [SerializeField] private bool facingLeft = false;

        private Vector3 velocity;

        private string anim_Run_String = "Running";
        private string anim_Jump_String = "Jump";
        private string anim_Falling_String = "Falling";
        private string anim_Land_String = "Land";
        private string anim_Attack_String = "Attack";
        private string anim_Hurt_String = "Hurt";
        private string anim_Slide_String = "Slide";
        private int anim_Run;
        private int anim_Jump;
        private int anim_Falling;
        private int anim_Land;
        private int anim_Attack;
        private int anim_Hurt;
        private int anim_Slide;

    #endregion

    private void Awake()
    {
        if(controller == null)
            controller = GetComponent<AdvancedWalkerController>();

        if(input == null)
            input = controller.CharacterInput as CharacterNewInput;
        
        if(animatorHero == null)
            animatorHero = GetComponentInChildren<Animator>();

        anim_Run = Animator.StringToHash(anim_Run_String);
        anim_Jump = Animator.StringToHash(anim_Jump_String);
        anim_Falling = Animator.StringToHash(anim_Falling_String);
        anim_Land = Animator.StringToHash(anim_Land_String);
        anim_Attack = Animator.StringToHash(anim_Attack_String);
        anim_Hurt = Animator.StringToHash(anim_Hurt_String);
        anim_Slide = Animator.StringToHash(anim_Slide_String);
    }

    private void OnEnable()
    {
        SidescrollerController.Jump += TriggerJumpAnimation;
        Mover.HasLanded += TriggerLandingAnimation;
        CharacterNewInput.Attacked += TriggerAttackAnimation;
        Hero.IsHurt += TriggerHurtAnimation;
        Slide.IsSliding += TriggerSlideAnimation;
    }

    private void OnDisable()
    {
        SidescrollerController.Jump -= TriggerJumpAnimation;
        Mover.HasLanded -= TriggerLandingAnimation;
        CharacterNewInput.Attacked += TriggerAttackAnimation;
        Hero.IsHurt -= TriggerHurtAnimation;
        Slide.IsSliding -= TriggerSlideAnimation;
    }

    private void Update()
    {
        DetermineFacingDirection();

		velocity = controller.GetVelocity();
        
        if(!controller.IsGrounded() && velocity.y > 0)
        {
            TriggerJumpAnimation();
        }
        else if(!controller.IsGrounded() && velocity.y <= 0)
        {
            animatorHero.SetTrigger(anim_Falling);
            
            animatorHero.ResetTrigger(anim_Jump);
            animatorHero.ResetTrigger(anim_Land);
            animatorHero.SetBool(anim_Run, false);
            return;
        }
        else if(controller.IsGrounded() && Mathf.Abs(velocity.x) > movementTolerance)
        {
            animatorHero.SetBool(anim_Run, true);

            animatorHero.ResetTrigger(anim_Jump);
            animatorHero.ResetTrigger(anim_Falling);
        }
        else if(controller.IsGrounded())
        {
            animatorHero.SetBool(anim_Run, false);

            animatorHero.ResetTrigger(anim_Jump);
            animatorHero.ResetTrigger(anim_Falling);
        }
        
        
    }

	private void DetermineFacingDirection()
	{
		if (input.FacingLeft != facingLeft)
		{
			facingLeft = input.FacingLeft;
			animatorHero.transform.localScale = new Vector3(-1 * animatorHero.transform.localScale.x, animatorHero.transform.localScale.y, animatorHero.transform.localScale.z);
		}
	}

	private void TriggerJumpAnimation()
	{
		animatorHero.ResetTrigger(anim_Falling);
		animatorHero.ResetTrigger(anim_Land);
		animatorHero.SetBool(anim_Run, false);

		animatorHero.SetTrigger(anim_Jump);
	}

    private void TriggerLandingAnimation(bool hasLanded)
    {
        if(hasLanded)
        {
            animatorHero.ResetTrigger(anim_Falling);
            animatorHero.SetTrigger(anim_Land);
        }
    }

    private void TriggerAttackAnimation()
    {
        animatorHero.SetTrigger(anim_Attack);
    }

    private void TriggerHurtAnimation()
    {
        animatorHero.SetTrigger(anim_Hurt);
    }

    private void TriggerSlideAnimation()
    {
        animatorHero.SetTrigger(anim_Slide);
    }
}
