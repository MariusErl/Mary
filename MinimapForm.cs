using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mary
{
    public partial class MaryMinimap : Form
    {
        #region Loading and initalizers

        public MaryMinimap()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            Program.MinimapFormWindow.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, MapSize(Settings.MINIMAPSIZE), MapSize(Settings.MINIMAPSIZE), MapSize(Settings.MINIMAPSIZE), MapSize(Settings.MINIMAPSIZE)));
            Program.MinimapFormWindow.Location = new Point(Settings.MINIMAPLOC.X, Settings.MINIMAPLOC.Y);
            Program.MinimapFormWindow.Opacity = (double)Settings.OPACITY;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                const int WS_EX_TOPMOST = 0x00000008;
                const int WS_EX_NOACTIVATE = 0x08000000;
                const int WS_EX_TOOLWINDOW = 0x00000080;
                const int WS_EX_TRANSPARENT = 0x00000020;

                baseParams.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT);
                return baseParams;
            }
        }

        static public int MapSize(int size)
        {
            int x = 160;
            if (size == 1)
            {
                return 160;
            }
            if (size == 2)
            {
                return 240;
            }
            if (size == 3)
            {
                return 320;
            }

            return x;
        }

        static public float MapScaling(int size)
        {
            float x = 1.0f;
            if (size == 1)
            {
                return 1.0f;
            }
            if (size == 2)
            {
                return 1.5f;
            }
            if (size == 3)
            {
                return 2.0f;
            }
            return x;
        }
        #endregion

        #region Hoovering and pressing minimap
        private void Form1_Hoover(object sender, EventArgs e)
        {
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //for (int x = 0; x < Arrows.Count(); x++)
            //{
            //    if (Math.Abs(Arrows[x].Rec.Location.X - e.Location.X) < 4 && Math.Abs(Arrows[x].Rec.Location.Y - e.Location.Y) < 4 && Arrows[x].car.P.x != 0 && Arrows[x].car.P.y != 0 && Arrows[x].car.onTrack)
            //    {
            //        Messages.SendLocalMsg("" + Arrows[x].car.PlayerName + " ^8clicked.");
            //        Program.SpectateUser(Arrows[x].car);
            //        break;
            //    }
            //}
        }
        #endregion

        static public int ZoomOut(int speed)
        {
            decimal percent;
            if (speed <= 10)
                percent = 1;
            else percent = 1 + (speed / 100);

            return (int)percent * 320;
        }

        #region Draw shit
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.ScaleTransform(MapScaling(Settings.MINIMAPSIZE), MapScaling(Settings.MINIMAPSIZE));

                Pen DefaultColor = new Pen(Color.FromArgb(244, 164, 66), 4);

                var fontFamily = new FontFamily("Arial Black");
                var font = new Font(fontFamily, 15, FontStyle.Bold, GraphicsUnit.Pixel);

                float xCentrConverted = (Program.ViewedUser.P.x + 1280) / 2;
                float yCentrConverted = (Program.ViewedUser.P.y * -1 + 1280) / 2;

                Rectangle destRect = new Rectangle(0, 0, 160, 160);
                float bw2 = 160 / 2f;
                float bh2 = 160 / 2f;
                e.Graphics.TranslateTransform(bw2, bh2);
                e.Graphics.RotateTransform(Math.Abs(Program.ViewedUser.P.heading / 32768f * 180));
                e.Graphics.TranslateTransform(-bw2, -bh2);
                e.Graphics.DrawImage(Kart, destRect, xCentrConverted - 160, yCentrConverted - 160, 320, 320, GraphicsUnit.Pixel);
                e.Graphics.DrawString("N", font, Brushes.Purple, 80, 0);

                PointF car;
                Pen circ, pen;

                if (Settings.MINIMAPGRID)
                {
                    DrawGrid();
                }

                List<Clients> TempAll = new List<Clients>(Program.AllConnections);
                foreach (Clients user in TempAll)
                {
                    if (user.onTrack && user.P.x != 0 && user.P.y != 0)
                    {
                        pen = DefaultColor;

                        float userX = (user.P.x + 1280) / 2; float userXConverted = (xCentrConverted - userX) * -1 / 2;
                        float userY = (user.P.y * -1 + 1280) / 2; float userYConverted = (yCentrConverted - userY) * -1 / 2;
                        car = new PointF(userXConverted + 80.0f, userYConverted + 80.0f);

                        if (user != Program.ViewedUser)
                        {
                            List<float> Dir = GetArrowDir(user, userXConverted, userYConverted);
                            pen.StartCap = LineCap.ArrowAnchor;
                            e.Graphics.DrawLine(pen, Dir[0] + 80, Dir[1] + 80, Dir[2] + 80, Dir[3] + 80);
                        }

                        if (Tracker.Trackee == user)
                        {
                            circ = new Pen(Color.FromArgb(80, 255, 251, 22), 10);
                            DrawCircle(e.Graphics, circ, car, 5);
                            if (Identification.MySelf.P.Chasing)
                            {
                                GenerateTrailLine(user, e, (user == Program.ViewedUser), (int)xCentrConverted, (int)yCentrConverted);
                            }
                            if (Tracker.GetDirectDistance(user, Program.ViewedUser) > 350)
                            {
                                int size = (Tracker.GetDirectDistance(user, Program.ViewedUser) - 300) / 4;
                                DrawCircle(e.Graphics, circ, car, 5 + size);
                            }
                        }

                        if (Identification.MySelf.P.Chase.Contains(user))
                        {
                            circ = (DateTime.Now.Second % 2 == 0 ? new Pen(Color.FromArgb(80, 0, 255, 255), 10) : new Pen(Color.FromArgb(80, 255, 0, 0), 10));
                            DrawCircle(e.Graphics, circ, car, 5);
                            if (Tracker.GetDirectDistance(Program.ViewedUser, user) > 350 && Program.ViewedUser == Identification.MySelf)
                            {
                                int size = (Tracker.GetDirectDistance(Program.ViewedUser, user) - 300) / 4;
                                DrawCircle(e.Graphics, circ, car, 5 + size);
                            }
                        }

                        if (Identification.MySelf.P.Chase.Count > 0 && user == Identification.MySelf)
                        {
                            GenerateTrailLine(user, e, (user == Program.ViewedUser), (int)xCentrConverted, (int)yCentrConverted);
                        }
                    }
                }

                e.Graphics.ResetTransform();
                e.Graphics.ScaleTransform(MapScaling(Settings.MINIMAPSIZE), MapScaling(Settings.MINIMAPSIZE));

                #region Draw center arrow (viewed user)
                pen = new Pen(Color.FromArgb(37, 239, 2), 4);
                circ = new Pen(Color.FromArgb(80, 37, 239, 2), 10);
                car = new Point(80, 80);
                DrawCircle(e.Graphics, circ, car, 5);

                pen.StartCap = LineCap.ArrowAnchor;
                e.Graphics.DrawLine(pen, 80, 73, 80, 85);
                #endregion

            }
            catch (Exception) { /*Program.ConsError(ee.ToString());*/ }
        }
        #endregion

        #region Grid
        static public void DrawGrid()
        {
            string alfabet = !Program.CurrentTrack.StartsWith("KY") ? "ABCDEFGHIJKLMNOP" : "PONMLKJIHGFEDCBA";
            var fontFamily = new FontFamily("Arial Black");
            var font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel);

            using (var graphics = Graphics.FromImage(Kart))
            {
                Pen pen = new Pen(Color.Red, 1);
                int numOfCells = 16, cellSize = 80;

                for (int i = 0; i < numOfCells; i++)
                {
                    int j = !Program.CurrentTrack.StartsWith("KY") ? i + 1 : 16 - i;
                    graphics.DrawLine(pen, i * cellSize, 0, i * cellSize, numOfCells * cellSize);
                    graphics.DrawLine(pen, 0, i * cellSize, numOfCells * cellSize, i * cellSize);

                    for (int x = 0; x < numOfCells; x++)
                    {
                        graphics.DrawString(alfabet.Substring(i, 1), font, Brushes.WhiteSmoke, i * 80 + 30, x * 80 + 30);
                    }
                    for (int y = 0; y < numOfCells; y++)
                    {
                        graphics.DrawString("" + j, font, Brushes.WhiteSmoke, y * 80 + 40, i * 80 + 30);
                    }
                }
            }
        }
        #endregion

        #region Create trail
        static public void GenerateTrailLine(Clients user, PaintEventArgs e, bool viewedCar, int xCentre, int yCentre)
        {
            List<Point> Trail = new List<Point>();

            if (user.P.SuspectTrail.Count > 5)
            {
                for (int æ = 0; æ < user.P.SuspectTrail.Count; æ++)
                {
                    int trailX = (Convert.ToInt16(user.P.SuspectTrail[æ].Split(',')[0]) + 1280) / 2; int trailXConverted = (xCentre - trailX) * -1 / 2;//Siste delingen avhenger av størrelsen av bildet som tegnes.
                    int trailY = (Convert.ToInt16(user.P.SuspectTrail[æ].Split(',')[1]) * -1 + 1280) / 2; int trailYConverted = (yCentre - trailY) * -1 / 2;//Hvis 160px tegnes av kartet, deles det ikke på noe, hvis 320px tegnes deles det på 2 osv.
                    Trail.Add(new Point { X = trailXConverted + 80, Y = trailYConverted + 80 });
                }
                Pen trail = viewedCar ? new Pen(Color.FromArgb(150, 37, 239, 2), 4) : new Pen(Color.FromArgb(100, 255, 251, 22), 4);
                Trail.Reverse();
                e.Graphics.DrawLines(trail, Trail.ToArray());
            }
        }
        #endregion

        #region DrawCircle
        static private void DrawCircle(Graphics drawingArea, Pen penToUse, PointF center, int radius)
        {
            RectangleF rect = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            drawingArea.DrawEllipse(penToUse, rect);
        }
        #endregion

        #region Refresh minimap (50ms)
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Settings.MINIMAP > 1)
            {
                bool display = true;
                if (!CruiseControl.ProcessIsLFS() || !Program.ShowMinimap || !Program.IsOnline)
                {
                    Program.MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = Program.MinimapFormWindow; temp.Visible = false; });
                    display = false;
                }
                if (display)
                {
                    Program.MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = Program.MinimapFormWindow; temp.Visible = true; });
                }
                Refresh();
            }
        }
        #endregion

        #region Move minimap on screen / change opacity
        static public void ChangeFormOpazity(bool up)
        {
            if (up) { Program.MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = Program.MinimapFormWindow; temp.Opacity += 0.1; }); }
            else { Program.MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = Program.MinimapFormWindow; temp.Opacity -= 0.1; }); }
        }

        static public void ChangeFormLocationDown(bool down)
        {
            Point ChangedPoint = new Point(Program.MinimapFormWindow.Location.X, Program.MinimapFormWindow.Location.Y);
            if (down)
            {
                ChangedPoint.Y += 5;
            }
            else
            {
                ChangedPoint.Y -= 5;
            }
            Program.MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = Program.MinimapFormWindow; temp.Location = ChangedPoint; });
        }
        static public void ChangeFormLocationRight(bool right)
        {
            Point ChangedPoint = new Point(Program.MinimapFormWindow.Location.X, Program.MinimapFormWindow.Location.Y);
            if (right)
            {
                ChangedPoint.X += 5;
            }
            else
            {
                ChangedPoint.X -= 5;
            }
            Program.MinimapFormWindow.Invoke((MethodInvoker)delegate { Form temp = Program.MinimapFormWindow; temp.Location = ChangedPoint; });
        }
        #endregion

        #region ArrowDirection
        static public List<float> GetArrowDir(Clients user, float x, float y)
        {
            List<float> Points = new List<float>();
            double dir = user.P.heading;

            if (dir >= 0 && dir < 4096 || dir >= 61440 && dir <= 65536)
            {
                Points.Add(x);
                Points.Add(y - 6.0f);
                Points.Add(x);
                Points.Add(y + 6.0f);
            }
            if (dir >= 4096 && dir < 12288)
            {
                Points.Add(x - 4.0f);
                Points.Add(y - 4.0f);
                Points.Add(x + 4.0f);
                Points.Add(y + 4.0f);

            }
            if (dir >= 12288 && dir < 20480)
            {
                Points.Add(x - 6.0f);
                Points.Add(y);
                Points.Add(x + 6.0f);
                Points.Add(y);
            }
            if (dir >= 20480 && dir < 28672)
            {
                Points.Add(x - 4.0f);
                Points.Add(y + 4.0f);
                Points.Add(x + 4.0f);
                Points.Add(y - 4.0f);

            }
            if (dir >= 28672 && dir < 36864)
            {
                Points.Add(x);
                Points.Add(y + 6.0f);
                Points.Add(x);
                Points.Add(y - 6.0f);
            }
            if (dir >= 36864 && dir < 45056)
            {
                Points.Add(x + 4.0f);
                Points.Add(y + 4.0f);
                Points.Add(x - 4.0f);
                Points.Add(y - 4.0f);
            }
            if (dir >= 45056 && dir < 53248)
            {
                Points.Add(x + 6.0f);
                Points.Add(y);
                Points.Add(x - 6.0f);
                Points.Add(y);
            }
            if (dir >= 53248 && dir < 61440)
            {
                Points.Add(x + 4.0f);
                Points.Add(y - 4.0f);
                Points.Add(x - 4.0f);
                Points.Add(y + 4.0f);
            }
            return Points;
        }
        #endregion

        #region Load correct backgroundmap
        static public Image Kart = Image.FromFile(@"Maps\AS.tif");
        static public void LoadBackgroundMap()
        {
            Kart.Dispose();
            if (Program.CurrentTrack.StartsWith("AS"))
            {
                Kart = Image.FromFile(@"Maps\AS.tif");
            }
            else if (Program.CurrentTrack.StartsWith("WE"))
            {
                Kart = Image.FromFile(@"Maps\WE.tif");
            }
            else if (Program.CurrentTrack.StartsWith("AU"))
            {
                Kart = Image.FromFile(@"Maps\AU.tif");
            }
            else if (Program.CurrentTrack.StartsWith("BL"))
            {
                Kart = Image.FromFile(@"Maps\BL.tif");
            }
            else if (Program.CurrentTrack.StartsWith("FE"))
            {
                Kart = Image.FromFile(@"Maps\FE.tif");
            }
            else if (Program.CurrentTrack.StartsWith("KY"))
            {
                Kart = Image.FromFile(@"Maps\KY.tif");
            }
            else if (Program.CurrentTrack.StartsWith("RO"))
            {
                Kart = Image.FromFile(@"Maps\RO.tif");
            }
            else if (Program.CurrentTrack.StartsWith("SO"))
            {
                Kart = Image.FromFile(@"Maps\SO.tif");
            }
        }
        #endregion
    }
}
