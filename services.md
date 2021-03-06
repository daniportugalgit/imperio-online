#Serviços | RESUMO:

##1) User: Create (register) / Login / Logout
-> Criação de usuários envolve username, senha e imagem
-> A resposta do login deve trazer os dados do usuário + a sua lista de personas

##2) Persona: Create (register) / Load (select)
-> Um usuário pode criar uma nova persona passando apenas NOME e ESPÉCIE.
-> A resposta da seleção de persona deve trazer os dados da Academia Imperial.

##3) Room: Create / AddPlayer / List
-> O servidor gerencia a criação de salas, mas registra cada uma delas no banco.
-> Ao criar uma sala, seu jogo ganha um ID? (a verificar)

##4) Game: Create (finish a match)
-> Quando um jogo termina, o sistema envia o resultado do jogo para o backend.

##5) Research: Start / Invest / Finish / Buy
-> Usuários podem iniciar, desenvolver, finalizar e comprar pesquisas tecnológicas.

##6) Tutorial: Mark Done (não é essencial)
-> Ao finalizar o tutorial, usuário recebe 5 Créditos (uma única vez).

##7) Descobrir os Guloks (não é essencial)

##8) Investir e usar artefatos alienígenas (não é essencial)

Serviços essenciais | DETALHES:

#1) USER
Create: fazer direto no banco
Login: /login [username, password] 
Logout: /disconnect?idUsuario=XXX

Resposta do Login: nas respostas do server para o client, o backend usa ESPAÇO como separador dos dados.
A primeira palavra deve  ser "inicio" e a última palavra deve ser "fim". Sempre é esperado um número PAR de palavras separadas por espaço (chave e valor).

inicio
username
userId
omnis
conhece_g //0 ou 1
qtde_personas //de 0 a 5
id1 //a partir daqui, repetem-se os dados para cada persona, adici9onando o número da persona ao final (id2, id3, etc), sempre após a sequencia completa de 15 params. Ou seja, pk_power_plays1 id2 ...
nome1
jogos1
vitorias1
pontos1
visionario1
arrebatador1
comerciante1
atacou1
creditos1
b_tutorial1
especie1
pk_fichas1
pk_vitorias1
pk_power_plays1
fim

#2) Após pegar o usuário, o sistema busca as pesquisas da persona escolhida:
/academia/buscar?idPersona=XX

A resposta devolve uma sequência de números separados por ESPAÇO. Na ordem, eles representam as seguintes pesquisas:
soldados-defesa-min
soldados-defesa-max
soldados-ataque-min
soldados-ataque-max
tanques-defesa-min
tanques-defesa-max
tanques-ataque-min
tanques-ataque-max
navios-defesa-min
navios-defesa-max
navios-ataque-min
navios-ataque-max
líder1-defesa-min
líder1-defesa-max
líder1-ataque-min
líder1-ataque-max
líder1-skill-1
líder1-skill-2
líder1-skill-3
líder1-skill-4
líder2-defesa-min
líder2-defesa-max
líder2-ataque-min
líder2-ataque-max
líder2-skill-1
líder2-skill-2
líder2-skill-3
líder2-skill-4
líder3-defesa-min
líder3-defesa-max
líder3-ataque-min
líder3-ataque-max
líder3-skill-1
líder3-skill-2
líder3-skill-3
líder3-skill-4
visionario-1
visionario-2
visionario-3
visionario-4
visionario-5
visionario-6
arrebatador-1
arrebatador-2
comerciante
diplomata
intel-1
intel-2
intel-3
pesquisa-em-andamento-id
pesquisa-em-andamento-minerio
pesquisa-em-andamento-petroleo
pesquisa-em-andamento-uranio
pesquisa-em-andamento-lider //não sei o que é
avancada-1
avancada-2
avancada-3
avancada-4
planetas
artifact-1
artifact-2

OU, o usuário pode criar uma nova persona:
/persona/criar?idUsuario=" @ %user.TAXOid @ "&nome=" @ %nome @ "&especie=" @ %especie;

OU, pode-se apagar uma persona
/persona/inativar?idPersona=" @ %persona.TAXOid @ "&idUsuario=" @ %user.TAXOid;

#3) Salas (não encontrei a listagem. Essa arquitetura está estranha)
Criar sala: /sala/adicionar?idPersona=XXX&sala=sala%20YYY&login=username
Adicionar player: /sala/adicionar?idPersona=XXX&idJogo=YYY&login=username
Remover player: /sala/remover?idPersona=XXX&idJogo=YYY&login=username

#4) Jogos
/jogo/finalizar?
idJogo=12
&
idJogoTorque=1
&
duracao=16789
&
idsPersona=1;6;8;
&
creditos=10;6;1;
&
b_vitoria=1;0;0;
&
pontos=8;7;6;
&
b_visionario=1;0;0;
&
b_atacou=0;1;0;
&
b_arrebatador=0;0;1;
&
b_comerciante=1;1;0;
&
onde_saiu=1;3;2; (é o ID interno do Grupo; China == 3, e assim por diante)
&
id_obj1=2;6;23;
&
id_obj2=3;7;24;
&
qts_matou=0;0;1;
&
b_pk_power_play=0;0;0;
&
termino=F;F;S;  //F = finalizou; S = suicidou; R = rendeu;
&
planeta=2

A resposta deve ser "OK"?


#5) Pesquisas

Iniciar:
/academia/iniciar?idPersona= @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&lider=" @ %liderNum @ "&creditos=-" @ %pesquisa.x @ "&idPesqTorque=" @ %myServerPesq.num;
===> /academia/iniciar?idPersona=9&pesq=12&lider=1&creditos=-99&idPesqTorque=12;

Investir:
/academia/investir?idPersona= @ %persona.TAXOid @ "&min=" @ %mineriosInvestidos @  "&pet=" @ %petroleosInvestidos @  "&ura= @ %uraniosInvestidos @ "&creditos=-" @ %creditosInvestidos @ "&idPesqTorque=" @ %myServerPesq.num;
===> /academia/investir?idPersona=9&min=3&pet=3&ura=3&creditos=-10&idPesqTorque=12;

Finalizar:
/academia/finalizar?idPersona= @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&creditos=0&idPesqTorque= @ %myServerPesq.num;
===> /academia/finalizar?idPersona=9&pesq=12&creditos=0&idPesqTorque=12;

Comprar com omnis:
/academia/comprar?idPersona= @ %persona.TAXOid @ "&pesq=" @ %pesquisaIDproTAXO @ "&omnis=-" @ %pesquisa.cOMNI @ "&idPesqTorque=" @ %myServerPesq.num;
===> /academia/comprar?idPersona=9&pesq=12&omnis=-10&idPesqTorque=12;


#6) Tutorial
/persona/tutorial?idPersona= @ %persona.taxoId @ "&creditos=5&idUsuario=" @ %persona.user.taxoId;
Resposta: "OK"

#7) Descobrir Guloks
/conhece_g/ @ %persona.user.nome @ "?idPesqTorque=" @ %serverPesq.num
Resposta: "OK"

#8) Artefatos:
/academia/usar_artefato?idPersona= @ %player.persona.TAXOid @ "&pesq=aca_art_1&idPesqTorque=" @ %myServerPesq.num;

/academia/investir_artefato?idPersona= @ %player.persona.TAXOid @ "&pesq=aca_art_2&qtde_cjs=" @ %player.nexusInvest @ "&idPesqTorque= @ %myServerPesq.num;



------------------------------

# Mais detalhes sobre a academia imperial
# Academia Imperial

## Visão Geral
Na resposta do serviço básico da academia (buscar academia) não usamos chave e valor; Iniciamos a resposta sempre com "dadosAcademia", seguido pelo username. Em seguida, passamos todos os demais valores separados por espaço. Ou seja:
"dadosAcademia" + " " + username + " " + valores dos demais campos separados por espaço (sem chave!)
Vale notar que esta resposta NÃO termina com "fim".

Cada campo possui um ID único (uma string que estou chamando de idDaBase) que deve ser guardado na base, além do seu valor numérico. Os campos tem valor numérico default == 0, com exceção dos campos comentados abaixo.

A seguir, a lista com
TIPO: subtipo :: idDaBase

## Campos
SOLDADO: defesa mínima :: aca_s_d_min //default=1
SOLDADO: defesa máxima :: aca_s_d_max //default=6
SOLDADO: ataque mínimo :: aca_s_a_min //default=1
SOLDADO: ataque máximo :: aca_s_a_max //default=6

TANQUE: defesa mínima  :: aca_t_d_min //default=1
TANQUE: defesa máxima  :: aca_t_d_max //default=12
TANQUE: ataque mínimo  :: aca_t_a_min //default=1
TANQUE: ataque máximo  :: aca_t_a_max //default=12

NAVIO: defesa mínima   :: aca_n_d_min //default=1
NAVIO: defesa máxima   :: aca_n_d_max //default=12
NAVIO: ataque mínimo   :: aca_n_a_min //default=1
NAVIO: ataque máximo   :: aca_n_a_max //default=12

LÍDER1: defesa mínima  :: aca_ldr_1_d_min //default=1
LÍDER1: defesa máxima  :: aca_ldr_1_d_max //default=12
LÍDER1: ataque mínimo  :: aca_ldr_1_a_min //default=1
LÍDER1: ataque máximo  :: aca_ldr_1_a_max //default=12
LÍDER1: skill 1        :: aca_ldr_1_h1
LÍDER1: skill 2        :: aca_ldr_1_h2
LÍDER1: skill 3        :: aca_ldr_1_h3
LÍDER1: skill 4        :: aca_ldr_1_h4

LÍDER2: defesa mínima  :: aca_ldr_2_d_min //default=1
LÍDER2: defesa máxima  :: aca_ldr_2_d_max //default=12
LÍDER2: ataque mínimo  :: aca_ldr_2_a_min //default=1
LÍDER2: ataque máximo  :: aca_ldr_2_a_max //default=12
LÍDER2: skill 1        :: aca_ldr_2_h1
LÍDER2: skill 2        :: aca_ldr_2_h2
LÍDER2: skill 3        :: aca_ldr_2_h3
LÍDER2: skill 4        :: aca_ldr_2_h4

LÍDER3: defesa mínima  :: aca_ldr_3_d_min //default=1
LÍDER3: defesa máxima  :: aca_ldr_3_d_max //default=12
LÍDER3: ataque mínimo  :: aca_ldr_3_a_min //default=1
LÍDER3: ataque máximo  :: aca_ldr_3_a_max //default=12
LÍDER3: skill 1        :: aca_ldr_3_h1
LÍDER3: skill 2        :: aca_ldr_3_h2
LÍDER3: skill 3        :: aca_ldr_3_h3
LÍDER3: skill 4        :: aca_ldr_3_h4

VISIONÁRIO: skill 1    :: aca_v_1
VISIONÁRIO: skill 2    :: aca_v_2
VISIONÁRIO: skill 3    :: aca_v_3
VISIONÁRIO: skill 4    :: aca_v_4
VISIONÁRIO: skill 5    :: aca_v_5
VISIONÁRIO: skill 6    :: aca_v_6
ARREBATADOR: skill 1   :: aca_a_1
ARREBATADOR: skill 2   :: aca_a_2
COMERCIANTE: skill 1   :: aca_c_1
DIPLOMATA: skill 1     :: aca_d_1
INTEL: skill 1         :: aca_i_1
INTEL: skill 2         :: aca_i_2
INTEL: skill 3         :: aca_i_3
ONGOING: id            :: aca_pea_id
ONGOING: minério       :: aca_pea_min
ONGOING: petróleo      :: aca_pea_pet
ONGOING: urânio        :: aca_pea_ura
ONGOING: líder         :: aca_pea_ldr
AVANÇADAS: skill 1     :: aca_av_1
AVANÇADAS: skill 2     :: aca_av_2
AVANÇADAS: skill 3     :: aca_av_3
AVANÇADAS: skill 4     :: aca_av_4
PLANETAS: skill 1      :: aca_pln_1
ARTEFATOS: skill 1     :: aca_art_1
ARTEFATOS: skill 2     :: aca_art_2


## Serviços:

### Buscar:
torque/academia/buscar?idPersona=XX
Resposta: "dadosAcademia " + {username} + " " + dados acima.

### Iniciar pesquisa:
/torque/academia/iniciar ?
idPersona=XX
pesq=idDaBase
lider=XX     //corresponde ao campo {ONGOING: líder}
creditos=-20 //quantos créditos devem ser "somados" para esta persona (o valor vem negativo)
idPesqTorque=idTorque; //valor que deve ser devolvido para o client sem alteração

Resposta: "academiaOK " + {idTorque}

### Finalizar pesquisa:
1) Soma 1 ao valor numérico desta pesquisa na base;
2) Adiciona créditos para esta persona na base, conforme parâmetro recebido;
3) Zera os seguintes campos desta persona na base:
ONGOING: id            :: aca_pea_id
ONGOING: minério       :: aca_pea_min
ONGOING: petróleo      :: aca_pea_pet
ONGOING: urânio        :: aca_pea_ura
ONGOING: líder         :: aca_pea_ldr

"/torque/academia/finalizar ? 
idPersona=XX
pesq=idDaBase
creditos=0 //soma isso aos créditos desta persona; o valor pode vir negativo; normalmente será 0;
idPesqTorque=idTorque

Resposta: "academiaOK " + {idTorque}   (sim, todas as respostas de pesquisas são iguais)


### Investir em pesquisa: Esta chamada NUNCA finaliza uma pesquisa - o client sabe disso.
"/torque/academia/investir ? 
idPersona=XX
min=XX //adiciona este valor ao campo aca_pea_min
pet=XX //adiciona este valor ao campo aca_pea_pet
ura=XX //adiciona este valor ao campo aca_pea_ura
creditos=-20 //adiciona este valor ao campo credits da persona
idPesqTorque=idTorque

Resposta: "academiaOK " + {idTorque}


---------------------------------

O que mais falta?
-> Gravar nas personas os resultados de cada jogo. Ou seja, se a persona ganhou, tem que incrementar as vitórias dela; se fez império, tem que incrementar o campo visionary dela; e assim por diante.

------------------------------------------NÃO Precisa--------------------------------
### Investir em artefato (só existe no terceiro planeta, Telúria, e ainda não lembro como funciona):
"/torque/academia/investir_artefato?idPersona=" @ %player.persona.TAXOid @ "&pesq=aca_art_2&qtde_cjs=" @ %player.nexusInvest @ "&idPesqTorque=" @ %myServerPesq.num;

### Usar artefato:
"/torque/academia/usar_artefato?idPersona=" @ %player.persona.TAXOid @ "&pesq=aca_art_1&idPesqTorque=" @ %myServerPesq.num;


### Comprar pesquisa (com OMNIS :: deixamos omnis sempre == 0 e o client vai bloquear as chamadas)
"/torque/academia/comprar ? 
idPersona=XX
pesq=idDaBase
omnis=-20
idPesqTorque=idTorque

