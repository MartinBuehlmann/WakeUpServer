namespace WakeUpServer.IoT.Edge
{
    using WakeUpServer.IoT.Edge.Native;

    public class JObjectConverter
    {
        public virtual T ToObject<T>(Message message)
        {
            T? convertedValue = message.Data.ToObject<T>();
            if (convertedValue == null)
            {
                throw new NullReferenceException($"Unable to convert JObject to {typeof(T)}. Result was null");
            }

            return convertedValue;
        }
    }
}