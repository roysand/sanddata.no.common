using Newtonsoft.Json;

namespace DataLayer.Domain.Models.AmsLeser;

public class AMSReaderDataDetail
{
    [JsonProperty("lv")]
    public string Lv { get; set; } = string.Empty;

    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("P")]
    public int P { get; set; }

    [JsonProperty("Q")]
    public int Q { get; set; }

    [JsonProperty("PO")]
    public int PO { get; set; }

    [JsonProperty("QO")]
    public int QO { get; set; }

    [JsonProperty("I1")]
    public double I1 { get; set; }

    [JsonProperty("I2")]
    public double I2 { get; set; }

    [JsonProperty("I3")]
    public double I3 { get; set; }

    [JsonProperty("U1")]
    public double U1 { get; set; }

    [JsonProperty("U2")]
    public double U2 { get; set; }

    [JsonProperty("U3")]
    public double U3 { get; set; }    
}