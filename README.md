# Nexxera


### Projeto Crud simulando uma conta bancaria.

Necessidades
- Criar conta virtual
- Realizar débito;
- Realizar crédito;
- Exibir extrato;
- Criar uma interface gráfica que seja possível visualizar o extrato da conta.

Funcionalidades esperadas:
- O sistema pode comportar ao menos 1 conta;
- Cada débito ou crédito deve ter uma descrição que é exibida no extrato.
- A função extrato deve exibir o saldo inicial e final do período, listando as
transações do período.
- Deve ser possível filtrar extrato apenas por crédito ou por débito.
- Deve ser disponibilizada API REST para utilizar o sistema
- Deve ser implementado um front para exibir o extrato da conta



## Informações Técnicas
Projeto foi estruturado em 4 camadas separadas
- Domains
- Repository
- WebApi
- AngularJs

Domains
Camada responsavel pelos objetos da classe.

Repository
Camada responsavel pelas Interface.

WebApi
Camada do serviço Rest API.

AngularJS
Front-End do projeto.

## tecnologias utilizadas
.Net Core 3.1.114
AngularJS

Tecnologias necessárias que seja instalada no computador
dotnet = https://dotnet.microsoft.com/download
nodejs = https://nodejs.org/en/

Como rodar projeto:
1) Em webapi executar comando : dotnet run  
2) Em angularjs executar comando :  http-server -a localhost -p 8000
3) Abrir navegador http://localhost:8000


Acessar documentação api swagger: https://localhost:5001/swagger/index.html