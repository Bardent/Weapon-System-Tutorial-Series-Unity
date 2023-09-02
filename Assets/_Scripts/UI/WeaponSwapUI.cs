using System;
using Bardent.CoreSystem;
using Bardent.Weapons;
using UnityEngine;

namespace Bardent.UI
{
    public class WeaponSwapUI : MonoBehaviour
    {
        [SerializeField] private WeaponSwap weaponSwap;
        [SerializeField] private WeaponInfoUI newWeaponInfo;
        [SerializeField] private WeaponSwapChoiceUI[] weaponSwapChoiceUIs;
        [SerializeField] private GameManager gameManager;
            
        private CanvasGroup canvasGroup;
        
        private Action<WeaponSwapChoice> choiceSelectedCallback;
        
        private void HandleChoiceRequested(WeaponSwapChoiceRequest choiceRequest)
        {
            gameManager.ChangeState(GameManager.GameState.UI);
            
            choiceSelectedCallback = choiceRequest.Callback;
            
            newWeaponInfo.PopulateUI(choiceRequest.NewWeaponData);
            
            foreach (var weaponSwapChoiceUi in weaponSwapChoiceUIs)
            {
                weaponSwapChoiceUi.TakeRelevantChoice(choiceRequest.Choices);
            }
            
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
        }

        private void HandleChoiceSelected(WeaponSwapChoice choice)
        {
            gameManager.ChangeState(GameManager.GameState.Gameplay);
            
            choiceSelectedCallback?.Invoke(choice);
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        private void OnEnable()
        {
            weaponSwap.OnChoiceRequested += HandleChoiceRequested;

            foreach (var weaponSwapChoiceUI in weaponSwapChoiceUIs)
            {
                weaponSwapChoiceUI.OnChoiceSelected += HandleChoiceSelected;
            }
        }

        private void OnDisable()
        {
            weaponSwap.OnChoiceRequested -= HandleChoiceRequested;
            
            foreach (var weaponSwapChoiceUI in weaponSwapChoiceUIs)
            {
                weaponSwapChoiceUI.OnChoiceSelected -= HandleChoiceSelected;
            }
        }
    }
}