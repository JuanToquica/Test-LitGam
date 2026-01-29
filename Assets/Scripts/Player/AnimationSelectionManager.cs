using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationSelectionManager : MonoBehaviour
{
    [Header ("Data")]
    [SerializeField] private DanceLibrary danceLibrary;
    [SerializeField] private PlayerSelection playerSelection;

    [Header ("References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject danceBTNPrefab;
    [SerializeField] private RectTransform BTNContainer;
    private List<Button> spawnedButtons = new List<Button>();
    private int currentDance = -1;

    private void Start()
    {
        CreateDanceButtons();
        PlayDance(danceLibrary.GetIndexByClip(playerSelection.selectedClip)); //Play saved clip, if it's null, play first dance
    }

    public void PlayDance(int index)
    {
        if (currentDance == index) return;

        if (currentDance >= 0 && currentDance < spawnedButtons.Count)
            spawnedButtons[currentDance].transform.localScale = Vector3.one;

        if (index >= 0 && index < spawnedButtons.Count)
            spawnedButtons[index].transform.localScale = Vector3.one * 1.2f;

        currentDance = index;
        AnimatorOverrideController overrideController = playerAnimator.runtimeAnimatorController as AnimatorOverrideController;

        if (overrideController != null)
        {
            overrideController["BaseDance"] = danceLibrary.dances[index].clip;          
            playerAnimator.Play("CurrentDance",0 ,0);
        }
    }

    private void CreateDanceButtons()
    {
        for (int i = 0; i < danceLibrary.dances.Count; i++)
        {
            int index = i;

            GameObject btn = Instantiate(danceBTNPrefab, BTNContainer);
            Button btnComponent = btn.GetComponent<Button>();
            spawnedButtons.Add(btnComponent);

            btnComponent.onClick.AddListener(() => PlayDance(index));

            TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null)
            {
                btnText.text = danceLibrary.dances[i].danceName;
            }
        }
    }

    public void SelectAnimation()
    {
        playerSelection.selectedClip = danceLibrary.dances[currentDance].clip;
        SceneManager.LoadScene("SandBox");
    }
}
