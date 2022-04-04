using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.DTO.ServicosExternos
{
    public class MakeDTO : BaseService
    {
        public MakeDTO(int id, string name):base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
