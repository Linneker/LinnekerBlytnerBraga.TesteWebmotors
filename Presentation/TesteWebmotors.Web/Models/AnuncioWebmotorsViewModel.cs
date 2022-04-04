using System.ComponentModel.DataAnnotations;

namespace TesteWebmotors.Web.Models
{
    public class AnuncioWebmotorsViewModel : EntidadeBaseViewModel
    {
        public AnuncioWebmotorsViewModel()
        {

        }

        public AnuncioWebmotorsViewModel(int id, string marca, string modelo, string versao, int ano, int quilometragem, string observacao) : base(id)
        {
            Marca = marca;
            Modelo = modelo;
            Versao = versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }
        [Required]
        [StringLength(45)]
        public string Marca { get;  set; }
        [Required]
        [StringLength(45)]
        public string Modelo { get; set; }
        [Required]
        [StringLength(45)]
        public string Versao { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public int Quilometragem { get; set; }
        [Required]
        [StringLength(65500)]
        public string Observacao { get; set; }

    }
}
