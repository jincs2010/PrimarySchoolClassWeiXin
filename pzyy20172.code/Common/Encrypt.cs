﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace pzyy20172.Common
{
	//加密解密操作类
	/// <summary>
	/// 加密解密操作类
	/// </summary>
	public class Encrypt
	{
		const string KEY_64 = "pyzz2017";//注意了，是8个字符，64位
		const string IV_64 = "pyzz2017";

		//加密
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public string Encode(string data)
		{
			byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
			byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
			int i = cryptoProvider.KeySize;
			MemoryStream ms = new MemoryStream();
			CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

			StreamWriter sw = new StreamWriter(cst);
			sw.Write(data);
			sw.Flush();
			cst.FlushFinalBlock();
			sw.Flush();
			return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
		}

		//解密
		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public string Decode(string data)
		{
			byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
			byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

			byte[] byEnc;
			try
			{
				byEnc = Convert.FromBase64String(data);
			}
			catch
			{
				return null;
			}

			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
			MemoryStream ms = new MemoryStream(byEnc);
			CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
			StreamReader sr = new StreamReader(cst);
			return sr.ReadToEnd();
		}
	}
}
