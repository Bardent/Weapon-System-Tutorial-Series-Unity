using System.Collections.Generic;

namespace Bardent.ModifierSystem
{
    /*
     * The Modifiers class is a generic class for holding and acting upon a list of Modifiers of a specific type. It also allows us to
     * easily apply all modifiers to a value. An example of where this is used can be seen in the DamageReceiver Core Component.
     */
    public class Modifiers<TModifierType, TValueType> where  TModifierType: Modifier<TValueType>
    {
        private readonly List<TModifierType> modifierList = new List<TModifierType>();

        /*
         * Runs through the modifierList and applies each modifier to the input value. Note that the output of the first modifier is used as the input of the next
         * modifier. This is not a smart system but works for our use case. Better systems might allow modifiers to be sorted first based on some property.
         */
        public TValueType ApplyAllModifiers(TValueType initialValue)
        {
            var modifiedValue = initialValue;

            foreach (var modifier in modifierList)
            {
                modifiedValue = modifier.ModifyValue(modifiedValue);
            }

            return modifiedValue;
        }

        public void AddModifier(TModifierType modifier) => modifierList.Add(modifier);

        public void RemoveModifier(TModifierType modifier) => modifierList.Remove(modifier);
    }
}