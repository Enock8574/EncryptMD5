﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptMD5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string hash = "Valores";
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(textBoxValue.Text);
            using(MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using(TripleDESCryptoServiceProvider tripleDES=new TripleDESCryptoServiceProvider() { Key=keys,Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7})
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    textBoxEncrypt.Text = Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            byte[] data = Convert.FromBase64String(textBoxEncrypt.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    textBoxDecrypt.Text = UTF8Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
