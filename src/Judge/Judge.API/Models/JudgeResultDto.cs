using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Judge.API.Models
{
    public class JudgeResultDto
    {
        [JsonProperty("status_code")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusCode StatusCode { get; set; } = StatusCode.ACCEPTED;

        [JsonProperty("error_message")] public string ErrorMessage { get; set; } = string.Empty;

        [JsonProperty("elapsed_time")] public double ElapsedTime { get; set; }

        [JsonProperty("consumed_memory")] public int ConsumedMemory { get; set; }

        [JsonProperty("testcase_results", NullValueHandling = NullValueHandling.Ignore)]
        public bool[] TestcaseResults { get; set; }
    }

    public enum StatusCode
    {
        ACCEPTED,
        WRONG_ANSWER,
        COMPILE_ERROR,
        RUNTIME_ERROR,
        TIME_LIMIT_EXCEEDED,
        MEMORY_LIMIT_EXCEEDED
    }
}