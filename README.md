# Nom

Nom foi um jogo desenvolvido para a disciplina Inteligência Artificial Para Jogos I, onde o objetivo do jogador é chegar até o canto inferior direito do labirinto, porém, há vários inimigos no caminho! Utilize as bombas coletadas para derrotá-los e liberar o caminho!!!

## Implementação do Labirinto

O labirinto desse jogo é gerado proceduralmente a cada rodada, logo, oferece uma experiência de gameplay diferente a cada partida.

## O Comportamento dos Inimigos

O inimigo, se estiver *numa certa distância* do jogador, ficará parado, mas caso o jogador entre no alcance do inimigo, o inimigo irá perseguir o jogador.
Caso o inimigo esteja perseguindo o jogador, e o jogador conseguir sair do alcance do inimigo, o inimigo para de perseguir o jogador e volta ao comportamento de espera.

O diagrama a seguir mostra a árvore de comportamento:
![Diagrama da árvore de comportamento](/arvore.png "Árvore de comportamento").

Mas como o inimigo detecta o quão longe ele está do jogador? Simples. O inimigo está efetuando a cada atualização o algorítmo A*, assim sendo capaz de saber o quão longe o jogador está dele com precisão.

Além disso, enquanto o inimigo segue o jogador, foi aplicado o comportamento de navegação "Arrive", não só na chegada do inimigo ao player, mas também na chegada dele em cada uma das células detectadas pelo A*, visto que ele está sempre se movendo à primeira célula mais próxima encontrada pelo pathfinding.

## Controles
Para jogar o jogo, é necessário um teclado. Os comandos do jogo estão na seguinte tabela:
|Ação |Controles
|-----|----------
|Andar|Setas do teclado ou W,A,S,D
|Atacar|Barra de espaço

## Onde jogar?

O jogo está disponível [nessa página](https://ariaronis.itch.io/nomnom), ele pode ser jogado pelo navegador!
