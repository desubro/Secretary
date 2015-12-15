
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Data;

namespace WpfApplication1
{
    static class CDataBase
    {
        public static void OutOfDateEventDelete(String path)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
            con.Open();
            DateTime now = DateTime.Now;
            now = now.AddHours(1);
            OleDbCommand cmd = new OleDbCommand("DELETE * FROM Мероприятия WHERE Дата < DATEADD('h', -1, NOW())", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void TableWrite(String path, String table, DataGrid GridData)
        {
            OutOfDateEventDelete(path);
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
            con.Open();
            OleDbDataAdapter da;
            if (table == "ДР")
                da = new OleDbDataAdapter("SELECT * From " + table + ";", con);
            else
            da = new OleDbDataAdapter("SELECT Код, Дата, Название, Описание From " + table + " ORDER BY Дата;", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();

            da.Fill(ds, table);
            GridData.ItemsSource = ds.Tables[table].DefaultView;
            con.Close();
        }

        public static void TableWriteLimit(String path, String table, DataGrid GridData)
        {
            OutOfDateEventDelete(path);
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
            con.Open();
            OleDbDataAdapter da;
            if (table == "ДР")
                da = new OleDbDataAdapter("SELECT Имя, Фамилия, Отчество From " + table + " WHERE DATEPART('m', ДатаРождения)=DATEPART('m', NOW()) AND DATEPART('d', ДатаРождения)=DATEPART('d', NOW());", con);
            else
                da = new OleDbDataAdapter("SELECT Дата, Название, Описание From " + table + " WHERE Дата < DATEADD('h', 24, NOW()) ORDER BY Дата;", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();

            da.Fill(ds, table);
            GridData.ItemsSource = ds.Tables[table].DefaultView;
            con.Close();
        }

        public static void insertEvent(String path, String date, String name, String description)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
            con.Open();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Мероприятия (Дата, Название, Описание)" + " VALUES (?, ?, ?)", con);
            cmd.Parameters.Add("Дата", OleDbType.Date, 30).Value = date;
            cmd.Parameters.Add("Название", OleDbType.VarWChar, 25).Value = name;
            cmd.Parameters.Add("Описание", OleDbType.VarWChar, 256).Value = description;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void EventRowDelete(String path, int rowId)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
            con.Open();
            OleDbCommand cmd = new OleDbCommand("DELETE * FROM  Мероприятия WHERE Код = " + rowId, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

       
    }
}
