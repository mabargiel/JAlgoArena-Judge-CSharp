using Newtonsoft.Json;

namespace Judge.API.Models
{
    public class JudgeResult
    {
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }
        
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("elapsed_time")]
        public double ElapsedTime { get; set; }

        [JsonProperty("consumed_memory")]
        public int ConsumedMemory { get; set; }

        [JsonProperty("testcase_results")]
        public bool[] TestcaseResults { get; set; }
    }
}