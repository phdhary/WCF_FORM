using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WCF_FORM_C
{
    public partial class addedit : Form
    {
        int status = 0;
        int id = 0;
        public addedit(int st,int id_inp)
        {
            InitializeComponent();
            status = st;
            id = id_inp;
            button2.Visible = false;

            if(status ==2)
            {
                getData(id.ToString());
                txt_hrg.Enabled = false;
                txt_psn.Enabled = false;
                button1.Enabled = false;
                button2.Visible = true;
            }
            
        }
        
        private void addedit_Load(object sender, EventArgs e)
        {

        }
        void getData(string inp)
        {
            DataPesanan pesanan = new DataPesanan();
            string result = new WebClient().DownloadString(address.basuri + "getdata/id=" + inp);
            pesanan = JsonConvert.DeserializeObject<DataPesanan>(result);
            txt_psn.Text = pesanan.pesanan;
            txt_hrg.Text = pesanan.harga;
        }
        void addData(DataPesanan pesanan)
        {
            
            //serialization : dari objek ke string json
            string request = JsonConvert.SerializeObject(pesanan);
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string result = client.UploadString(address.basuri + "adddata", request);
        }
        void updateData(DataPesanan pesanan)
        {
            string request = JsonConvert.SerializeObject(pesanan);
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string result = client.UploadString(address.basuri + "updatedata", request);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataPesanan input = new DataPesanan();
            try
            {
                input.pesanan = txt_psn.Text;
                input.harga = txt_hrg.Text;
                if (status == 1)
                {
                    addData(input);
                    Program.Form.getDataAll();
                    this.Close();
                }
                else if(status == 2)
                {
                    input.id = id;
                    updateData(input);
                    Program.Form.getDataAll();
                    this.Close();
                }
                else
                {

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_hrg.Enabled = true;
            txt_psn.Enabled = true;
            button1.Enabled = true;
        }
    }
}
