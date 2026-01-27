using UnityEngine;

public class PlayerAnimationInitializer : MonoBehaviour
{
    [SerializeField] private PlayerSelection playerSelection;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        ApplyDance();
    }

    private void ApplyDance()
    {
        if (playerSelection == null || playerSelection.selectedClip == null) return;

        AnimatorOverrideController overrideController = playerAnimator.runtimeAnimatorController as AnimatorOverrideController;
        if (overrideController != null)
        {
            overrideController["BaseDance"] = playerSelection.selectedClip;
            playerAnimator.Play("CurrentDance", 0, 0f);
        }
    }
}
