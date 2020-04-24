using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryPessoa
{
    public class Pessoa
    {
        #region ESTADO

        string sexo;
        int idade;
        string cartao_Cidadao;
        string nome;
        string morada;
        DateTime dataNasc;
        string municipio;

        #endregion

        #region PROPRIEDADES

        // Métodos usados para manipular atributos do Estado

        /// <summary>
        /// Manipula o atributo "municipio" string municipio
        /// </summary>
        public string Municipio
        {
            get => municipio;
            set { municipio = value; }
        }

        /// <summary>
        /// Manipula o atributo "morada" string morada
        /// </summary>
        public string Morada
        {
            get { return morada; }
            set { morada = value; }
        }

        /// <summary>
        /// Manipula o atributo "cartao_Cidadao" string cartao_Cidadao;
        /// </summary>
        /// <value>
        /// The cartao cidadao.
        /// </value>
        public string Cartao_Cidadao
        {
            set 
            {
                // Contabilizar os caracteres
                int lengt = value.Length;

                if (lengt == 8)
                {
                    cartao_Cidadao = value;
                }
                else 
                {
                    cartao_Cidadao = "00000000";
                }
            }
            get { return cartao_Cidadao; }
        }

        /// <summary>
        /// Manipula o atributo "sexo" string sexo;
        /// </summary>
        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        /// <summary>
        /// Manipula o atributo "idade"
        /// int idade;
        /// </summary>
        public int Idade
        {
            get => idade;
            set
            {
                if (value <= 0 || value >= 130)
                {
                    idade = 0;
                }
                else
                {
                    idade = value;
                }
            }
        }

        /// <summary>
        /// Manipula o atributo "nome"
        /// string nome;
        /// </summary>
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        /// <summary>
        /// Manipula o atributo "dataNasc" DateTime dataNasc;
        /// </summary>
        public DateTime DataNasc
        {
            get { return dataNasc; }
            set
            {
                DateTime aux;
                if (DateTime.TryParse(value.ToString(), out aux) == true)
                {
                    dataNasc = value;
                }
                else
                {
                    dataNasc = DateTime.Today;
                }
            }
        }
        #endregion

        #region METODOS

        #region CONSTRUTORES

        // Métodos usados na criação de novos objectos

        /// <summary>
        /// Construtor por omissao
        /// </summary>
        public Pessoa()
        {
            this.Nome = "";
            this.Cartao_Cidadao = "00000000";
            this.idade = -1;
            this.morada = "";
            this.Municipio = "";
            this.sexo= "";
            this.dataNasc = DateTime.Today;
        }


        /// <summary>
        /// Construtor com dados vindos do exterior
        /// </summary>
        /// <param name="i">Idade da Pessoa</param>
        /// <param name="n">Nome da Pessoa</param>
        public Pessoa(int i, string n, string cartao_Cida, string sex, string mora, string municipi,DateTime date)
        {
            cartao_Cidadao = cartao_Cida;
            idade = i;
            nome = n;
            sexo = sex;
            morada = mora;
            municipio = municipi;
            dataNasc = date;
        }

        /// <summary>
        /// Construtor com dados vindos do exterior
        /// </summary>
        /// <param name="nome">Nome da Pessoa</param>
        /// <param name="idade">Idade da Pessoa</param>
        public Pessoa(string nome, int idade, string cartao_Cida, string sexo, string mora, string municipi,DateTime date)
        {
            this.idade = idade;
            this.nome = nome;
            this.cartao_Cidadao = cartao_Cida;
            this.sexo = sexo;
            this.morada = mora;
            this.municipio = municipi;
            this.dataNasc = date;
        }

        #endregion

        #region OVERRIDES

        //public override bool Equals(Object obj)
        //{
        //    return (this.nome == ((Pessoa)obj).nome);
        //}

        //public override string ToString()
        //{
        //    return string.Format("Nome= {0} - Idade= {1}", Nome, Idade);
        //}
        #endregion

        #endregion
    }

    public class Pessoas
    {
        #region ATRIBUTOS

        const int MAX = 100;
        static Pessoa[] pess;
        static int numPess = 0;

        #endregion

        #region METODOS

        #region CONST

        /// <summary>
        /// Construtor de classe
        /// </summary>
        static Pessoas()
        {
            pess = new Pessoa[MAX];
        }
        #endregion

        /// <summary>
        /// Verifica se determinada pessoa existe
        /// </summary>
        /// <param name="nome">Nome da Pessoa</param>
        /// <returns></returns>
        private static bool ExistePessoa(string nif)
        {
            for (int i = 0; i < numPess; i++)
            {
                // Caso seja essa Pessoa
                if (pess[i].Cartao_Cidadao.CompareTo(nif) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tenta registar nova pessoa e devolve o resultado da operação
        /// </summary>
        /// <param name="p">Nova pessoa</param>
        /// <returns>Estado da Operação</returns>
        public static int InserePessoa(Pessoa p)
        {
            //Testar se está cheio
            if (numPess >= MAX) return 0;

            //testar se já existe; 
            if (ExistePessoa(p.Cartao_Cidadao)) return 0;

            pess[numPess++] = p;
            return 1;
        }

        /// <summary>
        /// Verifica se uma pessoa existe. Se existir devolve a Ficha da Pessoa
        /// </summary>
        /// <param name="nome">Nome da Pessoa</param>
        /// <returns>Ficha da Pessoa caso exista; NULL caso nao exista</returns>
        public static Pessoa devolver_ficha(string nif)
        {
            for (int i = 0; i < numPess; i++)
            {
                // Caso seja essa Pessoa
                if (pess[i].Cartao_Cidadao.CompareTo(nif) == 0)
                {
                    return pess[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Mostrar a ficha da Pessoa que se pretende.
        /// </summary>
        /// <param name="nif">The nif.</param>
        public static void mostrar_ficha(string nif)
        {

            Console.WriteLine("\n\n              FICHA DO INDIVIDUO\n");

            for (int i = 0; i < numPess; i++)
            {
                // Caso seja essa Pessoa
                if (string.Compare(nif,pess[i].Cartao_Cidadao) == 0)
                {
                    Console.WriteLine("\n===========================================================");
                    Console.WriteLine("\nNome: " + pess[i].Nome);
                    Console.WriteLine("\n-> Genero: " + pess[i].Sexo);
                    Console.WriteLine("\n-> Idade: " + pess[i].Idade);
                    Console.WriteLine("\n-> Nº Cartao Cidadao: " + pess[i].Cartao_Cidadao);
                    Console.WriteLine("\n-> Morada: " + pess[i].Morada);
                    Console.WriteLine("\n-> Data de Nascimento: " + pess[i].DataNasc);
                    Console.WriteLine("\n-> Municipio: " + pess[i].Municipio);
                    Console.WriteLine("\n===========================================================");
                }
            }
        }

        #endregion
    }
}
