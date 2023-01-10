using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _202001359_ex1_basic
{
    public partial class bookingcheck : Form
    {

        List<Std> lst = new List<Std>();
        int clickIndex;
        delegate string message();
        public bookingcheck()
        { 
            InitializeComponent();
            Init(); // 시간 변경 콤보박스
            DBRead(); // DB 읽기 함수
            DBnChart();
            clickIndex = 0;
        }
        private void DelCall(string type, message df) // 델리 게이트
        {
            string str = type+" 예매 정보 : " + df();
            MessageBox.Show(str);

        }
        private void DBnChart()
        {
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!";
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();
            string qry = String.Format("SELECT * FROM 영화예제");
            MySqlCommand cmd = new MySqlCommand(qry, msc);
            //MySql 데이터 조회
            MySqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                MovieChart.Series["점유율"].Points.AddXY(dr.GetString("영화명"), dr.GetDouble("점유율"));
            }
            msc.Close();

        }

        private void Init() // 시간 변경 콤보박스 값 초기화
        {
            comTime.Items.Add("10:00");
            comTime.Items.Add("13:00");
            comTime.Items.Add("16:00");
            comTime.Items.Add("20:00");
         //   throw new NotImplementedException();
        }

        private void DBRead() // DB읽기 함수
        {
            
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!";
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();
            listView1.BeginUpdate(); //리스트 초기화
            // sql 조회문과 접속주소의 결합
            string qry = String.Format("SELECT * FROM bookingsystem");
            MySqlCommand cmd = new MySqlCommand(qry, msc);
            // MySql 데이터 조회
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem lv = new ListViewItem(dr["BookingTime"].ToString());
                lv.SubItems.Add(dr["Time"].ToString());
                lv.SubItems.Add(dr["Theater"].ToString());
                lv.SubItems.Add(dr["movieName"].ToString());
                lv.SubItems.Add(dr["SeatNum"].ToString());
                // 읽은 레코드 단위로 리스트에 추가
                listView1.Items.Add(lv);
            }

            listView1.EndUpdate();
            msc.Close();
         
        }

        private void button2_Click(object sender, EventArgs e) // 삭제 기능
        {
            DelCall("삭제", db_delete);
          
        }

        private string db_delete()
        {
            if (clickIndex == 100)
            {
                MessageBox.Show("삭제할 예매내역을 리스트뷰에서 클릭해주세요");
                return "된 내역이 없습니다";
            }
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!"; // DB 연결
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();

            // 리스트 박스 클릭한 데이터 중 primary key인 booking_id 값 알아내기

            string deleteQuery = string.Format("DELETE FROM bookingsystem WHERE booking_id = {0};", clickIndex); // delete_key에 맞는 데이터 값 삭제

            MySqlCommand command = new MySqlCommand(deleteQuery, msc);
            command.ExecuteNonQuery();

            for (int i = clickIndex; i < listView1.Items.Count; i++) // DB 값 booking_id 재정렬
            {
                string query = string.Format("UPDATE bookingsystem SET booking_id ='{1}' WHERE booking_id={0};", i + 1, i);
                MySqlCommand cmd = new MySqlCommand(query, msc);
                cmd.ExecuteNonQuery();
            }
            

            listView1.Items.Clear();
            DBRead(); // 업데이트 된 리스트 박스 불러오기
            return listView1.Items[clickIndex].SubItems[0].Text+" "+ listView1.Items[clickIndex].SubItems[1].Text+ " " + listView1.Items[clickIndex].SubItems[2].Text+ " " + listView1.Items[clickIndex].SubItems[3].Text;
            //예매 정보 반환
        }

        private void button1_Click(object sender, EventArgs e) // 시간 변경 시
        {
            DelCall("변경", db_update);
        }

        private string db_update()
        {
            if (clickIndex == 100)
            {
                MessageBox.Show("변경할 예매내역을 리스트뷰에서 클릭해주세요");
                return "된 내역이 없습니다";
            }
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!"; // DB 연결
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();

            string query = string.Format("UPDATE bookingsystem SET Time ='{1}' WHERE booking_id={0};", clickIndex, comTime.Text);
            // clickey와 같은 booking_id를 가진 데이터를 알아내 예매 시간 바뀌게 하기

            MySqlCommand cmd = new MySqlCommand(query, msc);
            cmd.ExecuteNonQuery();

            if (cmd.ExecuteNonQuery() != 1) // 오류 날 경우 메세지 박스 출력
                MessageBox.Show("Failed to delete data.");

            msc.Close();
            listView1.Items.Clear();
            DBRead();
            return listView1.Items[clickIndex].SubItems[0].Text + " " + listView1.Items[clickIndex].SubItems[1].Text + " " + listView1.Items[clickIndex].SubItems[2].Text + " " + listView1.Items[clickIndex].SubItems[3].Text;
        }

        internal void getCount(ref int cnt)
        {
            cnt = listView1.Items.Count; // ref를 이용, 리스트 박스에 저장된 데이터의 개수를 가져옴
        }

        private void comTime_TextChanged(object sender, EventArgs e)
        {
            btUpdate.Enabled = true; // 시간 콤보박스 값(comTime.Text)이 설정되어야 변경하기 버튼(btUpDate) 활성화
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "텍스트 파일|*.txt|모든파일|*.*";
            string Addr = "Server=localhost;Database=movie;port=3306;username=root;" +
"password=English517!";
            MySqlConnection msc = new MySqlConnection(Addr);
            msc.Open();
            listView1.Clear(); //리스트 초기화
            // sql 조회문과 접속주소의 결합
            string qry = String.Format("SELECT * FROM bookingsystem");
            MySqlCommand cmd = new MySqlCommand(qry, msc);
            // MySql 데이터 조회
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // 읽은 레코드 단위로 리스트에 추가
                Std st = new Std();
                st.booing_id = dr["booking_id"];
                st.Theater = dr["Theater"];
                st.movieName = dr["movieName"];
                st.SeatNum = dr["SeatNum"];
                st.Time = dr["Time"];
                st.bookingTime = dr["BookingTime"];
                lst.Add(st);
            }

            
            msc.Close();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                String wrData;
                StreamWriter sr = new StreamWriter(sfd.FileName, true);
                foreach (Std st in lst)
                {// 텍스트 파일에 출력
                    wrData = String.Format($"{st.bookingTime} {st.Time} {st.Theater} {st.movieName} {st.SeatNum}번" +
                    $"\n");
                    sr.WriteLine(wrData);
                }
                sr.Close();
                msc.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0) { 
                  clickIndex = listView1.FocusedItem.Index;
                comTime.Enabled = true;
                btDelete.Enabled = true;
            }
        }
    }
}

