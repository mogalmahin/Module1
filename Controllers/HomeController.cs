using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Logout()
		{
			if (Session["id"] != null)
			{
				Session.Abandon();
				Session.Clear();
				return View("Index");
			}
			return View("Index");
		}
		[HttpGet]
		public ActionResult Edit(String id)
		{
			DataTable tb = new DataTable();
			UserDetails user = new UserDetails();
			string connection = "Server = tcp:muraliserver18.database.windows.net,1433; Initial Catalog = murali; Persist Security Info = False; User ID =murali; Password =NGTbatch2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
			SqlConnection con = new SqlConnection(connection);
			string cmdtxt = "select * from UserDetails where VAMID=@id";
			con.Open();
			SqlCommand cmd = new SqlCommand(cmdtxt, con);
			cmd.Parameters.Add(new SqlParameter("@id",id));
			SqlDataAdapter adp = new SqlDataAdapter(cmd);
			adp.Fill(tb);
			if(tb.Rows.Count == 1)
			{
				user.VAMID = tb.Rows[0][0].ToString();
				user.Name = tb.Rows[0][1].ToString();
				user.Phone_Number =Convert.ToInt32(tb.Rows[0][2].ToString());
				user.Password = tb.Rows[0][3].ToString();
				con.Close();
				return View(user);
			}
			con.Close();

			return View(user);
		}
		
		[HttpPost]
		public ActionResult Edit(UserDetails user)
		{
			if (ModelState.IsValid)
			{
				string connection = "Server = tcp:muraliserver18.database.windows.net,1433; Initial Catalog = murali; Persist Security Info = False; User ID =murali; Password =NGTbatch2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
				SqlConnection con = new SqlConnection(connection);
				con.Open();
					string cmdtxt = "update UserDetails set Name=@name,Phone_Number=@phone_no,Password=@password where VAMID=@id;";

					SqlCommand cmd = new SqlCommand(cmdtxt, con);
					cmd.Parameters.Add(new SqlParameter("@id", user.VAMID));
					cmd.Parameters.Add(new SqlParameter("@name", user.Name));
					cmd.Parameters.Add(new SqlParameter("@phone_no", user.Phone_Number));
					cmd.Parameters.Add(new SqlParameter("@password", user.Password));
					cmd.ExecuteNonQuery();
					con.Close();
				ViewBag.update = "User Updation successful";

				return View();			

			}
			else
			{
				return View("Edit", user);
			}
		}
		[HttpGet]
		public ActionResult SignUP()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult SignUP(UserDetails user)
        {
			if (ModelState.IsValid)
			{
				string connection = "Server = tcp:muraliserver18.database.windows.net,1433; Initial Catalog = murali; Persist Security Info = False; User ID =murali; Password =NGTbatch2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
				SqlConnection con = new SqlConnection(connection);
				string cmdtxt = "select count(*) from UserDetails where VAMID=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(cmdtxt, con);
				cmd.Parameters.Add(new SqlParameter("@id", user.VAMID));
				int row = (int)cmd.ExecuteScalar();
				
				if (row > 0)
				{
					con.Close();
					ViewBag.signup = "User Already Exists";
					return View();
				}
				else
				{					
					string cmdtxt2= "insert into UserDetails values(@id,@name,@phone_no,@password)";
					SqlCommand cmd1 = new SqlCommand(cmdtxt2, con);
					cmd1.Parameters.Add(new SqlParameter("@id", user.VAMID));
					cmd1.Parameters.Add(new SqlParameter("@name", user.Name));
					cmd1.Parameters.Add(new SqlParameter("@phone_no", user.Phone_Number));
					cmd1.Parameters.Add(new SqlParameter("@password", user.Password));
					cmd1.ExecuteNonQuery();
					con.Close();
					ViewBag.signups = "User Registered successful";
					return View();
				}
				
			}
			else
			{
				return View("SignUP",user);
			}
        }
        [HttpPost]
        public ActionResult Login(UserDetails user)
        {
            string connection = "Server = tcp:muraliserver18.database.windows.net,1433; Initial Catalog = murali; Persist Security Info = False; User ID = murali; Password =NGTbatch2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            SqlConnection con = new SqlConnection(connection);
            string cmdtxt = "select count(*) from UserDetails where VAMID=@id and Password=@password";
            con.Open();
            SqlCommand cmd = new SqlCommand(cmdtxt, con);
            cmd.Parameters.Add(new SqlParameter("@id", user.VAMID));
			cmd.Parameters.Add(new SqlParameter("@password", user.Password));

			int row =(int)cmd.ExecuteScalar();
            con.Close();
            if (row > 0)
            {
				Session["id"] = user.VAMID;
                return View("Index");
            }
            else
            {
				ViewBag.login = "User name or PAssword Donot Match";
				return View("Index");
            }

        }
		public ActionResult Rides()
		{

			return View();
		}

	}
}