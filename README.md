
<img src="https://img.shields.io/static/v1?label=Status&message=Finished&color=FFCB05&style=lat-square&logo=unity"/> <img src="https://img.shields.io/static/v1?label=Version&message=v1.2&color=FF3333&style=lat-square&logo=unity"/> <img src="https://img.shields.io/static/v1?label=License&message=MIT&color=33DD33&style=lat-square&logo=unity"/>    

<br>

<h1 align="center"> Geometry Wave </h1>

<p align="center">Geometry Wave é um jogo arcade de tiro centrado na atualização da nave espacial do usuário.
</p>    

<br>
<br>

<p align="center">
 <a href="#objetivo">Objetivo</a> •
 <a href="#historia">História</a> •
 <a href="#desenvolvimento">Desenvolvimento</a> •
 <a href="#acessibilidades">Acessibilidades</a> •
 <a href="#status">Status do Projeto</a> •
 <a href="#funcionamento">Features</a> • 
 <a href="#tecnologias">Tecnologias</a> • 
 <a href="#autor">Autores</a> 
</p>


<h2 id="objetivo">Objetivos</h2>
<p>Geometry Wave é um jogo que está sendo desenvolvido por uma equipe de alunos do curso de Desenvolvimento de Sistemas do COLTEC-UFMG, cujo foco é o desenvolvimento de um jogo acessível para pessoas que apresentem algum tipo de deficiência motora, visual ou cognitiva.</p>
<p>Nosso objetivo era criar um rogue-like shooter, onde o player enfrentaria ondas de inimigos com uma dificuldade gradual, enquanto upava seu nível e tentava sobreviver o maior tempo possível.</p>

<br>
<br>

<h2 id="historia">História</h2>
<p>Geometry Wave se passa em um cenário pós-apocalíptico em que, após uma guerra intergalática, máquinas auto-replicadoras foram criadas. Essa guerra trouxe destruição a grande parte do cosmos resultando na quase extinção da raça humana, sobrando apenas as máquinas espalhadas pelo universo com o único propósito de destruir vida inteligente. Assim, três anos após a guerra, o personagem que os jogadores irão controlar acorda, um comandante líder de uma nave capaz de se aprimorar conforme coleta partes de outras naves. A partir desse ponto a história se baseia em como o jogador irá melhorar a sua máquina para sobreviver o máximo de tempo possível.</p>

<br>
<br>

<h2 class="desenvolvimento">Desenvolvimento</h2>
<h3>Versão v1.0 - Desenvolvendo a base do jogo</h3>
<p>Primeiramente no projeto, nós definimos como seria a jogabilidade e começamos a desenvolvê-la, desenvolvemos então a movimentação do player, as funções gerais, como atirar e receber dano, e a geração das ondas de inimigos.</p>
<br>

<h3>Versão v1.1 - Desenvolvendo o multiplayer LAN e adicionando as acessibilidades</h3>
<p>Com a base da jogabilidade criada, nos focamos a terminar algumas features que ainda faltavam. Então, nos focamos em desenvolver o multiplayer do jogo, para implementá-lo utilizamos o Unity Netcode, que é um conjunto de bibliotecas que fazem interface entre o programador e a parte mais crua da programação de rede. Após isso, definimos que as acessibilidades do jogo são focadas em pessoas com deficiências motoras e visuais, e então adicionamos elas ao jogo.</p>

<br>

<h3>Versão v1.2 - Ajustes finais</h3>
<p>Nessa última parte desenvolvemos as últimas features que faltavam no jogo. Fizemos a movimentação dos inimigos baseando no comportamento de um algoritmo de Flock, para que os inimigos não seguissem diremente para o player, tornando o comportamento dos inimigos um pouco menos previsível. Depois adicionamos o boss e fizemos o sistema de level-up. Por fim, desenvolvemos o ajuste de níveis, que permite que o jogador selecione o nível de dificuldade, o que implica no dano dos inimigos e na quantidade de inimigos gerados.</p>

<br>
<br>

<h2 id="acessibilidades">Acessibilidades</h2>
<p>As acessibilidades do jogo são focadas em pessoas com deficiências motoras e visuais:</p>

<h3>Deficiências motoras</h3>
<ul>
    <li>O jogo conta com opções de dificuldade que diminuem a velocidade, vida e quantidade dos inimigos, além de aumentar a vida do jogador. Dessa forma, a necessidade de reflexos rápidos e agilidade com as mãos pode ser drasticamente reduzida.</li>
    <li>Existem opções de mira e tiro automáticos que tornam o jogo mais acessível para pessoas com condições que causam dificuldade no uso do mouse - por exemplo, pessoas com artrite podem sofrer desconforto com o movimento repetitivo, e pessoas com movimento reduzido nas mãos podem não conseguir fazer os movimentos precisos necessários para usar o equipamento.</li>
</ul>
<br>

<h3>Deficiências visuais</h3>
<ul>
    <li>O jogador pode configurar completamente as cores de todos os elementos do jogo através de um color picker no menu de opções, permitindo que a experiência visual seja customizada pelo próprio indivíduo. Além disso, serão disponibilizadas algumas configurações de cor pré-prontas para maior conveniência, como alto contraste e conjuntos diferentes de cores para daltônicos.</li>
    <li>A câmera do jogo pode ser configurada de forma a reduzir ou amplificar o zoom para facilitar a visibilidade.</li>
    <li>O jogo emite efeitos sonoros que sinalizam eventos importantes, como o início de uma nova onda ou um evento de level-up.</li>
    <li>Todo item de menu contém um símbolo que representa sua função. De tal forma, pessoas com dislexia e outras dificuldades de leitura de texto podem interagir com a interface de usuário sem precisar ler.</li>
</ul>
<br>

<h3>Deficiências cognitivas</h3>
<ul>
    <li>O jogador pode configurar os controles da forma que preferir, podendo escolher as teclas que forem mais confortáveis para eles. Além disso, o jogo aceita outros tipos de controle além do teclado e do mouse, podendo ser jogado com controles adapatados. Dessa formas pessoas com problemas motores, como Parkinson por exemplo, ou algum tipo de problema que exija o uso de um controle customizado podem melhorar a sua experiência ao jogar.</li>
</ul>
<br>

<h2 id="funcionamento">Funcionamento</h2>
<p></p>
<ul>
    <li>Menu</li>
    <li>Mecanicas</li>
</ul>
<br>

<h3>Menu</h3>
O menu inicial possui 3 botões disponíveis, sendo eles:

**Jogar:** Abre o menu de jogo.

**Config:** Abre um menu de opções.

**Sair:** Fecha o jogo no modo editor e no apk.

Além disso, nele temos um video de fundo, em loop, da gameplay do jogo.

<img src="https://github.com/EduardoBirchal/Geometry-Wave/assets/85091282/3c93aa62-5111-4640-a1e9-7f7b06e91bae" width="650px"/>


<h4>Menu jogar</h4>
No menu de jogo, temos 2 botões:

**Solo**: Permite que selecionemos a dificuldade do jogo entre quatro dificuldades e inicia o jogo single-player.

**Online**: Permite que o jogador escolha entre criar uma sala ou entrar em uma sala para jogar multi-player.

<br>
<h4>As Opções</h4>

Temos mais 3 botões:

**Gameplay**: Opções que modificam a jogabilidade, como por exemplo a sensibilidade do mouse.

**Sons**: Mudança nos sons do jogo, como o volume geral, da musica e efeitos.

**Gráficos**: Opções gráficas como modo daltonismo e tamanho da hud.
<br>
<br>


<h3>Mecânicas do jogo</h3>

**Movimentação do Player**: O player se movimenta baseando nas teclas pressionadas, 'w' para cima, 'a' para esquerda, 's' para baixo, 'd' para direita, além de possuir o dash que é uma habilidade de impulso para onde o player está se movendo. 

**Movimentação dos inimigos**: Os inimigos possuem uma movimentação baseada no algoritmo de flocking, onde os inimigos se movimentam com base no contexto atual, levando em consideração os inimigos próximos e o player, fazendo com que os inimigos não se movimentem diretamente para o player.

**Boss**: O boss é um inimigo especial spawnado a cada 10 levels, possuindo algumas mecânicas especiais como: ele possui três barreiras giratórias que o defendem, em seu 1° padrão de ataque ele atira uma rajada de balas direcionada aos players próximos, já no segundo ele atira uma linha de balas de forma circular.

**Sistema de Level-Up**: O player adquire experiência ao matar os inimigos, após uma determinada quantidade de experiência o player pode upar de nível, permitindo que ele selecione uma melhoria dentre as seguintes: aumento de vida, cooldown das balas, precisão da arma, dano da bala, cooldown do dash.

<br>
<br>

<h2 id="status">Status do Projeto</h2>
<p>O projeto se encontra concluído e está atualmente na sua versão 1.2 .</p>

<br>
<br>

<h2 id="tecnologias">Tecnologias</h2>
<h3>As seguintes ferramentas foram usadas na construção do projeto:</h3>


<h4>Linguagens</h4>
<a href="https://learn.microsoft.com/pt-br/dotnet/csharp/">C#</a>
<br>

<h4>IDE's</h4>
<a href="https://docs.unity.com/">Unity</a>
<br>

<h4>Referências</h4>
<a href="https://www.youtube.com/watch?v=mjKINQigAE4&list=PL5KbKbJ6Gf99UlyIqzV1UpOzseyRn5H1d">Vídeo do Canal Board to Bits da implementação do algoritmo de flocking.</a>

<br>
<br>
<br>

<h2 id="autor">Autores</h2>

| [<img src="https://avatars.githubusercontent.com/u/85091282?v=4" width=115><br><sub>Arthur Henrique Chaves</sub>](https://github.com/AHChaves) |  [<img src="https://avatars.githubusercontent.com/u/81756816?v=4" width=115><br><sub>Thales Davi</sub>](https://github.com/ThalesDaviSouza) |  [<img src="https://avatars.githubusercontent.com/u/84411590?v=4" width=115><br><sub>Erick Pedrosa</sub>](https://github.com/ErickPedrosa) | [<img src="https://avatars.githubusercontent.com/u/104566878?v=4" width=115><br><sub>Caio Cesar</sub>](https://github.com/CostaCesar) | [<img src="https://avatars.githubusercontent.com/u/104524053?v=4" width=115><br><sub>Eduardo Birchal</sub>](https://github.com/EduardoBirchal) |
| :---: | :---: | :---: | :---: | :---: |


<br>
<br>
