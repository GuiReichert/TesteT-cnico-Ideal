# &nbsp;&nbsp;Comentários e Explicações Breves Sobre o Projeto

&nbsp;&nbsp;Bom dia, se você está lendo isso, provavelmente está avaliando este programa para o teste de código, então bem vindo! Aqui neste README, 
vou explicar como o programa funciona e porque tomei certas decisões, mas provavelmente irei acabar esquecendo algo, então caso 
haja alguma dúvida com relação à minha linha de raciocínio ou código, fique à vontade para entrar em contato comigo. Tendo dito isso,
eu gostaria de comentar algumas coisas antes de explicar o código:

1) O meu conhecimento é um pouco mais aprofundado no Backend do que no Frontend, então provavelmente será possível notar uma discrepância
   entre as duas partes, principalmente no que tange à arquitetura do projeto.
2) Apesar do escopo do projeto, eu tentei ao máximo estruturá-lo de forma limpa e concisa, me baseando nos conceitos de clean code, SOLID,
   inversão de controle, injeção de dependência, e etc.
3) Neste projeto eu fiz algumas implementações a mais do que o pedido no teste porque eu queria aproveitar essa oportunidade para utilizar
   e demonstrar ao máximo possível todo o meu conhecimento adquirido até agora. Apesar disso, ainda existem algumas lacunas no meu conhecimento
   com relação a certas coisas e eu comentarei sobre elas na explicação do código.

&nbsp;&nbsp;Com isso dito, vamos à explicação do projeto em si. Essa aplicação é um sistema de operações de CRUD para o cadastro de um usuário com Id, Nome,
Sobrenome e Número de Telefone. O Frontend é feito em WPF e se comunica com a API através de um `HTTPClientFactory`. A API por sua vez, se comunica
diretamente com um banco de dados do SQL Server para fazer as requisições e realizar as operações de CRUD. O deploy tanto da API quanto do banco de 
dados foi feito no Azure, e a API pode ser verificada através do seguinte [link](https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net) (verificar os Endpoints).
Por isso, para testar o app, basta rodar a solução em WPF que já está automaticamente conectada com a API. Os Endpoints serão explicados mais para 
frente na parte do Backend, mas é importante comentar que, por algum motivo, aparentemente o site às vezes não abre ou fica fora do ar por alguns momentos,
fazendo com que às vezes, quando o programa WPF é aberto, nenhum dado da API ou dos cadastros seja processado. Caso isso aconteça, talvez seja interessante 
esperar alguns segundos ou abrir e fechar o programa.

&nbsp;&nbsp;Continuando para o projeto em si, ele é dividido em três soluções diferentes a API e Testes Unitários no Backend, e o programa em WPF no Frontend:



  # &nbsp;&nbsp;Backend - API

&nbsp;&nbsp;Eu desenvolvi a API usando o padrão Repository, juntamente com o UnitOfWork, e ela foi feita inteiramente usando interfaces e injeção de dependência para
melhorar a modularidade e manutenibilidade do código. A interface `IUserRepository` lida com a comunicação e operações de CRUD no banco de dados, enquanto o
`IUnitOfWork` lida apenas com a persistência das alterações no banco de dados (SaveChanges). Os controllers lidam com as requisições HTTP de GET,POST,PUT e DELETE,
e chamam a interface `IUserService`, que por sua vez é encarregada de realizar o mapeamento das entidades caso necessário, implementar as regras de negócio e por
fim chamar o `IUserRepository` e `IUnitOfWork` para realizar as requisições no banco de dados. Neste projeto em específico, não existia nenhuma regra de negócio, então
o `IUserService` não possui uma responsabilidade muito grande, porém, em projetos maiores e mais complexos, ele auxiliaria a deixar as responsabilidades mais separadas,
então por isso, decidi implementá-lo.

&nbsp;&nbsp;Neste programa eu também implementei um sistema de tratamento de exceções personalizadas para os possíveis erros esperados nos endpoints. Caso as requisições sejam
feitas com sucesso, será retornado o `StatusCode 200` (como todas as requisições neste aplicativo em específico serão feitas dentro do WPF, eu decidi que até a operação de Create
devolveria um `Status 200`, mas em casos normais, eu retornaria o `StatusCode 201`). Em caso de erros, se o erro já foi tratado e esperado, ele retornará um
`StatusCode 400`, entretanto, caso seja um problema não esperado e não tratado, por questões de segurança e para evitar que o usuário receba informações indevidas, eu decidi
que nesses casos, o programa deveria retornar um `StatusCode 500` com apenas uma mensagem de erro inesperado. Nesse caso, o ideal seria implementar também um sistema de Logging
para que possamos verificar tais erros durante o desenvolvimento do programa, entretanto, eu não tive tempo para introduzir essa feature.

&nbsp;&nbsp;A seguir está uma explicação básica dos Endpoints e de como acessá-los:

`GET` - "https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net/user/{id}"        
  Devolve os dados do cadastro do usuário com Id especificado
  
`GET` - "https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net/user/"            
  Devolve os dados de TODOS os cadastros no banco de dados
  
`POST` - "https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net/user/"            
  Adiciona o cadastro de um usuário no banco de dados
  
`PUT` - "https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net/user/{id}"         
  Altera os dados do cadastro do usuário com o Id especificado (Recebe dois parâmetros, o UserDTO e o Id do usuário a ser alterado)
  
`DELETE` - "https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net/user/{id}"      
  Deleta os dados do cadastro do usuário com o Id especificado

  # &nbsp;&nbsp;Backend - Testes Unitários

&nbsp;&nbsp;Os testes unitários foram feitos usando o xUnit e o Moq para criar os Mocks das dependências necessárias. Honestamente, eu tenho um certo conhecimento teórico sobre testes unitários,
mas ainda tenho um pouco de dificuldade ao implementá-los na prática, principalmente com relação à implementação das dependências necessárias e dos Mocks de interfaces, mas mesmo assim 
eu decidi implementar pelo menos os testes para os Controllers.

  # &nbsp;&nbsp;Frontend - WPF

&nbsp;&nbsp;Para o Frontend, eu tentei utilizar o padrão MVVM para desenvolver o aplicativo. Entretanto, em partes pelo escopo do aplicativo, mas também por certa falta de conhecimento de arquitetura
de software no Frontend, eu acabei não conseguindo abstrair muito a aplicação, e boa parte da lógica do programa acabou ficando nas próprias Views ao invés das ViewModels. Tendo dito isso, o
aplicativo roda majoritariamente na página principal, onde estão a tabela para visualização dos cadastros e os botões para adicionar, alterar ou deletar um cadastro de usuário. Uma vez que
algum desses botões é clicado, uma nova janela é criada para que o usuário possa fazer o input dos dados (no caso do Add e Update) ou confirmar a deleção do cadastro (no caso do Delete). Eu 
também decidi implementar uma feature para procurar um cadastro por um Id específico.
  
  # &nbsp;&nbsp;Deploy

&nbsp;&nbsp;Sinceramente, essa foi a primeira vez que eu realizei o deploy de uma aplicação, então tive que pesquisar como fazer, mas no final eu acabei ficando muito feliz por ter conseguido.



# &nbsp;&nbsp;Fim

&nbsp;&nbsp;E aqui acaba este README. Muito obrigado por aguentar até aqui e espero não ter causado muitas dores de cabeça com o código (kkkk). Novamente, fico à disposição para responder quaisquer
dúvidas ou receber feedbacks com relação ao teste.
