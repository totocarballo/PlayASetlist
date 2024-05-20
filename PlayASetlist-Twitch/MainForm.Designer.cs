namespace PlayASetlist_Twitch
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LblTimeLeft = new System.Windows.Forms.Label();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.GbxSettings = new System.Windows.Forms.GroupBox();
            this.TxtTwitchChannel = new System.Windows.Forms.TextBox();
            this.LblTwitchChannel = new System.Windows.Forms.Label();
            this.LblTwitchOAuth = new System.Windows.Forms.Label();
            this.TxtTwitchOAuth = new System.Windows.Forms.TextBox();
            this.LblTwitchUsername = new System.Windows.Forms.Label();
            this.TxtTwitchUsername = new System.Windows.Forms.TextBox();
            this.ChkTwitch = new System.Windows.Forms.CheckBox();
            this.LblWait = new System.Windows.Forms.Label();
            this.NumWait = new System.Windows.Forms.NumericUpDown();
            this.LblFilterInstrument = new System.Windows.Forms.Label();
            this.CbxFilterInstrument = new System.Windows.Forms.ComboBox();
            this.LblSecondsToVote = new System.Windows.Forms.Label();
            this.NumSecondsToVote = new System.Windows.Forms.NumericUpDown();
            this.LblSongsPerSetlist = new System.Windows.Forms.Label();
            this.NumSongsPerSetlist = new System.Windows.Forms.NumericUpDown();
            this.GbxVote = new System.Windows.Forms.GroupBox();
            this.TxtProperty6 = new System.Windows.Forms.TextBox();
            this.BtnVote6 = new System.Windows.Forms.Button();
            this.LblVote6 = new System.Windows.Forms.Label();
            this.TxtProperty5 = new System.Windows.Forms.TextBox();
            this.BtnVote5 = new System.Windows.Forms.Button();
            this.LblVote5 = new System.Windows.Forms.Label();
            this.TxtProperty4 = new System.Windows.Forms.TextBox();
            this.BtnVote4 = new System.Windows.Forms.Button();
            this.LblVote4 = new System.Windows.Forms.Label();
            this.TxtProperty3 = new System.Windows.Forms.TextBox();
            this.BtnVote3 = new System.Windows.Forms.Button();
            this.LblVote3 = new System.Windows.Forms.Label();
            this.TxtProperty2 = new System.Windows.Forms.TextBox();
            this.BtnVote2 = new System.Windows.Forms.Button();
            this.LblVote2 = new System.Windows.Forms.Label();
            this.TxtProperty1 = new System.Windows.Forms.TextBox();
            this.BtnVote1 = new System.Windows.Forms.Button();
            this.LblVote1 = new System.Windows.Forms.Label();
            this.LstVoted = new System.Windows.Forms.ListBox();
            this.TipMessage = new System.Windows.Forms.ToolTip(this.components);
            this.GbxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumSecondsToVote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumSongsPerSetlist)).BeginInit();
            this.GbxVote.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTimeLeft
            // 
            this.LblTimeLeft.Location = new System.Drawing.Point(93, 208);
            this.LblTimeLeft.Name = "LblTimeLeft";
            this.LblTimeLeft.Size = new System.Drawing.Size(246, 29);
            this.LblTimeLeft.TabIndex = 17;
            this.LblTimeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(345, 211);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 16;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(12, 211);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 13;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // GbxSettings
            // 
            this.GbxSettings.Controls.Add(this.TxtTwitchChannel);
            this.GbxSettings.Controls.Add(this.LblTwitchChannel);
            this.GbxSettings.Controls.Add(this.LblTwitchOAuth);
            this.GbxSettings.Controls.Add(this.TxtTwitchOAuth);
            this.GbxSettings.Controls.Add(this.LblTwitchUsername);
            this.GbxSettings.Controls.Add(this.TxtTwitchUsername);
            this.GbxSettings.Controls.Add(this.ChkTwitch);
            this.GbxSettings.Controls.Add(this.LblWait);
            this.GbxSettings.Controls.Add(this.NumWait);
            this.GbxSettings.Controls.Add(this.LblFilterInstrument);
            this.GbxSettings.Controls.Add(this.CbxFilterInstrument);
            this.GbxSettings.Controls.Add(this.LblSecondsToVote);
            this.GbxSettings.Controls.Add(this.NumSecondsToVote);
            this.GbxSettings.Controls.Add(this.LblSongsPerSetlist);
            this.GbxSettings.Controls.Add(this.NumSongsPerSetlist);
            this.GbxSettings.Location = new System.Drawing.Point(426, 12);
            this.GbxSettings.Name = "GbxSettings";
            this.GbxSettings.Size = new System.Drawing.Size(420, 124);
            this.GbxSettings.TabIndex = 15;
            this.GbxSettings.TabStop = false;
            this.GbxSettings.Text = "CurrentSettings";
            this.GbxSettings.Leave += new System.EventHandler(this.GbxSettings_Leave);
            // 
            // TxtTwitchChannel
            // 
            this.TxtTwitchChannel.Location = new System.Drawing.Point(321, 16);
            this.TxtTwitchChannel.Name = "TxtTwitchChannel";
            this.TxtTwitchChannel.Size = new System.Drawing.Size(93, 20);
            this.TxtTwitchChannel.TabIndex = 45;
            // 
            // LblTwitchChannel
            // 
            this.LblTwitchChannel.Location = new System.Drawing.Point(231, 16);
            this.LblTwitchChannel.Margin = new System.Windows.Forms.Padding(3);
            this.LblTwitchChannel.Name = "LblTwitchChannel";
            this.LblTwitchChannel.Size = new System.Drawing.Size(84, 20);
            this.LblTwitchChannel.TabIndex = 48;
            this.LblTwitchChannel.Text = "Twitch Channel:";
            this.LblTwitchChannel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTwitchOAuth
            // 
            this.LblTwitchOAuth.Location = new System.Drawing.Point(239, 42);
            this.LblTwitchOAuth.Margin = new System.Windows.Forms.Padding(3);
            this.LblTwitchOAuth.Name = "LblTwitchOAuth";
            this.LblTwitchOAuth.Size = new System.Drawing.Size(76, 20);
            this.LblTwitchOAuth.TabIndex = 47;
            this.LblTwitchOAuth.Text = "Twitch OAuth:";
            this.LblTwitchOAuth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtTwitchOAuth
            // 
            this.TxtTwitchOAuth.Location = new System.Drawing.Point(321, 41);
            this.TxtTwitchOAuth.Name = "TxtTwitchOAuth";
            this.TxtTwitchOAuth.PasswordChar = '*';
            this.TxtTwitchOAuth.Size = new System.Drawing.Size(93, 20);
            this.TxtTwitchOAuth.TabIndex = 44;
            // 
            // LblTwitchUsername
            // 
            this.LblTwitchUsername.Location = new System.Drawing.Point(6, 43);
            this.LblTwitchUsername.Margin = new System.Windows.Forms.Padding(3);
            this.LblTwitchUsername.Name = "LblTwitchUsername";
            this.LblTwitchUsername.Size = new System.Drawing.Size(100, 17);
            this.LblTwitchUsername.TabIndex = 46;
            this.LblTwitchUsername.Text = "Twitch Username:";
            this.LblTwitchUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtTwitchUsername
            // 
            this.TxtTwitchUsername.Location = new System.Drawing.Point(112, 42);
            this.TxtTwitchUsername.Name = "TxtTwitchUsername";
            this.TxtTwitchUsername.Size = new System.Drawing.Size(121, 20);
            this.TxtTwitchUsername.TabIndex = 43;
            // 
            // ChkTwitch
            // 
            this.ChkTwitch.AutoSize = true;
            this.ChkTwitch.Location = new System.Drawing.Point(6, 19);
            this.ChkTwitch.Name = "ChkTwitch";
            this.ChkTwitch.Size = new System.Drawing.Size(129, 17);
            this.ChkTwitch.TabIndex = 42;
            this.ChkTwitch.Text = "Twitch Chat can Vote";
            this.ChkTwitch.UseVisualStyleBackColor = true;
            this.ChkTwitch.CheckedChanged += new System.EventHandler(this.ChkTwitch_CheckedChanged);
            // 
            // LblWait
            // 
            this.LblWait.Location = new System.Drawing.Point(239, 94);
            this.LblWait.Margin = new System.Windows.Forms.Padding(3);
            this.LblWait.Name = "LblWait";
            this.LblWait.Size = new System.Drawing.Size(126, 20);
            this.LblWait.TabIndex = 35;
            this.LblWait.Text = "Seconds between Vote:";
            this.LblWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumWait
            // 
            this.NumWait.Location = new System.Drawing.Point(371, 94);
            this.NumWait.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumWait.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumWait.Name = "NumWait";
            this.NumWait.Size = new System.Drawing.Size(43, 20);
            this.NumWait.TabIndex = 34;
            this.NumWait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LblFilterInstrument
            // 
            this.LblFilterInstrument.Location = new System.Drawing.Point(6, 95);
            this.LblFilterInstrument.Margin = new System.Windows.Forms.Padding(3);
            this.LblFilterInstrument.Name = "LblFilterInstrument";
            this.LblFilterInstrument.Size = new System.Drawing.Size(100, 17);
            this.LblFilterInstrument.TabIndex = 33;
            this.LblFilterInstrument.Text = "Filter by instrument:";
            this.LblFilterInstrument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CbxFilterInstrument
            // 
            this.CbxFilterInstrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxFilterInstrument.FormattingEnabled = true;
            this.CbxFilterInstrument.Items.AddRange(new object[] {
            "None",
            "Guitar",
            "Bass",
            "Drums",
            "Drums Pro",
            "Keys",
            "Guitar GHL",
            "Bass GHL"});
            this.CbxFilterInstrument.Location = new System.Drawing.Point(112, 94);
            this.CbxFilterInstrument.Name = "CbxFilterInstrument";
            this.CbxFilterInstrument.Size = new System.Drawing.Size(121, 21);
            this.CbxFilterInstrument.TabIndex = 32;
            this.CbxFilterInstrument.SelectedIndexChanged += new System.EventHandler(this.CbxFilterInstrument_SelectedIndexChanged);
            // 
            // LblSecondsToVote
            // 
            this.LblSecondsToVote.Location = new System.Drawing.Point(274, 68);
            this.LblSecondsToVote.Margin = new System.Windows.Forms.Padding(3);
            this.LblSecondsToVote.Name = "LblSecondsToVote";
            this.LblSecondsToVote.Size = new System.Drawing.Size(91, 17);
            this.LblSecondsToVote.TabIndex = 29;
            this.LblSecondsToVote.Text = "Seconds to Vote:";
            this.LblSecondsToVote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumSecondsToVote
            // 
            this.NumSecondsToVote.Location = new System.Drawing.Point(371, 68);
            this.NumSecondsToVote.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.NumSecondsToVote.Name = "NumSecondsToVote";
            this.NumSecondsToVote.Size = new System.Drawing.Size(43, 20);
            this.NumSecondsToVote.TabIndex = 28;
            this.NumSecondsToVote.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // LblSongsPerSetlist
            // 
            this.LblSongsPerSetlist.Location = new System.Drawing.Point(6, 68);
            this.LblSongsPerSetlist.Margin = new System.Windows.Forms.Padding(3);
            this.LblSongsPerSetlist.Name = "LblSongsPerSetlist";
            this.LblSongsPerSetlist.Size = new System.Drawing.Size(100, 17);
            this.LblSongsPerSetlist.TabIndex = 27;
            this.LblSongsPerSetlist.Text = "Songs per Setlist:";
            this.LblSongsPerSetlist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumSongsPerSetlist
            // 
            this.NumSongsPerSetlist.Location = new System.Drawing.Point(112, 68);
            this.NumSongsPerSetlist.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NumSongsPerSetlist.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumSongsPerSetlist.Name = "NumSongsPerSetlist";
            this.NumSongsPerSetlist.Size = new System.Drawing.Size(43, 20);
            this.NumSongsPerSetlist.TabIndex = 26;
            this.NumSongsPerSetlist.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // GbxVote
            // 
            this.GbxVote.Controls.Add(this.TxtProperty6);
            this.GbxVote.Controls.Add(this.BtnVote6);
            this.GbxVote.Controls.Add(this.LblVote6);
            this.GbxVote.Controls.Add(this.TxtProperty5);
            this.GbxVote.Controls.Add(this.BtnVote5);
            this.GbxVote.Controls.Add(this.LblVote5);
            this.GbxVote.Controls.Add(this.TxtProperty4);
            this.GbxVote.Controls.Add(this.BtnVote4);
            this.GbxVote.Controls.Add(this.LblVote4);
            this.GbxVote.Controls.Add(this.TxtProperty3);
            this.GbxVote.Controls.Add(this.BtnVote3);
            this.GbxVote.Controls.Add(this.LblVote3);
            this.GbxVote.Controls.Add(this.TxtProperty2);
            this.GbxVote.Controls.Add(this.BtnVote2);
            this.GbxVote.Controls.Add(this.LblVote2);
            this.GbxVote.Controls.Add(this.TxtProperty1);
            this.GbxVote.Controls.Add(this.BtnVote1);
            this.GbxVote.Controls.Add(this.LblVote1);
            this.GbxVote.Location = new System.Drawing.Point(12, 12);
            this.GbxVote.Name = "GbxVote";
            this.GbxVote.Size = new System.Drawing.Size(408, 193);
            this.GbxVote.TabIndex = 14;
            this.GbxVote.TabStop = false;
            this.GbxVote.Text = "Vote:";
            // 
            // TxtProperty6
            // 
            this.TxtProperty6.Location = new System.Drawing.Point(87, 166);
            this.TxtProperty6.Name = "TxtProperty6";
            this.TxtProperty6.Size = new System.Drawing.Size(275, 20);
            this.TxtProperty6.TabIndex = 33;
            // 
            // BtnVote6
            // 
            this.BtnVote6.Location = new System.Drawing.Point(6, 164);
            this.BtnVote6.Name = "BtnVote6";
            this.BtnVote6.Size = new System.Drawing.Size(75, 23);
            this.BtnVote6.TabIndex = 31;
            this.BtnVote6.Text = "Vote #6";
            this.BtnVote6.UseVisualStyleBackColor = false;
            this.BtnVote6.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // LblVote6
            // 
            this.LblVote6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVote6.Location = new System.Drawing.Point(368, 164);
            this.LblVote6.Name = "LblVote6";
            this.LblVote6.Size = new System.Drawing.Size(34, 23);
            this.LblVote6.TabIndex = 32;
            this.LblVote6.Text = "0";
            this.LblVote6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtProperty5
            // 
            this.TxtProperty5.Location = new System.Drawing.Point(87, 137);
            this.TxtProperty5.Name = "TxtProperty5";
            this.TxtProperty5.Size = new System.Drawing.Size(275, 20);
            this.TxtProperty5.TabIndex = 30;
            // 
            // BtnVote5
            // 
            this.BtnVote5.Location = new System.Drawing.Point(6, 135);
            this.BtnVote5.Name = "BtnVote5";
            this.BtnVote5.Size = new System.Drawing.Size(75, 23);
            this.BtnVote5.TabIndex = 28;
            this.BtnVote5.Text = "Vote #5";
            this.BtnVote5.UseVisualStyleBackColor = false;
            this.BtnVote5.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // LblVote5
            // 
            this.LblVote5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVote5.Location = new System.Drawing.Point(368, 135);
            this.LblVote5.Name = "LblVote5";
            this.LblVote5.Size = new System.Drawing.Size(34, 23);
            this.LblVote5.TabIndex = 29;
            this.LblVote5.Text = "0";
            this.LblVote5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtProperty4
            // 
            this.TxtProperty4.Location = new System.Drawing.Point(87, 108);
            this.TxtProperty4.Name = "TxtProperty4";
            this.TxtProperty4.Size = new System.Drawing.Size(275, 20);
            this.TxtProperty4.TabIndex = 27;
            // 
            // BtnVote4
            // 
            this.BtnVote4.Location = new System.Drawing.Point(6, 106);
            this.BtnVote4.Name = "BtnVote4";
            this.BtnVote4.Size = new System.Drawing.Size(75, 23);
            this.BtnVote4.TabIndex = 25;
            this.BtnVote4.Text = "Vote #4";
            this.BtnVote4.UseVisualStyleBackColor = false;
            this.BtnVote4.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // LblVote4
            // 
            this.LblVote4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVote4.Location = new System.Drawing.Point(368, 106);
            this.LblVote4.Name = "LblVote4";
            this.LblVote4.Size = new System.Drawing.Size(34, 23);
            this.LblVote4.TabIndex = 26;
            this.LblVote4.Text = "0";
            this.LblVote4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtProperty3
            // 
            this.TxtProperty3.Location = new System.Drawing.Point(87, 79);
            this.TxtProperty3.Name = "TxtProperty3";
            this.TxtProperty3.Size = new System.Drawing.Size(275, 20);
            this.TxtProperty3.TabIndex = 24;
            // 
            // BtnVote3
            // 
            this.BtnVote3.Location = new System.Drawing.Point(6, 77);
            this.BtnVote3.Name = "BtnVote3";
            this.BtnVote3.Size = new System.Drawing.Size(75, 23);
            this.BtnVote3.TabIndex = 22;
            this.BtnVote3.Text = "Vote #3";
            this.BtnVote3.UseVisualStyleBackColor = false;
            this.BtnVote3.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // LblVote3
            // 
            this.LblVote3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVote3.Location = new System.Drawing.Point(368, 77);
            this.LblVote3.Name = "LblVote3";
            this.LblVote3.Size = new System.Drawing.Size(34, 23);
            this.LblVote3.TabIndex = 23;
            this.LblVote3.Text = "0";
            this.LblVote3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtProperty2
            // 
            this.TxtProperty2.Location = new System.Drawing.Point(87, 50);
            this.TxtProperty2.Name = "TxtProperty2";
            this.TxtProperty2.Size = new System.Drawing.Size(275, 20);
            this.TxtProperty2.TabIndex = 21;
            // 
            // BtnVote2
            // 
            this.BtnVote2.Location = new System.Drawing.Point(6, 48);
            this.BtnVote2.Name = "BtnVote2";
            this.BtnVote2.Size = new System.Drawing.Size(75, 23);
            this.BtnVote2.TabIndex = 19;
            this.BtnVote2.Text = "Vote #2";
            this.BtnVote2.UseVisualStyleBackColor = false;
            this.BtnVote2.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // LblVote2
            // 
            this.LblVote2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVote2.Location = new System.Drawing.Point(368, 48);
            this.LblVote2.Name = "LblVote2";
            this.LblVote2.Size = new System.Drawing.Size(34, 23);
            this.LblVote2.TabIndex = 20;
            this.LblVote2.Text = "0";
            this.LblVote2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtProperty1
            // 
            this.TxtProperty1.Location = new System.Drawing.Point(87, 21);
            this.TxtProperty1.Name = "TxtProperty1";
            this.TxtProperty1.Size = new System.Drawing.Size(275, 20);
            this.TxtProperty1.TabIndex = 18;
            // 
            // BtnVote1
            // 
            this.BtnVote1.Location = new System.Drawing.Point(6, 19);
            this.BtnVote1.Name = "BtnVote1";
            this.BtnVote1.Size = new System.Drawing.Size(75, 23);
            this.BtnVote1.TabIndex = 0;
            this.BtnVote1.Text = "Vote #1";
            this.BtnVote1.UseVisualStyleBackColor = false;
            this.BtnVote1.Click += new System.EventHandler(this.BtnVote_Click);
            // 
            // LblVote1
            // 
            this.LblVote1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblVote1.Location = new System.Drawing.Point(368, 19);
            this.LblVote1.Name = "LblVote1";
            this.LblVote1.Size = new System.Drawing.Size(34, 23);
            this.LblVote1.TabIndex = 2;
            this.LblVote1.Text = "0";
            this.LblVote1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LstVoted
            // 
            this.LstVoted.FormattingEnabled = true;
            this.LstVoted.Location = new System.Drawing.Point(426, 142);
            this.LstVoted.Name = "LstVoted";
            this.LstVoted.Size = new System.Drawing.Size(420, 95);
            this.LstVoted.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 246);
            this.Controls.Add(this.LblTimeLeft);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.GbxSettings);
            this.Controls.Add(this.GbxVote);
            this.Controls.Add(this.LstVoted);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Play A Setlist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.GbxSettings.ResumeLayout(false);
            this.GbxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumSecondsToVote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumSongsPerSetlist)).EndInit();
            this.GbxVote.ResumeLayout(false);
            this.GbxVote.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblTimeLeft;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.GroupBox GbxSettings;
        private System.Windows.Forms.TextBox TxtTwitchChannel;
        private System.Windows.Forms.Label LblTwitchChannel;
        private System.Windows.Forms.Label LblTwitchOAuth;
        private System.Windows.Forms.TextBox TxtTwitchOAuth;
        private System.Windows.Forms.Label LblTwitchUsername;
        private System.Windows.Forms.TextBox TxtTwitchUsername;
        private System.Windows.Forms.CheckBox ChkTwitch;
        private System.Windows.Forms.Label LblWait;
        private System.Windows.Forms.NumericUpDown NumWait;
        private System.Windows.Forms.Label LblFilterInstrument;
        private System.Windows.Forms.ComboBox CbxFilterInstrument;
        private System.Windows.Forms.Label LblSecondsToVote;
        private System.Windows.Forms.NumericUpDown NumSecondsToVote;
        private System.Windows.Forms.Label LblSongsPerSetlist;
        private System.Windows.Forms.NumericUpDown NumSongsPerSetlist;
        private System.Windows.Forms.GroupBox GbxVote;
        private System.Windows.Forms.TextBox TxtProperty6;
        private System.Windows.Forms.Button BtnVote6;
        private System.Windows.Forms.Label LblVote6;
        private System.Windows.Forms.TextBox TxtProperty5;
        private System.Windows.Forms.Button BtnVote5;
        private System.Windows.Forms.Label LblVote5;
        private System.Windows.Forms.TextBox TxtProperty4;
        private System.Windows.Forms.Button BtnVote4;
        private System.Windows.Forms.Label LblVote4;
        private System.Windows.Forms.TextBox TxtProperty3;
        private System.Windows.Forms.Button BtnVote3;
        private System.Windows.Forms.Label LblVote3;
        private System.Windows.Forms.TextBox TxtProperty2;
        private System.Windows.Forms.Button BtnVote2;
        private System.Windows.Forms.Label LblVote2;
        private System.Windows.Forms.TextBox TxtProperty1;
        private System.Windows.Forms.Button BtnVote1;
        private System.Windows.Forms.Label LblVote1;
        private System.Windows.Forms.ListBox LstVoted;
        private System.Windows.Forms.ToolTip TipMessage;
    }
}

