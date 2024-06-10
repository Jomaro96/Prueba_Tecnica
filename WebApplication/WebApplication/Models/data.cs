using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace WebApplication.Models
{
    public class Data
    {
        public string cliente { get; set; }
        [JsonPropertyName("clave pedimento")]
        public string clave_pedimento { get; set; }
        [JsonPropertyName("tipo operacion")]
        public string tipo_operacion { get; set; }
        public string referencia { get; set; }
        public string pedimento { get; set; }
        public string remesa { get; set; }
        public string caja { get; set; }
        public string sello { get; set; }
        [JsonPropertyName("DODA")]
        public string doda { get; set; }
        [JsonPropertyName("CP folios")]
        public string cp_folios { get; set; }
        [JsonPropertyName("cruce/SOIA")]
        public string cruce_soia { get; set; }
        public string usuario { get; set; }
        [JsonPropertyName("TIEMPO RECIBO BGTS")]
        public string t_recibo_bgts { get; set; }
        [JsonPropertyName("TIEMPO ACG CONFIRMADO")]
        public string t_acg_confirmado { get; set; }
        [JsonPropertyName("FECHA CCP")]
        public string fecha_ccp { get; set; }
        [JsonPropertyName("Fecha de remesa")]
        public string fecha_remesa { get; set; }
    }
}