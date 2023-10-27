using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private int horas = 0;
        private List<string> veiculos = new List<string>();
        private bool isMercosul = false;

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            // TODO: Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"
            // *IMPLEMENTE AQUI*
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            var placaCarro = Console.ReadLine().ToString();

            if(validaPlacaCarro(placaCarro))
            {
                if(!isMercosul && !placaCarro.Contains("-"))
                    placaCarro = formataPlacaPadrao(placaCarro);
                    
                veiculos.Add(placaCarro);
                imprimeCupomEntradaSaida(1,placaCarro);
            }
            else{
                Console.WriteLine("Placa inserida não é valida.\n\n retornando ao menu principal");
            }
        }
        private string formataPlacaPadrao(string placa)
        {
            return placa.Substring(0,3) + "-" + placa.Substring(3,4);
        }
        private bool validaPlacaCarro(string placa)
        {   
            if(placa.Length < 7 || placa.Length> 8)
                return false;

            if (char.IsLetter(placa, 4))
            {
                isMercosul = true;
                //Placa tipo Mercosul deve ter o formato: três letras, um número, uma letra e dois números.
                var mercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return mercosul.IsMatch(placa);
            }
            else
            {
                isMercosul = false;
                // placa padrao antigadeve ter  3 primeiros caracteres são letras e se os 4 últimos são números.
                var padrao = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padrao.IsMatch(placa.Replace("-",""));
            }
        }
        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            // *IMPLEMENTE AQUI*
            string placa = Console.ReadLine();

            if(!isMercosul && !placa.Contains("-"))
                    placa = formataPlacaPadrao(placa);

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                
                // TODO: Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // TODO: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
                // *IMPLEMENTE AQUI*
                var qtdHoraEstacionado = int.Parse(Console.ReadLine());
                
                if(qtdHoraEstacionado != null && qtdHoraEstacionado is int && qtdHoraEstacionado > 0)
                    horas = qtdHoraEstacionado;
                
                decimal valorTotal = valorTotal = precoInicial + (precoPorHora * qtdHoraEstacionado);

                // TODO: Remover a placa digitada da lista de veículos
                // *IMPLEMENTE AQUI*
                veiculos.Remove(placa);
                imprimeCupomEntradaSaida(2,placa,valorTotal);
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        private void imprimeCupomEntradaSaida(int tipoCupom, string placa, decimal valorTotal = 0.00M)
        {
            if(tipoCupom == 1)
            {
                Console.WriteLine("Carro inserido com sucesso.");
                Console.WriteLine($" Data Entrada - {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}\n Placa - {placa}");
            }
            else if(tipoCupom == 2)
            {
                Console.WriteLine($"O veículo {placa} foi removido.");
                Console.WriteLine($"Data saida - {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}\n Placa - {placa}\n Total hora - {horas}h\n Preço total foi de: R$ {valorTotal}.");
            }
        }


        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // TODO: Realizar um laço de repetição, exibindo os veículos estacionados
                // *IMPLEMENTE AQUI*
                foreach(string carro in veiculos)
                    Console.WriteLine($"{carro};");
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
