using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database
{
    public class StudentSheet
    {
        private MySqlConnection _conn;
        private MySqlCommand _excuteSql;

        public StudentSheet()
        {
            var str = "server=127.0.0.1;port=3306;user=root;password=apb34eol; database=test;";
            _conn = new MySqlConnection(str);

        }

        public void CreateTable()
        {
            try
            {
                _conn.Open();
                var sqlCmd = "CREATE TABLE StudentInfo (ID INT(11),Name VARCHAR(45),Age INT(11),Subject VARCHAR(45),Score INT(11),Date DATETIME,PRIMARY KEY(ID))";
                _excuteSql = new MySqlCommand(sqlCmd, _conn);
                _excuteSql.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Add(string name, int age, string subject, int score, DateTime dt)
        {
            try
            {
                _conn.Open();
                var sqlCmd = "INSERT INTO test.studentinfo (Name,Age,Subject,Score,Date) VALUES ('" + name + "','" + age + "','" + subject + "','" + score + "','" + dt + "')";
                _excuteSql = new MySqlCommand(sqlCmd, _conn);
                _excuteSql.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int id, string name, int age, string subject, int score, DateTime dt)
        {
            try
            {
                _conn.Open();
                var sqlCmd = $"UPDATE test.studentinfo SET NAME='{name}',Age='{age}',Subject='{subject}',Score='{score}',Date='{dt}' WHERE ID={id}";
                _excuteSql = new MySqlCommand(sqlCmd, _conn);
                _excuteSql.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _conn.Open();
                var sqlCmd = $"DELETE FROM test.studentinfo WHERE ID='{id}'";
                _excuteSql = new MySqlCommand(sqlCmd, _conn);
                _excuteSql.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet Search(string name, string age, string subject, string score)
        {
            try
            {
                if (name == "" && age != "" && subject == "" && score == "")
                {
                    _conn.Open();
                    var sqlCmd = $"SELECT * FROM test.studentinfo WHERE Age='{age}'";
                    var mysqlAdatper = new MySqlDataAdapter(sqlCmd, _conn);
                    var ds = new DataSet();
                    mysqlAdatper.Fill(ds, "studentinfo1");
                    _conn.Close();
                    return ds;
                }
                else if (name != "" && age == "" && subject == "" && score == "")
                {
                    _conn.Open();
                    var sqlCmd = $"SELECT * FROM test.studentinfo WHERE Name='{name}'";
                    var mysqlAdatper = new MySqlDataAdapter(sqlCmd, _conn);
                    var ds = new DataSet();
                    mysqlAdatper.Fill(ds, "studentinfo1");
                    _conn.Close();
                    return ds;
                }
                else if (name == "" && age == "" && subject != "" && score == "")
                {
                    _conn.Open();
                    var sqlCmd = $"SELECT * FROM test.studentinfo WHERE Subject='{subject}'";
                    var mysqlAdatper = new MySqlDataAdapter(sqlCmd, _conn);
                    var ds = new DataSet();
                    mysqlAdatper.Fill(ds, "studentinfo1");
                    _conn.Close();
                    return ds;
                }
                else if (name == "" && age == "" && subject == "" && score != "")
                {
                    _conn.Open();
                    var sqlCmd = $"SELECT * FROM test.studentinfo WHERE Score='{score}'";
                    var mysqlAdatper = new MySqlDataAdapter(sqlCmd, _conn);
                    var ds = new DataSet();
                    mysqlAdatper.Fill(ds, "studentinfo1");
                    _conn.Close();
                    return ds;
                }
                else
                {
                    _conn.Open();
                    var sqlCmd = "SELECT * FROM test.studentinfo";
                    var mysqlAdatper = new MySqlDataAdapter(sqlCmd, _conn);//先放入中间容器Adapter
                    var ds = new DataSet();
                    mysqlAdatper.Fill(ds, "studentinfo1");//再把Adapter中数据灌入DataSet中
                    _conn.Close();
                    return ds;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
