# 📑 Semana 11 e 12 - Avaliação Final

Avaliação final do plano de estágio.

## 💻 Configuração 💻

1. Baixe o repositório zipado ou clone pelo caminho do git em um repositório local.
2. Execute a ddl para criação do banco de dados db_TorneioLuta com a tabela Lutadores e os seus valores iniciais.
3. A aplicação já poderá ser executada após essa configuração.

## 🥊 Torneio de Luta - Super Web Fighter 🥋

A aplicação consiste em um torneio de luta que segue alguns critérios de vitória e desempate. O usuário deve escolher entre 16 lutadores dos 32 disponíveis na tela inicial para batalharem entre si. Também há a opção de escolher aleatoriamente clicando no botão no canto superior direito acima dos cards.
Depois dos 16 lutadores serem selecionados, o botão para iniciar o Torneio irá aparecer para começar o combate.

<img src="/docs/tela-inicial.gif"/>

## ⚔️ Critérios de vitória, derrota e desempate ⚔️

1. Primeiramente, os lutadores serão organizados em ordem crescente de idade, o mais jovem contra o segundo mais jovem, e assim sucessivamente.
2. O lutador que possui a primeira vantagem é aquele com o maior percentual de vitórias, calculado com a seguinte fórmula: vitórias / total de lutas * 100.
3. Caso os lutadores possuam o mesmo percentual de vitórias, o segundo critério será pelo número de artes marciais que aquele lutador conhece, quem conhecer mais irá vencer o duelo.
4. Mas caso os dois lutadores ainda estejam empatados após esses critérios, aquele que já lutou mais vezes é quem ganha, ou seja, quem tiver o maior número de lutas feitas.
5. Ainda assim, como último fator, caso nenhum desses seja suficiente para o desempate, o sistema irá escolher aleatoriamente entre os dois lutadores, de uma forma justa, já que o torneio é mata mata e não ão permitidos empates, É UMA LUTA ATÉ A MORTE.

## 🏆 Tela de Vencedor 🏆

A tela do vencedor aparece após 3 segundos que o torneio for iniciado, e nessa tela irá mostrar toda a tragetória dos lutadores, quem foi ficando para trás e quem foi o grande vencedor.

<img src="/docs/tela-vencedor.png"/>

## 💾 Customização 💾

É possível adicionar seus próprios lutadores ao campeonato, basta editar o arquivo de banco de dados para adicionar as características que deseja, e uma imagem do lutador na pasta wwwroot/img/Fighters 🔎 Vale ressaltar que o nome da imagem precisa ser renomeado para o número do card do lutador, o seu ID.

# ☘ Obrigado!!
