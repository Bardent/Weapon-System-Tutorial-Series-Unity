using System;
using Bardent.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Bardent.UI
{
    public class WeaponSwapChoiceUI : MonoBehaviour
    {
        public event Action<WeaponSwapChoice> OnChoiceSelected;

        [SerializeField] private WeaponInfoUI weaponInfoUI;
        [SerializeField] private CombatInputs input;
        [SerializeField] private Button button;

        private WeaponSwapChoice weaponSwapChoice;

        public void TakeRelevantChoice(WeaponSwapChoice[] choices)
        {
            var inputIndex = (int)input;

            if (choices.Length <= inputIndex)
            {
                return;
            }

            SetChoice(choices[inputIndex]);
        }

        private void SetChoice(WeaponSwapChoice choice)
        {
            weaponSwapChoice = choice;

            weaponInfoUI.PopulateUI(choice.WeaponData);
        }

        private void HandleClick()
        {
            OnChoiceSelected?.Invoke(weaponSwapChoice);
        }

        private void OnEnable()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(HandleClick);
        }
    }
}