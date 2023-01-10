using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// 미션 프로그램 설명 202001359 박준희
// 영화관 이름을 검색하면
// 해당 영화관 정보(이름, 주소, 상영관수, 좌석수, 위치)를 알려주는 프로그램입니다.
//cgv, megabox, lotte, 애관극장 중 하나의 영화관 이름을 입력하면 해당 영화관 정보를 레이블에 출력,
// 픽쳐박스에는 해당 영화관 사진이 알맞게 출력됩니다.
// 만약 4개의 영화관 이름이 아닌 다른 문자열이 입력되었을 경우
// messagebox를 통해 "존재하지 않습니다, 근처 영화관을 추천해드립니다" 메세지를 출력하고
// 랜덤으로 4곳의 영화관 중 하나의 정보와 사진을 출력합니다.
namespace _202001359_ex1
{
    public partial class Form1 : Form
    {
        List<Std> lst = new List<Std>();
        string []movieNo = {"애관극장", "megabox", "cgv", "lotte" }; // 엑셀 파일에 있는 영화관명 배열로 관리
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        private void btn_Click(object sender, EventArgs e)
        {
            String addr = "Server=localhost;Database=movie;port=3306;username=root;password=English517!";
            label1.Text = " ";
            string nameThea = movieSearch.Text;
            MySqlConnection msc = new MySqlConnection(addr);
            msc.Open();
            string qry = String.Format("SELECT * FROM `인천광역시 중구_영화관_20210720`");
            MySqlCommand cmd = new MySqlCommand(qry, msc);
            // MySql 데이터 조회
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // 읽은 레코드 단위로 리스트에 추가
                Std st = new Std();
                st.index = dr["연번"];
                st.theater = dr["영화관명"];
                st.address = dr["영화관 주소"];
                st.screenNum = dr["상영관수(관)"];
                st.seatNum = dr["좌석수"];
                st.PhoneNum = dr["전화번호"];

                lst.Add(st);
            }
            int check = 4;

            switch (nameThea)
            {
                case "애관극장":
                    pictureBox1.Image = Properties.Resources.애관극장;
                    check = 0;
                    break;
                case "megabox":
                    pictureBox1.Image = Properties.Resources.megabox;
                    check = 1;
                    break;
                case "cgv":
                    pictureBox1.Image = Properties.Resources.cgv;
                    check = 2;
                    break;
                case "lotte":
                    pictureBox1.Image = Properties.Resources.lotte;
                    check = 3;
                    break;
                default:
                    MessageBox.Show("해당 영화관은 근처에 존재하지 않습니다.\n 근처 영화관을 추천해드립니다!");//랜덤으로 영화관 추천
                    check = rnd.Next(4);
                    string picture = movieNo[check];
                    Object ob = Properties.Resources.ResourceManager.GetObject(picture);
                    pictureBox1.Image = ob as Image;
                    break;
            }

            label1.Text = String.Format($"영화관명: {lst[check].theater}\n\n주소: {lst[check].address}\n\n상영관수: {lst[check].screenNum}\n\n좌석수: {lst[check].seatNum} \n\n전화번호: {lst[check].PhoneNum}" +
          $"\n");



            msc.Close();
            // 리스트에 있는 레코드들을 레이블에 출력

        }
    }
}

    internal class Std
{
    internal object index;
    internal object theater;
    internal object address;
    internal object screenNum;
    internal object seatNum;

    public object PhoneNum { get; internal set; }
}

