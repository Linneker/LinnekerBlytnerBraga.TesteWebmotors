using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.Entidade
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase()
        {
        }
        protected EntidadeBase(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
