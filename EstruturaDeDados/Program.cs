using System;
using System.Collections.Generic;

namespace EstruturaDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<Loja> listaDefault = new LinkedList<Loja>();

            var loja1 = new Loja("Mercadinho", "Rua das Laranjeiras");
            var loja2 = new Loja("Hortifruit", "Rua do Pomar, 300");
            var loja3 = new Loja("Padaria", "Rua das Flores, 600");
           
            var lista = new ListaLigada();
            var celulaLoja3 = lista.InserirPrimeiro(loja3);
            var celulaLoja2 = lista.InserirPrimeiro(loja2);
            var celulaLoja1 = lista.InserirPrimeiro(loja1);

            var loja4 = new Loja("Supermercado", "Alameda Santos, 400");
            var celulaLoja4 = lista.InserirDepois(celulaLoja3, loja4);

            var loja5 = new Loja("Mini Mercado", "Rua da fazenda, 900");
            var celulaLoja5 = lista.InserirDepois(celulaLoja4, loja5);

            var lojaMurilo = new Loja("Murilo Casa", "Rua Pioneiro José Raimundo de Olivera, 128");
            lista.InserirNaPosicao(2, lojaMurilo);


            lista.RemoverDoPrimeiro();
            lista.RemoverDoPrimeiro();
            lista.Remover(celulaLoja5);

            Console.WriteLine($"Quantidade: {lista.Quantidade}");
            lista.ImprimirLista();

            Console.ReadKey();
        }
    }

    class Loja
    {
        private string nome;
        private string endereco;

        public Loja(string nome, string endereco)
        {
            this.nome = nome;
            this.endereco = endereco;
        }

        public override string ToString()
        {
            return $"[{nome}, {endereco}]";
        }
    }

    class Celula
    {
        public Celula(Loja conteudo, Celula proximo)
        {
            Conteudo = conteudo;
            Proximo = proximo;
        }

        public Loja Conteudo { get; private set; }
        public Celula Proximo { get;  set; }
    }

    class ListaLigada
    {
        public ListaLigada()
        {

        }

        public Celula InserirNaPosicao(int posicao, Loja loja)
        {
            Celula celulaNova = null;

            if (posicao == 0)
            {
                InserirPrimeiro(loja);
                return celulaNova;
            }

            Celula celulaNaPosicaoAnterior = BuscarCelula(posicao - 1);
            celulaNova = new Celula(loja, celulaNaPosicaoAnterior);
            celulaNova.Proximo = celulaNaPosicaoAnterior.Proximo;
            celulaNaPosicaoAnterior.Proximo = celulaNova;
            Quantidade += 1;

            return celulaNova;
        }

        private Celula BuscarCelula(int posicao)
        {
            var atual = Inicio;
            for (var i = 1; i <= posicao; i++)
                atual = atual.Proximo;
           
            return atual;
        }

        private Celula BuscarCelula(Celula celula)
        {
            var atual = Inicio;
            if (atual.Conteudo.Equals(celula.Conteudo))
            {
                return null;
            }

            Celula anterior = null;
            for (var i = 1; i <= Quantidade; i++)
            {
                anterior = atual;
                atual = atual.Proximo;

                if (atual.Conteudo.Equals(celula.Conteudo))
                {
                    return anterior;
                }
            }

            throw new Exception("Não foi encontrado a celula como referência de posição na lista");
        }

        public Celula InserirPrimeiro(Loja loja)
        {
            var celula = new Celula(loja, Inicio);

            Inicio = celula;
            Quantidade += 1;

            return celula;
        }



        public void Remover(Celula celula)
        {
            if (Inicio.Conteudo.Equals(celula.Conteudo))
            {
                RemoverDoPrimeiro();
            }

            var atual = Inicio;
            Celula anterior = null;
            for(var i = 1; i < Quantidade; i++)
            {
                anterior = atual;
                atual = atual.Proximo;

                if(atual.Conteudo.Equals(celula.Conteudo))
                {
                    anterior.Proximo = atual.Proximo;
                    atual.Proximo = null;
                    Quantidade -= 1;
                    return;
                }
            }
        }

        public void RemoverDoPrimeiro()
        {
            var removido = Inicio;
            Inicio = Inicio.Proximo;
            removido.Proximo = null;
            Quantidade -= 1;
        }


        public Celula InserirAntes(Celula celula, Loja loja)
        {
            var atual = Inicio;

            if (atual.Conteudo.Equals(celula.Conteudo))
                return InserirPrimeiro(loja);

            var anterior = atual;

            for (var i = 1; i <= Quantidade; i++)
            {
                if(atual.Conteudo.Equals(celula.Conteudo))
                {
                    var novoProximoDoAnterior = new Celula(loja, atual);
                    anterior.Proximo = novoProximoDoAnterior;
                    Quantidade += 1;

                    return novoProximoDoAnterior;
                }

                anterior = atual;
                atual = atual.Proximo;
            }

            throw new Exception("Não foi encontrado a celula como referência de posição na lista");
        }

        public Celula InserirDepois(Celula celula, Loja loja)
        {
            var atual = Inicio;

            for(var i = 1; i <= Quantidade; i++)
            {
                if (atual.Conteudo.Equals(celula.Conteudo))
                {
                    var antigoProximo = atual.Proximo;
                    var novoProximo = new Celula(loja, antigoProximo);
                    atual.Proximo = novoProximo;
                    Quantidade += 1;
                    return novoProximo;
                }

                atual = atual.Proximo;
            }

            throw new Exception("Não foi encontrado a celula como referência de posição na lista");
        }

        public void ImprimirLista()
        {
            var atual = Inicio;

            for(var i = 1; i <= Quantidade; i++)
            {
                if (atual == null)
                    break;

                Console.WriteLine(atual.Conteudo);
                atual = atual.Proximo;
            }
        }

        public Celula Inicio { get; set; }
        public  int Quantidade { get; private set; }
    }
}
