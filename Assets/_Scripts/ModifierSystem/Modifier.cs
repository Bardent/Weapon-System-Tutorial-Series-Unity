namespace Bardent.ModifierSystem
{
    /*
     * The base abstract Modifier class is used so that we can store all modifiers in a list together so that we can iterate though them
     */
    public abstract class Modifier
    {
        
    }

    /*
     * The Generic Base abstract Modifier class allows us to define the type that is being modified by the modifier. Most modifiers will inherit from this or a child
     * class that already specifies the type T.
     */
    public abstract class Modifier<T> : Modifier
    {
        public abstract T ModifyValue(T value);
    }
}