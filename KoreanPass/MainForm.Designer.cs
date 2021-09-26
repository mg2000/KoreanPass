
namespace KoreanPass
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.koreanPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.lstPIDList = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRunPatch = new System.Windows.Forms.Button();
			this.btnRefreshPIDList = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.patchFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnShowLicense = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstPIDList
			// 
			this.lstPIDList.FormattingEnabled = true;
			this.lstPIDList.ItemHeight = 15;
			this.lstPIDList.Location = new System.Drawing.Point(12, 29);
			this.lstPIDList.Name = "lstPIDList";
			this.lstPIDList.Size = new System.Drawing.Size(362, 79);
			this.lstPIDList.Sorted = true;
			this.lstPIDList.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "1. 게임 선택";
			// 
			// btnRunPatch
			// 
			this.btnRunPatch.Location = new System.Drawing.Point(12, 140);
			this.btnRunPatch.Name = "btnRunPatch";
			this.btnRunPatch.Size = new System.Drawing.Size(364, 23);
			this.btnRunPatch.TabIndex = 5;
			this.btnRunPatch.Text = "실행";
			this.btnRunPatch.UseVisualStyleBackColor = true;
			this.btnRunPatch.Click += new System.EventHandler(this.btnRunPatch_Click);
			// 
			// btnRefreshPIDList
			// 
			this.btnRefreshPIDList.Location = new System.Drawing.Point(299, 5);
			this.btnRefreshPIDList.Name = "btnRefreshPIDList";
			this.btnRefreshPIDList.Size = new System.Drawing.Size(75, 23);
			this.btnRefreshPIDList.TabIndex = 8;
			this.btnRefreshPIDList.Text = "새로고침";
			this.btnRefreshPIDList.UseVisualStyleBackColor = true;
			this.btnRefreshPIDList.Click += new System.EventHandler(this.btnRefreshPIDList_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 122);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 15);
			this.label2.TabIndex = 9;
			this.label2.Text = "2. 데이터 추출";
			// 
			// txtResult
			// 
			this.txtResult.Location = new System.Drawing.Point(14, 169);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.Size = new System.Drawing.Size(362, 159);
			this.txtResult.TabIndex = 12;
			// 
			// btnShowLicense
			// 
			this.btnShowLicense.Location = new System.Drawing.Point(283, 334);
			this.btnShowLicense.Name = "btnShowLicense";
			this.btnShowLicense.Size = new System.Drawing.Size(89, 23);
			this.btnShowLicense.TabIndex = 13;
			this.btnShowLicense.Text = "저작권 정보";
			this.btnShowLicense.UseVisualStyleBackColor = true;
			this.btnShowLicense.Click += new System.EventHandler(this.btnShowLicense_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 364);
			this.Controls.Add(this.btnShowLicense);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnRefreshPIDList);
			this.Controls.Add(this.btnRunPatch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstPIDList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "게임패스PC 한글 패치 도우미";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog koreanPathBrowserDialog;
		private System.Windows.Forms.ListBox lstPIDList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRunPatch;
		private System.Windows.Forms.Button btnRefreshPIDList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog patchFileDialog;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnShowLicense;
	}
}

