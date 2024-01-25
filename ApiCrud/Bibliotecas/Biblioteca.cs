namespace ApiCrud.Bibliotecas;

public class Biblioteca
{
    public Guid Id { get; init; }
    public string Nome { get; private set; }
    public string Livro {  get; private set; }
    public string Dia { get; private set; }
    public string Devolucao { get; private set; }
    public bool Ativo { get; set; }

    public Biblioteca(string nome, string livro, string dia, string devolucao)
    {
        Nome = nome;
        Livro = livro;
        Dia = dia;
        Devolucao = devolucao;
        Id = Guid.NewGuid();
        Ativo = true;
    }

    public void AtualizarConteudo(string devolucao)
    {
        Devolucao = devolucao;
    }
}
