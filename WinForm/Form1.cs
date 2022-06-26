namespace WinForm
{
    using FLib;
    using Microsoft.FSharp.Collections;
    public partial class Form1 : Form
    {
        JSON.Reader reader;
        public Form1(string file)
        {
            try { reader = new JSON.Reader(file); }
            catch { Application.Exit(); }
            InitializeComponent();
            l_desc.Text = "";
            FSharpList<Subject.BaseItem> list = reader.GetPCs();
            for (int i = 0; i < list.Length; i++)
                treeView.Nodes[0].Nodes.Add(list[i].Name);
            list = reader.GetConsoles();
            for (int i = 0; i < list.Length; i++)
                treeView.Nodes[1].Nodes.Add(list[i].Name);
            list = reader.GetPortables();
            for (int i = 0; i < list.Length; i++)
                treeView.Nodes[2].Nodes.Add(list[i].Name);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FSharpList<Subject.BaseItem> list = null;
            if (treeView.SelectedNode.Parent != null)
            {
                switch (treeView.SelectedNode.Parent.Index)
                {
                    case 0: list = reader.GetPCs(); break;
                    case 1: list = reader.GetConsoles(); break;
                    case 2: list = reader.GetPortables(); break;
                }
                l_desc.Text = list[treeView.SelectedNode.Index].GetDesc();
                pictureBox.ImageLocation = list[treeView.SelectedNode.Index].Image_url;
                comboBox.SelectedIndex = 0;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex != 0)
            {
                Subject.BaseItem item = null;
                switch(comboBox.SelectedIndex)
                {
                    case 1: item = reader.GetBiggestScreenPortable(); break;
                    case 2: item = reader.GetBiggestRAMDevice(); break;
                    case 3: item = reader.GetEarliestCommodore(); break;
                    case 4: item = reader.GetBiggestVRAMConsole(); break;
                    case 5: item = reader.GetMostOSPC(); break;
                }
                l_desc.Text = item.GetDesc();
                pictureBox.ImageLocation = item.Image_url;
            }
        }
    }
}