using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Univision.XUnitTest;
using Univision.XUnitTest.Helpers;
using Xunit;

namespace Univision.Fesp.API.Controllers.V1.Tests
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.1
    /// </summary>
    public class AgendaMedicaControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        public HttpClient Client { get; }

        public AgendaMedicaControllerTests(CustomWebApplicationFactory factory)
        {
            var byteArray = Encoding.ASCII.GetBytes("userDev:pass");
            Client = factory.CreateClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }




        //[Theory]
        //[InlineData("09700022004058004", HttpStatusCode.OK, 2)]
        ////[InlineData("0970", HttpStatusCode.BadRequest)]
        //public async Task AgendaMedicaControllerTest(string numeroCartao, HttpStatusCode statusCode, int result = 0)
        //{
        //    var response = await Client.GetAsync($"/api/v1/agenda-online/{numeroCartao}/agenda?codigoIbge=123&especialidade=E-500&nomePrestador=João&dataInicial=20190101&dataFinal=20190102");

        //    response.StatusCode.Should().Be(statusCode);

        //    if (statusCode != HttpStatusCode.OK)
        //        return;

        //    var stringResponse = await response.Content.ReadAsStringAsync();

        //    //var data = ConverterHelper.ConvertResponse<List<Model.AgendaMedica.AgendaMedica>>(stringResponse);

        //    //data.Should().HaveCount(result);
        //}

        [Theory]
        [InlineData("09700022004058004", HttpStatusCode.OK, 2)]
        public async Task AgendaMedicaControllerTest(string numeroCartao, HttpStatusCode statusCode, int result)
        {
            var response = await Client.GetAsync($"/api/v1/agenda-online/{numeroCartao}/agenda?codigoIbge=123&especialidade=E-500&nomePrestador=João&dataInicial=20190101&dataFinal=20190102");
            response.StatusCode.Should().Be(statusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();

            if (statusCode == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<List<Model.AgendaMedica.AgendaMedica>>(stringResponse);
                data.Should().HaveCount(result);
            }
            else if (statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.InternalServerError)
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                // Faça as verificações necessárias no errorResponse
            }
        }



        [Theory]
        [InlineData("09700022004058004", HttpStatusCode.OK, 2)]
        //[InlineData("9700", HttpStatusCode.BadRequest)]
        public async Task GetMeusMedicosTest(string numeroCartao, HttpStatusCode statusCode, int result = 0)
        {
            var response = await Client.GetAsync($"/api/v1/agenda-online/{numeroCartao}/meus-medicos");

            response.StatusCode.Should().Be(statusCode);

            if (statusCode != HttpStatusCode.OK)
                return;

            var stringResponse = await response.Content.ReadAsStringAsync();

            //var data = ConverterHelper.ConvertResponse<List<Model.AgendaMedica.AgendaMedica>>(stringResponse);

            //data.Should().HaveCount(result);
        }


        // Classes de modelo para resposta de erro (ajuste conforme necessário)
        public class ErrorResponse
        {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
            public string RemoteAddress { get; set; }
            public string Path { get; set; }
        }


    }
}
