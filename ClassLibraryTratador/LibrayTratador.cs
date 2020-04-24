using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Biblioteca Exterior que gere Pessoas
using ClassLibraryPessoa;

// Biblioteca Exterior que gere Infetados
using ClassLibraryInfetado;

namespace ClassLibraryTratador
{
    public class Tratador : Pessoa
    {
        #region ATRIBUTOS

        string distrito;
        string nome_do_Paciente_Cuidar;

        string numero_Cidadao_Paciente;

        #endregion

        #region PROPRIEDADES

        /// <summary>
        /// Manipula o atributo "distrito" string distrito;
        /// </summary>
        public string Distrito
        {
            get { return distrito; }
            set { distrito = value; }
        }

        /// <summary>
        /// Manipula o atributo "nome_do_Paciente_Cuidar" string nome_do_Paciente_Cuidar;
        /// </summary>
        public string Nome_Do_Paciente_Cuidar
        {
            get { return nome_do_Paciente_Cuidar; }
            set { nome_do_Paciente_Cuidar = value; }
        }

        /// <summary>
        /// Manipula o atributo "numero_Cidadao_Paciente" string numero_Cidadao_Paciente;
        /// </summary>
        public string Numero_Cidadao_Paciente
        {
            get { return numero_Cidadao_Paciente; }
            set
            {
                // Contabilizar os caracteres
                int lengt = value.Length;

                if (lengt == 8)
                {
                    numero_Cidadao_Paciente = value;
                }
                else
                {
                    numero_Cidadao_Paciente = "00000000";
                }
            }
        }

        #endregion

        #region METODOS

        #region CONSTRUTORES

        /// <summary>
        /// Construtor com valores por defeito/omissão
        /// </summary>
        public Tratador() : base()
        {
            numero_Cidadao_Paciente = "00000000";
            distrito = "";
            nome_do_Paciente_Cuidar = "";
        }

        /// <summary>
        /// Construtor com dados vindos do exterior
        /// </summary>
        public Tratador(int idad, string name, string cartao_Cida_Tratador, string genero, string mora, string municipi, DateTime date
             , string distrit, string nome_Pacient, string cartao_Cida_Cidadao) : base(idad, name, cartao_Cida_Tratador, genero, mora, municipi, date)
        {
            base.Cartao_Cidadao = cartao_Cida_Tratador;
            base.Idade = idad;
            base.Nome = name;
            base.Sexo = genero;
            base.Morada = mora;
            base.Municipio = municipi;
            base.DataNasc = date;
            this.Distrito = distrit;
            this.numero_Cidadao_Paciente = cartao_Cida_Cidadao;

            this.nome_do_Paciente_Cuidar = nome_Pacient;
        }

        #endregion

        #region OVERRIDE

        //public override bool Equals(object obj)
        //{
        //    Tratador aux = (Tratador)obj;

        //    return (aux.Cartao_Cidadao == this.Cartao_Cidadao) ? true:false;
        //}

        #endregion

        #endregion
    }

    public class Tratadores : Pessoa
    {
        #region ATRIBUTOS

        const int Max_Pacientes = 15;
        const int Max_Tratadores = 100;

        static Tratador[] tratadores;
        static int numero_Tratadores = 0;

        // Nif dos Pacientes a Tratar
        static string[] pessoas_Tratar;
        static int numero_Pessoas_Tratar = 0;

        #endregion

        #region METODOS

        #region CONSTRUTORES

        /// <summary>
        /// Construtor de Classe
        /// </summary>
        static Tratadores()
        {
            tratadores = new Tratador[Max_Tratadores];
            pessoas_Tratar = new string[Max_Pacientes];
        }

        #endregion

        #region CRIAR TRATADOR

        /// <summary>
        /// Saber se ja existe no sistema este Paciente a ser Tratado 
        /// </summary>
        /// <param name="numero_Cidada">O numero cidadao.</param>
        /// <returns></returns>
        private static bool Get_Paciente(string numero_Cidada)
        {
            for (int i = 0; i < numero_Pessoas_Tratar; i++)
            {
                if (string.Compare(pessoas_Tratar[i],numero_Cidada) == 0) return true;
            }

            return false;
        }

        /// <summary>
        /// Saber se ja existe no sistema este Tratador.
        /// </summary>
        /// <param name="numero_Cidada">The numero cidada.</param>
        /// <returns></returns>
        private static bool Get_Tratador(string numero_Cidada)
        {
            for (int i = 0; i < numero_Tratadores; i++)
            {
                if (string.Compare(tratadores[i].Cartao_Cidadao, numero_Cidada) == 0)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Insere Tratador no sistema 
        /// </summary>
        /// <param name="tratador">Tratadores</param>
        public static int criar_Tratador(Tratador tratador)
        {

            // Testar se pode Tratar mais alguem 
            if (numero_Pessoas_Tratar > Max_Pacientes) return 0;

            // Testar se ja esta a ser Tratado esse Paciente
            if (Get_Paciente(tratador.Numero_Cidadao_Paciente) == false)
            {
                pessoas_Tratar[numero_Pessoas_Tratar++] = tratador.Numero_Cidadao_Paciente;
            }

            // Testar se ja existe no sistema esse Tratador
            if (Get_Tratador(tratador.Cartao_Cidadao) == false)
            {
                tratadores[numero_Tratadores++] = tratador;
            }

            // Insere o Tratador na lista de Pessoas

            // NOME / IDADE / NIF / GENERO / MORADA / MUNICIPIO / DATA.NASC
            Pessoa aux = new Pessoa(tratador.Nome, tratador.Idade, tratador.Cartao_Cidadao, tratador.Sexo, tratador.Morada, tratador.Municipio, tratador.DataNasc);

            Pessoas.InserePessoa(aux);

            return 1;
        }

        /// <summary>
        /// Remover Paciente 
        /// </summary>
        /// <returns></returns>
        public static int remover_Paciente(string nif)
        {
            for (int i = 0; i < numero_Pessoas_Tratar; i++)
            {
                // Caso seja esse o nif o paciente é removido
                if (string.Compare(nif,pessoas_Tratar[i]) == 0)
                {
                    // Ordena os Pacientes de novo no array
                    for (int k = i; k < numero_Pessoas_Tratar; k++)
                    {
                        if (k + 1 == numero_Pessoas_Tratar)
                        {
                            pessoas_Tratar[k] = pessoas_Tratar[k];
                        }
                        else
                        {
                            pessoas_Tratar[k] = pessoas_Tratar[k + 1];
                        }
                    }

                    // Retira o Paciente do Sistema
                    numero_Pessoas_Tratar--;
                }
            }

            return 0;
        }

        /// <summary>
        /// Remover Tratador.
        /// </summary>
        /// <param name="nif">The nif.</param>
        /// <returns></returns>
        public static int remover_Tratador(string nif)
        {
            for (int i = 0; i < numero_Tratadores; i++)
            {
                // Caso seja esse o nif o Tratador é removido
                if (string.Compare(nif, tratadores[i].Cartao_Cidadao) == 0)
                {
                    remover_Paciente(tratadores[i].Numero_Cidadao_Paciente);
                    
                    // Ordena os Pacientes de novo no array
                    for (int k = i; k < numero_Tratadores; k++)
                    {
                        if (k + 1 == numero_Tratadores)
                        {
                           tratadores[k] = tratadores[k];
                        }
                        else
                        {
                           tratadores[k] = tratadores[k + 1];
                        }
                    }

                    // Retira o Tratador do Sistema
                    numero_Tratadores--;
                }
            }

            return 0;
        }

        /// <summary>
        /// Pesquisas se existe o Tratador se existir retorna a posicao onde se encontra
        /// </summary>
        /// <returns></returns>
        public static int pesquisa_Tratador(string nif)
        {
            for (int i = 0; i < numero_Tratadores ; i++)
            {
                if (string.Compare(nif,tratadores[i].Cartao_Cidadao) == 0)
                {
                    return i;    
                }
            }

            return -1;
        }

        #endregion

        #region MOSTAR QUE PACIENTES UM DETERMINADO TRATADOR TEM

        /// <summary>
        /// Mostra os Pacientes de um determinado Tratador
        /// </summary>
        /// <param name="pessoa"></param>
        public static void mostra_Pacientes_Tratar(Tratador pessoa)
        {
            int aux = pesquisa_Tratador(pessoa.Cartao_Cidadao);

            if (aux == -1)
            {

            }
            else
            {
                Console.WriteLine("\n\n\nO Tratador(a) {0} \n\n", pessoa.Nome);

                    for (int k = 0; k < numero_Tratadores; k++)
                    {
                        if (tratadores[k].Cartao_Cidadao == pessoa.Cartao_Cidadao)
                        {
                            // Envia para a funcao o nif do Paciente
                            Infetados.mostra_Infetado(tratadores[k].Numero_Cidadao_Paciente);
                        }
                    }
            }
        }

        #endregion

        /// <summary>
        /// Verifica se o Tratador existe. Se existir devolve a Ficha do Tratador
        /// </summary>
        /// <param name="nif"></param>
        /// <returns></returns>
        public static int devolver_ficha_Tratador(string nif)
        {
            for (int i = 0; i < numero_Tratadores; i++)
            {
                if (string.Compare(nif, tratadores[i].Cartao_Cidadao) == 0)
                {
                    Console.WriteLine("\n===========================================================");
                    Console.WriteLine("\nNome: " + tratadores[i].Nome);
                    Console.WriteLine("\n-> Genero: " + tratadores[i].Sexo);
                    Console.WriteLine("\n-> Idade: " + tratadores[i].Idade);
                    Console.WriteLine("\n-> Nº Cartao Cidadao: " + tratadores[i].Cartao_Cidadao);
                    Console.WriteLine("\n-> Morada: " + tratadores[i].Morada);
                    Console.WriteLine("\n-> Data de Nascimento: " + tratadores[i].DataNasc);
                    Console.WriteLine("\n-> Municipio: " + tratadores[i].Municipio);
                    Console.WriteLine("\n===========================================================");

                    break;
                }                
            }

            return 0;
        }

        #endregion
    }
}
