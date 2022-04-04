using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.DTO.ServicosExternos
{
    public class BaseService
    {
        public BaseService(int id)
        {
            Id = id;
        }

        [JsonPropertyName("ID")]
        public int Id { get; private set; }
    }
}
