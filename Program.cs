using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestaodeCliente
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string Nome;
            public string Email;
            public string Cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;

            while (!escolheuSair)
            {

                Console.WriteLine("Bem Vindo ao Sistema de Clientes!\n");
                Console.WriteLine("1) Listagem\n2) Adicionar\n3) Remover\n4) Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;

                    case Menu.Adicionar:
                        Adicionar();
                        break;

                    case Menu.Remover:
                        Remover();
                        break;

                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();

            }

        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Clientes: ");
            Console.Write("Nome do cliente: ");
            cliente.Nome = Console.ReadLine();
            Console.Write("E-mail do cliente: ");
            cliente.Email = Console.ReadLine();
            Console.Write("Cpf do cliente: ");
            cliente.Cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro Concluido!!!\nAperte ENTER para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if(clientes.Count > 0)
            {
                Console.WriteLine("Lista de Clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.Nome}");
                    Console.WriteLine($"E-mail: {cliente.Email}");
                    Console.WriteLine($"Cpf: {cliente.Cpf}");
                    Console.WriteLine("============================");
                    i++;
                }

            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado no momento!");
            }
            Console.WriteLine("Aperte ENTER para sair.");
            Console.ReadLine();

        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que deseja remover!");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id digitado é inválido!!!\nTente novamente.");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            /* criamos entao uma variavel chamada "stream" que vai receber como valor
             * o arquivo com o nome de ""clients.dat", ele vai tentar abrir o arquivo e
             * se o arquivo nao existir ele vai criar um novo.*/
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            /* Depois é criado uma variavel do tipo BinaryFormatter onde vamos utilizala para
             * salvar os dados em formato binario.*/
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);
            stream.Close();

        }
        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            {
                

                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();


            }

            stream.Close();

        }


    }

}