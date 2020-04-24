using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Biblioteca Exterior que gere Pessoas
using ClassLibraryPessoa;

namespace ClassLibraryInfetado
{
    public class Infetado : Pessoa
    {
        #region ATRIBUTOS

        string distrito;
        DateTime data_Em_Que_Foi_Infetado;

        #endregion

        #region PROPRIEDADES

        // Métodos usados para manipular atributos do Estado

        /// <summary>
        /// Manipula o atributo "distrito" string distrito;
        /// </summary>
        public string Distrito
        {
            get { return distrito; }
            set { distrito = value; }
        }

        /// <summary>
        /// Manipula o atributo "dataNasc" data_Em_Que_Foi_Infetado;
        /// </summary>
        public DateTime Data_Em_Que_Foi_Infetado
        {
            get { return data_Em_Que_Foi_Infetado; }
            set
            {
                DateTime aux;
                if (DateTime.TryParse(value.ToString(), out aux) == true)
                {
                    data_Em_Que_Foi_Infetado = value;
                }
            }
        }

        #endregion

        #region METODOS

        #region CONSTRUTOR

        // Métodos usados na criação de novos objectos

        /// <summary>
        /// Construtor por omissao
        /// </summary>
        public Infetado() : base()
        {
            this.distrito = "";
            this.data_Em_Que_Foi_Infetado = DateTime.Today;
        }

        /// <summary>
        /// Construtor com dados vindos do exterior
        /// </summary>
        public Infetado(string nome, int idade, string cartao, string sexo, DateTime data, DateTime date, string dist, string mora, string municipi) : base(nome, idade, cartao, sexo, mora, municipi,data)
        {
            base.Nome = nome;
            base.Idade = idade;
            base.Cartao_Cidadao = cartao;
            base.Sexo = sexo;
            base.Morada = mora;
            base.Municipio = municipi;
            base.DataNasc = data;
            this.distrito = dist;
            this.data_Em_Que_Foi_Infetado = date;
            
        }

        #endregion

        #endregion
    }

    public class Infetados : Pessoa
    {
        #region ATRIBUTOS

        const int MAX = 100;
        static Infetado[] infetado;
        static int numeroInfetados = 0;
        static Infetado[] curados;
        static int numeroCurados = 0;

        #endregion

        #region METODOS

        #region CONSTRUTOR

        /// <summary>
        /// Construtor de Classe
        /// </summary>
        static Infetados()
        {
            infetado = new Infetado[MAX];
            curados = new Infetado[MAX];
        }

        #endregion

        #region INSERIR INFETADOS

        /// <summary>
        /// Pesquisa o infetado na base de dados
        /// </summary>
        /// <param name="numero_cartao">numero cartao.</param>
        /// <returns>Informacao toda do  infetado</returns>
        public static Infetado pesquisa_Infetado(string numero_cartao)
        {
            for (int i = 0; i < numeroInfetados; i++)
            {
                if (string.Compare(infetado[i].Cartao_Cidadao, numero_cartao) == 0) return infetado[i];
            }

            return null;
        }

        /// <summary>
        /// Saber se ja existe no sistema com a categoria de infetado
        /// </summary>
        /// <param name="numero_Cartao_Cidadao">The numero cartao cidadao.</param>
        /// <returns></returns>
        private static bool Get_Infetado(string numero_Cartao_Cidadao)
        {
            for (int i = 0; i < numeroInfetados; i++)
            {
                if (string.Compare(numero_Cartao_Cidadao, infetado[i].Cartao_Cidadao) == 0) return true;
            }

            return false;
        }

        /// <summary>
        /// Insere Infetados no sistema 
        /// </summary>
        /// <param name="inf">Infetado</param>
        public static int insere_Infetados(Infetado infe)
        {
            // Testar se e posivel inserir mais infetados
            if (numeroInfetados > MAX) return 0;

            // Testar se o infetado ja esta na base de dados
            if (Get_Infetado(infe.Cartao_Cidadao) == true) return -1;

            // Insere tb na lista de Pessoas o Infetado

            // NOME / IDADE / NIF / GENERO / MORADA / MUNICIPIO / DATA.NASC
            Pessoa aux = new Pessoa(infe.Nome, infe.Idade, infe.Cartao_Cidadao, infe.Sexo, infe.Morada, infe.Municipio, infe.DataNasc);

            Pessoas.InserePessoa(aux);


            infetado[numeroInfetados++] = infe;

            return 1;
        }

        #endregion

        #region CURADOS

        /// <summary>
        /// Lista de ja se recuperaram
        /// </summary>
        /// <param name="infetado"></param>
        /// <returns></returns>
        private static int insere_Curados(Infetado infetado)
        {
            // Testar se e posivel inserir mais curados
            if (numeroInfetados > MAX) return 0;

            curados[numeroCurados++] = infetado;

            return 1;
        }

        /// <summary>
        /// Mostrar lista curados.
        /// </summary>
        public static void mostra_Curados()
        {
            Console.WriteLine("\n\n\t\tRELATORIO DE CURADOS");
            Console.WriteLine("\n");

            for (int i = 0; i < numeroCurados; i++)
            {
                Console.WriteLine("\n===========================================================");
                Console.WriteLine("\nNome do Curado: " + curados[i].Nome);
                Console.WriteLine("\n-> Genero: " + curados[i].Sexo);
                Console.WriteLine("\n-> Idade: " + curados[i].Idade);
                Console.WriteLine("\n-> Nº Cartao Cidadao: " + curados[i].Cartao_Cidadao);
                Console.WriteLine("\n-> Data de Nascimento: " + curados[i].DataNasc);
                Console.WriteLine("\n-> Morada: " + curados[i].Morada);
                Console.WriteLine("\n-> Data Primeira infecao: " + curados[i].Data_Em_Que_Foi_Infetado);
                Console.WriteLine("\n===========================================================");
            }
        }

        /// <summary>
        /// Remove o infetado do sistema e mete o array de novo direito
        /// </summary>
        /// <param name="cartao">Cartao Cidadao</param>
        /// <returns></returns>
        public static int remove_Infetado(string cartao)
        {
            for (int i = 0; i < numeroInfetados; i++)
            {

                if (string.Compare(cartao,infetado[i].Cartao_Cidadao) == 0)
                {
                    insere_Curados(infetado[i]);

                    // Ordena os Infetados de novo no array
                    for (int k = i; k < numeroInfetados; k++)
                    {
                        if (k + 1 == numeroInfetados)
                        {
                            infetado[k] = infetado[k];
                        }
                        else
                        {
                            infetado[k] = infetado[k + 1];
                        }
                    }

                    // Retira o Infetado do sistema
                    numeroInfetados--;

                    break;
                }
            }

            return 0;
        }

        #endregion

        #region FUNCOES UTILIZADAS POR OUTRAS BIBLIOTECAS

        //=========================================================================================================================

        /// <summary>
        /// Mostra infetado.
        /// Utilizado pela classe Tratadores para mostrar os casos que determinado Tratador tem  
        /// </summary>
        /// <param name="cartaoCidado">cartao cidado</param>
        public static void mostra_Infetado(string cartaoCidado) 
        {
            Console.WriteLine("\n\n              FICHA DO INDIVIDUO\n");
            
            for (int i = 0; i < numeroInfetados; i++)
            {
                if (string.Compare(infetado[i].Cartao_Cidadao,cartaoCidado) == 0)
                {
                    Console.WriteLine("\n===========================================================");
                    Console.WriteLine("\nNome: " + infetado[i].Nome);
                    Console.WriteLine("\n-> Genero: " + infetado[i].Sexo);
                    Console.WriteLine("\n-> Idade: " + infetado[i].Idade);
                    Console.WriteLine("\n-> Nº Cartao Cidadao: " + infetado[i].Cartao_Cidadao);
                    Console.WriteLine("\n-> Morada: " +infetado[i].Morada);
                    Console.WriteLine("\n-> Data de Nascimento: " + infetado[i].DataNasc);
                    Console.WriteLine("\n-> Data Primeira infecao: " + infetado[i].Data_Em_Que_Foi_Infetado);
                    Console.WriteLine("\n===========================================================");

                    break;
                }
            }
        
        }

        //=========================================================================================================================

        #endregion

        #region CONSULTA DE CASOS

        // ========================== POR REGIAO ========================== 

        /// <summary>
        /// Mostra todos os infetados agrupados por regiao 
        /// </summary>
        /// <param name="distrito">distrito.</param>
        public static void mostra_Casos_Regiao(string distrito)
        {
            Console.WriteLine("\n\n\t\tRELATORIO DO DISTRITO DE {0}", distrito);
            Console.WriteLine("\t\tCASOS CONTABILIZADOS A NIVEL GLOBAL     " + numeroInfetados);
            Console.WriteLine("\n");

            for (int i = 0; i < numeroInfetados; i++)
            {
                if (infetado[i].Distrito.CompareTo(distrito) == 0)
                {

                    if (infetado[i].Idade > 70)  
                    {
                        Console.WriteLine("\n\n                     AVISO !!");
                        Console.WriteLine("\n======== ESTE PACIENTE E UM DOENTE DE RISCO ========");
                    }

                    Console.WriteLine("\n===========================================================");
                    Console.WriteLine("\nNome do Infetado: " + infetado[i].Nome);
                    Console.WriteLine("\n-> Genero: " + infetado[i].Sexo);
                    Console.WriteLine("\n-> Idade: " + infetado[i].Idade);
                    Console.WriteLine("\n-> Nº Cartao Cidadao: " + infetado[i].Cartao_Cidadao);
                    Console.WriteLine("\n-> Morada: " + infetado[i].Morada);
                    Console.WriteLine("\n-> Data de Nascimento: " + infetado[i].DataNasc);
                    Console.WriteLine("\n-> Data Primeira infecao: " + infetado[i].Data_Em_Que_Foi_Infetado);
                    Console.WriteLine("\n===========================================================");


                    Console.WriteLine("\n\n");
                }
            }
        }

        // ==========================  POR  SEXO  ========================== 

        /// <summary>
        /// Mostra todos os infetados agrupados por Sexo
        /// </summary>
        /// <param name="genero">genero.</param>
        public static void mostra_Casos_Sexo(string genero)
        {
            Console.WriteLine("\n\n\t\tRELATORIO RELATIVO AO SEXO {0}", genero);
            Console.WriteLine("\t\tCASOS CONTABILIZADOS A NIVEL GLOBAL       " + numeroInfetados);
            Console.WriteLine("\n");

            for (int i = 0; i < numeroInfetados; i++)
            {
                if (infetado[i].Sexo.CompareTo(genero) == 0)
                {
                    if (infetado[i].Idade > 70)  
                    {
                        Console.WriteLine("\n\n                     AVISO !!");
                        Console.WriteLine("\n======== ESTE PACIENTE E UM DOENTE DE RISCO ========");
                    }

                    Console.WriteLine("\n===========================================================");
                    Console.WriteLine("\nNome do Infetado: " + infetado[i].Nome);
                    Console.WriteLine("\n-> Distrito: " + infetado[i].Distrito);
                    Console.WriteLine("\n-> Idade: " + infetado[i].Idade);
                    Console.WriteLine("\n-> Nº Cartao Cidadao: " + infetado[i].Cartao_Cidadao);
                    Console.WriteLine("\n-> Morada: " + infetado[i].Morada);
                    Console.WriteLine("\n-> Data de Nascimento: " + infetado[i].DataNasc);
                    Console.WriteLine("\n-> Data Primeira infecao: " + infetado[i].Data_Em_Que_Foi_Infetado);
                    Console.WriteLine("\n===========================================================");


                    Console.WriteLine("\n\n");
                }
            }
        }

        // ==========================   POR  IDADE  ========================== 

        /// <summary>
        /// Mostra todos os casos agrupados por Idade
        /// </summary>
        /// <param name="idade">Idade</param>
        public static void mostra_Casos_Idade(int idade)
        {
            Console.WriteLine("\n\n\t\tRELATORIO RELATIVO AOS INFETADOS COM IDADE {0} ANOS\n", idade);
            Console.WriteLine("\t\tCASOS CONTABILIZADOS A NIVEL GLOBAL     " + numeroInfetados);
            Console.WriteLine("\n");

            for (int i = 0; i < numeroInfetados; i++)
            {
                if (infetado[i].Idade == idade)
                {
                    if (infetado[i].Idade > 70)  
                    {
                        Console.WriteLine("\n\n                     AVISO !!");
                        Console.WriteLine("\n======== ESTE PACIENTE E UM DOENTE DE RISCO ========");
                    }

                    Console.WriteLine("\n===========================================================");
                    Console.WriteLine("\nNome do Infetado: " + infetado[i].Nome);
                    Console.WriteLine("\n-> Genero: " + infetado[i].Sexo);
                    Console.WriteLine("\n-> Distrito: " + infetado[i].Distrito);
                    Console.WriteLine("\n-> Nº Cartao Cidadao: " + infetado[i].Cartao_Cidadao);
                    Console.WriteLine("\n-> Morada: " + infetado[i].Morada);
                    Console.WriteLine("\n-> Data de Nascimento: " + infetado[i].DataNasc);
                    Console.WriteLine("\n-> Data Primeira infecao: " + infetado[i].Data_Em_Que_Foi_Infetado);
                    Console.WriteLine("\n===========================================================");


                    Console.WriteLine("\n\n");
                }
            }
        }

        #endregion

        #endregion
    }
}
