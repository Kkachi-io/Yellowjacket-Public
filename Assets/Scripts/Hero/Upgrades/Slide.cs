using UnityEngine;
using CMF;
using DG.Tweening;
using System;

[System.Serializable]
public class Slide
{
    #region Variables

        public static event Action IsSliding;

        [SerializeField] private bool isSlideOnCooldown = false;
        [SerializeField] private float slideForce = 15f;
        [SerializeField] private float slideTime = 0.5f;
        [SerializeField] private float slideCooldown = 1f;
        private ColliderSettings defaultColliderSettings;
        private ColliderSettings SlideColliderSettings = new ColliderSettings{
                                                                    StepHeightRatio = 0,
                                                                    ColliderHeight = 0.35f,
                                                                    ColliderOffset = new Vector3(0, 1.55f, 0)};
        private AdvancedWalkerController controller;
        private Mover mover;
        private string sequenceID = "Slide_Tween";

	#endregion

	public void SetControllerAndMover(AdvancedWalkerController controller, Mover mover)
    {
        this.controller = controller;
        this.mover = mover;
        defaultColliderSettings = mover.GetColliderSettings();
    }


    public void SlideHero(bool facingLeft)
    {
        if(!controller.IsGrounded()) return;

        if(isSlideOnCooldown) return;

        isSlideOnCooldown = true;
        
        var addForce = facingLeft? slideForce * -1 : slideForce;

        if(DOTween.IsTweening(sequenceID))
            DOTween.Kill(sequenceID);
        
        Sequence Sequence = DOTween.Sequence().SetId(sequenceID);
        Sequence.AppendCallback(delegate{mover.SetColliderSettings(SlideColliderSettings);})
                .AppendCallback(delegate{IsSliding?.Invoke();})
                .AppendCallback(delegate{controller.AddMomentum(Vector3.right * addForce);})
                .InsertCallback(slideTime, delegate{mover.SetColliderSettings(defaultColliderSettings);})
                .InsertCallback(slideTime, delegate{controller.SetMomentum(Vector3.zero);})
                .InsertCallback(slideTime + slideCooldown, delegate{isSlideOnCooldown = false;});
    }

}


public class ColliderSettings
{
    public float StepHeightRatio;
    public float ColliderHeight;
    public Vector3 ColliderOffset;

    public ColliderSettings() {}
    public ColliderSettings(float stepHeightRatio, float colliderHeight, Vector3 colliderOffset)
    {
        StepHeightRatio = stepHeightRatio;
        ColliderHeight = colliderHeight;
        ColliderOffset = colliderOffset;
    }
}