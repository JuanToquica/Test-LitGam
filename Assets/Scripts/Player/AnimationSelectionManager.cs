using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSelectionManager : MonoBehaviour
{
    [SerializeField] private DanceLibrary danceLibrary;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject danceBTNPrefab;
    [SerializeField] private RectTransform BTNContainer;

    private void Start()
    {
        CreateDanceButtons();
    }

    public void SelectDance(int index)
    {
        Debug.Log("Playing animation: " + index);
    }

    private void CreateDanceButtons()
    {
        for (int i = 0; i < danceLibrary.dances.Count; i++)
        {
            int index = i;

            GameObject btn = Instantiate(danceBTNPrefab, BTNContainer);
            Button btnComponent = btn.GetComponent<Button>();

            btnComponent.onClick.AddListener(() => SelectDance(index));

            TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null)
            {
                btnText.text = danceLibrary.dances[i].danceName;
            }
        }
    }
}
