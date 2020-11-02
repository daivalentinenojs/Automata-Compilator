using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_TAK___Online_Shop_Compilator
{
    class Token
    {
        private string m_nama;
        private string m_nilai;

        public string Nama
        {
            get { return m_nama; }
            set { m_nama = value; }
        }

        public string Nilai
        {
            get { return m_nilai; }
            set { m_nilai = value; }
        }

        public Token()
        {
            Nama = "Error";
            Nilai = null;
        }

        public Token(string nama)
        {
            Nama = nama;
            Nilai = null;
        }

        public Token(string nama, string nilai)
        {
            Nama = nama;
            Nilai = nilai;
        }
    }
}
