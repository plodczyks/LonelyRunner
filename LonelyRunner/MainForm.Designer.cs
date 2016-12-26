namespace LonelyRunner
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.circleLengthLbl = new System.Windows.Forms.Label();
            this.roundCountLbl = new System.Windows.Forms.Label();
            this.speedAccuracyLbl = new System.Windows.Forms.Label();
            this.circleLengthTbx = new System.Windows.Forms.TextBox();
            this.roundCountTbx = new System.Windows.Forms.TextBox();
            this.speedAccuracyTbx = new System.Windows.Forms.TextBox();
            this.strategyGrb = new System.Windows.Forms.GroupBox();
            this.strategyHardRbt = new System.Windows.Forms.RadioButton();
            this.strategyEasyRdb = new System.Windows.Forms.RadioButton();
            this.gameTypeGpb = new System.Windows.Forms.GroupBox();
            this.computerPlayerRbt = new System.Windows.Forms.RadioButton();
            this.playerComputerRbt = new System.Windows.Forms.RadioButton();
            this.startBtn = new System.Windows.Forms.Button();
            this.gameStateLbl = new System.Windows.Forms.Label();
            this.player1VelocityLbl = new System.Windows.Forms.Label();
            this.player2VelocityLbl = new System.Windows.Forms.Label();
            this.player1VelocityTbx = new System.Windows.Forms.TextBox();
            this.player2VelocityTbx = new System.Windows.Forms.TextBox();
            this.nextBtn = new System.Windows.Forms.Button();
            this.gameDatasLbx = new System.Windows.Forms.ListBox();
            this.runnersCountLbl = new System.Windows.Forms.Label();
            this.nLetterLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.pbCircle = new System.Windows.Forms.PictureBox();
            this.strategyGrb.SuspendLayout();
            this.gameTypeGpb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCircle)).BeginInit();
            this.SuspendLayout();
            // 
            // circleLengthLbl
            // 
            this.circleLengthLbl.AutoSize = true;
            this.circleLengthLbl.Location = new System.Drawing.Point(52, 9);
            this.circleLengthLbl.Name = "circleLengthLbl";
            this.circleLengthLbl.Size = new System.Drawing.Size(84, 13);
            this.circleLengthLbl.TabIndex = 0;
            this.circleLengthLbl.Text = "Długość okręgu";
            // 
            // roundCountLbl
            // 
            this.roundCountLbl.AutoSize = true;
            this.roundCountLbl.Location = new System.Drawing.Point(75, 51);
            this.roundCountLbl.Name = "roundCountLbl";
            this.roundCountLbl.Size = new System.Drawing.Size(81, 13);
            this.roundCountLbl.TabIndex = 1;
            this.roundCountLbl.Text = "Liczba rund (T):";
            // 
            // speedAccuracyLbl
            // 
            this.speedAccuracyLbl.AutoSize = true;
            this.speedAccuracyLbl.Location = new System.Drawing.Point(12, 78);
            this.speedAccuracyLbl.Name = "speedAccuracyLbl";
            this.speedAccuracyLbl.Size = new System.Drawing.Size(144, 13);
            this.speedAccuracyLbl.TabIndex = 2;
            this.speedAccuracyLbl.Text = "Dokładność prędkości (1/k):";
            // 
            // circleLengthTbx
            // 
            this.circleLengthTbx.Location = new System.Drawing.Point(162, 13);
            this.circleLengthTbx.Name = "circleLengthTbx";
            this.circleLengthTbx.Size = new System.Drawing.Size(48, 20);
            this.circleLengthTbx.TabIndex = 4;
            // 
            // roundCountTbx
            // 
            this.roundCountTbx.Location = new System.Drawing.Point(162, 49);
            this.roundCountTbx.Name = "roundCountTbx";
            this.roundCountTbx.Size = new System.Drawing.Size(48, 20);
            this.roundCountTbx.TabIndex = 5;
            // 
            // speedAccuracyTbx
            // 
            this.speedAccuracyTbx.Location = new System.Drawing.Point(162, 78);
            this.speedAccuracyTbx.Name = "speedAccuracyTbx";
            this.speedAccuracyTbx.Size = new System.Drawing.Size(48, 20);
            this.speedAccuracyTbx.TabIndex = 6;
            // 
            // strategyGrb
            // 
            this.strategyGrb.Controls.Add(this.strategyHardRbt);
            this.strategyGrb.Controls.Add(this.strategyEasyRdb);
            this.strategyGrb.Location = new System.Drawing.Point(233, 20);
            this.strategyGrb.Name = "strategyGrb";
            this.strategyGrb.Size = new System.Drawing.Size(117, 72);
            this.strategyGrb.TabIndex = 7;
            this.strategyGrb.TabStop = false;
            this.strategyGrb.Text = "Strategia";
            // 
            // strategyHardRbt
            // 
            this.strategyHardRbt.AutoSize = true;
            this.strategyHardRbt.Location = new System.Drawing.Point(7, 43);
            this.strategyHardRbt.Name = "strategyHardRbt";
            this.strategyHardRbt.Size = new System.Drawing.Size(59, 17);
            this.strategyHardRbt.TabIndex = 1;
            this.strategyHardRbt.Text = "Trudna";
            this.strategyHardRbt.UseVisualStyleBackColor = true;
            // 
            // strategyEasyRdb
            // 
            this.strategyEasyRdb.AutoSize = true;
            this.strategyEasyRdb.Checked = true;
            this.strategyEasyRdb.Location = new System.Drawing.Point(7, 20);
            this.strategyEasyRdb.Name = "strategyEasyRdb";
            this.strategyEasyRdb.Size = new System.Drawing.Size(55, 17);
            this.strategyEasyRdb.TabIndex = 0;
            this.strategyEasyRdb.TabStop = true;
            this.strategyEasyRdb.Text = "Łatwa";
            this.strategyEasyRdb.UseVisualStyleBackColor = true;
            // 
            // gameTypeGpb
            // 
            this.gameTypeGpb.Controls.Add(this.computerPlayerRbt);
            this.gameTypeGpb.Controls.Add(this.playerComputerRbt);
            this.gameTypeGpb.Location = new System.Drawing.Point(375, 20);
            this.gameTypeGpb.Name = "gameTypeGpb";
            this.gameTypeGpb.Size = new System.Drawing.Size(128, 72);
            this.gameTypeGpb.TabIndex = 8;
            this.gameTypeGpb.TabStop = false;
            this.gameTypeGpb.Text = "Rodzaj gry";
            // 
            // computerPlayerRbt
            // 
            this.computerPlayerRbt.AutoSize = true;
            this.computerPlayerRbt.Location = new System.Drawing.Point(6, 43);
            this.computerPlayerRbt.Name = "computerPlayerRbt";
            this.computerPlayerRbt.Size = new System.Drawing.Size(115, 17);
            this.computerPlayerRbt.TabIndex = 2;
            this.computerPlayerRbt.Text = "Komputer vs Gracz";
            this.computerPlayerRbt.UseVisualStyleBackColor = true;
            // 
            // playerComputerRbt
            // 
            this.playerComputerRbt.AutoSize = true;
            this.playerComputerRbt.Checked = true;
            this.playerComputerRbt.Location = new System.Drawing.Point(6, 20);
            this.playerComputerRbt.Name = "playerComputerRbt";
            this.playerComputerRbt.Size = new System.Drawing.Size(115, 17);
            this.playerComputerRbt.TabIndex = 1;
            this.playerComputerRbt.TabStop = true;
            this.playerComputerRbt.Text = "Gracz vs Komputer";
            this.playerComputerRbt.UseVisualStyleBackColor = true;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(523, 76);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 9;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartGameClick);
            // 
            // gameStateLbl
            // 
            this.gameStateLbl.AutoSize = true;
            this.gameStateLbl.Location = new System.Drawing.Point(12, 137);
            this.gameStateLbl.Name = "gameStateLbl";
            this.gameStateLbl.Size = new System.Drawing.Size(156, 13);
            this.gameStateLbl.TabIndex = 11;
            this.gameStateLbl.Text = "Aktualne położenia i prędkości:";
            // 
            // player1VelocityLbl
            // 
            this.player1VelocityLbl.AutoSize = true;
            this.player1VelocityLbl.Location = new System.Drawing.Point(309, 118);
            this.player1VelocityLbl.Name = "player1VelocityLbl";
            this.player1VelocityLbl.Size = new System.Drawing.Size(121, 13);
            this.player1VelocityLbl.TabIndex = 12;
            this.player1VelocityLbl.Text = "Prędkość gracza 1 (l/k):";
            // 
            // player2VelocityLbl
            // 
            this.player2VelocityLbl.AutoSize = true;
            this.player2VelocityLbl.Location = new System.Drawing.Point(439, 118);
            this.player2VelocityLbl.Name = "player2VelocityLbl";
            this.player2VelocityLbl.Size = new System.Drawing.Size(121, 13);
            this.player2VelocityLbl.TabIndex = 13;
            this.player2VelocityLbl.Text = "Prędkość gracza 2 (l/k):";
            // 
            // player1VelocityTbx
            // 
            this.player1VelocityTbx.Enabled = false;
            this.player1VelocityTbx.Location = new System.Drawing.Point(312, 134);
            this.player1VelocityTbx.Name = "player1VelocityTbx";
            this.player1VelocityTbx.Size = new System.Drawing.Size(100, 20);
            this.player1VelocityTbx.TabIndex = 14;
            // 
            // player2VelocityTbx
            // 
            this.player2VelocityTbx.Enabled = false;
            this.player2VelocityTbx.Location = new System.Drawing.Point(442, 134);
            this.player2VelocityTbx.Name = "player2VelocityTbx";
            this.player2VelocityTbx.Size = new System.Drawing.Size(100, 20);
            this.player2VelocityTbx.TabIndex = 15;
            // 
            // nextBtn
            // 
            this.nextBtn.Enabled = false;
            this.nextBtn.Location = new System.Drawing.Point(555, 131);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(62, 23);
            this.nextBtn.TabIndex = 16;
            this.nextBtn.Text = "Dalej";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.CheckPlayerMove);
            // 
            // gameDatasLbx
            // 
            this.gameDatasLbx.FormattingEnabled = true;
            this.gameDatasLbx.Location = new System.Drawing.Point(15, 201);
            this.gameDatasLbx.Name = "gameDatasLbx";
            this.gameDatasLbx.Size = new System.Drawing.Size(335, 277);
            this.gameDatasLbx.TabIndex = 17;
            // 
            // runnersCountLbl
            // 
            this.runnersCountLbl.AutoSize = true;
            this.runnersCountLbl.Location = new System.Drawing.Point(52, 22);
            this.runnersCountLbl.Name = "runnersCountLbl";
            this.runnersCountLbl.Size = new System.Drawing.Size(83, 13);
            this.runnersCountLbl.TabIndex = 18;
            this.runnersCountLbl.Text = "Liczba biegaczy";
            // 
            // nLetterLbl
            // 
            this.nLetterLbl.AutoSize = true;
            this.nLetterLbl.Location = new System.Drawing.Point(132, 16);
            this.nLetterLbl.Name = "nLetterLbl";
            this.nLetterLbl.Size = new System.Drawing.Size(24, 13);
            this.nLetterLbl.TabIndex = 19;
            this.nLetterLbl.Text = "(N):";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Enabled = false;
            this.cancelBtn.Location = new System.Drawing.Point(555, 162);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(62, 23);
            this.cancelBtn.TabIndex = 20;
            this.cancelBtn.Text = "Anuluj grę";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelGameClick);
            // 
            // pbCircle
            // 
            this.pbCircle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbCircle.Location = new System.Drawing.Point(410, 201);
            this.pbCircle.Name = "pbCircle";
            this.pbCircle.Size = new System.Drawing.Size(300, 300);
            this.pbCircle.TabIndex = 21;
            this.pbCircle.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 672);
            this.Controls.Add(this.pbCircle);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.nLetterLbl);
            this.Controls.Add(this.runnersCountLbl);
            this.Controls.Add(this.gameDatasLbx);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.player2VelocityTbx);
            this.Controls.Add(this.player1VelocityTbx);
            this.Controls.Add(this.player2VelocityLbl);
            this.Controls.Add(this.player1VelocityLbl);
            this.Controls.Add(this.gameStateLbl);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.gameTypeGpb);
            this.Controls.Add(this.strategyGrb);
            this.Controls.Add(this.speedAccuracyTbx);
            this.Controls.Add(this.roundCountTbx);
            this.Controls.Add(this.circleLengthTbx);
            this.Controls.Add(this.speedAccuracyLbl);
            this.Controls.Add(this.roundCountLbl);
            this.Controls.Add(this.circleLengthLbl);
            this.Name = "MainForm";
            this.Text = "Samotny Biegacz";
            this.strategyGrb.ResumeLayout(false);
            this.strategyGrb.PerformLayout();
            this.gameTypeGpb.ResumeLayout(false);
            this.gameTypeGpb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCircle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label circleLengthLbl;
        private System.Windows.Forms.Label roundCountLbl;
        private System.Windows.Forms.Label speedAccuracyLbl;
        private System.Windows.Forms.TextBox circleLengthTbx;
        private System.Windows.Forms.TextBox roundCountTbx;
        private System.Windows.Forms.TextBox speedAccuracyTbx;
        private System.Windows.Forms.GroupBox strategyGrb;
        private System.Windows.Forms.RadioButton strategyHardRbt;
        private System.Windows.Forms.RadioButton strategyEasyRdb;
        private System.Windows.Forms.GroupBox gameTypeGpb;
        private System.Windows.Forms.RadioButton computerPlayerRbt;
        private System.Windows.Forms.RadioButton playerComputerRbt;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label gameStateLbl;
        private System.Windows.Forms.Label player1VelocityLbl;
        private System.Windows.Forms.Label player2VelocityLbl;
        private System.Windows.Forms.TextBox player1VelocityTbx;
        private System.Windows.Forms.TextBox player2VelocityTbx;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.ListBox gameDatasLbx;
        private System.Windows.Forms.Label runnersCountLbl;
        private System.Windows.Forms.Label nLetterLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.PictureBox pbCircle;
    }
}

