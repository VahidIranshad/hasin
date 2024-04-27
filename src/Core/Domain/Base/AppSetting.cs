namespace Domain.Base
{
    public class AppSetting
    {
        public int RedisDefaultET_Hour { get; set; }
        public int RedisDefaultET_Minute { get; set; }
        public int RedisDefaultET_Second { get; set; }
        public TimeSpan RedisDefaultET
        {
            get
            {
                return new TimeSpan(RedisDefaultET_Hour, RedisDefaultET_Minute, RedisDefaultET_Second);
            }
        }
        

        public int InMemoryCacheET_Hour { get; set; }
        public int InMemoryCacheET_Minute { get; set; }
        public int InMemoryCacheET_Second { get; set; }

        public TimeSpan InMemoryCacheET
        {
            get
            {
                return new TimeSpan(InMemoryCacheET_Hour, InMemoryCacheET_Minute, InMemoryCacheET_Second);
            }
        }


        public string HasinServerUrl { get; set; }
    }
}
