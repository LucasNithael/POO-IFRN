using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

static class NLivro{
  private static List<Livro> livros;
  public static void Inserir(Livro l){
    livros = Abrir();
    int id = 0;
    foreach(Livro i in livros){
      if(i.Titulo.ToUpper() == l.Titulo.ToUpper())
       throw new NullReferenceException("Livro já Cadastrado"); 
    }
    foreach(Livro i in livros)
      if(i.Id > id) id = i.Id;
    id++;
    l.Id = id;
    livros.Add(l);
    Salvar(livros);
  }
  public static List<Livro> Listar(){
    livros = Abrir();
    if(livros.Count == 0) throw new NullReferenceException("Não existe Livros");
    else return livros;
  }
  public static void Atualizar(Livro l){
    Livro x = Pesquisar(l.Id);
    x.Titulo = l.Titulo;
    Salvar(livros);
      
  }
  public static void Excluir(Livro l){
    Livro x = Pesquisar(l.Id);
    if(x != null){
      livros.Remove(x);
      Salvar(livros);
    }
  }
  public static Livro Pesquisar(int id){
    foreach(Livro i in Listar())
      if(i.Id == id) return i;
    return null;
  }

  public static List<Livro> ListarLivroGenero(Genero g){
    List<Livro> x = new List<Livro>();
    foreach(Livro i in Listar())
      if(i.IdGenero == g.Id) x.Add(i);
    return x;
  }
  
  //Parte dos arquvos
  private static string arquivo = "Arquivos/livros.xml";
  
  private static List<Livro> Abrir() {
    XmlSerializer xml = new XmlSerializer(typeof(List<Livro>));
    StreamReader f = null;
    List<Livro> obj;
    try {
      f = new StreamReader(arquivo);
      obj = (List<Livro>) xml.Deserialize(f);
    }
    catch(FileNotFoundException) {
      obj = new List<Livro>();
    }
    finally {
     if (f != null) f.Close();
    }
    return obj;
  }
  private static void Salvar(List<Livro> obj) {
    XmlSerializer xml = new
      XmlSerializer(typeof(List<Livro>));
    StreamWriter f = new StreamWriter(arquivo, false);
    xml.Serialize(f, obj);
    f.Close();
  }
}