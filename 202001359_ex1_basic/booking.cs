using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _202001359_ex1_basic
{
    public partial class booking : Form
    {
        string theName;
        static int id;
        string dateStr="";
        private MailMessage msg;
        private Button[] btn = new Button[20]; // button 25개 생성
        static int margin = 300;
        static int btnWidth = 33;
        static int btnHeight = 28;
        int selected = 0;
        int index=0; // 클릭한 자리 번호, 초기값 좌석 범위 외 숫자 지정
        int count;
        public booking(string str, int check)
        {
            theName = str; // 검색한 영화관 이름
            id = check; // 현재 저장되어 있는 데이터 개수, 예매 안하고 창을 닫을 시 고려
            InitializeComponent(); 
            Init2(); // 영화내용, 콤보박스, 라디오 박스 초기화
            MailInit();// 메일 보내기
        }

        private void MailInit()
        {
            msg = new MailMessage();
            msg.From = new MailAddress("wnsgml517@naver.com");
            // 현재 사용자 : 박준희 의 메일 주소로 시도
        }

        private void Init2() // 예매 윈폼 시작시 필요한 정보 출력 및 초기화
        {
            label4.Text = "** " + theName + " 예매 페이지 입니다 **";
            count = 5; // 5초 타이머 시작
            
            comMovie.Items.Add("닥터 스트레인지 2");
            comMovie.Items.Add("신기한 동물사전");
            comMovie.Items.Add("공기살인");
            comMovie.Items.Add("인생은 아름다워");
           
            comTime.Items.Add("10:00");
            comTime.Items.Add("13:00");
            comTime.Items.Add("16:00");
            comTime.Items.Add("20:00");

            Seat.Text = "";

            rbMovie.TabStop = false; // 라디오버튼, 콤보박스 비활성화
            rbSeat.Enabled = false;
            rbSeat.TabStop = false;
            rbTime.TabStop = false;
            rbTime.Enabled = false;
            comMovie.Enabled = false;
            comTime.Enabled = false;

        }

        private void rbMovie_CheckedChanged(object sender, EventArgs e) // 라디오 버튼 체크 되어 있을 때만 콤보박스 허용
        {
            if (comMovie.Enabled) 
                comMovie.Enabled = false;
            else
            {
                comMovie.Enabled = true;
                rbTime.Enabled=true;
            }
        }

        private void rbSeat_CheckedChanged(object sender, EventArgs e) // 라디오 버튼 체크 되어 있을 때만 콤보박스 허용
        {
            if (comTime.Text == "" || comMovie.Text == "")
                MessageBox.Show("영화이름과 시간을 선택해주세요");
            else
                checkBooking(); // 예약 가능 여부 확인

        }

        private void rbTime_CheckedChanged(object sender, EventArgs e) // 라디오 버튼 체크 되어 있을 때만 콤보박스 허용
        {

            if (comTime.Enabled)
                comTime.Enabled = false;
            else
            {
                comTime.Enabled = true;
                rbSeat.Enabled = true;
                
            }
        }

        private void button3_Click(object sender, EventArgs e) // 예매하기 버튼 클릭
        {
            
            if (dateStr == "")
                MessageBox.Show("날짜를 선택해주세요");
            else if ((comMovie.Text == "" || Seat.Text == "") || (comTime.Text == "")) // 세 가지 정보가 다 클릭되어야 함(영화 이름, 시간, 좌석 번호)
            {
                MessageBox.Show("영화예매 정보가 다 선택되지 않았습니다.");
            }
            else
            {
                string str = "예매날짜: " + dateStr + "\n예매시간: " + comTime.Text + "\n영화이름: " + comMovie.Text + "\n좌석: " + Seat.Text;

                SqlInput(); // DB에 예약정보 저장

                MessageBox.Show("예약이 완료되었습니다. \n" + str);
                if (chbAgree.Checked)
                    mailBooking(); // 메일 서비스 이용시
                id++;
                timer1.Start();
                
                
            }
        }

        private void mailBooking()
        {
            
                msg.To.Add("wnsgml517@naver.com");
                msg.Subject= "고객님의 예매 내역을 보냅니다";
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = dateStr + comTime.Text + comMovie.Text + Seat.Text;
                msg.BodyEncoding = Encoding.UTF8;
                try
                {
                    SmtpClient smtp = new SmtpClient("smtp.naver.com", 587);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("wnsgml517", "20grace!-!");
                    smtp.Send(msg);
                    MessageBox.Show("고객님의 메일로 예매 내역을 보내드렸습니다");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("메일전송오류");
         
                }
        }

        private void checkBooking() // 예약 좌석 가능 확인
        {
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!";
            int search;
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();
            // sql 조회문과 접속주소의 결합
         
            string sql = string.Format("SELECT * FROM bookingsystem WHERE Theater='{0}' AND movieName = '{1}' AND Time = '{2}' AND BookingTime = '{3}';",theName,comMovie.Text,comTime.Text, dateStr);
            // 해당 영화관, 선택한 영화 이름, 선택한 시간에 맞는 데이터 값 읽어오기
            
            MySqlCommand cmd = new MySqlCommand(sql, msc);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()) // 해당 데이터가 존재한다면 그 좌석은 검정 표시, 누르지 못하게 만들기
            {
                search = Convert.ToInt32(rdr["SeatNum"]);
                btn[search-1].BackColor= Color.Black; // 읽어온 데이터 값 중 SeatNum(좌석)을 검정색으로 변경
                btn[search-1].Enabled = false; // 사용못하게 만듬
            }
            msc.Close();

        }

        internal void getCount(ref int bookingNum) // 예약 내역 수 불러오기
        {
            bookingNum = id;
        }

        private void SqlInput() // DB 저장
        {
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!"; // DB 연결
            
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();
            // sql 조회문과 접속주소의 결합
            string num =id.ToString();
            string selectQuery = string.Format("SELECT COUNT(booking_id) FROM bookingsystem"); //booking_id의 개수(DB에 저장되어 있는 데이터 수) 카운트
            MySqlCommand checkCmd = new MySqlCommand(selectQuery, msc);
            id = int.Parse(checkCmd.ExecuteScalar().ToString()); // 읽어온 값(DB 저장되어 있는 수=예약 내역 수)을 id 변수에 저장
            
            string qry = String.Format($"INSERT INTO " +
                $"bookingsystem (booking_id, Theater, " +
                $"movieName, SeatNum, Time, BookingTime) VALUES ('{id}'," +
                $"'{theName}','{comMovie.Text}'," +
                $"'{Convert.ToInt32(Seat.Text)}','{comTime.Text}','{dateStr}')"); // DB 삽입 쿼리
            MySqlCommand cmd = new MySqlCommand(qry, msc); 
            if (cmd.ExecuteNonQuery() != 1)
                MessageBox.Show("Failed to insert data."); // 실패 시 오류 메시지박스 출력
            msc.Close();
            //throw new NotImplementedException();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            dateStr = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            if (dt1.Year < DateTime.Now.Year)
                dateStr = "";
            else if (dt1.Month < DateTime.Now.Month)
                dateStr = "";
            else if (dt1.Day < DateTime.Now.Day)
                dateStr = "";
            if (dateStr == "")
                MessageBox.Show("선택하신 날짜로는 예매할 수 없습니다(과거 시간)\n시간을 다시 입력해주세요"); 
            // 과거 일자를 택했을 경우 메세지 창 출력, 날짜 선택 안된 상태로 변경

            
        }

        private void booking_Load(object sender, EventArgs e)
        {
            this.Width = 2 * margin + 4 * btnWidth + 2 * 39 + 2 * 4; // 3: btn 사이의 간격, 4는 form의 boundary 두께
            this.Height = 2 * margin + 39 * btnWidth + 2 * 2 + 2 * 4 + 24 ;
            
            for (int i =0; i < btn.Length; i++)
            {
                int index = i;
               
                this.btn[i] = new Button();

                this.Controls.Add(this.btn[i]);
                btn[i].Text = (i+1).ToString();
                btn[i].Size = new Size(btnWidth, btnHeight);
                btn[i].Location = new Point(margin + i % 4 * btn[i].Width, margin-150 + i / 4 * btn[i].Height);
                this.btn[i].Enabled = true;
                this.btn[i].Visible = true;
                //-index라는 별도의 변수로 이벤트로 발생해야한다.
                //-i를 변수로 사용해선 안된다. 
 
                this.btn[i].Click += (sender1, ex) => this.btnOutputLink_Click(index);
               
            }
        }

        private void btnOutputLink_Click(int index)
        {
            if (!rbSeat.Checked)
                MessageBox.Show("좌석 라디오박스 버튼을 체크해주세요");
            else
            {
                btn[selected].BackColor = Color.White;
                this.Seat.Text = (index + 1).ToString();
                btn[index].BackColor = Color.Red;
                selected = index;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count--;
            label4.Text = count.ToString()+"초후 자동으로 닫힙니다.";
            if (count == 0)
            {
                timer1.Stop();
                Close();
            }
        }
    }
}
