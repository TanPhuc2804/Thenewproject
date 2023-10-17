using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    internal class MyThread
    {
        const double PI = 3.1416;
        public Thread t; //tham khảo ₫ến thread hiện hành
        public Boolean start; //trạng thái Start của thread
        public Boolean stop; //trạng thái Start của thread
        public Boolean suspend; //trạng thái Suspend của thread
        public Boolean WaitOne = false; //trạng thái chờ truy xuất cell
        public Bitmap Pic; //icon miêu tả thread
        internal int Xmax; //₫ộ rộng vùng chạy của thread
        internal int Ymax; //₫ộ cao vùng chạy của thread
        public Point Pos; //vị trí của thread trong vùng chạy
        double dblGocChay; //góc chạy của thread
        internal double tx, ty; //bước tăng theo x và y
                                //hàm khởi tạo các thông số của thread
        public MyThread(Random rnd, int xMax, int yMax)
        {
            Xmax = xMax; Ymax = yMax;
            Pos.X = (int)(rnd.Next(0, Xmax));
            Pos.Y = (int)(rnd.Next(0, Ymax));
            dblGocChay = ChinhGocChay(rnd.Next(0, 360));
        }
        double ChinhGocChay(double dblGocChay)
        {
            double goc = dblGocChay;
            if (0 <= goc && goc < 90) return 45;
            if (90 <= goc && goc < 180) return 135;
            if (180 <= goc && goc < 270) return 225;
            if (270 <= goc) return 315;
            return goc;
        }
        //=========================================================
        //Tính góc phản xạ mới khi thread ₫ụng thành ₫ứng (bên trái hay phải).
        //=========================================================
        double DoiGocChayX(double dblGocChay)
        {
            double goc;
            if (dblGocChay > 0 && dblGocChay < 180) goc = 180 - dblGocChay;
            else goc = 180 + 360 - dblGocChay;
            return ChinhGocChay(goc);
        }
        //=========================================================
        //Tính góc phản xạ mới khi thread ₫ụng thành ngang (trên hay dưới).
        //=========================================================
        double DoiGocChayY(double dblGocChay)
        {
            return ChinhGocChay(360 - dblGocChay);
        }
        //=========================================================
        //Hiệu chỉnh vị trí của thread
        //=========================================================
        public void HieuchinhVitri()
        {
            int x, y;
            x = Pos.X;
            y = Pos.Y;
            if (x == 0 || x == Xmax - 1 || y == 0 || y == Ymax - 1)
            {
                //icon ₫ụng thành ngang hay dọc -> thay ₫ổi góc chạy 
                if (x == 0 || x == Xmax - 1)
                {
                    dblGocChay = DoiGocChayX(dblGocChay);
                }
                else if (y == 0 || y == Ymax - 1)
                    dblGocChay = DoiGocChayY(dblGocChay);
            }
            //Hiệu chỉnh tọa ₫ộ x của thread
            tx = 2 * Math.Cos(dblGocChay * PI / 180);
            x = x + (int)tx;
            if (x < 0)
            {
                x = 0;
            }
            else if (x >= Xmax)
            {
                x = Xmax - 1;
            }
            //Hiệu chỉnh tọa ₫ộ y của thread
            ty = 2 * Math.Sin(dblGocChay * PI / 180);
            y = y + (int)ty;
            if (y < 0)
            {
                y = 0;
            }
            else if (y >= Ymax)
            {
                y = Ymax - 1;
            }
            //chỉnh góc chạy khi ₫ụng 1 trong 4 góc
            if (x == 0 && y == 0) //góc trên trái
                ChinhGocChay(dblGocChay + 45);
            else if (x == 0 && y == Ymax - 1) //góc dưới trái
                ChinhGocChay(dblGocChay + 45);
            else if (x == Xmax - 1 && y == 0) //góc trên phải
                ChinhGocChay(dblGocChay + 45);
            else if (x == Xmax - 1 && y == Ymax - 1) //góc dưới phải
                ChinhGocChay(dblGocChay + 45);
            //Lưu vị trí mới
            Pos.X = (int)x;
            Pos.Y = (int)y;
        }
    }
}
