namespace Editor
{
    public interface IValidatable
    {
        void Validate();
    }

    public abstract class ValidatableObject : IValidatable
    {
        public abstract void Validate(); 
    
        protected static void CheckNull<T>(T value, string paramName)
        {
            if (value is null)
            {
                throw new System.ArgumentNullException(paramName);
            }
        }
    }
}