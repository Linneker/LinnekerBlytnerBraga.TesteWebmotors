using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.Ajudantes
{
    public class FiltroAnuncioWebmotors
    {
        private string? _marca, _versao,_modelo;
        public FiltroAnuncioWebmotors()
        {

        }
        public FiltroAnuncioWebmotors(int? id, string? marca, string? modelo, string? versao, int? ano, int? quilometragem, string? observacao) 
        {
            
            Id = id;
            Marca = marca == "0" ? null : marca;
            Modelo = modelo == "0" ? null : modelo;
            Versao = versao == "0" ? null : versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }

        public int? Id { get; set; }
        public string? Marca { get=> _marca; set { _marca = value == "0" ? null : value; } }
        public string? Modelo { get => _modelo; set { _modelo= value == "0" ? null : value; } }
        public string? Versao { get=> _versao; set {  _versao = value == "0" ? null : value; } }
        public int? Ano { get; set; }
        public int? Quilometragem { get; set; }
        public string? Observacao { get; set; }
    }
}
