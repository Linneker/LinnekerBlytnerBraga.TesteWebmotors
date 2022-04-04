using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.Entidade
{
    public class AnuncioWebmotors : EntidadeBase
    {
        StringBuilder erros= new StringBuilder();  

        public AnuncioWebmotors(string marca, string modelo, string versao, int ano, int quilometragem, string observacao)
        {
            bool erro = ValidarDados(marca, modelo, versao, quilometragem, observacao);
            if (erro)
                throw new Exception(erros.ToString());

            Marca = marca;
            Modelo = modelo;
            Versao = versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }

   
        public AnuncioWebmotors(int id, string marca, string modelo, string versao, int ano, int quilometragem, string observacao) :  base(id)
        {
            bool erro = ValidarDados(marca, modelo, versao, quilometragem, observacao);
            
            if (id <= 0)
            {
                erros.Append("ID invalido!");
            }
            if (erro)
                throw new Exception(erros.ToString());

            Marca = marca;
            Modelo = modelo;
            Versao = versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }


        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Versao { get; private set; }
        public int Ano { get; private set; }
        public int Quilometragem { get; private set; }
        public string Observacao { get; private set; }

        private bool ValidarDados(string marca, string modelo, string versao, int quilometragem, string observacao)
        {
            bool erro = false;
            if (string.IsNullOrWhiteSpace(marca) || marca.Length > 45)
            {
                erros.Append("Marca deve ser preenchido e não pode ter mais de 45 carcteres!");
                erro = true;
            }
            if (string.IsNullOrWhiteSpace(modelo) || modelo.Length > 45)
            {
                erros.Append("Modelo deve ser preenchido e não pode ter mais de 45 carcteres!");
                erro = true;
            }
            if (string.IsNullOrEmpty(versao) || versao.Length > 45)
            {
                erros.Append("Versao deve ser preenchida e não pode ter mais de 45 carcteres!");
                erro = true;
            }
            if (Ano > 1885 && Ano <= (DateTime.Now.Month >= 6 ? DateTime.Now.AddYears(1).Year : DateTime.Now.Year))
            {
                erros.Append($"Ano deve estar entre 1886  E {(DateTime.Now.Month >= 6 ? DateTime.Now.AddYears(1).Year : DateTime.Now.Year)}!");
                erro = true;
            }
            if (quilometragem < 0)
            {
                erros.Append("Quilometragem deve ser maior ou igual a zero!");
                erro = true;
            }
            if (string.IsNullOrWhiteSpace(observacao)  || observacao.Length > 65535)
            {
                erros.Append("Observacao deve ser preenchido e no maximo com 65.535 caracteres!");
                erro = true;
            }

            return erro;
        }

    }
}
