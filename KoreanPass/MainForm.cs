using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoreanPass
{
	public partial class MainForm : Form
	{
		//private List<AvailableGame> mAvailableList = new List<AvailableGame>();
			
		private List<AvailableGame> mAvailableList = null;

		public MainForm()
		{
			InitializeComponent();

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "NieR Automata",
			//	KoreanName = "니어 오토마타",
			//	ProcessName = "NieRAutomata"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Alien: Isolation",
			//	KoreanName = "에일리언: 아이솔레이션",
			//	ProcessName = "AI"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Dishorned",
			//	KoreanName = "디스아너드",
			//	ProcessName = "Dishonored"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Grim Fandango Remastered",
			//	KoreanName = "그림 판당고",
			//	ProcessName = "GrimFandango"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Yakuza 6: Song of Life",
			//	KoreanName = "용과 같이 6",
			//	ProcessName = "Yakuza6"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Wasteland 2",
			//	KoreanName = "웨이스트랜드 2",
			//	ProcessName = "WL2"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Wasteland 3",
			//	KoreanName = "웨이스트랜드 3",
			//	ProcessName = "WL3"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Psychonauts2",
			//	KoreanName = "사이코너츠2",
			//	ProcessName = "Psychonauts2-WinGDK-Shipping"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "FFVIII",
			//	KoreanName = "파이널 판타지 8",
			//	ProcessName = "FFVIII"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "Two Point Hospital",
			//	KoreanName = "투 포인트 호스피탈",
			//	ProcessName = "TPH"
			//});

			//mAvailableList.Add(new AvailableGame()
			//{
			//	Name = "AI: The Somnium Files",
			//	KoreanName = "AI: 솜니움 파일즈",
			//	ProcessName = "AI_TheSomniumFiles"
			//});
		}

		private void LoadProcessList() {
			if (mAvailableList != null)
			{
				var processList = Process.GetProcesses();

				foreach (var process in processList)
				{
					foreach (var availableProcess in mAvailableList)
					{
						if (process.ProcessName == availableProcess.ProcessName)
							lstPIDList.Items.Add($"{availableProcess.KoreanName} - {process.ProcessName}({process.Id})");
					}
				}
			}
		}

		private void btnRefreshPIDList_Click(object sender, EventArgs e)
		{
			lstPIDList.Items.Clear();
			LoadProcessList();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			RequestLoadList();
			//LoadProcessList();
		}

		private async void RequestLoadList() {
			try
			{
				var client = new HttpClient();
#if DEBUG
				var response = await client.PostAsync("http://127.0.0.1:3000/windows_mod_list", null);
#else
				var response = await client.PostAsync("http://xbox-korean-viewer-server2.herokuapp.com/windows_mod_list", null);
#endif

				var str = await response.Content.ReadAsStringAsync();

				mAvailableList = JsonConvert.DeserializeObject<List<AvailableGame>>(str);
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine($"서버에 연결할 수 없음: {e.Message}");
			}


			LoadProcessList();
		}

		private void btnRunPatch_Click(object sender, EventArgs e)
		{
			if (lstPIDList.SelectedItem == null)
			{
				MessageBox.Show("패치할 프로그램을 선택하지 않았습니다. 목록이 보이지 않는다면 해당 게임이 실행중인지 확인하시고, 새로고침 버튼을 눌러 주십시오.", "게임 미선택", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var processInfo = lstPIDList.SelectedItem as string;
			var startIdx = processInfo.IndexOf(" - ");
			startIdx += " - ".Length;

			var nameEndIdx = processInfo.LastIndexOf("(");

			var processName = processInfo.Substring(startIdx, nameEndIdx - startIdx);
			nameEndIdx++;

			var pidLength = processInfo.LastIndexOf(")") - nameEndIdx;

			var pid = processInfo.Substring(nameEndIdx, pidLength);

			var destRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GamePass");
			if (!Directory.Exists(destRootPath))
				Directory.CreateDirectory(destRootPath);

			var destPath = Path.Combine(destRootPath, processName);
			if (!Directory.Exists(destPath))
				Directory.CreateDirectory(destPath);

			MessageBox.Show("게임 데이터 추출을 시작합니다. 추출하는 동안 게임을 종료하거나 최소화하지 마시고, 창모드로 실행해 주십시오.", "게임 데이터 복사 준비", MessageBoxButtons.OK, MessageBoxIcon.Information);

			txtResult.Text = "게임 데이터 추출 시작\r\n";

			var p = new Process();
			p.StartInfo.FileName = $"{AppDomain.CurrentDomain.BaseDirectory}UWPInjector.exe";
			p.StartInfo.Arguments = $"-p {pid} -d \"{destPath}\"";
			p.Start();
			p.WaitForExit();

			txtResult.Text += "게임 데이터 추출 완료\r\n";

			var runningProcess = Process.GetProcessById(int.Parse(pid));
			runningProcess.Kill();
			runningProcess.WaitForExit();

			if (MessageBox.Show("게임 데이터를 복사하였습니다. 이전 게임을 제거하시고, 확인 버튼을 눌러 주십시오. 게임을 제거하지 않고 확인버튼을 누르면 게임 데이터가 손상될 수 있습니다.", "게임 데이터 복사 완료", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
				Register(destPath);
			}
		}

		private void Register(string destPath) {
			using var powerShell = PowerShell.Create();
			powerShell.AddScript("Get-ExecutionPolicy");
			var output = powerShell.Invoke();
			if (output.Count > 0)
			{
				if (output[0].ToString() != "Unrestricted")
				{
					powerShell.AddScript("Set-ExecutionPolicy Unrestricted");
					powerShell.Invoke();
				}
			}

			powerShell.AddScript("Get-ExecutionPolicy");
			output = powerShell.Invoke();
			if (output.Count > 0)
			{
				if (output[0].ToString() != "Unrestricted")
				{
					MessageBox.Show("복사한 데이터를 등록할 권한이 없습니다: " + output[0].ToString(), "등록 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);

					txtResult.Text += "게임 데이터 등록 권한 없음\r\n";

					return;
				}
			}

			powerShell.AddScript($"Add-AppxPackage -Register \"{destPath}\\AppxManifest.xml\"");
			powerShell.Invoke();

			var errors = powerShell.Streams.Error;

			if (errors != null && errors.Count > 0)
			{
				var errorMessage = new StringBuilder();

				errorMessage.Append("복사한 데이터를 등록할 수 없습니다.\r\n\r\n");

				bool alreadyInstalled = false;
				foreach (var error in errors)
				{
					if (error.ToString().IndexOf("이미 설치되어") >= 0)
					{
						errorMessage.Append("이미 설치된 게임이 있습니다. 이전 설치 게임을 제거하시고, '확인'버튼을 눌러 다시 시도해 주십시오. 등록을 원치 않으면 취소 버튼을 눌러 주십시오.");
						alreadyInstalled = true;
						break;
					}
					//else if (error.ToString().IndexOf("Import-Module") >= 0) {
					//	errors.Clear();
					//	powerShell.AddScript($"Import-Module Appx -UseWindowsPowerShell");
					//	powerShell.Invoke();

					//	Register(destPath);
					//	return;
					//}
					else
					{
						errorMessage.Append(error.ToString()).Append("\r\n");
					}
				}

				if (!alreadyInstalled)
				{
					errorMessage.Append("\r\n확인 버튼을 눌러 재시도 하시거나, Windows Powershell을 실행한 후, 아래 명령어를 실행해서 수동으로 등록해 주십시오.\r\n");
					errorMessage.Append("(아래 명령은 클립보드에 복사되었습니다. Powershell에서 마우스 오른쪽 버튼을 누르면 붙여넣기가 됩니다.)\r\n\r\n");
					errorMessage.Append($"Add-AppxPackage -Register \"{destPath}\\AppxManifest.xml\"");

					Clipboard.SetText($"Add-AppxPackage -Register \"{destPath}\\AppxManifest.xml\"");
				}

				txtResult.Text += $"게임 데이터 등록 실패\r\n{errorMessage}\r\n";

				if (MessageBox.Show(errorMessage.ToString(), "등록 실패", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
					Register(destPath);
			}
			else
			{
				txtResult.Text += $"게임 데이터 등록 완료\r\n";
				MessageBox.Show("복사한 데이터를 등록하였습니다. 한글 패치를 적용해 주십시오.", "등록 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnShowLicense_Click(object sender, EventArgs e)
		{
			//new License().ShowDialog();
		}
	}
}
