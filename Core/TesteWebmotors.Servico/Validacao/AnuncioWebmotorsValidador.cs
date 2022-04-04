using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.DTO.Api;
using TesteWebmotors.Servico.Requisicao;

namespace TesteWebmotors.Servico.Validadores
{
    public class AnuncioWebmotorsValidador : AbstractValidator<AnuncioWebmotorsDTO>
    {
        private const string URL_SERVICO = "https://desafioonline.webmotors.com.br/api/OnlineChallenge/";
        private static MakeRequisicao _makeRequisicao;

        public AnuncioWebmotorsValidador(MakeRequisicao makeRequisicao)
        {
            _makeRequisicao = makeRequisicao;
            RuleFor(_ => _.Marca)
                    .MinimumLength(3).WithMessage("Marca deve ter no minimo 3 caracteres")
                    .MaximumLength(45).WithMessage("Marca deve ter no maximo 45 caracteres")
                    .NotEmpty().WithMessage("Marca deve ser preechido")
                    .NotNull().WithMessage("Marca deve ser preechido");
                    //.MustAsync(async (t,e,ec) => await ExisteMake(t.Marca)).WithMessage("Marca não encontrada!");
                    //deixei comentado para mostrar que pode ser feito mas como está muito lerdo é preferivel remover daqui e deixar
                    //somente para classe de serviço validar


            RuleFor(_ => _.Observacao)
                .MinimumLength(3).WithMessage("Observação deve ser preechido com pelo menos 3 carcteres!")
                .MinimumLength(65530).WithMessage("Observação deve ser preechido no maximo 65530 carcteres!")
                .NotEmpty().WithMessage("Observação deve ser preechido!")
                .NotNull().WithMessage("Observação deve ser preechido!");
            RuleFor(_ => _.Modelo)
                .MinimumLength(3).WithMessage("Modelo deve ser preechido com  menos 3 carcteres!").
                MaximumLength(45).WithMessage("Modelo deve ser preechido com no maximo 45 caracteres!").
                NotEmpty().WithMessage("Modelo deve ser preechido!").
                NotNull().WithMessage("Modelo deve ser preechido!");

            RuleFor(_ => _.Versao)
                .MinimumLength(1).WithMessage("Versão deve ser preechido com pelo menos 1 carcteres!")
                .MaximumLength(45).WithMessage("Versão deve ser preechido com no maximo 45 carcteres!")
                .NotEmpty().WithMessage("Versão deve ser preechido!")
                .NotNull().WithMessage("Versão deve ser preechido!");
            RuleFor(_ => _.Ano)
                .ExclusiveBetween(1886, (DateTime.Now.Month >= 6 ? DateTime.Now.AddYears(1).Year : DateTime.Now.Year)).WithMessage($"Ano deve ser preechido entre 1886 e {(DateTime.Now.Month >= 6 ? DateTime.Now.AddYears(1).Year : DateTime.Now.Year)}")
                .NotEmpty().WithMessage("Ano deve ser preechido!")
                .NotNull().WithMessage("Ano deve ser preechido!");

            RuleFor(_ => _.Quilometragem)
                .GreaterThanOrEqualTo(0).WithMessage("Quilometragem deve ser maior que zero!")
                .NotNull().WithMessage("Quilometragem deve ser preechido!");

        }

        public async Task<bool> ExisteMake(string marca)
        {
            var makes = await _makeRequisicao.ObterMakes(URL_SERVICO);
            return makes.Where(t => t.Name.Equals(marca)).Any();
            
        }
      }
}
