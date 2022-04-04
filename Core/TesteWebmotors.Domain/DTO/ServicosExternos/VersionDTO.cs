using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.DTO.ServicosExternos
{
    public class VersionDTO: BaseService
    {
        public VersionDTO(int id, int modelId,string name) : base(id)
        {
            ModelId = modelId;
            Name = name;
        }

        [JsonPropertyName("ModelID")]
        public int ModelId { get; private set; }

        public string Name { get; private set; }
    }
}
