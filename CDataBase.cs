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
        public static void TableWrite(String path,String table, DataGrid GridData)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+path);
            con.Open();
            OleDbDataAdapter da;
             if (table=="ДР")
                da = new OleDbDataAdapter("SELECT * From " + table + ";", con);
            else
                da = new OleDbDataAdapter("SELECT Дата, Название, Описание From " + table + ";", con);           
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
            cmd.Parameters.Add("Название", OleDbType.VarWChar, 30).Value = name;
            cmd.Parameters.Add("Описание", OleDbType.VarWChar, 30).Value = description;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
