# CRMALL

Foi desenvolvido um CRUD para um cadastro de clientes, dividido em duas partes:
- Backend - API responsável pela regra de negócio
- Frontend - Interface com o usuário que faz a comunicação com a API criada

# 1. Instruções para configuração do ambiente:

1.1. Ao abrir o projeto do frontend, executar o seguinte comando para baixar os pacotes necessários:

node install --save

1.2. Caso a API (backend) esteja rodando em um endereço diferente de http://localhost:49306/api 
as constantes serverHost (presentes em controller.js e client.js) devem ser alteradas para o novo caminho

# 2. Melhorias:

2.1. O correto seria criar uma tabela de cidades, afim de normalizar o banco de dados. 
Porém, no enunciado do exercício pedia para os dados estivessem na tabela de clientes

2.2. A verificação de obrigatoriedade dos campos poderia ser feita de forma mais efetiva usando anottations na classe controller da API, 
porém, isso obrigaria que esses campos fossem preenchidos em todas as requisições

# 3. BÔNUS!
3.1. Dentro da pasta BONUS! Encontra-se um projeto desenvolvido em Delphi (Firemonkey) que faz a comunicação com a API criada.
O projeto pode ser compilado para de forma nativa para Windows (x86), Windows (x64), Linux, Android e iOS

Tecnologias utilizadas: C#.NET, NodeJS, AngularJS, Delphi (Firemonkey)
