using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.DTO.ServicosExternos
{
    public class ModelDTO: BaseService
    {
        public ModelDTO(int id, int makeId, string name) : base(id)
        {
            MakeId = makeId;
            Name = name;
        }

        [JsonPropertyName("MakeID")]
        public int MakeId { get; private set; }
        
        public string Name { get; private set; }
    }
}
