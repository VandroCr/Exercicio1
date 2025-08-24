using System;

namespace ExercicioTres
{
    // Enums para os estados
    public enum EstadoBraco { Repouso, EmAtividade, Ocupado }
    public enum EstadoCotovelo { Repouso, Contraido }
    public enum EstadoMovimento { SemMovimento, EmMovimento }
    public enum Direcao { Norte = 0, Leste = 90, Sul = 180, Oeste = 270 }

    public class Drone
    {
        // Propriedades de voo
        public double Altura { get; private set; } = 0.5;
        public double DirecaoAtual { get; private set; } = 0; // Norte = 0°
        public double Velocidade { get; private set; } = 0;
        public bool ProximoObjeto { get; private set; } = false;
        
        // Estados dos braços
        public Braco BracoEsquerdo { get; private set; }
        public Braco BracoDireito { get; private set; }
        
        // Câmera
        public Camera Camera { get; private set; }
        
        public Drone()
        {
            BracoEsquerdo = new Braco("Esquerdo");
            BracoDireito = new Braco("Direito");
            Camera = new Camera();
        }
        
        // Controle de voo
        public bool DefinirAltura(double novaAltura)
        {
            if (ProximoObjeto)
            {
                Console.WriteLine("ERRO: Não é possível alterar altura quando próximo de objeto!");
                return false;
            }
            
            if (novaAltura >= 0.5 && novaAltura <= 25.0)
            {
                Altura = novaAltura;
                Console.WriteLine($"SUCESSO: Altura alterada para: {Altura:F1} metros");
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Altura deve estar entre 0.5 e 25.0 metros!");
                return false;
            }
        }
        
        public bool Subir()
        {
            return DefinirAltura(Altura + 0.5);
        }
        
        public bool Descer()
        {
            return DefinirAltura(Altura - 0.5);
        }
        
        // Controle de direção
        public bool DefinirDirecao(double novaDirecao)
        {
            if (ProximoObjeto)
            {
                Console.WriteLine("ERRO: Não é possível alterar direção quando próximo de objeto!");
                return false;
            }
            
            if (novaDirecao >= 0 && novaDirecao <= 360)
            {
                DirecaoAtual = novaDirecao;
                Console.WriteLine($"SUCESSO: Direção alterada para: {DirecaoAtual:F1}°");
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Direção deve estar entre 0° e 360°!");
                return false;
            }
        }
        
        public bool GirarEsquerda()
        {
            double novaDirecao = DirecaoAtual - 5;
            if (novaDirecao < 0) novaDirecao += 360;
            return DefinirDirecao(novaDirecao);
        }
        
        public bool GirarDireita()
        {
            double novaDirecao = DirecaoAtual + 5;
            if (novaDirecao >= 360) novaDirecao -= 360;
            return DefinirDirecao(novaDirecao);
        }
        
        // Controle de velocidade
        public bool AlterarVelocidade(double delta)
        {
            if (ProximoObjeto)
            {
                Console.WriteLine("ERRO: Não é possível alterar velocidade quando próximo de objeto!");
                return false;
            }
            
            double novaVelocidade = Velocidade + delta;
            if (novaVelocidade >= 0 && novaVelocidade <= 15)
            {
                Velocidade = novaVelocidade;
                Console.WriteLine($"SUCESSO: Velocidade alterada para: {Velocidade:F1} m/s");
                
                // Atualiza estado dos braços baseado na velocidade
                if (Velocidade > 0)
                {
                    BracoEsquerdo.ForcarRepouso();
                    BracoDireito.ForcarRepouso();
                }
                
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Velocidade deve estar entre 0 e 15 m/s!");
                return false;
            }
        }
        
        public bool AumentarVelocidade()
        {
            return AlterarVelocidade(0.5);
        }
        
        public bool DiminuirVelocidade()
        {
            return AlterarVelocidade(-0.5);
        }
        
        // Aproximação de objeto
        public bool AproximarObjeto()
        {
            if (Velocidade > 0)
            {
                Console.WriteLine("ERRO: Drone deve estar parado para aproximar de objeto!");
                return false;
            }
            
            if (ProximoObjeto)
            {
                Console.WriteLine("ERRO: Drone já está próximo de objeto!");
                return false;
            }
            
            ProximoObjeto = true;
            Velocidade = 0;
            Console.WriteLine("SUCESSO: Drone aproximou-se do objeto. Funcionalidades limitadas.");
            return true;
        }
        
        public bool DistanciarObjeto()
        {
            if (!ProximoObjeto)
            {
                Console.WriteLine("ERRO: Drone não está próximo de objeto!");
                return false;
            }
            
            ProximoObjeto = false;
            Console.WriteLine("SUCESSO: Drone distanciou-se do objeto. Funcionalidades restauradas.");
            return true;
        }
        
        // Status geral
        public void MostrarStatus()
        {
            Console.WriteLine("\n=== STATUS DO DRONE ===");
            Console.WriteLine($"Altura: {Altura:F1} metros");
            Console.WriteLine($"Direção: {DirecaoAtual:F1}° ({ObterNomeDirecao()})");
            Console.WriteLine($"Velocidade: {Velocidade:F1} m/s ({ObterEstadoMovimento()})");
            Console.WriteLine($"Próximo de objeto: {(ProximoObjeto ? "Sim" : "Não")}");
            Console.WriteLine("========================");
        }
        
        private string ObterNomeDirecao()
        {
            if (DirecaoAtual >= 315 || DirecaoAtual < 45) return "Norte";
            if (DirecaoAtual >= 45 && DirecaoAtual < 135) return "Leste";
            if (DirecaoAtual >= 135 && DirecaoAtual < 225) return "Sul";
            return "Oeste";
        }
        
        private string ObterEstadoMovimento()
        {
            return Velocidade > 0 ? "Em Movimento" : "Sem Movimento";
        }
    }
    
    public class Braco
    {
        public string Nome { get; private set; }
        public EstadoBraco Estado { get; private set; } = EstadoBraco.Repouso;
        public EstadoCotovelo EstadoCotovelo { get; private set; } = EstadoCotovelo.Repouso;
        public double RotacaoPulso { get; private set; } = 0;
        
        public Braco(string nome)
        {
            Nome = nome;
        }
        
        public bool Ativar()
        {
            if (Estado == EstadoBraco.Ocupado)
            {
                Console.WriteLine($"ERRO: Braço {Nome} está ocupado!");
                return false;
            }
            
            Estado = EstadoBraco.EmAtividade;
            Console.WriteLine($"SUCESSO: Braço {Nome} ativado");
            return true;
        }
        
        public bool Desativar()
        {
            if (Estado == EstadoBraco.Ocupado)
            {
                Console.WriteLine($"ERRO: Braço {Nome} está ocupado!");
                return false;
            }
            
            Estado = EstadoBraco.Repouso;
            Console.WriteLine($"SUCESSO: Braço {Nome} desativado");
            return true;
        }
        
        public void ForcarRepouso()
        {
            if (Estado != EstadoBraco.Ocupado)
            {
                Estado = EstadoBraco.Repouso;
            }
        }
        
        public bool ContrairCotovelo()
        {
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            EstadoCotovelo = EstadoCotovelo.Contraido;
            Console.WriteLine($"SUCESSO: Cotovelo do braço {Nome} contraído");
            return true;
        }
        
        public bool RelaxarCotovelo()
        {
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            EstadoCotovelo = EstadoCotovelo.Repouso;
            Console.WriteLine($"SUCESSO: Cotovelo do braço {Nome} relaxado");
            return true;
        }
        
        public bool RotacionarPulso(double delta)
        {
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            double novaRotacao = RotacaoPulso + delta;
            if (novaRotacao >= -360 && novaRotacao <= 360)
            {
                RotacaoPulso = novaRotacao;
                Console.WriteLine($"SUCESSO: Pulso do braço {Nome} rotacionado para {RotacaoPulso:F1}°");
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Rotação deve estar entre -360° e 360°!");
                return false;
            }
        }
        
        public bool DefinirRotacaoPulso(double rotacao)
        {
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativoo!");
                return false;
            }
            
            if (rotacao >= 0 && rotacao <= 360)
            {
                RotacaoPulso = rotacao;
                Console.WriteLine($"SUCESSO: Pulso do braço {Nome} definido para {RotacaoPulso:F1}°");
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Rotação deve estar entre 0° e 360°!");
                return false;
            }
        }
        
        // Ferramentas específicas do braço esquerdo
        public bool Pegar()
        {
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            if (EstadoCotovelo != EstadoCotovelo.Contraido)
            {
                Console.WriteLine($"ERRO: Cotovelo do braço {Nome} deve estar contraído!");
                return false;
            }
            
            Estado = EstadoBraco.Ocupado;
            Console.WriteLine($"SUCESSO: Braço {Nome} pegou objeto");
            return true;
        }
        
        public bool Armazenar()
        {
            if (Estado != EstadoBraco.Ocupado)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ocupado!");
                return false;
            }
            
            if (EstadoCotovelo != EstadoCotovelo.Repouso)
            {
                Console.WriteLine($"ERRO: Cotovelo do braço {Nome} deve estar em repouso!");
                return false;
            }
            
            Estado = EstadoBraco.EmAtividade;
            Console.WriteLine($"SUCESSO: Braço {Nome} armazenou objeto");
            return true;
        }
        
        // Ferramentas específicas do braço direito
        public bool Cortar()
        {
            if (Nome != "Direito")
            {
                Console.WriteLine("ERRO: Função disponível apenas para braço direito!");
                return false;
            }
            
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            if (EstadoCotovelo != EstadoCotovelo.Contraido)
            {
                Console.WriteLine($"ERRO: Cotovelo do braço {Nome} deve estar contraído!");
                return false;
            }
            
            Console.WriteLine($"SUCESSO: Braço {Nome} cortou objeto");
            return true;
        }
        
        public bool Coletar()
        {
            if (Nome != "Direito")
            {
                Console.WriteLine("ERRO: Função disponível apenas para braço direito!");
                return false;
            }
            
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            if (EstadoCotovelo != EstadoCotovelo.Repouso)
            {
                Console.WriteLine($"ERRO: Cotovelo do braço {Nome} deve estar em repouso!");
                return false;
            }
            
            Estado = EstadoBraco.Ocupado;
            Console.WriteLine($"SUCESSO: Braço {Nome} coletou substância");
            return true;
        }
        
        public bool Bater()
        {
            if (Nome != "Esquerdo")
            {
                Console.WriteLine("ERRO: Função disponível apenas para braço esquerdo!");
                return false;
            }
            
            if (Estado != EstadoBraco.EmAtividade)
            {
                Console.WriteLine($"ERRO: Braço {Nome} deve estar ativo!");
                return false;
            }
            
            if (EstadoCotovelo != EstadoCotovelo.Contraido)
            {
                Console.WriteLine($"ERRO: Cotovelo do braço {Nome} deve estar contraído!");
                return false;
            }
            
            Console.WriteLine($"SUCESSO: Braço {Nome} bateu objeto");
            return true;
        }
        
        public void MostrarStatus()
        {
            Console.WriteLine($"\n--- Braço {Nome} ---");
            Console.WriteLine($"Estado: {Estado}");
            Console.WriteLine($"Cotovelo: {EstadoCotovelo}");
            Console.WriteLine($"Rotação do pulso: {RotacaoPulso:F1}°");
        }
    }
    
    public class Camera
    {
        public double RotacaoHorizontal { get; private set; } = 0;
        public double RotacaoVertical { get; private set; } = 0;
        
        public bool RotacionarHorizontal(double delta)
        {
            double novaRotacao = RotacaoHorizontal + delta;
            if (novaRotacao >= -180 && novaRotacao <= 180)
            {
                RotacaoHorizontal = novaRotacao;
                Console.WriteLine($"SUCESSO: Câmera rotacionada horizontalmente para {RotacaoHorizontal:F1}°");
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Rotação horizontal deve estar entre -180° e 180°!");
                return false;
            }
        }
        
        public bool RotacionarVertical(double delta)
        {
            double novaRotacao = RotacaoVertical + delta;
            if (novaRotacao >= -90 && novaRotacao <= 90)
            {
                RotacaoVertical = novaRotacao;
                Console.WriteLine($"SUCESSO: Câmera rotacionada verticalmente para {RotacaoVertical:F1}°");
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Rotação vertical deve estar entre -90° e 90°!");
                return false;
            }
        }
        
        public void CapturarFoto()
        {
            Console.WriteLine($"FOTO: Foto capturada! Posição: H={RotacaoHorizontal:F1}°, V={RotacaoVertical:F1}°");
        }
        
        public void IniciarGravacao()
        {
            Console.WriteLine($"VIDEO: Gravação iniciada! Posição: H={RotacaoHorizontal:F1}°, V={RotacaoVertical:F1}°");
        }
        
        public void PararGravacao()
        {
            Console.WriteLine("STOP: Gravação parada!");
        }
        
        public void MostrarStatus()
        {
            Console.WriteLine("\n--- Câmera ---");
            Console.WriteLine($"Rotação Horizontal: {RotacaoHorizontal:F1}°");
            Console.WriteLine($"Rotação Vertical: {RotacaoVertical:F1}°");
        }
    }
    
    class ExercicioTres
    {
        static Drone drone = new Drone();
        
        public static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE CONTROLE DE DRONE ===");
            Console.WriteLine("Bem-vindo ao controle do drone!");
            
            bool continuar = true;
            while (continuar)
            {
                MostrarMenuPrincipal();
                string opcao = Console.ReadLine() ?? "";
                
                switch (opcao)
                {
                    case "1":
                        MenuControleVoo();
                        break;
                    case "2":
                        MenuDirecao();
                        break;
                    case "3":
                        MenuVelocidade();
                        break;
                    case "4":
                        MenuAproximacao();
                        break;
                    case "5":
                        MenuBracoEsquerdo();
                        break;
                    case "6":
                        MenuBracoDireito();
                        break;
                    case "7":
                        MenuCamera();
                        break;
                    case "8":
                        drone.MostrarStatus();
                        drone.BracoEsquerdo.MostrarStatus();
                        drone.BracoDireito.MostrarStatus();
                        drone.Camera.MostrarStatus();
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("Encerrando sistema de controle...");
                        break;
                    default:
                        Console.WriteLine("ERRO: Opção inválida!");
                        break;
                }
                
                if (continuar)
                {
                    Console.WriteLine("\nPressione Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
        
        static void MostrarMenuPrincipal()
        {
            Console.WriteLine("\n=== MENU PRINCIPAL ===");
            Console.WriteLine("1 - Controle de Voo");
            Console.WriteLine("2 - Controle de Direção");
            Console.WriteLine("3 - Controle de Velocidade");
            Console.WriteLine("4 - Aproximação de Objeto");
            Console.WriteLine("5 - Braço Esquerdo");
            Console.WriteLine("6 - Braço Direito");
            Console.WriteLine("7 - Câmera");
            Console.WriteLine("8 - Status Geral");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
        }
        
        static void MenuControleVoo()
        {
            Console.WriteLine("\n=== CONTROLE DE VOO ===");
            Console.WriteLine("1 - Definir altura específica");
            Console.WriteLine("2 - Subir 0.5m");
            Console.WriteLine("3 - Descer 0.5m");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    Console.Write("Digite a altura (0.5 - 25.0m): ");
                    if (double.TryParse(Console.ReadLine(), out double altura))
                        drone.DefinirAltura(altura);
                    break;
                case "2":
                    drone.Subir();
                    break;
                case "3":
                    drone.Descer();
                    break;
            }
        }
        
        static void MenuDirecao()
        {
            Console.WriteLine("\n=== CONTROLE DE DIREÇÃO ===");
            Console.WriteLine("1 - Definir direção específica");
            Console.WriteLine("2 - Girar 5° para esquerda");
            Console.WriteLine("3 - Girar 5° para direita");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    Console.Write("Digite a direção (0° - 360°): ");
                    if (double.TryParse(Console.ReadLine(), out double direcao))
                        drone.DefinirDirecao(direcao);
                    break;
                case "2":
                    drone.GirarEsquerda();
                    break;
                case "3":
                    drone.GirarDireita();
                    break;
            }
        }
        
        static void MenuVelocidade()
        {
            Console.WriteLine("\n=== CONTROLE DE VELOCIDADE ===");
            Console.WriteLine("1 - Aumentar 0.5 m/s");
            Console.WriteLine("2 - Diminuir 0.5 m/s");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    drone.AumentarVelocidade();
                    break;
                case "2":
                    drone.DiminuirVelocidade();
                    break;
            }
        }
        
        static void MenuAproximacao()
        {
            Console.WriteLine("\n=== APROXIMAÇÃO DE OBJETO ===");
            Console.WriteLine("1 - Aproximar de objeto");
            Console.WriteLine("2 - Distanciar de objeto");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    drone.AproximarObjeto();
                    break;
                case "2":
                    drone.DistanciarObjeto();
                    break;
            }
        }
        
        static void MenuBracoEsquerdo()
        {
            Console.WriteLine("\n=== BRAÇO ESQUERDO ===");
            Console.WriteLine("1 - Ativar/Desativar");
            Console.WriteLine("2 - Controle do cotovelo");
            Console.WriteLine("3 - Controle do pulso");
            Console.WriteLine("4 - Ferramentas (Pegar, Armazenar, Bater)");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    if (drone.BracoEsquerdo.Estado == EstadoBraco.EmAtividade)
                        drone.BracoEsquerdo.Desativar();
                    else
                        drone.BracoEsquerdo.Ativar();
                    break;
                case "2":
                    MenuCotovelo(drone.BracoEsquerdo);
                    break;
                case "3":
                    MenuPulso(drone.BracoEsquerdo);
                    break;
                case "4":
                    MenuFerramentasEsquerdo();
                    break;
            }
        }
        
        static void MenuBracoDireito()
        {
            Console.WriteLine("\n=== BRAÇO DIREITO ===");
            Console.WriteLine("1 - Ativar/Desativar");
            Console.WriteLine("2 - Controle do cotovelo");
            Console.WriteLine("3 - Controle do pulso");
            Console.WriteLine("4 - Ferramentas (Pegar, Armazenar, Cortar, Coletar)");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    if (drone.BracoDireito.Estado == EstadoBraco.EmAtividade)
                        drone.BracoDireito.Desativar();
                    else
                        drone.BracoDireito.Ativar();
                    break;
                case "2":
                    MenuCotovelo(drone.BracoDireito);
                    break;
                case "3":
                    MenuPulso(drone.BracoDireito);
                    break;
                case "4":
                    MenuFerramentasDireito();
                    break;
            }
        }
        
        static void MenuCotovelo(Braco braco)
        {
            Console.WriteLine("\n=== CONTROLE DO COTOVELO ===");
            Console.WriteLine("1 - Contrair");
            Console.WriteLine("2 - Relaxar");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    braco.ContrairCotovelo();
                    break;
                case "2":
                    braco.RelaxarCotovelo();
                    break;
            }
        }
        
        static void MenuPulso(Braco braco)
        {
            Console.WriteLine("\n=== CONTROLE DO PULSO ===");
            Console.WriteLine("1 - Rotacionar +5°");
            Console.WriteLine("2 - Rotacionar -5°");
            Console.WriteLine("3 - Definir rotação específica");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    braco.RotacionarPulso(5);
                    break;
                case "2":
                    braco.RotacionarPulso(-5);
                    break;
                case "3":
                    Console.Write("Digite a rotação (0° - 360°): ");
                    if (double.TryParse(Console.ReadLine(), out double rotacao))
                        braco.DefinirRotacaoPulso(rotacao);
                    break;
            }
        }
        
        static void MenuFerramentasEsquerdo()
        {
            Console.WriteLine("\n=== FERRAMENTAS BRAÇO ESQUERDO ===");
            Console.WriteLine("1 - Pegar objeto");
            Console.WriteLine("2 - Armazenar objeto");
            Console.WriteLine("3 - Bater objeto");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    drone.BracoEsquerdo.Pegar();
                    break;
                case "2":
                    drone.BracoEsquerdo.Armazenar();
                    break;
                case "3":
                    drone.BracoEsquerdo.Bater();
                    break;
            }
        }
        
        static void MenuFerramentasDireito()
        {
            Console.WriteLine("\n=== FERRAMENTAS BRAÇO DIREITO ===");
            Console.WriteLine("1 - Pegar objeto");
            Console.WriteLine("2 - Armazenar objeto");
            Console.WriteLine("3 - Cortar objeto");
            Console.WriteLine("4 - Coletar substância");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    drone.BracoDireito.Pegar();
                    break;
                case "2":
                    drone.BracoDireito.Armazenar();
                    break;
                case "3":
                    drone.BracoDireito.Cortar();
                    break;
                case "4":
                    drone.BracoDireito.Coletar();
                    break;
            }
        }
        
        static void MenuCamera()
        {
            Console.WriteLine("\n=== CONTROLE DA CÂMERA ===");
            Console.WriteLine("1 - Rotação horizontal");
            Console.WriteLine("2 - Rotação vertical");
            Console.WriteLine("3 - Capturar foto");
            Console.WriteLine("4 - Iniciar gravação");
            Console.WriteLine("5 - Parar gravação");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            switch (opcao)
            {
                case "1":
                    Console.Write("Digite o delta de rotação horizontal: ");
                    if (double.TryParse(Console.ReadLine(), out double deltaH))
                        drone.Camera.RotacionarHorizontal(deltaH);
                    break;
                case "2":
                    Console.Write("Digite o delta de rotação vertical: ");
                    if (double.TryParse(Console.ReadLine(), out double deltaV))
                        drone.Camera.RotacionarVertical(deltaV);
                    break;
                case "3":
                    drone.Camera.CapturarFoto();
                    break;
                case "4":
                    drone.Camera.IniciarGravacao();
                    break;
                case "5":
                    drone.Camera.PararGravacao();
                    break;
            }
        }
    }
}

