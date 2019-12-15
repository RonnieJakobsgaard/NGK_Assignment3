namespace SignalRSimpleDemo.Models
{
    public class counter
    {
        long value = 0;

        public long Inc()
        {
            return ++value;
        }
    }
}
