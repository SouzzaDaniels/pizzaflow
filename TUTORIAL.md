# Tutorial Completo — PizzaFlow (do zero até publicado)

Este tutorial assume que você **nunca programou nada parecido antes**. Vá seguindo os passos na ordem, sem pular. Sempre que aparecer um bloco cinza de código, é um comando para você digitar no terminal (ou PowerShell, no Windows).

Tempo estimado: 3 a 6 horas, divididas em quantas sessões você quiser.

---

## Índice

1. [Contas que você vai precisar criar](#1-contas-que-você-vai-precisar-criar)
2. [Instalar os programas necessários](#2-instalar-os-programas-necessários)
3. [Baixar os arquivos do projeto](#3-baixar-os-arquivos-do-projeto)
4. [Criar o repositório no GitHub e subir o projeto](#4-criar-o-repositório-no-github-e-subir-o-projeto)
5. [Configurar o banco de dados no Supabase](#5-configurar-o-banco-de-dados-no-supabase)
6. [Rodar o Backend (.NET) na sua máquina](#6-rodar-o-backend-net-na-sua-máquina)
7. [Rodar o Frontend (Angular) na sua máquina](#7-rodar-o-frontend-angular-na-sua-máquina)
8. [Rodar o App do Gestor (Flutter) na sua máquina](#8-rodar-o-app-do-gestor-flutter-na-sua-máquina)
9. [Publicar o Backend no Render](#9-publicar-o-backend-no-render)
10. [Publicar o Frontend no Netlify](#10-publicar-o-frontend-no-netlify)
11. [Gerar o .apk do app do gestor e instalar no celular](#11-gerar-o-apk-do-app-do-gestor-e-instalar-no-celular)
12. [Testando tudo de ponta a ponta](#12-testando-tudo-de-ponta-a-ponta)
13. [Segurança antes de usar de verdade](#13-segurança-antes-de-usar-de-verdade)
14. [Problemas comuns](#14-problemas-comuns)

---

## 1. Contas que você vai precisar criar

Crie uma conta (grátis) em cada um destes sites. Use o mesmo e-mail em todos, para facilitar:

- **GitHub** — [github.com](https://github.com) → guarda o código do projeto.
- **Supabase** — [supabase.com](https://supabase.com) → banco de dados PostgreSQL grátis.
- **Render** — [render.com](https://render.com) → hospeda o backend (.NET). Pode entrar direto com sua conta do GitHub.
- **Netlify** — [netlify.com](https://netlify.com) → hospeda o frontend (Angular). Também pode entrar com o GitHub.

Nenhum deles vai pedir cartão de crédito para o que vamos fazer aqui.

---

## 2. Instalar os programas necessários

Instale, na ordem, na sua máquina (Windows, Mac ou Linux):

1. **Git** — [git-scm.com/downloads](https://git-scm.com/downloads). Depois de instalar, abra um terminal e confirme:
   ```
   git --version
   ```
2. **Visual Studio Code** — [code.visualstudio.com](https://code.visualstudio.com) (editor de código; não é obrigatório, mas facilita muito).
3. **.NET 8 SDK** — [dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0). Confirme depois:
   ```
   dotnet --version
   ```
   (deve mostrar algo começando com `8.`)
4. **Node.js LTS** (versão 20 ou superior) — [nodejs.org](https://nodejs.org). Confirme:
   ```
   node --version
   npm --version
   ```
5. **Angular CLI** — depois de instalar o Node, rode:
   ```
   npm install -g @angular/cli
   ng version
   ```
6. **Flutter SDK** — siga o instalador oficial para o seu sistema operacional em [docs.flutter.dev/get-started/install](https://docs.flutter.dev/get-started/install). Depois, rode:
   ```
   flutter doctor
   ```
   Resolva os itens marcados com "✗" que o `flutter doctor` apontar (geralmente pede para aceitar licenças do Android com `flutter doctor --android-licenses`).
7. **Android Studio** — [developer.android.com/studio](https://developer.android.com/studio). Necessário para compilar o app Flutter e/ou rodar um emulador Android. Durante a instalação, deixe marcada a opção de instalar o Android SDK.

> Se algum desses passos der erro, feche e abra o terminal de novo (às vezes é preciso reiniciar o terminal para reconhecer o novo comando instalado).

---

## 3. Baixar os arquivos do projeto

Você recebeu (junto com este tutorial) uma pasta chamada `pizzaflow/` contendo três subpastas: `backend/`, `frontend/` e `mobile/`.

1. Crie uma pasta em um local organizado no seu computador, por exemplo `C:\projetos\` (Windows) ou `~/projetos/` (Mac/Linux).
2. Copie a pasta `pizzaflow` inteira para dentro dessa pasta.
3. Abra o terminal dentro dela:
   ```
   cd caminho/para/pizzaflow
   ```

A partir de agora, todos os comandos deste tutorial assumem que você está dentro da pasta `pizzaflow/`, a não ser que eu diga para entrar em outra subpasta.

---

## 4. Criar o repositório no GitHub e subir o projeto

### 4.1. Criar o repositório vazio no GitHub

1. Entre em [github.com](https://github.com) já logado.
2. Clique no botão verde **"New"** (ou no `+` no canto superior direito → **"New repository"**).
3. Em **Repository name**, digite `pizzaflow`.
4. Deixe como **Public** (ou Private, se preferir).
5. **Não marque** nenhuma caixa de "Add README" ou "Add .gitignore" — o repositório deve ficar totalmente vazio.
6. Clique em **Create repository**.
7. O GitHub vai mostrar uma página com comandos e uma URL parecida com:
   ```
   https://github.com/SEU-USUARIO/pizzaflow.git
   ```
   Copie essa URL, você vai usar já já.

### 4.2. Configurar o Git na sua máquina (só na primeira vez)

```
git config --global user.name "Seu Nome"
git config --global user.email "seu-email@exemplo.com"
```

### 4.3. Subir o projeto

Dentro da pasta `pizzaflow/`, rode um por um:

```
git init
git add .
git commit -m "Primeiro commit: estrutura inicial do PizzaFlow"
git branch -M main
git remote add origin https://github.com/SEU-USUARIO/pizzaflow.git
git push -u origin main
```

Na hora do `git push`, o GitHub vai pedir login. Se pedir **senha** e não aceitar sua senha normal, é porque o GitHub exige um **Personal Access Token** no lugar da senha:

1. No GitHub, clique na sua foto → **Settings** → role até **Developer settings** (no final do menu à esquerda).
2. **Personal access tokens** → **Tokens (classic)** → **Generate new token (classic)**.
3. Dê um nome, marque a caixa **repo**, e clique em **Generate token**.
4. Copie o token gerado (ele só aparece uma vez!) e cole no lugar da senha quando o terminal pedir.

Pronto — seu projeto já está no GitHub. **Sempre que você fizer alguma alteração** nos arquivos, salve no GitHub repetindo:

```
git add .
git commit -m "Descreva o que você mudou"
git push
```

---

## 5. Configurar o banco de dados no Supabase

1. Entre em [supabase.com](https://supabase.com) e clique em **New project**.
2. Escolha uma organização (ou crie uma), dê um nome ao projeto (ex: `pizzaflow`), crie uma **senha do banco de dados forte** e **anote essa senha** — você vai precisar dela.
3. Escolha a região mais próxima de você e clique em **Create new project**. Aguarde alguns minutos até o projeto ficar pronto.
4. Quando estiver pronto, vá em **Project Settings** (ícone de engrenagem) → **Database**.
5. Procure a seção **Connection string** e selecione o modo **URI** ou **.NET/Npgsql** se disponível. Você vai ver algo parecido com:
   ```
   postgresql://postgres:[SUA-SENHA]@db.xxxxxxxxxxxx.supabase.co:5432/postgres
   ```
6. Anote separadamente estas informações (vamos montar a connection string do jeito que o .NET entende):
   - **Host**: `db.xxxxxxxxxxxx.supabase.co`
   - **Porta**: `5432`
   - **Banco**: `postgres`
   - **Usuário**: `postgres`
   - **Senha**: a senha que você criou no passo 2

A connection string no formato que o backend usa (Npgsql) fica assim:

```
Host=db.xxxxxxxxxxxx.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=SUA-SENHA;SSL Mode=Require;Trust Server Certificate=true
```

Guarde essa linha — você vai colar no `appsettings.Development.json` no próximo passo, e depois de novo no Render.

---

## 6. Rodar o Backend (.NET) na sua máquina

### 6.1. Configurar a connection string local

Abra o arquivo `backend/PizzaFlow.Api/appsettings.Development.json` no VS Code e troque o valor de `DefaultConnection` pela connection string do Supabase que você montou no passo anterior:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=db.xxxxxxxxxxxx.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=SUA-SENHA;SSL Mode=Require;Trust Server Certificate=true"
  },
  "Jwt": {
    "Key": "troque-esta-chave-por-algo-bem-grande-e-aleatorio-1234567890",
    "Issuer": "PizzaFlow",
    "Audience": "PizzaFlowClientes"
  }
}
```

> A chave `Jwt.Key` é usada para "assinar" os tokens de login. Troque por qualquer texto longo e aleatório (mínimo 32 caracteres). Não precisa decorar, só não pode ficar vazio.

### 6.2. Instalar dependências e ferramentas do Entity Framework

Entre na pasta do backend:

```
cd backend/PizzaFlow.Api
dotnet restore
dotnet tool install --global dotnet-ef
```

(Se já tiver o `dotnet-ef` instalado, o segundo comando vai avisar que já existe — tudo bem, pode ignorar.)

### 6.3. Criar a primeira migration e aplicar no banco

Uma "migration" é o histórico de como as tabelas do banco devem ser criadas/alteradas.

```
dotnet ef migrations add InicialPizzaFlow
dotnet ef database update
```

Se tudo der certo, ao abrir o Supabase (**Table Editor**, no menu lateral), você já deve ver as tabelas `Usuarios`, `Pizzas`, `Pedidos` e `ItemPedidos`.

### 6.4. Rodar a API

```
dotnet run
```

O terminal vai mostrar algo como:

```
Now listening on: http://localhost:5000
```

Abra o navegador em `http://localhost:5000/swagger` — essa é a documentação interativa da API (Swagger), onde você pode testar cada endpoint manualmente (ex: fazer um cadastro, um login, listar pizzas, etc). Deixe este terminal aberto rodando enquanto for usar o sistema; para parar, use `Ctrl + C`.

Ao rodar pela primeira vez, o próprio backend já cadastra automaticamente as **5 pizzas iniciais** e **1 usuário gestor** com:
- Telefone: `11999999999`
- Senha: `admin123`

(Vamos trocar essa senha no passo 13, antes de usar de verdade.)

---

## 7. Rodar o Frontend (Angular) na sua máquina

Abra **um novo terminal** (deixe o backend rodando no outro) e entre na pasta do frontend:

```
cd frontend
```

### 7.1. Instalar dependências

```
npm install
```

### 7.2. Adicionar o Angular Material (componentes visuais)

```
ng add @angular/material
```

Durante o processo, ele vai perguntar:
- Qual tema escolher → escolha qualquer um (ex: **Magenta/Violet**).
- **Set up global Angular Material typography styles?** → responda `Yes`.
- **Include the Animations module?** → responda `Yes` (Include and enable animations).

Isso vai ajustar automaticamente os arquivos de estilo/tema do projeto.

### 7.3. Conferir a URL da API

Abra `frontend/src/app/core/config/api.config.ts` e confirme que está apontando para o backend local:

```ts
export const API_URL = 'http://localhost:5000/api';
```

### 7.4. Rodar o frontend

```
ng serve
```

Abra o navegador em `http://localhost:4200`. Você deve ver a tela inicial do PizzaFlow pedindo o telefone. Teste o fluxo completo: digite um telefone novo → ele vai te mandar para cadastro → depois de cadastrar, você cai no cardápio → adicione pizzas ao carrinho → finalize o pedido.

---

## 8. Rodar o App do Gestor (Flutter) na sua máquina

Abra **um terceiro terminal**. Primeiro, vamos deixar o Flutter gerar as pastas de projeto Android/iOS que faltam (elas não vêm prontas neste tutorial porque dependem da versão do seu SDK):

```
cd mobile
flutter create --org com.pizzaflow --project-name pizzaflow_gestor pizzaflow_gestor_tmp
```

Isso cria uma pastinha temporária só para gerar as pastas `android/` e `ios/` corretas para a sua máquina. Agora copie essas pastas para dentro do projeto real:

**Windows (PowerShell):**
```
Copy-Item -Recurse pizzaflow_gestor_tmp\android pizzaflow_gestor\android
Copy-Item -Recurse pizzaflow_gestor_tmp\ios pizzaflow_gestor\ios
Remove-Item -Recurse -Force pizzaflow_gestor_tmp
```

**Mac/Linux:**
```
cp -R pizzaflow_gestor_tmp/android pizzaflow_gestor/android
cp -R pizzaflow_gestor_tmp/ios pizzaflow_gestor/ios
rm -rf pizzaflow_gestor_tmp
```

### 8.1. Instalar dependências

```
cd pizzaflow_gestor
flutter pub get
```

### 8.2. Ajustar a URL da API para testes locais

Abra `mobile/pizzaflow_gestor/lib/services/api_service.dart`. Se for testar em um **emulador Android**, o `localhost` do seu computador é acessado pelo endereço especial `10.0.2.2`:

```dart
static const String baseUrl = 'http://10.0.2.2:5000/api';
```

Se for testar em um **celular físico** conectado por USB ou na mesma rede Wi-Fi, use o IP local do seu computador (descubra com `ipconfig` no Windows ou `ifconfig`/`ip a` no Mac/Linux), por exemplo:

```dart
static const String baseUrl = 'http://192.168.0.10:5000/api';
```

### 8.3. Rodar o app

Abra um emulador Android pelo Android Studio (**Device Manager** → play em algum dispositivo) ou conecte um celular físico com a **depuração USB** ativada. Depois:

```
flutter run
```

Faça login com o gestor padrão (`11999999999` / `admin123`) e veja a fila de pedidos aparecer — inclusive os que você criou pelo site Angular!

---

## 9. Publicar o Backend no Render

### 9.1. Criar o Web Service

1. Entre em [render.com](https://render.com) e clique em **New +** → **Web Service**.
2. Conecte sua conta do GitHub, se ainda não conectou, e selecione o repositório `pizzaflow`.
3. Configure:
   - **Name**: `pizzaflow-api`
   - **Root Directory**: `backend/PizzaFlow.Api`
   - **Environment**: **Docker** (o Render vai detectar o `Dockerfile` automaticamente)
   - **Instance Type**: **Free**
4. Antes de criar, role até **Environment Variables** e adicione:

   | Key | Value |
   |---|---|
   | `ConnectionStrings__DefaultConnection` | a mesma connection string do Supabase (passo 5) |
   | `Jwt__Key` | uma chave longa e aleatória (pode ser a mesma do passo 6.1, ou gere outra) |
   | `Jwt__Issuer` | `PizzaFlow` |
   | `Jwt__Audience` | `PizzaFlowClientes` |
   | `Cors__AllowedOrigins__0` | `http://localhost:4200` (vamos trocar depois pela URL do Netlify) |

   > Repare no uso de **dois underlines (`__`)** — é assim que o .NET entende hierarquias de configuração via variáveis de ambiente.

5. Clique em **Create Web Service**. O Render vai construir a imagem Docker e publicar — isso pode levar alguns minutos na primeira vez.
6. Quando terminar, você verá uma URL parecida com:
   ```
   https://pizzaflow-api.onrender.com
   ```
   Teste abrindo `https://pizzaflow-api.onrender.com/swagger` no navegador.

> **Importante (plano Free do Render):** o serviço "dorme" depois de um tempo sem uso e demora ~30-60 segundos para acordar na primeira requisição seguinte. Isso é normal no plano gratuito.

---

## 10. Publicar o Frontend no Netlify

### 10.1. Atualizar a URL da API para produção

Antes de publicar, edite `frontend/src/app/core/config/api.config.ts` para apontar para o backend publicado no Render:

```ts
export const API_URL = 'https://pizzaflow-api.onrender.com/api';
```

Salve, e suba essa alteração para o GitHub:

```
git add .
git commit -m "Aponta frontend para a API em produção"
git push
```

### 10.2. Criar o site no Netlify

1. Entre em [netlify.com](https://netlify.com) → **Add new site** → **Import an existing project**.
2. Conecte sua conta GitHub e escolha o repositório `pizzaflow`.
3. O Netlify deve detectar automaticamente o arquivo `frontend/netlify.toml` com as configurações de build. Confirme que os campos ficaram:
   - **Base directory**: `frontend`
   - **Build command**: `npm install && npm run build -- --configuration production`
   - **Publish directory**: `dist/pizzaflow-frontend/browser`
4. Clique em **Deploy site**. Aguarde o build terminar.
5. O Netlify vai gerar uma URL parecida com:
   ```
   https://nome-aleatorio.netlify.app
   ```
   (Você pode trocar esse nome em **Site settings** → **Change site name**.)

### 10.3. Liberar o CORS no Render para o domínio do Netlify

Volte ao Render → seu serviço `pizzaflow-api` → **Environment** → edite a variável:

```
Cors__AllowedOrigins__0 = https://nome-aleatorio.netlify.app
```

Salve — o Render vai reiniciar o serviço automaticamente. Agora acesse a URL do Netlify e teste o fluxo completo: telefone → cadastro → cardápio → pedido.

---

## 11. Gerar o .apk do app do gestor e instalar no celular

### 11.1. Apontar o app para a API de produção

Edite `mobile/pizzaflow_gestor/lib/services/api_service.dart`:

```dart
static const String baseUrl = 'https://pizzaflow-api.onrender.com/api';
```

### 11.2. Gerar o APK

Dentro de `mobile/pizzaflow_gestor`:

```
flutter build apk --release
```

Ao final, o arquivo gerado fica em:

```
mobile/pizzaflow_gestor/build/app/outputs/flutter-apk/app-release.apk
```

### 11.3. Instalar no celular do gestor

1. Envie esse arquivo `.apk` para o celular (por e-mail, WhatsApp Web, Google Drive, cabo USB — o que for mais fácil).
2. No celular Android, abra o arquivo `.apk` recebido.
3. O Android vai avisar que a instalação de fontes desconhecidas está bloqueada — toque em **Configurações**, ative **"Permitir desta fonte"** para o app que está usando (ex: Gmail, Arquivos, WhatsApp) e volte para instalar.
4. Abra o app e faça login com o telefone/senha do gestor.

---

## 12. Testando tudo de ponta a ponta

1. No site (Netlify), simule um cliente: informe um telefone novo, cadastre-se, escolha pizzas e finalize um pedido.
2. No celular (app Flutter), como gestor, veja o pedido aparecer na fila em até 15 segundos.
3. No app, avance o status do pedido (Recebido → Em preparo → Saiu para entrega → Entregue).
4. De volta ao site, na tela de detalhe do pedido do cliente, veja o status atualizar automaticamente (também em até 15 segundos).

Se tudo isso funcionar, o fluxo ponta a ponta está completo. 🎉

---

## 13. Segurança antes de usar de verdade

Antes de usar o PizzaFlow com clientes reais, faça pelo menos isto:

1. **Troque a senha do gestor padrão.** Ela está definida em `backend/PizzaFlow.Api/Data/DbSeeder.cs` como `11999999999` / `admin123`. Você pode:
   - Editar diretamente na tabela `Usuarios` do Supabase (gere um novo hash com bcrypt e cole no campo `SenhaHash`), ou
   - Criar um endpoint temporário de "trocar senha" (não incluído neste MVP por escopo).
2. **Use uma `Jwt:Key` forte e diferente em produção**, nunca a mesma usada em testes/tutoriais públicos.
3. **Não deixe a connection string do Supabase exposta** em nenhum arquivo enviado ao GitHub. Nunca faça commit de `appsettings.Development.json` com a senha real preenchida — se você já fez isso sem querer, troque a senha do banco no Supabase depois.

---

## 14. Problemas comuns

**`dotnet: command not found`** → o .NET SDK não foi instalado corretamente ou o terminal não foi reaberto depois da instalação. Feche e abra o terminal de novo.

**Erro de conexão com o banco (`could not connect to server`)** → confira se a connection string está exatamente como o Supabase mostrou, com a senha certa e sem espaços extras.

**`ng: command not found`** → rode `npm install -g @angular/cli` de novo, e confira se a pasta global do npm está no PATH do seu sistema.

**App Flutter não conecta na API local** → lembre-se: no emulador Android, `localhost` do seu PC é `10.0.2.2`, não `localhost`.

**CORS bloqueando o frontend** → confira se a variável `Cors__AllowedOrigins__0` no Render está com a URL exata do seu site no Netlify (sem barra `/` no final).

**Render "dormindo"** → no plano gratuito, é esperado que a primeira requisição depois de um tempo demore. Não é erro.

**Git pedindo senha e recusando** → use um Personal Access Token no lugar da senha (veja passo 4.3).

---

Pronto! Você tem agora o PizzaFlow rodando localmente, publicado na internet e disponível no seu GitHub.
