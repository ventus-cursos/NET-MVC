namespace Ventus.Helper
{
    internal interface ICache
    {
        object Get(string key);
        void Set(string key, object value);
        void Remove(string key);
        string[] AllKeys { get; }
        bool IsAvailable { get; }
    }
}