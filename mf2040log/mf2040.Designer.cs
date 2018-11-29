namespace IncomingSample
{
    partial class frmmf2040
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
            this.lbCalls = new System.Windows.Forms.ListBox();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.localVidPlaceholder = new System.Windows.Forms.Panel();
            this.remoteVidPlaceholder = new System.Windows.Forms.Panel();
            this._tapiMgr = new JulMar.Tapi3.TTapi();
            this.lbRing = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbCalls
            // 
            this.lbCalls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCalls.FormattingEnabled = true;
            this.lbCalls.IntegralHeight = false;
            this.lbCalls.Location = new System.Drawing.Point(12, 12);
            this.lbCalls.Name = "lbCalls";
            this.lbCalls.Size = new System.Drawing.Size(284, 14);
            this.lbCalls.TabIndex = 0;
            this.lbCalls.Visible = false;
            this.lbCalls.SelectedIndexChanged += new System.EventHandler(this.OnSelectedCallChange);
            // 
            // btnAnswer
            // 
            this.btnAnswer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAnswer.Enabled = false;
            this.btnAnswer.Location = new System.Drawing.Point(-15, 48);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(74, 23);
            this.btnAnswer.TabIndex = 1;
            this.btnAnswer.Text = "Answer";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Visible = false;
            this.btnAnswer.Click += new System.EventHandler(this.OnAnswerClicked);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(91, -9);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(74, 23);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Visible = false;
            this.btnDisconnect.Click += new System.EventHandler(this.OnDisconnectClicked);
            // 
            // localVidPlaceholder
            // 
            this.localVidPlaceholder.Location = new System.Drawing.Point(180, 12);
            this.localVidPlaceholder.Name = "localVidPlaceholder";
            this.localVidPlaceholder.Size = new System.Drawing.Size(169, 156);
            this.localVidPlaceholder.TabIndex = 3;
            this.localVidPlaceholder.Visible = false;
            // 
            // remoteVidPlaceholder
            // 
            this.remoteVidPlaceholder.Location = new System.Drawing.Point(355, 12);
            this.remoteVidPlaceholder.Name = "remoteVidPlaceholder";
            this.remoteVidPlaceholder.Size = new System.Drawing.Size(169, 58);
            this.remoteVidPlaceholder.TabIndex = 4;
            this.remoteVidPlaceholder.Visible = false;
            // 
            // _tapiMgr
            // 
            this._tapiMgr.TE_CALLMEDIA += new System.EventHandler<JulMar.Tapi3.TapiCallMediaEventArgs>(this.OnMediaChange);
            this._tapiMgr.TE_CALLSTATE += new System.EventHandler<JulMar.Tapi3.TapiCallStateEventArgs>(this.OnCallStateChange);
            this._tapiMgr.TE_CALLNOTIFICATION += new System.EventHandler<JulMar.Tapi3.TapiCallNotificationEventArgs>(this.OnNewCall);
            // 
            // lbRing
            // 
            this.lbRing.AutoSize = true;
            this.lbRing.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbRing.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRing.Location = new System.Drawing.Point(-1, 14);
            this.lbRing.Name = "lbRing";
            this.lbRing.Size = new System.Drawing.Size(474, 73);
            this.lbRing.TabIndex = 0;
            this.lbRing.Text = "0888 88 88 88 ";
            // 
            // tbLog
            // 
            this.tbLog.Enabled = false;
            this.tbLog.Location = new System.Drawing.Point(1, 0);
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(473, 20);
            this.tbLog.TabIndex = 0;
            this.tbLog.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmmf2040
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 89);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.lbRing);
            this.Controls.Add(this.remoteVidPlaceholder);
            this.Controls.Add(this.localVidPlaceholder);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.lbCalls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmmf2040";
            this.Text = "mf 2040 log v 0.0.2";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IncomingForm_FormClosed);
            this.Load += new System.EventHandler(this.IncomingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbCalls;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Panel localVidPlaceholder;
        private System.Windows.Forms.Panel remoteVidPlaceholder;
        private JulMar.Tapi3.TTapi _tapiMgr;
        private System.Windows.Forms.Label lbRing;
        private System.Windows.Forms.TextBox tbLog;
    }
}

