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
    public partial class Home : Form
    {
        
        public Home()
        {
            InitializeComponent();
            getDataAll();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
        public void getDataAll()
        {
            List<DataPesanan> pesanan = new List<DataPesanan>();
            string result = new WebClient().DownloadString(address.basuri + "getalldata");
            //deserialization : ubah dari string json ke objek
            pesanan = JsonConvert.DeserializeObject<List<DataPesanan>>(result);
            dgv.DataSource = pesanan;
        }
        void getData(string inp)
        {
            DataPesanan pesanan = new DataPesanan();
            string result = new WebClient().DownloadString(address.basuri + "getdata/id=" + inp);
            pesanan = JsonConvert.DeserializeObject<DataPesanan>(result);
            List<DataPesanan> pesananList = new List<DataPesanan>();
            pesananList.Add(pesanan);
            dgv.DataSource = pesananList;
        }
        void deleteData(DataPesanan dp)
        {
            string request = JsonConvert.SerializeObject(dp);
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string result = client.UploadString(address.basuri + "deletedata", request);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            
            if (txt_search.Text != "")
            {
                getData(txt_search.Text);
            }
            else
            {
                getDataAll();
            }
        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            addedit form = new addedit(1,1);
            form.Show();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            addedit form = new addedit(2, Convert.ToInt32(dgv.CurrentRow.Cells[0].Value));
            form.Show();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void del_btn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            DataPesanan pesanan = new DataPesanan();
            pesanan.id = id;

            deleteData(pesanan);
            getDataAll();
        }
    }
}
