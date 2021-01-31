using System;
using Newtonsoft.Json;

namespace PrimesSearchWithThreads
{
    public class Settings
    {
        [JsonProperty("primesFrom")]
        public int PrimesFrom { get; set; }

        [JsonProperty("primesTo")]
        public int PrimesTo { get; set; }
    }
}