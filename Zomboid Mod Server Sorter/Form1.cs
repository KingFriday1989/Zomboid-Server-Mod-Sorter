namespace Zomboid_Mod_Server_Sorter
{
    public partial class Form1 : Form
    {
        string[] modList;
        string[] INI;
        List<string> workShopID = new();
        List<string> modID = new();

        string outputPath;


        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var stream = openFileDialog1.OpenFile();
            StreamReader sr = new StreamReader(stream);
            modList = sr.ReadToEnd().Split("\n");
            GetIds();
            sr.Close();
            sr.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var stream = openFileDialog1.OpenFile();
            StreamReader sr = new StreamReader(stream);
            INI = sr.ReadToEnd().Split("\n");
            outputPath = openFileDialog1.FileName;
            sr.Close();
            sr.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (workShopID.Count > 0 && modID.Count > 0 ) 
            {
                UpdateINI();
                var NewINI = string.Join("\n", INI);
                System.IO.File.WriteAllText(outputPath,NewINI);
                MessageBox.Show("Complete!");
                Close();
            }
        }

        private void UpdateINI() 
        {
           for (int i = 0;  i < INI.Length; i++) 
            {
                if (INI[i].Contains("Mods=")) 
                {
                    INI[i] = "Mods=" + string.Join(";", modID);
                }
                else if (INI[i].Contains("WorkshopItems=")) 
                {
                    INI[i] = "WorkshopItems=" + string.Join (";", workShopID);
                }

            }
        }

        private void GetIds()
        {
            foreach (var line in modList)
            {
                if (line.Contains("Workshop ID: "))
                {
                    workShopID.Add(line.Replace("Workshop ID:", "").Replace("\r", "").Trim());
                }
            }
            foreach (var line in modList)
            {
                if (line.Contains("Mod ID: "))
                {
                    modID.Add(line.Replace("Mod ID:", "").Replace("\r", "").Trim());
                }
            }
        }


    }
}
