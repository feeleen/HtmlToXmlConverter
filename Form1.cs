using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Sgml;
using System.Xml.Linq;

namespace HTMLToXMLConverter
{
	public class Form1 : System.Windows.Forms.Form
	{
		#region Props


		private string XmlFile = "xml.xml";
		private string XslFile = "xsl.xsl";
		private string tpXmlInText = "Xml";
		private string tpXslInText = "Xsl";
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.MenuItem miTransform;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tpXmlIn;
		private ICSharpCode.TextEditor.TextEditorControl textBox1;
		private System.Windows.Forms.TabPage tpXslIn;
		private ICSharpCode.TextEditor.TextEditorControl textBox2;
		private System.Windows.Forms.Panel panel3;
		private System.ComponentModel.IContainer components;

		#endregion

		public Form1()
		{
			InitializeComponent();
			InitializeTextEditors();
		}
		
		
		public void InitializeTextEditors()
		{
			textBox1.Document.HighlightingStrategy = 
				HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			textBox1.Encoding = System.Text.Encoding.UTF8;
			textBox1.ShowEOLMarkers = false;
			textBox1.ShowSpaces = false;
			textBox1.ShowTabs = false;
			textBox1.EnableFolding = true;
			
			textBox2.Document.HighlightingStrategy = 
				HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			textBox2.Encoding = System.Text.Encoding.UTF8;
			textBox2.ShowEOLMarkers = false;
			textBox2.ShowSpaces = false;
			textBox2.ShowTabs = false;
			textBox2.EnableFolding = true;
		}

		public void SaveFiles()
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				string path = Application.StartupPath + "/" + XmlFile;

				if (File.Exists(path)) 
				{
					File.Delete(path);
				}

				using (StreamWriter sw = new StreamWriter(path)) 
				{
					sw.Write(textBox1.Text);
					tpXmlIn.Text = tpXmlInText;
				}


				path = Application.StartupPath + "/" + XslFile;

				if (File.Exists(path)) 
				{
					File.Delete(path);
				}

				using (StreamWriter sw = new StreamWriter(path)) 
				{
					sw.Write(textBox2.Text);
					tpXslIn.Text = tpXslInText;
				}
			}
			catch(Exception ex)
			{
				Cursor.Current = Cursors.Default;
				MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private string GetParsedHtml(string html)
		{
			return GetParsedHtml(html, true);
		}

		private string GetParsedHtml(string html, bool outer)
		{
			try
			{
				SgmlReader sgmlRdr = new SgmlReader();
				sgmlRdr.DocType = "HTML";
				sgmlRdr.InputStream = new StringReader(html);
				XmlDocument sgmlDoc = new XmlDocument();
				sgmlDoc.Load(sgmlRdr);

				string s = "";
				if (outer)
					s = sgmlDoc.OuterXml;
				else
					s = sgmlDoc.InnerXml;

				XDocument doc = XDocument.Parse(s);
				return doc.ToString();

			}
			catch (Exception ex)
			{
				throw new Exception("HTML Parsing error", ex);
			}
		}

		public void Transform()
		{
			if (textBox2.Text.Trim().Length == 0)
			{
				MessageBox.Show("Please paste HTML code into the HTML tab");
				return;
			}

			textBox1.Text = GetParsedHtml(textBox2.Text);
			tabControl2.SelectedTab = tpXmlIn;
		}


		#region WinFormCode

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.miTransform = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tpXmlIn = new System.Windows.Forms.TabPage();
			this.textBox1 = new ICSharpCode.TextEditor.TextEditorControl();
			this.tpXslIn = new System.Windows.Forms.TabPage();
			this.textBox2 = new ICSharpCode.TextEditor.TextEditorControl();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tpXmlIn.SuspendLayout();
			this.tpXslIn.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Red;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miTransform,
            this.menuItem7});
			// 
			// miTransform
			// 
			this.miTransform.Index = 0;
			this.miTransform.Text = "Transform!";
			this.miTransform.Click += new System.EventHandler(this.miTransform_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem8});
			this.menuItem7.Text = "Help";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "About";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(768, 495);
			this.panel4.TabIndex = 3;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.tabControl2);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(0, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(768, 495);
			this.panel5.TabIndex = 1;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tpXslIn);
			this.tabControl2.Controls.Add(this.tpXmlIn);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.HotTrack = true;
			this.tabControl2.ImageList = this.imageList1;
			this.tabControl2.ItemSize = new System.Drawing.Size(51, 19);
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(768, 495);
			this.tabControl2.TabIndex = 1;
			// 
			// tpXmlIn
			// 
			this.tpXmlIn.Controls.Add(this.textBox1);
			this.tpXmlIn.ImageIndex = 1;
			this.tpXmlIn.Location = new System.Drawing.Point(4, 23);
			this.tpXmlIn.Name = "tpXmlIn";
			this.tpXmlIn.Padding = new System.Windows.Forms.Padding(4);
			this.tpXmlIn.Size = new System.Drawing.Size(760, 468);
			this.tpXmlIn.TabIndex = 0;
			this.tpXmlIn.Text = "Xml";
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.IsReadOnly = false;
			this.textBox1.Location = new System.Drawing.Point(4, 4);
			this.textBox1.Name = "textBox1";
			this.textBox1.ShowEOLMarkers = true;
			this.textBox1.ShowSpaces = true;
			this.textBox1.ShowTabs = true;
			this.textBox1.Size = new System.Drawing.Size(752, 460);
			this.textBox1.TabIndex = 0;
			// 
			// tpXslIn
			// 
			this.tpXslIn.Controls.Add(this.textBox2);
			this.tpXslIn.ImageIndex = 2;
			this.tpXslIn.Location = new System.Drawing.Point(4, 23);
			this.tpXslIn.Name = "tpXslIn";
			this.tpXslIn.Padding = new System.Windows.Forms.Padding(4);
			this.tpXslIn.Size = new System.Drawing.Size(760, 468);
			this.tpXslIn.TabIndex = 1;
			this.tpXslIn.Text = "HTML";
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox2.IsReadOnly = false;
			this.textBox2.Location = new System.Drawing.Point(4, 4);
			this.textBox2.Name = "textBox2";
			this.textBox2.ShowEOLMarkers = true;
			this.textBox2.ShowSpaces = true;
			this.textBox2.ShowTabs = true;
			this.textBox2.Size = new System.Drawing.Size(752, 460);
			this.textBox2.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(4, 4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(768, 495);
			this.panel3.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(776, 503);
			this.Controls.Add(this.panel3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HTML To XML Converter";
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tpXmlIn.ResumeLayout(false);
			this.tpXslIn.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			Transform();

		}
		
		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			using (About a = new About() )
			{
				a.ShowDialog();
			}
		}

		private void miTransform_Click(object sender, System.EventArgs e)
		{
			Transform();
		}
	}
}
