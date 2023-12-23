# TaskAPI 
***Pausado - Acho que aprendi o básico da Minimal API. Decidi começar a estudar o WebAPi agora, baseado em controllers***

<p>A TaskAPI é responsável por criar e salvar tarefas, implementando um CRUD para que as <strong>Tasks</strong> sejam salvas no Back-end! O sistema foi desenvolvido utilizando <strong>ASP.NET</strong>, em específico, o template de <strong>Minimal API</strong>, utilizando o <strong>C#</strong> para o seu desenvolvimento!</p>

## Execução
<p>
Para executar o projeto, basta você usar o <code>git clone https://github.com/hav0kinho/TaskAPI.git</code>

Com o código na sua máquina, basta você entrar no diretório da aplicação com um <code>cd TaskAPI</code> e então utilizar <code>dotnet build</code> para instalar os pacotes e preparar o código para execução. Após isso, basta usar o <code>dotnet run</code> para inicializar a API.
</p>

## Features
A feature implementada é o CRUD das tasks, permitindo os usuários criarem, excluirem, buscarem e atualizarem as Tasks usando as rotas! As principais rotas são as seguintes:

<code>GET - /api/task</code> -> Retorna todas as Tasks.
<code>GET - /api/task/{id:int}</code> -> Retorna uma Task específica, dado o Id dela.
<code>POST - /api/task</code> -> Cria uma Task e salva no banco, deve receber os dados da Task.
<code>PUT - /api/task/{id:int}</code> -> Atualiza uma Task baseada no Id dado, deve receber os dados da Task.
<code>DELETE - /api/task/{id:int}</code> ->  Deleta uma Task baseada no Id dado.

**O banco implementado é baseado no InMemory**

## TODO
<p>Aqui está os passos que ainda estou fazendo e as features que ainda estou implementando.<br/>
</p>

**( )** -> *Não Iniciado* <br/>
**(/)** -> *Em Desenvolvimento* <br/>
**(X)** -> *Concluido*

---

* Configuração Inicial (X)
* Criação de rotas (X)
    * **GET** Tasks (X)
        * Criação da rota(X)
        * Criação de validações () -> Precisa?
    * **POST** Tasks (X)
        * Criação da rota(X)
        * Criação de validações (X)
    * **PUT** Tasks (X)
        * Criação da rota(X)
        * Criação de validações (X)
    * **DELETE** Tasks (X)
        * Criação da rota(X)
        * Criação de validações (X)
* Configuração do *Swagger* (X)
* Documentação aprofundada da API (X)
* Configuração do AutoMapper para facilitar mapeamento de objetos e manutenabilidade(X)
* Criação de validações com o FluentValidation (X)
* Criação de DTO para a TaskModel (X)
* Configuração de Base de dados (/)
    * <s>Baseada em Classe (/)</s>
    * Baseada em InMemory (X)
    * Baseada em SQL Server ()



