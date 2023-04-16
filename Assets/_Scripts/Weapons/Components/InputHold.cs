using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class InputHold : WeaponComponent
    {
        private Animator anim;

        private bool minHoldPassed;

        private bool input;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            minHoldPassed = false;
        }

        private void HandleMinHoldPassed()
        {
            minHoldPassed = true;
            
            SetAnimatorParameter();
        }

        private void HandleCurrentInputChange(bool newInput)
        {
            input = newInput;
            
            SetAnimatorParameter();
        }

        private void SetAnimatorParameter()
        {
            if (input)
            {
                anim.SetBool("hold", input);
                return;
            }

            if (minHoldPassed)
            {
                anim.SetBool("hold", input);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            anim = GetComponentInChildren<Animator>();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
            eventHandler.OnMinHoldPassed += HandleMinHoldPassed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
            eventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
        }
    }
}