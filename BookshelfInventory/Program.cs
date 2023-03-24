using System;
using System.Net.Http.Headers;
using System.Reflection;
using BookshelfInventory;

List<Book> bookshelf = new List<Book>();
if (File.Exists("Bookshelf.bks"))
    bookshelf = ReadFile("Bookshelf.bks");
do
{
    Book found;
    switch (Menu())
    {
        case "1":
            bookshelf = WriteFile(CreateBook());
            Console.WriteLine("Livro adicionado com sucesso!");
            break;

        case "2":
            break;

        case "3":
            found = FindBook();
            if (found == null)
                Console.WriteLine("Título inválido. Por favor tente novamente.");
            else
            {
                bookshelf.Remove(found);
                UpdateFile("Bookshelf.bks");
                Console.WriteLine("Livro removido com sucesso!");
            }
            break;

        case "4":
            break;

        case "5":
            if (PrintBookshelf(bookshelf))
                Console.WriteLine("Não há nenhum livro na estante.");
            break;

        case "6":
            break;

        case "7":
            break;


        case "8":
            break;

        case "9":
            Console.WriteLine("Encerrando...");
            Thread.Sleep(1000);
            System.Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }
    Console.Write("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
} while (true);

Book CreateBook()
{
    bool p;
    string title = VerifyString('o', "Título");
    string edition = VerifyString('a', "Edição");
    string author = VerifyString('o', "Autor");
    string description = VerifyString('a', "Descrição");
    long isbn = VerifyIsbn();
    int pages;
    do
    {
        p = int.TryParse(VerifyString('o', "número de páginas"), out pages);
        if (p == false)
        {
            Console.WriteLine("Páginas devem ser um número inteiro");
        }
    } while (!p);
    Book book = new(title, edition, author, description, isbn, pages);

    return book;
}

Book FindBook()
{
    Console.WriteLine("Informe o título do Livro: ");
    var n = Console.ReadLine();
    foreach (var item in bookshelf)
    {
        if (item.Title.Equals(n))
        {
            return item;
        }
    }
    return null;
}

long VerifyIsbn()
{
    long isbn;
    string converter;
    bool inputguide = true;
    bool length;
    int aux = 0;

    do
    {
        length = false;

        if (aux > 0)
        {
            Console.WriteLine("O ISBN é um número e deve ter 10 ou 13 dígitos.");
        }

        converter = VerifyString('o', "ISBN");
        if (converter.Length == 10 || converter.Length == 13)
            length = true;

        inputguide = long.TryParse(converter, out isbn);
        aux++;
    } while (!(length && inputguide));
    return isbn;
}

string Menu()
{
    Console.WriteLine("\t>>>>> MENU <<<<<\n1 - Inserir Livro na Estante\n2 - Editar Livro\n3 - Deletar Livro" +
        "\n4 - Inserir sessão de leitura de um Livro\n5 - Mostrar Estante\n6 - Emprestar Livro da Estante\n7 - Retornar Livro a Estante" +
        "\n8 - Mostrar Livros emprestados\n9 - Sair\n\nEscolha uma opção: ");
    return Console.ReadLine();
}

string VerifyString(char article, string variable)
{
    string verified;
    bool aux = true;

    do
    {
        Console.Write($"Informe {article} {variable}: ");
        verified = Console.ReadLine();
        aux = string.IsNullOrEmpty(verified);
        if (aux)
            Console.WriteLine($"{variable} inválid{article}");
    } while (aux);

    return verified;
}

List<Book> WriteFile(Book book)
{
    List<Book> temp = new();
    try
    {
        if (File.Exists("Bookshelf.bks"))
        {
            temp = ReadFile("Bookshelf.bks");
            temp.Add(book);
            StreamWriter sw = new("Bookshelf.bks");
            foreach (var item in temp)
            {
                sw.WriteLine(item.ToString());
            }
            sw.Close();
        }
        else
        {
            temp.Add(book);
            StreamWriter sw = new("Bookshelf.bks");
            sw.Close();

        }
    }
    catch (Exception)
    {
        throw;
    }
    return temp;
}

List<Book> UpdateFile(string file)
{
    List<Book> temp = new();
    try
    {
        temp = ReadFile(file);
        StreamWriter sw = new(file);
        foreach (var item in temp)
        {
            sw.WriteLine(item.ToString());
        }
        sw.Close();
    }
    catch (Exception)
    {
        throw;
    }
    return temp;
}

List<Book> ReadFile(string f)
{
    string[] aux = new string[8];
    List<Book> update = new List<Book>();
    Book insert;

    try
    {
        var verify = "";
        StreamReader sr = new(f);

        while (verify != null)
        {
            verify = sr.ReadLine();
            if (verify == null)
            {
                sr.Close();
                return update;
            }
            else
            {
                aux = verify.Split("|");
                insert = new(aux[0], aux[1], aux[2], aux[3], long.Parse(aux[4]), int.Parse(aux[5]), int.Parse(aux[6]), bool.Parse(aux[7]));
                update.Add(insert);
            }
        }
        sr.Close();
    }
    catch (Exception)
    {
        throw;
    }
    return update;
}

bool PrintBookshelf(List<Book> printbook)
{
    int i = 0;
    bool empty = true;
    string[] aux = new string[8];
    foreach (var item in printbook)
    {
        string obj = item.ToString();
        aux = obj.Split("|");
        Console.WriteLine($"Título: {aux[0]}\nEdição: {aux[1]}\nAutores: {aux[2]}\nDescrição: {aux[3]}\nISBN: {aux[4]}\nPáginas lidas: {aux[6]} de {aux[5]}\n------------------{i}\n");
        empty = false;
        i++;
    }
    return empty;
}