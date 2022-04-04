using TesteWebmotors.Dominio.Ajudantes;
using TesteWebmotors.Dominio.Entidade;

namespace TesteWebmotors.Web.Models
{
    public class AnuncioFiltroViewModel
    {
        public IEnumerable<AnuncioWebmotors> Anuncios { get; set; } = new List<AnuncioWebmotors>();
        public FiltroAnuncioWebmotors Filtro { get; set; } = new FiltroAnuncioWebmotors();

    }
}
