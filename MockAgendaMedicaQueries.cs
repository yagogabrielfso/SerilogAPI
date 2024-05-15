using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Univision.Fesp.Domain.AgendaMedica.Entities;
using Univision.Fesp.Domain.AgendaMedica.Queries;

namespace Univision.XUnitTest.Data.Mocks
{
    public class MockAgendaMedicaQueries : IAgendaMedicaQueries
    {
        public Task<List<ConsultorioDisponibilidade>> ConsultarDisponibilidadeMedica(string numeroCartao, string codigoIbge, string especialidade, string nomePrestador, string dataInicial, string dataFinal, string plano, string rede)
        {
            var result = new List<ConsultorioDisponibilidade>
            {
                new ConsultorioDisponibilidade
                {
                    BairroConsultorio = "BAIRRO",
                    CidadeConsultorio = "São Paulo",
                    NomePrestador = "Nome 1",
                    TelefoneConsultorio = "1129995039",
                    UfConselho = "SP",
                    UfConsultorio = "SP",
                    EnderecoConsultorio = "RUA TESTE",
                    CodigoConselho = 1001,
                    CodigoConsultorio = "1",
                    CodigoIbgeConsultorio = 4215455,
                    ComplementoConsultorio = "Complemento",
                    Conselho = "CRM",
                    DataDisponibilidade = "20190201",
                    DescricaoEspecialidade = "Consulta de endocrinologia pediátrica",
                    Especialidade = "S-751",
                    DatasDisponibilidade = new List<MedicoDisponibilidade>
                            {
                                new MedicoDisponibilidade { DataDisponibilidade = "20190201" },
                                new MedicoDisponibilidade { DataDisponibilidade = "20190202" },
                                new MedicoDisponibilidade { DataDisponibilidade = "20190203" },
                                new MedicoDisponibilidade { DataDisponibilidade = "20190204" }
                            },
                    Plano = "NA05",
                    Rede = "432042005"
                },
                new ConsultorioDisponibilidade
                {
                    BairroConsultorio = "BAIRRO 2",
                    CidadeConsultorio = "São Paulo",
                    NomePrestador = "Nome 2",
                    TelefoneConsultorio = "1129995039",
                    UfConselho = "SP",
                    UfConsultorio = "SP",
                    EnderecoConsultorio = "RUA TESTE 2",
                    CodigoConselho = 1002,
                    CodigoConsultorio = "2",
                    CodigoIbgeConsultorio = 4215455,
                    ComplementoConsultorio = "Complemento 2",
                    Conselho = "CRM",
                    DataDisponibilidade = "20190201",
                    DescricaoEspecialidade = "Consulta de endocrinologia pediátrica",
                    Especialidade = "S-751",
                    DatasDisponibilidade = new List<MedicoDisponibilidade>
                            {
                                new MedicoDisponibilidade { DataDisponibilidade = "20190201" },
                                new MedicoDisponibilidade { DataDisponibilidade = "20190202" },
                                new MedicoDisponibilidade { DataDisponibilidade = "20190203" },
                                new MedicoDisponibilidade { DataDisponibilidade = "20190204" }
                            },
                    Plano = "NA05",
                    Rede = "432042005"
                }
            };

            return Task.Run(() => result);
        }

        public Task<List<AgendaMedica>> GetAgendaMedicaAsync(string cartao, string codigoIbge, string especialidade, string nomePrestador, string dataInicial, string dataFinal, string plano, string rede)
        {
            var result = new List<AgendaMedica>
            {
                new AgendaMedica
                {
                   BairroConsultorio = "BAIRRO",
                    CidadeConsultorio = "São Paulo",
                    NomePrestador = "Nome 1",
                    TelefoneConsultorio = "1129995039",
                    UfConselho = "SP",
                    UfConsultorio = "SP",
                    EnderecoConsultorio = "RUA TESTE",
                    CodigoConselho = 1001,
                    CodigoConsultorio = "1",
                    CodigoIbgeConsultorio = 4215455,
                    ComplementoConsultorio = "Complemento",
                    Conselho = "CRM",
                    DataDisponibilidade = DateTime.Now,
                    DescricaoEspecialidade = "Consulta de endocrinologia pediátrica",
                    Especialidade = "S-751",
                    Plano = "NA05",
                    Rede = "432042005"
                },
                new AgendaMedica
                {
                    BairroConsultorio = "BAIRRO 2",
                    CidadeConsultorio = "São Paulo",
                    NomePrestador = "Nome 2",
                    TelefoneConsultorio = "1129995039",
                    UfConselho = "SP",
                    UfConsultorio = "SP",
                    EnderecoConsultorio = "RUA TESTE 2",
                    CodigoConselho = 1002,
                    CodigoConsultorio = "2",
                    CodigoIbgeConsultorio = 4215455,
                    ComplementoConsultorio = "Complemento 2",
                    Conselho = "CRM",
                    DataDisponibilidade = DateTime.Now,
                    DescricaoEspecialidade = "Consulta de endocrinologia pediátrica",
                    Especialidade = "S-751",
                    Plano = "NA05",
                    Rede = "432042005"
                }
            };


            return Task.Run(() => result);
        }

        public Task<List<AtendimentoDisponibilidade>> GetConsultarHorariosDisponibilidadeAsync(string numeroCartao, string codigoConsultorio, string conselho, int codigoConselho, string ufConselho, string tipoAgendamento, string especialidade, string dataInicial, string dataFinal, string rede, string plano)
        {
            throw new NotImplementedException();
        }

        public Task<List<Consulta>> GetHistoricoConsultasAsync(string numeroCartao, string conselho, int codigoConselho, string ufConselho)
        {
            throw new NotImplementedException();
        }

        public Task<List<Medico>> GetMeusMedicosAsync(string numeroCartao)
        {
            var medicos = new List<Medico>
            {
                new Medico("CRM", 123456, "PR", "João José da Silva 1"),
                new Medico("CRO", 654321, "PR", "João José da Silva 2")
            };

            return Task.Run(() => medicos);

        }

        public Task<bool> PostAgendarConsultaAsync(Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostAgendarConsultaAsync(string cartao, Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Task PostCancelarAgendamentoAsync(Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Task PostCancelarAgendamentoAsync(string cartao, Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostReagendarConsultaAsync(Reagenda reagenda)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostReagendarConsultaAsync(string cartao, Reagenda reagenda)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultaDeRetorno> VerificarConsultaDeRetorno(string numeroCartao, string conselho, string ufConselho, string codigoConselho, string dataConsulta)
        {
            throw new NotImplementedException();
        }
    }
}
