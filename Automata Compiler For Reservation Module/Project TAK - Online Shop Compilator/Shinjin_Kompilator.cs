using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project_TAK___Online_Shop_Compilator
{
    public partial class Form_Menu : Form
    {
        public Form_Menu()
        {
            InitializeComponent();
        }

        System.Media.SoundPlayer SoundWelcome = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 1.wav");
        System.Media.SoundPlayer SoundPesanLagi = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 2.wav");
        System.Media.SoundPlayer SoundSetuju = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 3.wav");
        System.Media.SoundPlayer SoundTerimaKasih = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 4.wav");
        System.Media.SoundPlayer SoundPesan = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 5.wav");
        System.Media.SoundPlayer SoundInputSalah = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 6.wav");
        System.Media.SoundPlayer SoundNama = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 7.wav");
        System.Media.SoundPlayer SoundNomor = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 8.wav");
        System.Media.SoundPlayer SoundJumlah = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 9.wav");
        System.Media.SoundPlayer SoundSelesai = new System.Media.SoundPlayer(Application.StartupPath + "\\Music\\Voice 10.wav");

        Stack<Token> StackToken = new Stack<Token>();
        List<Token> ListToken = new List<Token>();
        
        Token[] DaftarToken; //Nama Token Saat Input
        Token[] DaftarItems = new Token[37];
        string[,] brgs = { { "Meja", "Kursi", "Lemari" },
                          { "0","0","0"},
                          {"0","0","0"}};
        string[,] TabelSLR = new string[37, 22];
        int Baris = 0, Kolom = 0;
        int urutanPertanyaan = 1;
        int TotalBayar = 0;
        /*Urutannya :
            1. Pesan
            2. Ya / Tidak
            3. Setuju / Tidak
            4. Nama
            5. Nomor
            1. Kuantiti yang dipesan
        */

        #region Token Non Terminal
        Token Subjek;
        Token Predikat;
        Token Produk;
        Token Kuantiti;
        Token Koma;
        Token Jawaban;
        Token Nama;
        Token Nomor;
        Token Entr;

        Token S;
        Token S1;
        Token S2;
        Token S3;
        Token S4;
        Token S5;
        Token S6;

        Token A;
        Token B;
        Token C;
        Token D;
        Token E;
        #endregion

        string Welcome = "Selamat Datang di Fantasy Online Shop. Kami menjual berbagai macam perabot rumah tangga."
                       + "Pilihan perabot rumah tangga yang tersedia beserta harganya, yaitu:"
                       + "\r\n- Kursi (Rp 10.000,-)"
                       + "\r\n- Meja (Rp 20.000,-)"
                       + "\r\n- Lemari (Rp 30.000,-)"
                       + "\r\nSebutkan pilihan perabot rumah tangga beserta jumlah yang ingin Anda beli!";
        //string jawabanAnda = "";

        private void btnKirim_Click(object sender, EventArgs e)
        {
            /*if (urutanPertanyaan==2 || urutanPertanyaan == 3)
            {
                jawabanAnda = txtInput.Text;
            }*/
            txtJawabanPemesan.Text = txtInput.Text;
            Status = false;
            EnterChat(txtInput.Text, 1);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
        }

        int Lulus = 0, jumlahProdukInput=0;
        int checkProduk = 0, checkKuantiti = 0, checkP=0, adaKoma = 0;

        public int LulusGagal(string Teks)
        {
            Lulus = 0;
            DaftarToken = new Token[100];
            string[] hasilsplit = Teks.Split(new Char[] { ' ' });
            for (int i = 0; i < hasilsplit.Length; i++)
            {
                if (hasilsplit[i] != "")
                {
                    DaftarToken[i] = NamaToken(hasilsplit[i]);
                }
            }

            for (int i = 0; i < DaftarToken.Count(); i++)
            {
                if (DaftarToken[i] != null)
                {
                    
                    if (DaftarToken[i].Nama == "Error")
                    {
                        Lulus = 0;
                        i = 100;
                    }
                    else
                    {
                        if (urutanPertanyaan == 1)
                        {
                            if (DaftarToken[i].Nama == "Koma")
                            {
                                adaKoma = 1;
                            }
                            if (DaftarToken[i].Nama == "Produk")
                            { checkProduk = 1; }
                            if (DaftarToken[i].Nama == "Kuantiti")
                            { checkKuantiti = 1; }
                            if (DaftarToken[i].Nama == "Predikat")
                            { checkP = 1; }

                            if (checkProduk == 1 && checkKuantiti == 1)
                            {
                                jumlahProdukInput++;
                                Lulus = 1;
                            }
                            else if (checkProduk == 1 && checkKuantiti == 0 && checkP == 1)
                            {
                                Lulus = 1;
                            }
                            else
                            {
                                Lulus = 0;
                            }
                        }
                        else if (urutanPertanyaan == 2 || urutanPertanyaan == 3)
                        {
                            if (DaftarToken[i].Nama == "Jawaban")
                            {
                                Lulus = 1;
                            }
                        }
                        else if (urutanPertanyaan == 4)
                        {
                            if (DaftarToken[i].Nama == "Nama")
                            {
                                Lulus = 1;
                            }
                        }
                        else if (urutanPertanyaan == 10)
                        {
                            if (DaftarToken[i].Nama == "Nomor")
                            {
                                Lulus = 1;
                            }
                        }
                        else if (urutanPertanyaan == 5)
                        {
                            if (DaftarToken[i].Nama == "Kuantiti")
                            {
                                Lulus = 1;
                            }
                        }
                    }
                }
                else
                { i = 100; }
            }
            return Lulus;
        }

        public void EnterChat(string Teks, int Check)
        {
            Lulus = 0;
            if (Check == 1)
            {
                if (LulusGagal(Teks) == 1)
                {
                    txtChat.Text += ("["+DateTime.Now.ToString("hh:mm:ss")+"] Anda: " + Teks + "\r\n");
                    txtJawabanPemesan.Text = txtInput.Text;
                    GetToken(Teks + " enter");

                    Parsing();
                }
                else
                {
                    txtJawabanKafra.Text += "\r\n\r\nInputan yang Anda masukkan salah ! Silahkan Input ulang.";
                    SoundInputSalah.Play();
                    MessageBox.Show("Inputan yang Anda masukkan salah ! Silahkan Input ulang.", "Informasi");
                }
            }
            else
            {
                txtChat.Text += ("[" + DateTime.Now.ToString("hh:mm:ss") + "] FOS: " + Teks + "\r\n");
            }
            txtInput.Clear();
            txtInput.Focus();
        }

        private void Form_Menu_Load(object sender, EventArgs e)
        {
            #region IMK

            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Characters\\ba1.jpg");
            picBalonKafra.Image = Image.FromFile(Application.StartupPath + "\\Characters\\b1.png");
            picBalonPemain.Image = Image.FromFile(Application.StartupPath + "\\Characters\\b2.png");
            picSilang.Image = Image.FromFile(Application.StartupPath + "\\Characters\\CancelMerah.png");
            picKeluar.Image = Image.FromFile(Application.StartupPath + "\\Characters\\CancelMerah.png");
            this.CenterToScreen();
            #endregion

            #region Music
            SoundWelcome.Play();
            #endregion

            txtJawabanKafra.Text = "";
            txtJawabanPemesan.Text = "";
            txtInput.Focus();
            EnterChat(Welcome, 0);
            txtJawabanKafra.Text = "        "+Welcome;
            #region TabelSLR
            TabelSLR[0, 0] = "Shift S8";
            TabelSLR[0, 1] = "Shift S11";
            TabelSLR[0, 2] = "Shift S16";
            TabelSLR[0, 3] = "Shift S15";
            TabelSLR[0, 5] = "Shift S17";
            TabelSLR[0, 6] = "Shift S18";
            TabelSLR[0, 7] = "Shift S19";

            TabelSLR[0, 10] = "S1";
            TabelSLR[0, 11] = "S2";
            TabelSLR[0, 12] = "S3";
            TabelSLR[0, 13] = "S4";
            TabelSLR[0, 14] = "S5";
            TabelSLR[0, 15] = "S6";
            TabelSLR[0, 16] = "S7";
            TabelSLR[0, 17] = "S9";
            TabelSLR[0, 18] = "S10";
            TabelSLR[0, 19] = "S13";
            TabelSLR[0, 20] = "S14";
            TabelSLR[0, 21] = "S12";
            
            TabelSLR[1, 9] = "Accept";

            TabelSLR[2, 8] = "Shift S20";

            TabelSLR[3, 4] = "Shift S22";
            TabelSLR[3, 8] = "Shift S21";
            
            TabelSLR[4, 8] = "Shift S23";
            TabelSLR[5, 8] = "Shift S24";
            TabelSLR[6, 8] = "Shift S25";
            TabelSLR[7, 8] = "Shift S26";

            TabelSLR[8, 1] = "Shift S11";
            TabelSLR[8, 17] = "S27";
            TabelSLR[8, 18] = "S28";

            TabelSLR[9, 8] = "Reduce 8";
            TabelSLR[10, 8] = "Reduce 11";
            
            TabelSLR[11, 2] = "Shift S31";
            TabelSLR[11, 3] = "Shift S30";
            TabelSLR[11, 12] = "S29";
            TabelSLR[11, 19] = "S13";
            TabelSLR[11, 20] = "S14";
            TabelSLR[11, 21] = "S12";
            
            TabelSLR[12, 2] = "Shift S16";
            TabelSLR[12, 3] = "Shift S30";
            TabelSLR[12, 19] = "S32";
            TabelSLR[12, 20] = "S33";

            TabelSLR[13, 4] = "Reduce 15";
            TabelSLR[13, 8] = "Reduce 15";
            
            TabelSLR[14, 4] = "Reduce 16";
            TabelSLR[14, 8] = "Reduce 16";
            
            TabelSLR[15, 2] = "Shift S34";
            TabelSLR[15, 8] = "Reduce 21";

            TabelSLR[16, 3] = "Shift S35";

            TabelSLR[17, 8] = "Reduce 20";
            TabelSLR[18, 8] = "Reduce 22";
            TabelSLR[19, 8] = "Reduce 23";

            TabelSLR[20, 9] = "Reduce 1";
            TabelSLR[21, 9] = "Reduce 2";

            TabelSLR[22, 2] = "Reduce 19";
            TabelSLR[22, 3] = "Reduce 19";

            TabelSLR[23, 9] = "Reduce 3";
            TabelSLR[24, 9] = "Reduce 4";
            TabelSLR[25, 9] = "Reduce 5";
            TabelSLR[26, 9] = "Reduce 6";
            TabelSLR[27, 8] = "Reduce 7";

            TabelSLR[28, 8] = "Reduce 10";
            TabelSLR[29, 4] = "Shift S36";
            TabelSLR[29, 8] = "Reduce 9";

            TabelSLR[30, 2] = "Shift S34";
            TabelSLR[31, 3] = "Shift S35";
            TabelSLR[31, 8] = "Reduce 12";

            TabelSLR[32, 4] = "Reduce 13";
            TabelSLR[32, 8] = "Reduce 13";
            TabelSLR[33, 4] = "Reduce 14";
            TabelSLR[33, 8] = "Reduce 14";
            TabelSLR[34, 4] = "Reduce 17";
            TabelSLR[34, 8] = "Reduce 17";
            TabelSLR[35, 4] = "Reduce 18";
            TabelSLR[35, 8] = "Reduce 18";
            TabelSLR[36, 2] = "Reduce 19";
            TabelSLR[36, 3] = "Reduce 19";
            #endregion

            for (int i = 0; i < 37; i++)
            {
                DaftarItems[i] = new Token("Items", i.ToString());
            }

            #region Token of Non Terminals

            Subjek = new Token("Subjek");
            Predikat = new Token("Predikat");
            Produk = new Token("Produk");
            Kuantiti = new Token("Kuantiti");
            Koma = new Token("Koma");
            Jawaban = new Token("Jawaban");
            Nama = new Token("Nama");
            Nomor = new Token("Nomor");
            Entr = new Token("Entr");

            S = new Token("S");
            S1 = new Token("S1");
            S2 = new Token("S2");
            S3 = new Token("S3");
            S4 = new Token("S4");
            S5 = new Token("S5");
            S6 = new Token("S6");

            A = new Token("A");
            B = new Token("B");
            C = new Token("C");
            D = new Token("D");
            E = new Token("E");
            #endregion
        }

        public void GetToken(string input)
        {
            DaftarToken = new Token[100];
            string[] hasilsplit = input.Split(new Char[] { ' ' });

            int cnt = 0;
            for (int i = 0; i < hasilsplit.Length; i++)
            {
                if (hasilsplit[i] != "")
                {
                    DaftarToken[cnt] = NamaToken(hasilsplit[i]);
                    cnt++;
                }
            }
            
            for (int i = 0; i < DaftarToken.Count(); i++)
            {
                if (DaftarToken[i] != null)
                {
                    if (DaftarToken[i].Nama != "Error")
                    {
                        ListToken.Add(DaftarToken[i]);
                    }
                }
                else
                    i = 100;
            }
            ListToken.Add(new Token("SDolar", "$"));
        }
    
        private Token NamaToken(string teks)
        {
            Token helper = new Token();
            teks = teks.ToLower();

            if (teks == ",")
            {
                helper.Nama = "Koma";
                helper.Nilai = teks;
            }
            else if (teks == "tidak" || teks == "tdk" || teks == "ga" || teks == "gak" || teks == "g"
                || teks == "y" || teks == "ya" || teks == "k" || teks == "ok" || teks == "oke")
            {
                helper.Nama = "Jawaban";
                helper.Nilai = teks;
            }
            else if (teks == "ak" || teks == "aku" || teks == "aq" || teks == "saya" || teks == "gue" || teks == "gw"
                || teks == "gua" || teks == "sya" || teks == "sy")
            {
                helper.Nama = "Subjek";
                helper.Nilai = teks;
            }
            else if (teks == "mau" || teks == "memesan" || teks == "pesan" || teks == "pesen" || teks == "pengen" || teks == "membeli" || teks == "beli"
                || teks == "ingin")
            {
                helper.Nama = "Predikat";
                helper.Nilai = teks;
            }
            else if (teks == "kursi" || teks == "meja" || teks == "lemari")
            {
                helper.Nama = "Produk";
                helper.Nilai = teks;
            }
            else if (teks == "enter")
            {
                helper.Nama = "Entr";
                helper.Nilai = teks;
            }
            else if (urutanPertanyaan == 1 || urutanPertanyaan == 5)
            {
                if (CekKuantiti(teks) == true)
                {
                    helper.Nama = "Kuantiti";
                    helper.Nilai = teks;
                }
            }
            else if (urutanPertanyaan == 4)
            {
                if (CekNama(teks) == true)
                {
                    helper.Nama = "Nama";
                    helper.Nilai = teks;
                }
            }
            else if (urutanPertanyaan == 10)
            {
                if (CekNomor(teks) == true)
                {
                    helper.Nama = "Nomor";
                    helper.Nilai = teks;
                }
            }
            else
            {
                helper.Nama = "Error";
                helper.Nilai = teks;
            }
            return helper;
        }
        
        public bool CekNama(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= 'a' && s[i] <= 'z')
                    continue;
                else
                    return false;
            }
            return true;
        }

        public bool CekNomor(string num)
        {
            bool cek = false;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] >= '0' && num[i] <= '9' && num.Length>=10 && num.Length<=12)
                {
                    if (num.Substring(0, 1) == "0")
                    {
                           cek = true;
                    }
                    else
                    {
                           cek = false;
                    }
                }
                else
                {
                    cek = false;
                    break;
                }
            }
            return cek;
        }

        public bool CekKuantiti(string num) //input cek string output boolean
        {
            bool c = true;
            for (int ab = 0; ab<num.Length; ab++)
            {

                if (("0123456789").IndexOf(num[ab]) == -1)
                    c= false;
                //if (!(num[ab] >= 'a' && num[ab] <= 'z'))
                //{
                //    if (int.Parse(num) > 0 && int.Parse(num) <= 100)
                //        return true;
                //    else
                //        return false;
                //}
                //else
                //{
                //    return false;
                //}
            }
            return c;
        }

        public int getNomorKolom(string Nama)//input nomor output tulisan
        {
            int nomor = -1;
            if (Nama == "Subjek")
            {
                nomor = 0;
            }
            else if (Nama == "Predikat")
            {
                nomor = 1;
            }
            else if (Nama == "Produk")
            {
                nomor = 2;
            }
            else if (Nama == "Kuantiti")
            {
                nomor = 3;
            }
            else if (Nama == "Koma")
            {
                nomor = 4;
            }
            else if (Nama == "Jawaban")
            {
                nomor = 5;
            }
            else if (Nama == "Nama")
            {
                nomor = 6;
            }
            else if (Nama == "Nomor")
            {
                nomor = 7;
            }
            else if (Nama == "Entr")
            {
                nomor = 8;
            }
            else if (Nama == "SDolar")
            {
                nomor = 9;
            }
            else if (Nama == "S")
            {
                nomor = 10;
            }
            else if (Nama == "S1")
            {
                nomor = 11;
            }
            else if (Nama == "S2")
            {
                nomor = 12;
            }
            else if (Nama == "S3")
            {
                nomor = 13;
            }
            else if (Nama == "S4")
            {
                nomor = 14;
            }
            else if (Nama == "S5")
            {
                nomor = 15;
            }
            else if (Nama == "S6")
            {
                nomor = 16;
            }
            else if (Nama == "A")
            {
                nomor = 17;
            }
            else if (Nama == "B")
            {
                nomor = 18;
            }
            else if (Nama == "C")
            {
                nomor = 19;
            }
            else if (Nama == "D")
            {
                nomor = 20;
            }
            else if (Nama == "E")
            {
                nomor = 21;
            }
            else
                nomor = -1;
            return nomor;
        }

        int tampungHargaBarang = 0;

        public string getNonTermKiri(int angka)
        {
            string hasil = "";
        
            if (angka == 1 || angka == 2 || angka == 3 || angka == 4 || angka == 5 || angka == 6)
                hasil = "S";
            else if (angka == 7 || angka == 8 || angka == 10 || angka == 11)
                hasil = "S1";
            else if (angka == 9)
                hasil = "A";
            else if (angka == 12)
                hasil = "B";
            else if (angka == 17 )
                hasil = "C";
            else if (angka == 18)
                hasil = "D";
            else if (angka == 19)
                hasil = "E";
            else if (angka == 13 || angka == 14 || angka == 15 || angka == 16)
                hasil = "S2";
            else if (angka == 20)
                hasil = "S3";
            else if (angka == 21)
                hasil = "S4";
            else if (angka == 22)
                hasil = "S5";
            else if (angka == 23)
                hasil = "S6";
            return hasil;
        }

        public int getJumlahdiKanan(int angka)
        {
            int hasil = -1;
            if (angka == 1 || angka == 2 || angka == 3 || angka == 4 || angka == 5 || angka == 6 || angka == 7 || angka == 9 || angka == 10 || angka == 12 || angka == 13 || angka == 14 || angka == 17 || angka == 18 || angka == 19 )
                hasil = 2;
            else  
                hasil = 1;

            return hasil;
        }

        bool Status = false;
        int nomor = 1;
        string [] barang = new string[100];
        int [] jumlahBarang = new int[100];
        int[] hargaBarang = new int[100];

        int barangKe = 0, barangKeKoma = 0, checkKurang=0;

        public void Parsing()
        {
            Token Stok = new Token();
            Token SAksen = new Token();
            Status = false;

            StackToken.Push(DaftarItems[0]);

            Baris = int.Parse(StackToken.Peek().Nilai);
            Kolom = getNomorKolom(ListToken[0].Nama);
            string perintah = TabelSLR[Baris, Kolom];
            Token tmp;

            while (!Status) // belum di parsing
            {
                if (perintah != null)
                {
                    if (perintah[0] == 'R') //berarti Reduce X
                    {
                        int angka = int.Parse(perintah.Substring(7));
                        int jmlPop = getJumlahdiKanan(angka) * 2;
                        string NonTerminal = getNonTermKiri(angka);
                        for (int i = 0; i < jmlPop; i++)
                        {
                            StackToken.Pop();
                        }
                        int angkabaru = int.Parse(StackToken.Peek().Nilai);

                        StackToken.Push(new Token(NonTerminal, NonTerminal));
                        Baris = angkabaru;
                        Kolom = getNomorKolom(StackToken.Peek().Nama);

                        perintah = TabelSLR[Baris, Kolom];
                        int angkaGT = int.Parse(perintah.Substring(1)); //langsung Go To
                        StackToken.Push(DaftarItems[angkaGT]);
                        Baris = angkaGT;
                        Kolom = getNomorKolom(ListToken[0].Nama);
                        perintah = TabelSLR[Baris, Kolom];

                        //brrti angka itu masih angkanya reduce
                        if (angka == 0) // S' --> S
                        {
                            SAksen.Nilai = Stok.Nilai;
                        }
                        else if (angka == 1) //S --> S1 Enter
                        {
                            Stok.Nilai = S1.Nilai;
                        }
                        else if (angka == 2) //S --> S2 Enter
                        {
                            Stok.Nilai = S2.Nilai;
                        }
                        else if (angka == 3) //S --> S3 Enter
                        {
                            Stok.Nilai = S3.Nilai;
                        }
                        else if (angka == 4) //S --> S4 Enter
                        {
                            Stok.Nilai = S4.Nilai;
                        }
                        else if (angka == 5) //S --> S5 Enter
                        {
                            Stok.Nilai = S5.Nilai;
                        }
                        else if (angka == 6) //S --> S6 Enter
                        {
                            Stok.Nilai = S6.Nilai;
                        }
                        else if (angka == 7) //S1 --> Subjek A
                        {
                            S1.Nilai = A.Nilai;
                        }
                        else if (angka == 8) //S1 --> A
                        {
                            S1.Nilai = A.Nilai;
                        }
                        else if (angka == 9) //A --> Predikat S2
                        {
                            A.Nilai = S2.Nilai;
                        }/*Check ke bawah*/
                        else if (angka == 10) //S1 --> Subjek B
                        {
                            S1.Nilai = B.Nilai;
                        }
                        else if (angka == 11) //S1 --> B
                        {
                            S1.Nilai = B.Nilai;
                        }
                        else if (angka == 12) //B --> Predikat Produk
                        {
                            if (Produk.Nilai == "meja")
                            { B.Nilai = "meja"; }
                            else if (Produk.Nilai == "lemari")
                            { B.Nilai = "lemari"; }
                            else if (Produk.Nilai == "kursi")
                            { B.Nilai = "kursi"; }
                        }
                        else if (angka == 13) //S2 --> E C
                        {
                            S2.Nilai = E.Nilai + C.Nilai;
                        }
                        else if (angka == 14) //S2 --> E D
                        {
                            S2.Nilai = E.Nilai + D.Nilai;
                        }
                        else if (angka == 15) //S2 --> C
                        {
                            S2.Nilai = C.Nilai;
                        }
                        else if (angka == 16) //S2 --> D
                        {
                            S2.Nilai = D.Nilai;
                        }
                        else if (angka == 17) //C --> Kuantiti Produk
                        {
                            if (Produk.Nilai == "kursi")
                            { C.Nilai = (int.Parse(Kuantiti.Nilai) * 10000).ToString(); }
                            else if (Produk.Nilai == "meja")
                            { C.Nilai = (int.Parse(Kuantiti.Nilai) * 20000).ToString(); }
                            else if (Produk.Nilai == "lemari")
                            { C.Nilai = (int.Parse(Kuantiti.Nilai) * 30000).ToString(); }
                        }
                        else if (angka == 18) //D --> Produk Kuantiti
                        {
                            if (Produk.Nilai == "kursi")
                            { D.Nilai = (int.Parse(Kuantiti.Nilai) * 10000).ToString(); }
                            else if (Produk.Nilai == "meja")
                            { D.Nilai = (int.Parse(Kuantiti.Nilai) * 20000).ToString(); }
                            else if (Produk.Nilai == "lemari")
                            { D.Nilai = (int.Parse(Kuantiti.Nilai) * 30000).ToString(); }
                        }
                        else if (angka == 19) //E --> S2 Koma
                        {
                            E.Nilai += S2.Nilai;
                        }
                        else if (angka == 20) //S3 --> Jawaban
                        {
                            S3.Nilai = Jawaban.Nilai;
                        }
                        else if (angka == 21) //S4 --> Kuantiti
                        {
                            S4.Nilai = Kuantiti.Nilai;
                        }
                        else if (angka == 22) //S5 --> Nama
                        {
                            S5.Nilai = Nama.Nilai;
                        }
                        else if (angka == 23) //S6 --> Nomor
                        {
                            S6.Nilai = Nomor.Nilai;
                        }
                        else
                        { }

                    }
                    else if (perintah[0] == 'S') //Shift X
                    {
                        int angka = int.Parse(perintah.Substring(7));
                        tmp = ListToken[0];
                        ListToken.Remove(ListToken[0]);
                        if (tmp.Nama == "Produk")
                        {
                            Produk.Nilai = tmp.Nilai;
                            if (adaKoma == 1)
                            {
                                /**/
                                barangKeKoma = barangKe;
                                barang[barangKeKoma] = tmp.Nilai;
                                if (jumlahBarang[barangKeKoma] != 0)
                                { barangKeKoma++; }
                                barangKe = barangKeKoma;
                            }
                        }
                        else if (tmp.Nama == "Kuantiti")
                        {
                            Kuantiti.Nilai = tmp.Nilai;
                            if (adaKoma == 1)
                            {
                                barangKeKoma = barangKe;
                                jumlahBarang[barangKeKoma] += int.Parse(tmp.Nilai);
                                if (barang[barangKeKoma] != null)
                                { barangKeKoma++; }
                                barangKe = barangKeKoma;
                            }
                        }
                        else if (tmp.Nama == "Nama")
                        {
                            Nama.Nilai = tmp.Nilai;
                        }
                        else if (tmp.Nama == "Nomor")
                        {
                            Nomor.Nilai = tmp.Nilai;
                        }
                        else if (tmp.Nama == "Jawaban")
                        {
                            Jawaban.Nilai = tmp.Nilai;
                        }
                        StackToken.Push(tmp);
                        StackToken.Push(DaftarItems[angka]);

                        Baris = angka;
                        Kolom = getNomorKolom(ListToken[0].Nama);
                        perintah = TabelSLR[Baris, Kolom];
                    }
                    else if (perintah == "Accept")
                    {
                        Status = true;
                        if (Stok.Nilai != null)
                        {
                            //brrti dia input pesanan
                            if (urutanPertanyaan == 1)
                            {
                                if (Kuantiti.Nilai != null && checkKuantiti == 1)
                                {
                                    if (adaKoma == 1)
                                    {
                                        int TotalBayarKoma = 0;
                                        string produkDiBeli;//=gbung();
                                        for (int i = 0; i < barang.Count(); i++)
                                        {
                                            if (jumlahBarang[i] != 0)
                                            {
                                                if (barang[i] == "kursi")
                                                { hargaBarang[i] = jumlahBarang[i] * 10000; }
                                                else if (barang[i] == "meja")
                                                { hargaBarang[i] = jumlahBarang[i] * 20000; }
                                                else if (barang[i] == "lemari")
                                                { hargaBarang[i] = jumlahBarang[i] * 30000; }
                                                TotalBayar += hargaBarang[i];
                                                TotalBayarKoma += hargaBarang[i];
                                               // produkDiBeli += "\r\n" + (i + 1) + ". " + barang[i] + " sebanyak " + jumlahBarang[i] + " buah dengan harga Rp." + hargaBarang[i] + ",-";
                                            }
                                            else
                                            {
                                                i = 100;
                                            }
                                        }
                                        produkDiBeli = gbung();
                                        S.Nilai = "Total Pesanan Anda Rp " + TotalBayarKoma + ",- dengan perincian : " + produkDiBeli + "\r\nApakah Anda ingin memesan lagi ?";
                                        txtJawabanKafra.Text = S.Nilai;
                                        EnterChat(S.Nilai, 0);
                                        urutanPertanyaan = 2;
                                    }
                                    else
                                    {
                                        if (checkKurang == 0)
                                        {
                                            S.Nilai = "Total Pesanan Anda Rp " + Stok.Nilai + ",- dengan perincian : \r\n"+
                                                "1." + Produk.Nilai + " sebanyak " + Kuantiti.Nilai + " buah\r\nApakah Anda ingin memesan lagi ?";
                                            txtJawabanKafra.Text = S.Nilai;
                                            barang[barangKe] = Produk.Nilai;
                                            if (Kuantiti.Nilai != null)
                                            {
                                                jumlahBarang[barangKe] = int.Parse(Kuantiti.Nilai);
                                                hargaBarang[barangKe] = int.Parse(Stok.Nilai);
                                            }
                                            barangKe++;
                                        }
                                        else if (checkKurang == 1) // tidak masuk quantity
                                        {
                                            Stok.Nilai = (tampungHargaBarang * int.Parse(Kuantiti.Nilai)).ToString();
                                            S.Nilai = "Total Pesanan Anda Rp " + Stok.Nilai + ",- dengan perincian : \r\n"+
                                                "1. " + Produk.Nilai + " sebanyak " + Kuantiti.Nilai + " buah dengan harga Rp." + Stok.Nilai + ",-\r\nApakah Anda ingin memesan lagi ?";
                                            txtJawabanKafra.Text = S.Nilai;
                                            barang[barangKe] = Produk.Nilai;
                                            if (Kuantiti.Nilai != null)
                                            {
                                                jumlahBarang[barangKe] = int.Parse(Kuantiti.Nilai);
                                                hargaBarang[barangKe] = int.Parse(Stok.Nilai);
                                            }
                                            barangKe++;
                                        }

                                        if (urutanPertanyaan == 1)
                                        {
                                            TotalBayar += int.Parse(Stok.Nilai);
                                        }
                                        EnterChat(S.Nilai, 0);
                                        urutanPertanyaan = 2;
                                        checkKurang = 0;
                                    }
                                    SoundPesanLagi.Play();
                                }
                                else
                                {
                                    if (Produk.Nilai == "kursi")
                                    { tampungHargaBarang = 10000; }
                                    else if (Produk.Nilai == "meja")
                                    { tampungHargaBarang = 20000; }
                                    else if (Produk.Nilai == "lemari")
                                    { tampungHargaBarang = 30000; }
                                    checkKurang = 1;

                                    S.Nilai = "Masukkan jumlah barang yang ingin Anda pesan ?";
                                    txtJawabanKafra.Text = S.Nilai;
                                    EnterChat(S.Nilai, 0);
                                    urutanPertanyaan = 1;

                                    SoundJumlah.Play();
                                }
                            }
                            else if (urutanPertanyaan == 2)
                            {
                                if (Stok.Nilai == "ya" || Stok.Nilai == "y" || Stok.Nilai == "k" || Stok.Nilai == "ok" || Stok.Nilai == "oke")
                                {
                                    S.Nilai = "Masukkan pilihan perabotan rumah beserta jumlah yang ingin Anda beli!";
                                    txtJawabanKafra.Text = S.Nilai;
                                    EnterChat(S.Nilai, 0);
                                    urutanPertanyaan = 1;
                                    Kuantiti.Nilai = null;
                                    checkKuantiti = 0;
                                    checkProduk = 0;
                                    SoundPesan.Play();
                                }
                                else
                                {
                                    S.Nilai = "Apakah Anda setuju dengan transaksi ini?";
                                    txtJawabanKafra.Text = S.Nilai;
                                    EnterChat(S.Nilai, 0);
                                    urutanPertanyaan = 3;
                                    SoundSetuju.Play();
                                }
                            }
                            else if (urutanPertanyaan == 3)
                            {
                                if (Stok.Nilai == "ya" || Stok.Nilai == "y" || Stok.Nilai == "k" || Stok.Nilai == "ok" || Stok.Nilai == "oke")
                                {
                                    S.Nilai = "Masukkan nama Anda ?";
                                    txtJawabanKafra.Text = S.Nilai;
                                    EnterChat(S.Nilai, 0);
                                    urutanPertanyaan = 4;
                                    SoundNama.Play();
                                }
                                else
                                {
                                    S.Nilai = "Terima Kasih";
                                    txtJawabanKafra.Text = S.Nilai + "\r\n\r\n" + Welcome;
                                    EnterChat(S.Nilai, 0);
                                    txtChat.Text += "-----------------------------------------------------------------------------------------------------------------------------------------------\r\n\r\n";
                                    urutanPertanyaan = 1;
                                    EnterChat(Welcome, 0);
                                    urutanPertanyaan = 1;
                                    barangKe = 0;
                                    jumlahBarang = new int[100];
                                    hargaBarang = new int[100];
                                    barang = new string[100];
                                    jumlahProdukInput = 0;
                                    checkProduk = 0;
                                    checkKuantiti = 0;
                                    Kuantiti.Nilai = null;
                                    adaKoma = 0;
                                    SoundTerimaKasih.Play();
                                }
                            }
                            else if (urutanPertanyaan == 4)
                            {
                                S.Nilai = "Masukkan nomor Anda ? (Gunakan Kode Area atau Kode Provider)";
                                txtJawabanKafra.Text = S.Nilai;
                                EnterChat(S.Nilai, 0);
                                urutanPertanyaan = 10;
                                SoundNomor.Play();
                            }
                            else if (urutanPertanyaan == 10)
                            {
                                string produkDiBeli = gbung(); 
                                
                                S.Nilai = "Nota pesanan Anda nomor " + nomor + ", atas nama " + Nama.Nilai + ", nomor telepon " + Nomor.Nilai +
                                          ".\r\nAnda membeli " + produkDiBeli +
                                          ".\r\nTotal yang harus anda bayar sebesar Rp." + TotalBayar + ",-" +
                                          "\r\nPesanan Anda akan sampai dalam 2 hari. Terima Kasih";
                                txtJawabanKafra.Text = S.Nilai;
                                nomor++;

                                SoundSelesai.Play();

                                EnterChat(S.Nilai, 0);
                                txtChat.Text += "-----------------------------------------------------------------------------------------------------------------------------------------------\r\n\r\n";
                                EnterChat(Welcome, 0);
                                urutanPertanyaan = 1;
                                barangKe = 0;
                                jumlahBarang = new int[100];
                                hargaBarang = new int[100];
                                barang = new string[100];
                                jumlahProdukInput = 0;
                                checkProduk = 0;
                                checkKuantiti = 0;
                                Kuantiti.Nilai = null;
                                adaKoma = 0;
                            }
                        }
                        StackToken = new Stack<Token>();
                        ListToken = new List<Token>();
                    }
                }
                else
                {
                    MessageBox.Show("Perintah Null (Ada yang salah dalam parsing)", "Informasi");
                    Status = true;
                    txtInput.Clear();
                    txtInput.Focus();
                    txtChat.Clear();
                    //EnterChat(Welcome, 0);
                    //urutanPertanyaan = 1;
                    //this.Dispose();
                    //InitializeComponent();
                    //Form_Menu_Load(new object(), new EventArgs());

                    Application.Exit();
                    break;
                }
            }
            Status = false;
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            txtChat.Visible = true;
            picSilang.Visible = true;
            txtChat.BringToFront();
        }

        private void picSilang_Click(object sender, EventArgs e)
        {
            txtChat.Visible = false;
            picSilang.Visible = false;
        }

        private void btnUlang_Click(object sender, EventArgs e)
        {
            SoundWelcome.Play();

            urutanPertanyaan = 1;
            barangKe = 0;
            jumlahBarang = new int[100];
            hargaBarang = new int[100];
            barang = new string[100];
            jumlahProdukInput = 0;
            checkProduk = 0;
            checkKuantiti = 0;
            Kuantiti.Nilai = null;
            adaKoma = 0;

            txtInput.Focus();
            EnterChat(Welcome, 0);
            txtJawabanKafra.Text = "        "+Welcome;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string gbung()
        {
            brgs[0,0] ="Meja";
            brgs[0,1]="Kursi";
            brgs[0,2]="Lemari";
            brgs[1,0]="0";
            brgs[1,1]="0";
            brgs[1,2]="0";
            brgs[2,0]="0";
            brgs[2,1]="0";
            brgs[2, 2] = "0";
            string g = "";
            TotalBayar = 0;
            int cc = 0;
            for (int i = 0; i < 100; i++)// barang.Count(); i++)
            {
                if (jumlahBarang[i] != 0)
                {
                    // produkDiBeli += "\r\n" + (i + 1) + ". " + barang[i] + " sebanyak " + jumlahBarang[i] + " buah dengan harga Rp." + hargaBarang[i] + ",-";
                    TotalBayar += hargaBarang[i];
                    if (barang[i].ToLower() == "meja")
                    {
                        cc = 0;
                    }
                    else if (barang[i].ToLower() == "kursi")
                    {
                        cc = 1;
                    }
                    else cc = 2;
                    brgs[1, cc] = (int.Parse(brgs[1, cc]) + jumlahBarang[i]).ToString();
                    brgs[2, cc] = (int.Parse(brgs[2, cc]) + hargaBarang[i]).ToString();
                }
                else
                {
                    i = 100;
                }
            }
            for (int a = 0; a < 3; a++)
            {
                if (int.Parse(brgs[1, a]) != 0)
                    g += "\r\n" + (a + 1) + ". " + brgs[0, a] + " sebanyak " + brgs[1, a] + " buah dengan harga Rp." + brgs[2, a] + ",-";

            }

            return g;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                txtInput.Text = "";
            }
        }

        bool hold = false;
        int xx, yy;
        private void Form_Menu_MouseDown(object sender, MouseEventArgs e)
        {
            hold = true;
            xx = e.X;
            yy = e.Y;

        }

        private void Form_Menu_MouseUp(object sender, MouseEventArgs e)
        {
            hold = false;
        }

        int x1, y1;
        private void Form_Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (x1 != Cursor.Position.X && y1!=Cursor.Position.Y)
            {
                x1 = Cursor.Position.X;
                y1 = Cursor.Position.Y;
                if (hold)
                {
                    this.Left = x1 - xx;
                    this.Top = y1 - yy;

                }
            }

        }

        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnKirim_Click(sender, new EventArgs());
                txtInput.Text = "";

            }
        }
    }
}
