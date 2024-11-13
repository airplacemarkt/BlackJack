using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Media;
using Microsoft.VisualBasic.ApplicationServices;
using System.Threading;

namespace BlackJack
{
    public partial class BlackJack : Form
    {
        List<string> baralho = new List<string>();
        Random rnd = new Random();
        int pontuacaoJogador = 0;
        int pontuacaoComputador = 0;
        PictureBox[] jogadorPictureBoxes;
        PictureBox[] computadorPictureBoxes;
        int indiceJogador = 0;
        int indiceComputador = 0;
        private SoundPlayer Fundo;
        private SoundPlayer Blackjack;
        private SoundPlayer botao;
        private SoundPlayer Game_Over;
        private SoundPlayer Inicio;
        private SoundPlayer Ganhou;
        private SoundPlayer Extrapolou;


        public BlackJack()
        {
            InitializeComponent();
            int raioBorda = 20;
            ArredondarBordaPictureBox(pic_jogador11, raioBorda);
            ArredondarBordaPictureBox(pic_jogador12, raioBorda);
            ArredondarBordaPictureBox(pic_jogador13, raioBorda);
            ArredondarBordaPictureBox(pic_jogador14, raioBorda);
            ArredondarBordaPictureBox(pic_jogador15, raioBorda);
            ArredondarBordaPictureBox(pic_jogador16, raioBorda);
            ArredondarBordaPictureBox(pic_jogador17, raioBorda);
            ArredondarBordaPictureBox(pic_jogador18, raioBorda);
            ArredondarBordaPictureBox(pic_jogador19, raioBorda);
            ArredondarBordaPictureBox(pic_jogador21, raioBorda);
            ArredondarBordaPictureBox(pic_jogador22, raioBorda);
            ArredondarBordaPictureBox(pic_jogador23, raioBorda);
            ArredondarBordaPictureBox(pic_jogador24, raioBorda);
            ArredondarBordaPictureBox(pic_jogador25, raioBorda);
            ArredondarBordaPictureBox(pic_jogador26, raioBorda);
            ArredondarBordaPictureBox(pic_jogador27, raioBorda);
            ArredondarBordaPictureBox(pic_jogador28, raioBorda);
            ArredondarBordaPictureBox(pic_jogador29, raioBorda);

            Blackjack = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\Blackjack.wav");
            botao = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\botao.wav");
            Fundo = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\Fundo.wav");
            Game_Over = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\Game_Over.wav");
            Inicio = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\inicio.wav");
            Ganhou = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\Ganhou.wav");
            Extrapolou = new SoundPlayer(@"E:\Projetos UNINOVE C#\BlackJack\Sounds\Extrapolou.wav");
            jogadorPictureBoxes = new PictureBox[] { pic_jogador11, pic_jogador12, pic_jogador13, pic_jogador14, pic_jogador15, pic_jogador16, pic_jogador17, pic_jogador18, pic_jogador19 };
            computadorPictureBoxes = new PictureBox[] { pic_jogador21, pic_jogador22, pic_jogador23, pic_jogador24, pic_jogador25, pic_jogador26, pic_jogador27, pic_jogador28, pic_jogador29 };
            txt_nomeUsuario.Enter += txt_nomeUsuario_Enter;
            txt_nomeUsuario.Leave += txt_nomeUsuario_Leave;
            ResetarPictureBoxes();
            painel_usuario.Visible = true;
        }
        private void ArredondarBordaPictureBox(PictureBox pictureBox, int raioBorda)
        {
            GraphicsPath caminho = new GraphicsPath();
            caminho.StartFigure();
            caminho.AddArc(new Rectangle(0, 0, raioBorda, raioBorda), 180, 90);
            caminho.AddArc(new Rectangle(pictureBox.Width - raioBorda, 0, raioBorda, raioBorda), 270, 90);
            caminho.AddArc(new Rectangle(pictureBox.Width - raioBorda, pictureBox.Height - raioBorda, raioBorda, raioBorda), 0, 90);
            caminho.AddArc(new Rectangle(0, pictureBox.Height - raioBorda, raioBorda, raioBorda), 90, 90);
            caminho.CloseFigure();
            pictureBox.Region = new Region(caminho);
        }

        private void ResetarPictureBoxes()
        {
            foreach (var pb in jogadorPictureBoxes)
            {
                pb.Visible = false;
                pb.Image = null;
            }

            foreach (var pb in computadorPictureBoxes)
            {
                pb.Visible = false;
                pb.Image = null;
            }

            indiceJogador = 0;
            indiceComputador = 0;
        }

        private void btn_reiniciar_Click(object sender, EventArgs e)
        {
            Inicio.Play();
            pontuacaoJogador = 0;
            pontuacaoComputador = 0;
            lbl_jogador2.Text = "0";
            lbl_jogador1.Text = "0";
            pic_resultado.Image = null;
            pic_black.Image = null;
            pic_black_pc.Image = null;

            baralho.Clear();
            string caminho = @"E:\Projetos UNINOVE C#\BlackJack\ImagensCartas\";
            string[] naipes = { "Copas", "Espadas", "Ouro", "Paus" };
            string[] valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            foreach (string valor in valores)
                foreach (string naipe in naipes)
                    baralho.Add($"{caminho}{valor}_{naipe}.jpg");

            ResetarPictureBoxes();
            btn_puxar.Enabled = true;
            btn_parar.Enabled = true;
            btn_desistir.Enabled = true;
            pic_resultado.Visible = false;
            pic_black.SendToBack();
            pic_black.Visible = false;
        }

        private void btn_puxar_Click(object sender, EventArgs e)
        {
            botao.Play();
            if (indiceJogador >= jogadorPictureBoxes.Length || baralho.Count == 0) return;

            int indice = rnd.Next(baralho.Count);
            string carta = baralho[indice];
            baralho.RemoveAt(indice);

            jogadorPictureBoxes[indiceJogador].ImageLocation = carta;
            jogadorPictureBoxes[indiceJogador].Visible = true;
            jogadorPictureBoxes[indiceJogador].BringToFront();
            indiceJogador++;

            int valorCarta = ObterValorCarta(carta);
            pontuacaoJogador += valorCarta;
            lbl_jogador1.Text = pontuacaoJogador.ToString();

            if (pontuacaoJogador > 21)
            {
                btn_puxar.Enabled = false;
                pic_black.Visible = true;
                pic_black.BringToFront();

                ComputadorJoga();
            }
            else if (pontuacaoJogador == 21)
            {
                btn_puxar.Enabled = false;
                pic_black.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\BLACK.png");
                Blackjack.Play();
                pic_black.Visible = true;
                pic_black.BringToFront();
                ComputadorJoga();
            }
            else
            {
                pic_black.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\EXTRAPOLOU.png");
            }
        }

        private int ObterValorCarta(string carta)
        {
            string nomeCarta = System.IO.Path.GetFileNameWithoutExtension(carta).Split('_')[0];
            if (nomeCarta == "A") return 1;
            if (nomeCarta == "K" || nomeCarta == "Q" || nomeCarta == "J") return 10;
            return int.Parse(nomeCarta);
        }

        private void btn_parar_Click(object sender, EventArgs e)
        {
            botao.Play();
            btn_puxar.Enabled = false;
            btn_desistir.Enabled = false;
            ComputadorJoga();
        }

        private void ComputadorJoga()
        {
            btn_reiniciar.Enabled = false;
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            while (pontuacaoComputador < 17 && baralho.Count > 0 && indiceComputador < computadorPictureBoxes.Length)
            {
                btn_parar.Enabled = false;
                int indice = rnd.Next(baralho.Count);
                string carta = baralho[indice];
                baralho.RemoveAt(indice);

                computadorPictureBoxes[indiceComputador].ImageLocation = carta;
                computadorPictureBoxes[indiceComputador].Visible = true;
                computadorPictureBoxes[indiceComputador].BringToFront();
                indiceComputador++;

                int valorCarta = ObterValorCarta(carta);
                pontuacaoComputador += valorCarta;
                lbl_jogador2.Text = pontuacaoComputador.ToString();
                botao.Play();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);

                if (pontuacaoComputador == 21)
                {
                    pic_black_pc.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\BLACK.png");
                    pic_black_pc.Visible = true;
                    Blackjack.Play();
                }
                else if (pontuacaoComputador > 21)
                {
                    pic_black_pc.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\EXTRAPOLOU.png");
                    pic_black_pc.Visible = true;
                }
                btn_desistir.Enabled = false;
            }
            VerificarVencedor();
        }

        private void VerificarVencedor()
        {
            if (pontuacaoJogador == 21 && pontuacaoComputador == 21)
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\EMPATE.png");
            }
            else if (pontuacaoJogador == 21)
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\GANHOU.png");
                Ganhou.Play();
            }
            else if (pontuacaoComputador == 21)
            {
                pic_black_pc.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\BLACK.png");
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\PERDEU.png");
                Game_Over.Play();
            }
            else if (pontuacaoJogador > 21 && pontuacaoComputador > 21)
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\NINGUEM.png");
            }

            else if (pontuacaoJogador > 21)
            {
                pic_black.Visible = true;
                pic_black.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\EXTRAPOLOU.png");
                pic_black.BringToFront();
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\PERDEU.png");
                Game_Over.Play();
            }
            else if (pontuacaoComputador > 21)
            {
                pic_black_pc.Visible = true;
                pic_black_pc.BringToFront();
                pic_black_pc.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\EXTRAPOLOU.png");
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\GANHOU.png");
                Ganhou.Play();
            }
            else if (pontuacaoJogador == pontuacaoComputador || pontuacaoJogador < 21 && pontuacaoComputador < 21)
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\NINGUEM.png");
            }
            else if (pontuacaoJogador < pontuacaoComputador)
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\PERDEU.png");
                Game_Over.Play();
            }
            else if (pontuacaoComputador < pontuacaoJogador)
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\GANHOU.png");
                Ganhou.Play();
            }
            else
            {
                pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\GANHOU.png");
                Ganhou.Play();
            }

            btn_puxar.Enabled = false;
            btn_reiniciar.Enabled = true;
            pic_resultado.Visible = true;
        }

        private void btn_desistir_Click(object sender, EventArgs e)
        {
            botao.Play();
            btn_parar.Enabled = false;
            btn_puxar.Enabled = false;
            pic_resultado.Visible = true;
            pic_resultado.Image = Image.FromFile(@"E:\Projetos UNINOVE C#\BlackJack\PERDEU.png");
            Game_Over.Play();
        }

        private void btn_inicio_Click(object sender, EventArgs e)
        {
            string nomeUsuario = txt_nomeUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeUsuario))
            {
                nomeUsuario = "Jogador 1";
            }

            lbl_usuario.Text = nomeUsuario;

            painel_usuario.Visible = false;
            painel_jogo.Visible = true;
        }

        private void txt_nomeUsuario_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_nomeUsuario_Enter(object sender, EventArgs e)
        {
            
        }

        private void txt_nomeUsuario_Leave(object sender, EventArgs e)
        {
           
        }

        private void btn_inicio_jogo_Click(object sender, EventArgs e)
        {            
            txt_nomeUsuario.Text = "";
            painel_usuario.Visible = true;
            painel_jogo.Visible = false;
            ResetarPictureBoxes();
            baralho.Clear();
            pontuacaoJogador = 0;
            pontuacaoComputador = 0;
            lbl_jogador2.Text = "0";
            lbl_jogador1.Text = "0";
            pic_resultado.Image = null;
            pic_black.Image = null;
            pic_black_pc.Image = null;            
        }

        private void lbl_pc_Click(object sender, EventArgs e)
        {
            
        }
    }
}
