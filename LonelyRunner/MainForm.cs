using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LonelyRunner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            timer=new Timer(){ Interval=1000};
            timer.Tick+=RealizeNextMove;
        }

        LonelyRunnerEngine engine = new LonelyRunnerEngine();
        GameType gameType;
        Timer timer=new Timer(){ Interval=1000};

        private void startBtn_Click(object sender, EventArgs e)
        {
            string error = "";
            string nString, kString, tString;
            int n, k = 0, t;

            nString = circleLengthTbx.Text;
            if (!int.TryParse(nString, out n) || n <= 0 || n % 2 == 1)
                error += "Niepoprawna wartość długości okręgu.\n";

            tString = roundCountTbx.Text;
            if (!int.TryParse(tString, out t) || t <= 0)
                error += "Niepoprawna liczba rund.\n";

            kString = speedAccuracyTbx.Text;
            if (kString.Length < 3 || kString[0] != '1' || kString[1] != '/' || !int.TryParse(kString.Substring(2), out k) || k <= 0)
                error += "Niepoprawna wartość dokładności prędkości.\n";

            if (error != "")
            {
                MessageBox.Show(error, "Błędne dane"); return;
            }

            if (strategyEasyRdb.Checked) { engine.Strategy = Strategy.EASY; }
            else { engine.Strategy = Strategy.HARD; }

            if (playerComputerRbt.Checked) { engine.GameType = gameType = GameType.PLAYER_VS_COMPUTER; }
            else { engine.GameType = gameType = GameType.COMPUTER_VS_PLAYER; }

            circleLengthTbx.Enabled = false; roundCountTbx.Enabled = false; speedAccuracyTbx.Enabled = false;
            strategyGrb.Enabled = false; gameTypeGpb.Enabled = false; startBtn.Enabled = false;

            gameDatasLbx.Items.Clear();
            player1VelocityTbx.Text = player2VelocityTbx.Text = "";
            engine.StartGame(n, t, k);
            WaitForNextMove();
        }


        public void WaitForNextMove()
        {          
            timer.Start();
        }

        void RealizeNextMove(object sender, EventArgs e)
        {
            timer.Stop();
            if (engine.N == engine.Positions.Count) { CheckGameState(); }
            else
            {
                player1VelocityTbx.Text = player2VelocityTbx.Text = "";
                //computer moving
                if (gameType == GameType.COMPUTER_VS_PLAYER)
                {
                    int l = engine.GenerateComputerVelocity();
                    player1VelocityTbx.Text = l + "/" + engine.k;
                    player2VelocityTbx.Enabled = true;
                }
                else
                {
                    player1VelocityTbx.Enabled = true;
                }
                nextBtn.Enabled = true;
                //wait for player move
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (gameType == GameType.COMPUTER_VS_PLAYER)
            {
                int l = ReadVelocity(player2VelocityTbx.Text);
                if (l == 0 || !engine.AddVelocity(l))
                {
                    MessageBox.Show("Niepoprawna wartość prędkości", "Błędne dane"); return;
                }
            }
            else
            {
                int l = ReadVelocity(player1VelocityTbx.Text);
                if (l == 0 || !engine.AddVelocity(l))
                {
                    MessageBox.Show("Niepoprawna wartość prędkości", "Błędne dane"); return;
                }
                int l2 = engine.GenerateComputerVelocity();
                player2VelocityTbx.Text = l2 + "/" + engine.k;
            }
            nextBtn.Enabled = false;
            player1VelocityTbx.Enabled = player2VelocityTbx.Enabled = false;
            CheckGameState();
        }

        private void CheckGameState()
        {
            engine.MoveAll();
            gameDatasLbx.Items.Clear();
            for (int i = 0; i < engine.Positions.Count; i++)
            {
                gameDatasLbx.Items.Add(
                    "Pozycja: " + engine.Positions[i] + "/" + engine.k + "  " +
                    "Prędkość: " + engine.Velocities[i] + "/" + engine.k);
            }
            if (engine.CheckWinState())
            {
                MessageBox.Show("Pierwszy gracz wygrał!!");
                //disable down
                circleLengthTbx.Enabled = true; roundCountTbx.Enabled = true; speedAccuracyTbx.Enabled = true;
                strategyGrb.Enabled = true; gameTypeGpb.Enabled = true; startBtn.Enabled = true;
            }
            else
            {
                engine.ActualRound++;
                if (engine.ActualRound == engine.T)
                {
                    MessageBox.Show("Drugi gracz wygrał!!");
                    //disable down
                    circleLengthTbx.Enabled = true; roundCountTbx.Enabled = true; speedAccuracyTbx.Enabled = true;
                    strategyGrb.Enabled = true; gameTypeGpb.Enabled = true; startBtn.Enabled = true;
                }
                else
                {
                    WaitForNextMove();
                }
            }
        }

        private int ReadVelocity(string velocity)
        {
            int l = 0, k = 0;
            var parts = velocity.Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out l) && int.TryParse(parts[1], out k) && l > 0 && l < engine.N * engine.k && k == engine.k)
                return l;
            else
                return 0;
        }
    }
}
