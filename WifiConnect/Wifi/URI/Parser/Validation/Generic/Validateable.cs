namespace WifiConnect.Wifi.URI.Parser.Validation.Generic
{
    internal abstract class Validateable<T> : IValidateable<T>
    {
        public virtual IEnumerable<IValidator<T>> GetValidators()
        {
            return new List<IValidator<T>>();
        }

        public abstract void Validate();
    }
}
