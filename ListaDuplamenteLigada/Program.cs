using System;
using System.Collections.Generic;

namespace ListaDuplamenteLigada
{
    public class Program
    {
        static void Main(string[] args)
        {
            var loja1 = new Loja("Mercadinho", "Rua das Laranjeiras, 900");
            var loja2 = new Loja("Hortifruit", "Rua do Pomar, 300");
            var loja3 = new Loja("Padaria", "Rua das Flores, 600");

            var listaDuplamenteLigada = new ListaDuplamenteLigada<Loja>();
            listaDuplamenteLigada.InserirEmListaVazia(loja1);
            listaDuplamenteLigada.InserirNoInicio(loja3);
            listaDuplamenteLigada.InserirNoInicio(loja2);

            var loja4 = new Loja("Mercadinho FIM", "Rua fim do mundo, 900");
            listaDuplamenteLigada.InserirNoFim(loja4);

            var loja5 = new Loja("Bem Bom", "Rua Da Igreja, 900");
            listaDuplamenteLigada.Inserir(loja5, 2);

            //listaDuplamenteLigada.RemoverDoFim();
            //listaDuplamenteLigada.RemoverDoInicio();
            //listaDuplamenteLigada.Remover(3);

            Console.WriteLine($"Quantidade: { listaDuplamenteLigada.Quantidade }");
            listaDuplamenteLigada.ImprimirLista();

            var prato1 = "Prato1";
            var prato2 = "Prato2";
            var prato3 = "Prato3";
            var prato4 = "Prato4";
            var prato5 = "Prato5";

            var pilhaDePratos = new Pilha<String>();
            pilhaDePratos.Empilhar(prato1);
            pilhaDePratos.Empilhar(prato2);
            pilhaDePratos.Empilhar(prato3);
            pilhaDePratos.Empilhar(prato4);
            pilhaDePratos.Empilhar(prato5);

            pilhaDePratos.Desimpilhar();

            Console.WriteLine("-- Analisando pilha -- ");
            Console.WriteLine($"Qual o proximo prato? {pilhaDePratos.Topo()}");

            //var arvore = new Arvore<String>("Raiz");
            //arvore.raiz.InserirFilho("Filho 1");
            //arvore.raiz.InserirFilho("Filho 2");

            // Respeitando Open/Closed principle.
            var arvore = new Arvore<String>("Raiz");
            arvore.InserirNo("Raiz", "Filho 1");
            arvore.InserirNo("Raiz", "Filho 2");
            arvore.InserirNo("Filho 1", "Neto A1");
            arvore.InserirNo("Filho 1", "Neto A2");
            arvore.InserirNo("Filho 2", "Neto B1");
            arvore.InserirNo("Filho 2", "Neto B2");

            arvore.RemoverNo("Neto B2");
            arvore.RemoverNo("Filho 1");

            var resultadoBuscaArvore =  arvore.Buscar("Filho 2");

            Console.WriteLine("-- Analisando Arvore  --");
            arvore.Imprimir();

            Console.WriteLine($"Busca: {resultadoBuscaArvore}");
            Console.ReadKey();
        }
    }

    public class Loja
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

    // O mesmo que LinkListedNode<T>
    public class Celula<T>
    {
        public T Conteudo { get; set; }
        public Celula<T> Anterior { get; set; }
        public Celula<T> Proximo { get; set; }

        public Celula(T conteudo, Celula<T> anterior, Celula<T> proximo)
        {
            Conteudo = conteudo;
            Anterior = anterior;
            Proximo = proximo;
        }

        public override string ToString()
        {
            return Conteudo.ToString();
        }
    }

    // O mesmo que o LinkedList<T> 
    public class ListaDuplamenteLigada<T>
    {
        protected Celula<T> inicio;
        protected Celula<T> fim;
        public int Quantidade { get; private set; }

        public void InserirEmListaVazia(T conteudo)
        {
            var celula = new Celula<T>(conteudo, null, null);
            inicio = celula;
            fim = celula;
            Quantidade += 1;
        }

        public void InserirNoInicio(T conteudo)
        {
            if (Quantidade == 0)
            {
                InserirEmListaVazia(conteudo);
                return;
            }

            var celula = new Celula<T>(conteudo, null, inicio.Proximo);
            inicio.Anterior = celula;
            celula.Proximo = inicio;
            inicio = celula;

            Quantidade += 1;
        }

        public void InserirNoFim(T conteudo)
        {
            if (Quantidade == 0)
            {
                InserirEmListaVazia(conteudo);
                return;
            }

            var celula = new Celula<T>(conteudo, fim, null);
            fim.Proximo = celula;
            fim = celula;
            Quantidade += 1;
        }

        public void Inserir(T conteudo, int posicao)
        {
            if (posicao == 0)
            {
                InserirNoInicio(conteudo);
                return;
            }

            if (posicao == Quantidade)
            {
                InserirNoFim(conteudo);
                return;
            }

            var celulaAnteriorDaPosicao = Item(posicao - 1);
            var novaCelula = new Celula<T>(conteudo, celulaAnteriorDaPosicao, celulaAnteriorDaPosicao.Proximo);
            celulaAnteriorDaPosicao.Proximo.Anterior = novaCelula;
            celulaAnteriorDaPosicao.Proximo = novaCelula;
            Quantidade += 1;
        }

        private void RemoverUltimo()
        {
            var removido = inicio;
            inicio.Anterior = null;
            inicio.Proximo = null;
            inicio = null;
            fim = null;
            Quantidade -= 1;
        }

        public void RemoverDoInicio()
        {
            if(Quantidade == 1)
            {
                RemoverUltimo();
                return;
            }

            var removido = inicio;
            inicio = removido.Proximo;
            removido.Proximo = null;
            inicio.Anterior = null;
            Quantidade -= 1;
        }

        public void RemoverDoFim()
        {
            if(Quantidade == 1)
            {
                RemoverUltimo();
                return;
            }

            var removido = fim;
            fim = removido.Anterior;
            fim.Proximo = null;
            removido.Anterior = null;
            Quantidade -= 1;
        }

        public void Remover(int posicao)
        {
            if(posicao == 0)
            {
                RemoverDoInicio();
                return;
            }

            if (Quantidade == 1)
            {
                RemoverUltimo();
                return;
            }

            if(posicao == (Quantidade - 1))
            {
                RemoverDoFim();
                return;
            }
    
            var removido = Item(posicao);
            var anteriorRemovido = removido.Anterior;
            var proximoRemovido = removido.Proximo;

            proximoRemovido.Anterior = anteriorRemovido;
            anteriorRemovido.Proximo = proximoRemovido;

            Quantidade -= 1;
        }

        public Celula<T> Item(int posicao)
        {
            if(ValidarPosicao(posicao))
            {
                if (posicao == 0)
                    return inicio;

                var ultimaPosicao = Quantidade - 1;

                if (posicao == ultimaPosicao)
                    return fim;

                if (posicao < ultimaPosicao / 2)
                    return BuscarApartirDoInicio(posicao);
              
                return BuscarApartirDoFim(posicao);
            }

            throw new Exception("Não existe elementos nessa posição");
        }

        private Celula<T> BuscarApartirDoInicio(int posicao)
        {
            var atual = inicio;
            for (var i = 0; i < posicao; i++)
                atual = atual.Proximo;

            return atual;
        }

        private Celula<T> BuscarApartirDoFim(int posicao)
        {
            var atual = fim;
            for (var i = Quantidade - 1; i > posicao; i--)
                atual = atual.Anterior;

            return atual;
        }

        public virtual void ImprimirLista()
        {
            var atual = inicio;
            for(var i = 0; i < Quantidade; i++)
            {
                Console.WriteLine(i +". "+ atual);
                atual = atual.Proximo;
            }
        }

        private bool ValidarPosicao(int posicao)
        {
            if(posicao <= Quantidade)
            {
                return true;
            }

            return false;
        }
    }

    // A pilha é um padrão LIFO (Last In, First Out or Ultimo a entrar, primeiro a sair)
    // Exemplo: Uma pilha de pratos.
    public class Pilha<T>
    {
        private ListaDuplamenteLigada<T> pilha;

        public Pilha()
        {
            pilha = new ListaDuplamenteLigada<T>();
        }

        public void Empilhar(T conteudo)
        {
            pilha.InserirNoInicio(conteudo);
        }

        public void Desimpilhar()
        {
            pilha.RemoverDoInicio();
        }

        public T Topo()
        {
            return pilha.Item(0).Conteudo;
        }

        public bool EstaVazia()
        {
            return pilha.Quantidade == 0;
        }
    }

    // A Fila segue um padrão FIFO (First In, First Out or primeiro a chegar é o primeiro a sair)
    // Exemplos: Fila de pedidos, fila de banco, fila de pedágio. 
    public class Fila<T>
    {
        private ListaDuplamenteLigada<T> fila;

        public Fila()
        {
            fila = new ListaDuplamenteLigada<T>();
        }

        public void Entrar(T conteudo)
        {
            fila.InserirNoFim(conteudo);
        }

        public void Sair()
        {
            fila.RemoverDoInicio();
        }

        public bool EstaVazia()
        {
            return fila.Quantidade == 0;
        }
    }

    public class ListaDeNos<T> : ListaDuplamenteLigada<No<T>>  
    {
        public No<T> Buscar(T conteudoBuscado)
        {
            var atual = inicio;
            for(var i = 0; i < Quantidade; i++)
            {
                var encontrado = atual.Conteudo.Buscar(conteudoBuscado);

                if (encontrado != null)
                    return encontrado;

                atual = atual.Proximo;
            }

            return null;
        }

        public int BuscarPosicao(T conteudoBuscado)
        {
            var atual = inicio;
            for (var i = 0; i < Quantidade; i++)
            {
                var encontrado = atual.Conteudo.Buscar(conteudoBuscado);

                if (encontrado != null)
                    return i;

                atual = atual.Proximo;
            }

            return -1;
        }

        public void Imprimir(int nivel)
        {
            var atual = inicio;

            for(var i = 0; i < Quantidade; i++)
            {
                atual.Conteudo.Imprimir(nivel);
                atual = atual.Proximo;
            }
        }

    }

    public class No<T>
    {
        private No<T> pai;
        private ListaDeNos<T> filhos;

        public T Conteudo { get; private set; }

        public No(T conteudo, No<T> noPai)
        {
            Conteudo = conteudo;
            this.pai = noPai;
        }

        public void InserirFilho(T conteudo, No<T> noPai)
        {
            if (filhos == null)
                filhos = new ListaDeNos<T>();

            filhos.InserirNoFim(new No<T>(conteudo, noPai));
        }

        public void Imprimir(int nivel = 0)
        {
            ImprimirPorNivel(Conteudo, nivel);

            if (filhos != null)
            {
                filhos.Imprimir(nivel + 1);
            }
        }

        public No<T> Buscar(T conteudoBuscado)
        {
            if (Conteudo.Equals(conteudoBuscado))
                return this;

            if(filhos != null)
                return filhos.Buscar(conteudoBuscado);

            return null;
        }

        private void ImprimirPorNivel(T conteudo, int nivel = 0)
        {
            Console.WriteLine(new String(' ', nivel) + conteudo);
        }

        public override string ToString()
        {
            return Conteudo.ToString();
        }

        public No<T> Remover()
        {
            if (this.pai != null)
            {
                var posicao = pai.filhos.BuscarPosicao(this.Conteudo);
                pai.filhos.Remover(posicao);
            }
              
            return this;
        }
    }

    public class Arvore<T>
    {
        private No<T> raiz;

        public Arvore(T conteudo)
        {
            raiz = new No<T>(conteudo, null);
        }

        public No<T> Buscar(T conteudo)
        {
            return raiz.Buscar(conteudo);
        }

        public void InserirNo(T pai, T filho)
        {
            var noPai = Buscar(pai);
            noPai?.InserirFilho(filho, noPai);
        }

        public No<T> RemoverNo(T remover)
        {
            var encontrado = Buscar(remover);

            if (encontrado.Equals(raiz))
            {
                raiz = null;
                return encontrado;
            }

            return encontrado.Remover();
        }

        public void Imprimir()
        {
            raiz.Imprimir();
        }
    }

}
