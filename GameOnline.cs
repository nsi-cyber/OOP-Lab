using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
/*
 @author Enes Ay
 @date 23.03.2022
downgrade to net 5
 */
using System;
using System.Xml.Serialization;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace ooplab1
{
    public partial class GameOnline : Form
    {
        private char PlayerChar;
        private char OpponentChar;
        private Socket sock;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;

        bool isAdmin = false;
        String[] usr = { "user", "admin" };
        String[] pass = { "user", "admin" };
        TextWriter txt, txtx;
        bool square, triangle, circle, hexagon;
        int dif;
        int x, y;
        int tempx, tempy;
        int color;
        String actv;
        bool isClicked = false;
        string movShape;
        public static string lastUser;
        List<UserBase> list = new List<UserBase>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        string[,] gameArr;
        int[,] gameArrInt;
        int thisuserscore;
        int highscore = 0;


        public GameOnline(bool isHost, string ip = null)
        {
            InitializeComponent();
            MessageReceiver = new BackgroundWorker();
            MessageReceiver.DoWork += MessageReceiver_DoWork;
            CheckForIllegalCrossThreadCalls = false;
           // MessageReceiver.RunWorkerCompleted += MessageReceiver_RunWorkerCompleted;
            MessageReceiver.WorkerSupportsCancellation = true;


            System.Media.SoundPlayer moveSound = new System.Media.SoundPlayer(@"c:\mywavfile.wav");
            System.Media.SoundPlayer pointSound = new System.Media.SoundPlayer(@"c:\mywavfile.wav");

         





            if (File.Exists("settings.txt"))


            {
                string[] lines = System.IO.File.ReadAllLines("settings.txt");

                if (lines[0] == "3")
                {
                    dif = int.Parse(lines[0]);
                    x = int.Parse(lines[1]);
                    y = int.Parse(lines[2]);

                    square = bool.Parse(lines[3]);
                    triangle = bool.Parse(lines[4]);
                    circle = bool.Parse(lines[5]);

                    color = int.Parse(lines[6]);

                }
                else
                {
                    dif = int.Parse(lines[0]);
                    switch (lines[0])
                    {
                        case "0":
                            x = 15;
                            y = 15;
                            break;
                        case "1":
                            x = 9;
                            y = 9;
                            break;
                        case "2":
                            x = 6;
                            y = 6;
                            break;
                    }

                    square = bool.Parse(lines[3]);
                    triangle = bool.Parse(lines[4]);
                    circle = bool.Parse(lines[5]);

                    color = int.Parse(lines[6]);

                }

            }

            gameArr = new string[x, y];
            gameArrInt = new int[x, y];
            CreateField(x, y);
            ButtonClickAyarla();
            if (isHost)
            {
               
                server = new TcpListener(System.Net.IPAddress.Any, 5732);
                server.Start();
                sock = server.AcceptSocket();

                randomYerlestir(gameArr, gameArrInt);

                nextMap(x * y, gameArr);

                clickBlock(gameArrInt);

            }
            else
            {
             
                try
                {
                    client = new TcpClient(ip, 5732);
                    sock = client.Client;
                    MessageReceiver.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }


        }

        private void MessageReceiver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageReceiver.RunWorkerAsync();

        }
        private void FreezeBoard()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                panel1.Controls[i].Enabled = false;
            }
        }

        private void UnfreezeBoard()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                panel1.Controls[i].Enabled = true;
            }
        }
        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            
           // FreezeBoard();
            label1.Text = "Opponent's Turn!";
            ReceiveMove();
            label1.Text = "Your Trun!";
           
           UnfreezeBoard();

        }

        private void ReceiveMove()
        {
     
            byte[] buffer = new byte[200];
            sock.Receive(buffer);
            gameArr = ByteToTwo(buffer);
            gameArrInt = toIntArr(gameArr);

           
            randomYerlestir(gameArr, gameArrInt);
            
            nextMap(gameArr.GetLength(0) * gameArr.GetLength(1), gameArr);
            label1.Text = "Opponent's Turn!";
        }
        public int[,] toIntArr(string[,] asd)
        {
            int[,] arr = new int[asd.GetLength(0), asd.GetLength(1)];
            for (int i = 0; i < asd.GetLength(0); i++)
            {
                for (int j = 0; j < asd.GetLength(1); j++)
                {
                    if (asd[i, j].Length > 2)
                        arr[i, j] = 1;
                    else
                        arr[i, j] = 0;
                }
            }

            return arr;
        }

        public byte[] TwoToByte(string[,] DataValue)
        {
            const int PIPEDELIMETER = 124;
            List<byte> ListOfBytes = new List<byte>();

            // Adding metadata - dimension of the first index. Plus the delimiter.
            ListOfBytes.Add(Convert.ToByte(DataValue.GetUpperBound(0) + 1));
            ListOfBytes.Add(PIPEDELIMETER);

            // Adding metadata - dimension of the second index. Plus the delimiter.
            ListOfBytes.Add(Convert.ToByte(DataValue.GetUpperBound(1) + 1));
            ListOfBytes.Add(PIPEDELIMETER);

            for (int i = 0; i <= DataValue.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= DataValue.GetUpperBound(1); j++)
                {
                    try { ListOfBytes.AddRange(Encoding.ASCII.GetBytes(DataValue[i, j])); }
                    // Add the string as bytes in ListOfBytes.
                    catch (Exception a) { DataValue[i, j] = "0";
                        ListOfBytes.AddRange(Encoding.ASCII.GetBytes(DataValue[i, j]));

                    };

                    // Add the delimiter
                    ListOfBytes.Add(PIPEDELIMETER);
                }
            }

            byte[] Bytes = ListOfBytes.ToArray();
            return Bytes;
        }

        public string[,] ByteToTwo(byte[] Bytes)
        { /*Reconstruction*/
            int FirstDimensionCount = Bytes[0]; // First dimension from metadata.
            int SecondDimensionCount = Bytes[2]; // Second dimension from metadata.

            // String array to hold the reconstructed data.
            string[,] DataValueConvertedBack = new string[FirstDimensionCount, SecondDimensionCount];
            int BytesCount = 4; // Data starts from the 4-th index.

            // Get the entire bytes array into character array.
            char[] CharsFromBytes = Encoding.UTF8.GetString(Bytes).ToCharArray();

            StringBuilder SBuilder = new StringBuilder();
            for (int i = 0; i < FirstDimensionCount; i++)
            {
                for (int j = 0; j < SecondDimensionCount; j++)
                {
                    while (CharsFromBytes[BytesCount] != '|')
                        SBuilder.Append(CharsFromBytes[BytesCount++]); // Build the string character by character.
                    DataValueConvertedBack[i, j] = SBuilder.ToString(); // Add the string to the specified string array index.
                    BytesCount++;   // Increase the bytes pointer as it is currently pointing to the pipe
                    SBuilder.Clear();   // Clear out for the next operation.
                }
            }

            // DataValueConvertedBack (receiver end) = DataValue (sending end) at this point.}
            return DataValueConvertedBack;
        }

        public void givePuan(int count)
        {
            int a = Int32.Parse(label3.Text);
            System.Media.SoundPlayer pointSound = new System.Media.SoundPlayer("pointSound.wav");
            pointSound.Play();
            switch (dif)
            {
                case 3:
                    a += count * 2;
                    break;
                case 0:
                    a += count * 1;
                    break;
                case 1:
                    a += count * 3;
                    break;
                case 2:
                    a += count * 5;
                    break;

            }
            label3.Text = a + "";
        }


        public void clickBlock(int[,] arrint)
        {
            //picturebox button
            PictureBox btn = null;
            int ase = 0;
            int a, b, c, aw, aws, sdd;

            ///3d to 2d
            for (int i = 0; i < x * y; i++)
            {
                if (i < x)
                    aw = 0;
                else
                {
                    aw = (i / x);
                    aws = i % x;
                    if (aws == 0)
                        aw = aw - 1;
                }
                sdd = (i - aw * x) - 1;
                if (sdd == -1)
                    sdd++;

                btn = panel1.Controls[i] as PictureBox;


                switch (arrint[sdd, aw])
                {
                    case 0:
                        btn.Enabled = false;

                        btn.Image = new Bitmap(Image.FromFile(@"new\button.png"));

                        break;
                    case 1:
                        btn.Enabled = true;
                        switch (gameArr[sdd, aw])
                        {
                            case "triangle":
                                btn.Image = new Bitmap(Image.FromFile(@"new\triangle.jpg"));
                                break;
                            case "circle":
                                btn.Image = new Bitmap(Image.FromFile(@"new\circle.jpg"));
                                break;
                            case "square":
                                btn.Image = new Bitmap(Image.FromFile(@"new\square.jpg"));
                                break;




                        }
                        break;

                }
            }

        }

        public void blockEvery()
        {
            //picturebox button
            PictureBox btn = null;


            ///3d to 2d
            for (int i = 0; i < x * y; i++)
            {


                btn = panel1.Controls[i] as PictureBox;


                btn.Enabled = false;




            }
        }



        public void nextMap(int size, string[,] arr)
        {//PictureBox
            PictureBox btn = null;
            int ase, aw, sdd, aws, sdds = 0;


            ///3d to 2d
            for (int i = 0; i < x * y; i++)
            {
                if (i < x)
                    aw = 0;
                else
                {
                    aw = (i / x);
                    aws = i % x;
                    if (aws == 0)
                        aw = aw - 1;
                }
                sdd = (i - aw * x) - 1;
                if (sdd == -1)
                    sdd++;
                //PictureBox
                btn = panel1.Controls[i] as PictureBox;

                btn.Image = new Bitmap(Image.FromFile(@"new\button.png"));
                switch (arr[sdd, aw])
                {
                    case "triangle":
                        btn.Image = new Bitmap(Image.FromFile(@"new\triangle.jpg"));
                        break;
                    case "circle":
                        btn.Image = new Bitmap(Image.FromFile(@"new\circle.jpg"));
                        break;
                    case "square":
                        btn.Image = new Bitmap(Image.FromFile(@"new\square.jpg"));
                        break;
                    default:
                        btn.Image = new Bitmap(Image.FromFile(@"new\button.jpg"));
                        break;
                }
            }


        }



        public void randomYerlestir(string[,] arr, int[,] arrint)
        {
            string a;
            int rX;
            int rY;
            int shape;
            Random rnd = new Random();
            if (yerVar(arrint))
            {

                for (int i = 0; i < 3; i++)
                {
                    rX = rnd.Next(0, x);
                    rY = rnd.Next(0, y);
                    shape = rnd.Next(0, 3);
                    //stay
                    while (true)
                    {
                        if (0 == arrint[rX, rY])
                            break;
                        else
                        {
                            rX = rnd.Next(0, x);
                            rY = rnd.Next(0, y);
                        }
                    }
                    //

                    if (square && !triangle && !circle)
                    { arr[rX, rY] = "square"; }
                    else if (!square && triangle && !circle) { arr[rX, rY] = "triangle"; }
                    else if (!square && !triangle && circle) { arr[rX, rY] = "circle"; }
                    else if (square && !triangle && circle)
                    {
                        shape = rnd.Next(0, 2);

                        switch (shape)
                        {
                            case 0:
                                //triangle
                                arr[rX, rY] = "square";
                                break;
                            case 1:
                                //circle
                                arr[rX, rY] = "circle";
                                break;

                        }




                    }

                    else if (square && triangle && !circle)
                    {
                        shape = rnd.Next(0, 2);

                        switch (shape)
                        {
                            case 0:
                                //triangle
                                arr[rX, rY] = "square";
                                break;
                            case 1:
                                //circle
                                arr[rX, rY] = "triangle";
                                break;

                        }
                    }
                    else if (!square && triangle && circle)
                    {
                        shape = rnd.Next(0, 2);

                        switch (shape)
                        {
                            case 0:
                                //triangle
                                arr[rX, rY] = "triangle";
                                break;
                            case 1:
                                //circle
                                arr[rX, rY] = "circle";
                                break;

                        }
                    }

                    else
                    {
                        switch (shape)
                        {
                            case 0:
                                //triangle
                                arr[rX, rY] = "triangle";
                                break;
                            case 1:
                                //circle
                                arr[rX, rY] = "circle";
                                break;
                            case 2:
                                //square
                                arr[rX, rY] = "square";
                                break;
                        }
                    }
                    arrint[rX, rY] = 1;

                }

            }

        }



        public static bool IsTextFileEmpty(string fileName)
        {
            var info = new FileInfo(fileName);
            if (info.Length == 0)
                return true;

            // only if your use case can involve files with 1 or a few bytes of content.
            if (info.Length < 6)
            {
                var content = File.ReadAllText(fileName);
                return content.Length == 0;
            }
            return false;
        }











        private void CreateField(int x, int y)

        {

            int X = 1, Y = 1;

            int btnNameIndis = 0;

            for (int i = 0; i < x; i++)

            {

                for (int j = 0; j < y; j++)

                {
                    //picturebox button
                    PictureBox btn = new PictureBox();

                    btn.Left = X; btn.Top = Y;

                    btn.Height = 25; btn.Width = 25;

                    btn.Name = "btn" + btnNameIndis;


                    btn.TabIndex = btnNameIndis;

                    btn.Image = new Bitmap(Image.FromFile(@"new\button.jpg"));

                    // btn.FlatStyle = FlatStyle.Popup;

                    btn.BackgroundImageLayout = ImageLayout.Stretch;


                    panel1.Controls.Add(btn);

                    X += btn.Width + 1;

                    btnNameIndis++;

                }

                Y += 25 + 1;

                X = 1;

            }



            panel1.Height = 26 * x + 1;

            panel1.Width = 26 * y + 1;

            panel1.Height = panel1.Height + panel1.Top + 29;

            panel1.Width = panel1.Width + panel1.Left + 7;



        }





        private void ButtonClickAyarla()
        {
            foreach (Control ctl in panel1.Controls)
            {
                ctl.MouseClick += new MouseEventHandler(Form1_MouseClick);
            }
        }


        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Event(sender);
        }

        private void Event(object sender)
        {
            if (sender.GetType().Name == "PictureBox")
            {
                
                nextMap(gameArr.GetLength(0) * gameArr.GetLength(1), gameArr);
                //picturebox button
                PictureBox btn = (PictureBox)sender;
                string a = btn.Name;
                string b, c, h;
                int d, e;

                int aw, aws, sdd;
                a = a.Remove(0, 3);
                // using the method

                int i = Int32.Parse(a);
                ///3d to 2d

                if (i < x)
                    aw = 0;
                else
                {
                    aw = (i / x);
                    aws = i % x;
                    if (aws == 0)
                        aw = aw - 1;
                }
                sdd = (i - aw * x) - 1;
                if (sdd == -1)
                    sdd++;


                if (sdd == tempx && aw == tempy)
                {
                    isClicked = false;
                    gameArrInt[tempx, tempy] = 1;
                    clickBlock(gameArrInt);
                    tempx = x + 1; tempy = y + 1;
                    movShape = "";
                }


                else if (isClicked == false)
                {







                    movShape = gameArr[sdd, aw];
                    clickOpen(x * y, gameArrInt);
                    btn.Enabled = true;
                    isClicked = true;
                    tempx = sdd;
                    tempy = aw;

                }
                else
                {
                    isClicked = false;




                    gameArrInt[tempx, tempy] = 0;
                    gameArr[tempx, tempy] = "0";
                    btn.Enabled = true;

                    int sonuc = BFS(gameArrInt, new Point(tempx, tempy), new Point(sdd, aw));
                    if (sonuc == -1)
                    {
                        isClicked = true;
                        MessageBox.Show("cant go");
                        gameArrInt[tempx, tempy] = 1;
                        gameArr[tempx, tempy] = movShape;
                    }
                    else
                    {
                        System.Media.SoundPlayer moveSound = new System.Media.SoundPlayer("moveSound.wav");
                        moveSound.Play();
                        gameArr[sdd, aw] = movShape;
                        gameArrInt[sdd, aw] = 1;
                        // ShowAns(gameArr, gameArrInt);
                        puanCheck(sdd, aw);

                       // randomYerlestir(gameArr, gameArrInt);
                        nextMap(x * y, gameArr);
                        clickBlock(gameArrInt);


                        byte[] num = TwoToByte(gameArr);
                        sock.Send(num);
                        System.Threading.Thread.Sleep(1000);
                        Application.DoEvents(); 
                        MessageReceiver = new BackgroundWorker();
                        MessageReceiver.DoWork += MessageReceiver_DoWork;
                        MessageReceiver.RunWorkerAsync();



                    }
                }


            }

            if (!yerVar(gameArrInt))
            {
                MessageBox.Show("Game End - Score: " + label3.Text);
                blockEvery();
                thisuserscore = Int32.Parse(label3.Text);
             
             


            }

        }

        public void gameStart()
        {
            CreateField(x, y);
            ButtonClickAyarla();

            randomYerlestir(gameArr, gameArrInt);

            nextMap(x * y, gameArr);

            clickBlock(gameArrInt);
        }


        // To store matrix cell coordinates
        public class Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        };

        // A Data Structure for queue used in BFS
        public class queueNode
        {
            // The coordinates of a cell
            public Point _pt;

            // cell's distance of from the source
            public int dist;

            public queueNode(Point pt, int dist)
            {
                _pt = pt;
                this.dist = dist;

            }

            public Point pt
            {
                get { return _pt; }
                set { _pt = value; }
            }

        };
        Queue<queueNode> road = new Queue<queueNode>();
        // check whether given cell (row, col)
        // is a valid cell or not.
        bool isValids(int row, int col)
        {
            // return true if row number and
            // column number is in range
            return (row >= 0) && (row < x) &&
                   (col >= 0) && (col < y);
        }

        // These arrays are used to get row and column
        // numbers of 4 neighbours of a given cell
        static int[] rowNum = { -1, 0, 0, 1 };
        static int[] colNum = { 0, -1, 1, 0 };

        // function to find the shortest path between
        // a given source cell to a destination cell.
        int BFS(int[,] mat, Point src,
                                  Point dest)
        {
            // check source and destination cell
            // of the matrix have value 1
            if (mat[src.X, src.Y] != 0 ||
                mat[dest.X, dest.Y] != 0)
                return -1;

            bool[,] visited = new bool[x, y];

            // Mark the source cell as visited
            visited[src.X, src.Y] = true;
            road = new Queue<queueNode>();
            // Create a queue for BFS
            Queue<queueNode> q = new Queue<queueNode>();
            //queue olustur ve onun elemanlari cek
            // Distance of source cell is 0
            queueNode s = new queueNode(src, 0);
            q.Enqueue(s); // Enqueue source cell
            road.Enqueue(s); //

            // Do a BFS starting from source cell
            while (q.Count != 0)
            {
                queueNode curr = q.Peek();
                Point pt = curr.pt;

                // If we have reached the destination cell,
                // we are done
                if (pt.X == dest.X && pt.Y == dest.Y)
                    return curr.dist;

                // Otherwise dequeue the front cell
                // in the queue and enqueue
                // its adjacent cells
                q.Dequeue();
                road.Dequeue();


                for (int i = 0; i < 4; i++)
                {
                    int row = pt.X + rowNum[i];
                    int col = pt.Y + colNum[i];

                    // if adjacent cell is valid, has path
                    // and not visited yet, enqueue it.
                    if (isValids(row, col) &&
                            mat[row, col] == 0 &&
                       !visited[row, col])
                    {
                        // mark cell as visited and enqueue it
                        visited[row, col] = true;
                        queueNode Adjcell = new queueNode
                                   (new Point(row, col),
                                        curr.dist + 1);
                        q.Enqueue(Adjcell);
                        road.Enqueue(Adjcell);

                    }
                }
            }

            // Return -1 if destination cannot be reached
            return -1;
        }


        public bool yerVar(int[,] arr)//oyunu bitirmek icin kontrol
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (0 == arr[i, j]) return true;
                }
            }


            return false;
        }

        public void clickOpen(int size, int[,] arrint)
        {
            //PictureBox
            PictureBox btn = null;
            int a, b, c, aw, aws, sdd;

            ///3d to 2d
            for (int i = 0; i < x * y; i++)
            {
                if (i < x)
                    aw = 0;
                else
                {
                    aw = (i / x);
                    aws = i % x;
                    if (aws == 0)
                        aw = aw - 1;
                }
                sdd = (i - aw * x) - 1;
                if (sdd == -1)
                    sdd++;

                btn = panel1.Controls[i] as PictureBox;


                switch (arrint[sdd, aw])
                {
                    case 1:
                        btn.Enabled = false;
                        switch (gameArr[sdd, aw])
                        {
                            case "triangle":
                                btn.Image = new Bitmap(Image.FromFile(@"new\triangle.png"));
                                break;
                            case "circle":
                                btn.Image = new Bitmap(Image.FromFile(@"new\circle.png"));
                                break;
                            case "square":
                                btn.Image = new Bitmap(Image.FromFile(@"new\square.png"));
                                break;




                        }
                        break;
                    case 0:
                        btn.Enabled = true;
                        btn.Image = new Bitmap(Image.FromFile(@"new\button.jpg"));
                        break;

                }
            }

        }

        public void puanCheck(int a, int b)
        {
            int temx = a;
            int temy = b;
            int count = 1;
            string obj = gameArr[temx, temy];
            //check right
            while (temx + 1 < gameArr.GetLength(0) && obj == gameArr[temx + 1, temy])
            {
                count++;
                temx++;
            }
            temx = a;
            //check left
            while (temx - 1 >= 0 && obj == gameArr[temx - 1, temy])
            {
                count++;
                temx--;
            }
            temx = a;
            if (count >= 5)
            {
                givePuan(count);
                //delete right
                while (temx + 1 < gameArr.GetLength(0) && obj == gameArr[temx + 1, temy])
                {
                    gameArr[temx + 1, temy] = "0";
                    gameArrInt[temx + 1, temy] = 0;
                    temx++;
                }
                temx = a;
                //delete left
                while (temx - 1 >= 0 && obj == gameArr[temx - 1, temy])
                {
                    gameArr[temx - 1, temy] = "0";
                    gameArrInt[temx - 1, temy] = 0;
                    temx--;
                }
                temx = a;
                gameArr[a, b] = "0";
                gameArrInt[a, b] = 0;
                PictureBox btn = panel1.Controls[b * x + a] as PictureBox;
                btn.Enabled = false;
                btn.Image = new Bitmap(Image.FromFile(@"new\button.png"));
            }
            count = 1;
            //check up
            while (temy + 1 < gameArr.GetLength(1) && obj == gameArr[temx, temy + 1])
            {
                count++;
                temy++;
            }
            temy = b;
            //check down
            while (temy - 1 >= 0 && obj == gameArr[temx, temy - 1])
            {
                count++;
                temy--;
            }
            temy = b;

            if (count >= 5)
            {
                givePuan(count);
                //delete up
                while (temy + 1 < gameArr.GetLength(1) && obj == gameArr[temx, temy + 1])
                {
                    gameArr[temx, temy + 1] = "0";
                    gameArrInt[temx, temy + 1] = 0;
                    temy++;
                }
                temy = b;
                //delete down
                while (temy - 1 >= 0 && obj == gameArr[temx, temy - 1])
                {
                    gameArr[temx, temy - 1] = "0";
                    gameArrInt[temx, temy - 1] = 0;
                    temy--;
                }
                gameArr[a, b] = "0";
                gameArrInt[a, b] = 0;
                PictureBox btn = panel1.Controls[b * x + a] as PictureBox;
                btn.Enabled = false;
                btn.Image = new Bitmap(Image.FromFile(@"new\button.png"));
            }

            byte[] num =TwoToByte(gameArr);
            sock.Send(num);
            label1.Text = "Opponent's Turn!";
            MessageReceiver.RunWorkerAsync();


        }

        // ROW x COL matrix
        public static int ROW, COL;

        // Check if it is possible to go to (x, y) from current position.
        public bool isSafe(int[,] matrix, int[,] visited, int x, int y)
        {
            // Returns false if the cell has value 0 or already visited
            return !(matrix[x, y] == 1 || visited[x, y] != 0);
        }

        // Check whether given cell (row, col) is a valid cell or not.
        public bool isValid(int row, int col)
        {
            // Return true if row number and column number is in range
            return (row >= 0) && (row < ROW) && (col >= 0) && (col < COL);
        }

        // Find Shortest Possible Route in the matrix from source cell (0, 0) to destination cell (x, y)

        // 'min_dist' stores length of longest path from source to destination found so far
        // and 'dist' maintains length of path from source cell to the current cell (i, j)
        public int shortestPathBinaryMatrixHelper(int[,] matrix, int[,] visited, int i, int j, int x, int y, int min_dist, int dist)
        {
            // If destination is found, update min_dist
            if (i == x && j == y)
            {
                if (dist < min_dist)
                    return dist;
                else
                    return min_dist;
            }

            // Set (i, j) cell as visited
            visited[i, j] = 1;

            // Go to bottom cell
            if (isValid(i + 1, j) && isSafe(matrix, visited, i + 1, j))
            {
                min_dist = shortestPathBinaryMatrixHelper(matrix, visited, i + 1, j, x, y, min_dist, dist + 1);
            }

            // Go to right cell
            if (isValid(i, j + 1) && isSafe(matrix, visited, i, j + 1))
            {
                min_dist = shortestPathBinaryMatrixHelper(matrix, visited, i, j + 1, x, y, min_dist, dist + 1);
            }

            // Go to top cell
            if (isValid(i - 1, j) && isSafe(matrix, visited, i - 1, j))
            {
                min_dist = shortestPathBinaryMatrixHelper(matrix, visited, i - 1, j, x, y, min_dist, dist + 1);
            }

            // Go to left cell
            if (isValid(i, j - 1) && isSafe(matrix, visited, i, j - 1))
            {
                min_dist = shortestPathBinaryMatrixHelper(matrix, visited, i, j - 1, x, y, min_dist, dist + 1);
            }

            // Backtrack - Remove (i, j) from visited matrix
            visited[i, j] = 0;

            return min_dist;
        }
        public int shortestPathBinaryMatrix(int[,] matrix, Point src, Point dest)
        {
            ROW = matrix.GetLength(0);
            COL = matrix.GetLength(1);

            // Check source and destination cell of the matrix have value 1
            if (matrix[src.X, src.Y] != 0 || matrix[dest.X, dest.Y] != 0)
            {
                return -1;
            }

            int[,] visited = new int[ROW, COL];
            int min_dist = shortestPathBinaryMatrixHelper(matrix, visited, src.X, src.Y, dest.X, dest.Y, Int32.MaxValue, 0);

            // If min_dist doesn't change
            if (min_dist == Int32.MaxValue)
            {
                return -1;
            }
            return min_dist;
        }

    }
}
