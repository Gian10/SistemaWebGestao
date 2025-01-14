# Sistema Web de Gestao
Objetivo: Desenvolver a base de um Sistema Web de Gestão que permitirá à empresa gerenciar usuários e agendar tarefas de forma eficiente.

[Funcionalidades]

Autenticação de Usuários

Campos necessários: • Nome Completo • Data de Nascimento • Telefone Fixo • Celular • E-mail • Endereço • Foto do Usuário • Identificação dos usuários gestores, que terão acesso ao cadastro de novos usuários.

Login: • Acesso ao sistema através de e-mail e senha. • Criação de um usuário padrão para o gestor operacional com as seguintes credenciais: • E-mail: oportunidades@smn.com.br • Senha: teste123

Agendamento de Tarefas: gestores poderão cadastrar tarefas para seus subordinados.

Campos da tarefa: Mensagem e Data Limite para Execução.

Monitoramento de Tarefas: Gestores poderão acompanhar o andamento das tarefas atribuídas.

Notificações por E-mail: • Subordinados receberão um e-mail sempre que uma nova tarefa for cadastrada para eles. • Gestores receberão um e-mail quando uma tarefa for finalizada pelo usuário.

Requisitos Técnicos (Tecnologias Disponíveis) • Frontend: HTML, CSS, JavaScript, React, Tailwind CSS, Tailwind UI Components, UI Kit, Razor, jQuery. • Backend: C#. • Banco de Dados: SQL Server, PostgreSQL, MongoDB. • Controle de Versão: GitHub. • Plataforma de Hospedagem: Heroku ou similar (sugestões são bem-vindas). • Documentação: README.md - Incluir um passo a passo detalhado para execução da aplicação em outras máquinas. • Configuração do Banco de Dados: Instruções completas para a criação e configuração do banco de dados utilizado na aplicação.

# Configuração do Ambiente
  # Comando para instalar as dependencias do .net:
    #dotnet restore
  # Comandos para criar o banco no SQLSERVE usando o SSMS:
    #Add-Migration migrationInicial
    #Update-Database
  # Foi usando RDS para a aplicação consumir a banco, já que o Heroku não oferece um serviço de sqlServe:
    #A instancia do banco RDS está no arquivo appsettings.json com as credenciais
# Git
  #git clone https://github.com/Gian10/WebGestao.git
