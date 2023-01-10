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
//미션1 프로그램 설명 202001359 박준희
// 영화관 이름을 검색하면
// 해당 영화관 정보(이름, 주소, 상영관수, 좌석수, 위치)를 알려주는 프로그램입니다.
//cgv, megabox, lotte, 애관극장 중 하나의 영화관 이름을 입력하면 해당 영화관 정보를 레이블에 출력,
// 픽쳐박스에는 해당 영화관 사진이 알맞게 출력됩니다.
// 만약 4개의 영화관 이름이 아닌 다른 문자열이 입력되었을 경우
// messagebox를 통해 "존재하지 않습니다, 근처 영화관을 추천해드립니다" 메세지를 출력하고
// 랜덤으로 4곳의 영화관 중 하나의 정보와 사진을 출력합니다.

//미션2 프로그램 설명 202001359 박준희
// 미션 1에서 검색한 영화관을 기준으로 영화 예매 시스템(영화 예매 및 예매 내역확인, 변경) 을 추가하였습니다.
// 메인 폼(Form1.cs)에서 영화관을 검색할 수 있고
// 각 버튼을 통해 영화 예매 내역 확인 및 변경(bookingcheck.cs)와 영화 예매 윈폼(booking.cs)를 호출할 수 있습니다.
// 총 3개의 폼이 존재합니다
// 1) 영화관 검색 폼(Form1.cs)
// 2) 영화 예매 윈폼(booking.cs)
// 3) 영화 예매 내역 확인 및 변경(bookingcheck.cs)
namespace _202001359_ex1_basic
{
    public partial class Form1 : Form
    {
        List<Std> lst = new List<Std>(); //sql 자료 리스트로 관리
        string[] movieNo = { "애관극장", "megabox", "cgv", "lotte" }; // 엑셀 파일에 있는 영화관명 배열로 관리
        Random rnd = new Random(); 
        int check = 4; 
        int bookingNum = 0; // 예약 내역 수
        public Form1()
        {
            InitializeComponent();
            bookingcheck bc = new bookingcheck(); // 현재 DB에 저장되어 있는 예매 내역 수를 알기 위해 bookingcheck 폼 이용함
            bc.getCount(ref bookingNum); // 현재 저장된 예약내역 수를 읽어옴
            bookingCnt.Text = bookingNum.ToString(); // 레이블 출력
        }

        private void btn_Click(object sender, EventArgs e)
        {
            String addr = "Server=localhost;Database=movie;port=3306;username=root;password=English517!";
            label1.Text = " ";
            string nameThea = movieSearch.Text; //영화 검색 텍스트박스(입력 값) 문자열
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
            

            switch (nameThea) // nameThea는 영화 검색 입력 문자열
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
          $"\n"); // 레이블에 검색한 영화관 정보 출력



            msc.Close();
        }

        private void button1_Click(object sender, EventArgs e) // 영화 예매 윈폼 호출
        {
            string movie = lst[check].theater.ToString(); // 검색한 영화관 이름 문자열
            booking service = new booking(movie, bookingNum); //호출을 할 때 문자열도 함께 보낸다.
            service.ShowDialog();
            service.getCount(ref bookingNum); //현재 예약한 데이터 수(=예약 내역 수)를 가져온다.
            bookingCnt.Text = bookingNum.ToString(); // 예약 내역 수를 레이블에 출력한다.
        }

        private void label1_TextChanged(object sender, EventArgs e) // 영화관이 검색이 이루어지면
        { 
            button1.Enabled = true; // 영화 예매 버튼 활성화 한다. 
        }

        private void button2_Click(object sender, EventArgs e) // 예매 확인 및 변경 윈폼 호출
        {
            bookingcheck bc = new bookingcheck();
            bc.ShowDialog();
            bc.getCount(ref bookingNum); // 해당 윈폼을 닫으면 수를 예매 내역 수를 가져온다.
            bookingCnt.Text = bookingNum.ToString(); // 예매 내역 수를 레이블에 출력
        }

        private void 옵션ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 예고편ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rest rs = new Rest();
            rs.ShowDialog();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 미니게임ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    internal class Std
    {
        internal object index;
        internal object theater;
        internal object Theater; // 극장 이름
        internal object address;
        internal object screenNum; 
        internal object seatNum; 
        internal object SeatNum;// 예매 좌석
        internal object movieName; // 영화 이름
        internal object Time; // 시간
        internal object booing_id; // 예매 순서 번호, primary key를 뜻함

        public object PhoneNum { get; internal set; }
        public object bookingTime;
    }
}
