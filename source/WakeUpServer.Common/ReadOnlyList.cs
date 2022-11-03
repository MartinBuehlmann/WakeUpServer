namespace WakeUpServer.Common
{
    public static class ReadOnlyList
    {
        public static IReadOnlyList<T> Empty<T>()
        {
            return new List<T>().AsReadOnly();
        }
    }
}