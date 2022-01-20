using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Xml.Serialization;
using Final_OOP_PROJECT.Models;
using System.Threading.Tasks;

namespace Final_OOP_PROJECT.Controllers
{
    public class LoginExistentController : Controller
    {
        private AesCryptoServiceProvider aes= new AesCryptoServiceProvider();

        public static int id_forever;
        private IndividDBContext iDb2 = new IndividDBContext();

        // GET: LoginExistent
        public ActionResult LoginExistent()
        {
            return View();//iDb.User.ToList()
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LoginExistent(Individ obj)
        {
            string salt = "andrei";

            if (ModelState.IsValid)
            {

                string Name = Request.Form["Username"].ToString();
                string Password = Request.Form["Password"].ToString();

                if (Name != "admin")
                {
                    using (IndividDBContext rDb = new IndividDBContext())
                    {
                        string password = Password;

                        using (var sha = new System.Security.Cryptography.SHA256Managed())
                        {
                            // Convert the string to a byte array first, to be processed
                            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                            byte[] hashBytes = sha.ComputeHash(textBytes);

                            // Convert back to a string, removing the '-' that BitConverter adds
                            string hash = BitConverter
                                .ToString(hashBytes)
                                .Replace("-", String.Empty);

                            Password = hash;
                        }
                    }
                }

                    using (IndividDBContext iDb = new IndividDBContext())
                {
                    var obj_ = from a in iDb.User
                              where a.Username == Name
                              where a.Password == Password
                              select a;

                    var res = from per in iDb.User
                              where per.Username == Name
                              where per.Password == Password
                              select new
                              {
                                  per.IdUser,
                                  per.LastName,
                                  per.FirstName,
                                  per.Username,
                                  per.Password
                              };

                    foreach (var idu in res)
                    {
                        id_forever = idu.IdUser;
                        obj = iDb.User.Find(id_forever);
                    }
                    if (obj_.Any())
                    {
                        if (id_forever == 1)
                        {
                            //XmlSerializer serial = new XmlSerializer(obj.GetType());
                            //System.IO.StreamWriter w = new System.IO.StreamWriter("C: \Program Files(x86)\IIS Express\ \Users\Alex\source\repos\Final_OOP_PROJECT\Content\Files\loginchain.xml");
                            //serial.Serialize(w,"Admin logged in at: "+DateTime.Now+" \n");
                        
                            return RedirectToAction("Admin");

                            //xml file admin logged in at datetime
                            //string path = Server.MapPath("~/Content/Files");

                            //XmlSerializer serial = new XmlSerializer(obj.GetType());
                            //System.IO.StreamWriter w = new System.IO.StreamWriter(path +"\\.loginchain.xml");
                            //serial.Serialize(w, obj);
                            //w.Close();
                        
                            
                        }
                        else
                        {
                            return View();
                            //xml file user id logged in at datetime

                        }
                    }
                }

               
            }
            return View(obj);
        }

        async Task register(Individ msg)
        {
            await Task.Run(() =>
            {
                using (IndividDBContext rDb = new IndividDBContext())
                {
                    rDb.User.Add(msg);
                    rDb.SaveChanges();

                }
            });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Individ msg)
        {
            string salt = "andrei";
            if (ModelState.IsValid)
            {
                using (IndividDBContext rDb = new IndividDBContext())
                {
                    var u = new Individ{
                        Username = msg.Username,
                        Password = msg.Password
                    };

                    if (u.Username != "admin")
                    {
                        using (var sha = new System.Security.Cryptography.SHA256Managed())
                        {
                            // Convert the string to a byte array first, to be processed
                            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(u.Password + salt);
                            byte[] hashBytes = sha.ComputeHash(textBytes);

                            // Convert back to a string, removing the '-' that BitConverter adds
                            string hash = BitConverter
                                .ToString(hashBytes)
                                .Replace("-", String.Empty);

                            msg.Password = hash;
                            msg.RePassword = hash;
                        }
                    }
                    Task.Run(() => register(msg));
                    return RedirectToAction("LoginExistent");
                }
            }
            return View(msg);
        }

        public ActionResult Admin()
        {
            IndividDBContext iDb = new IndividDBContext();
            return View(iDb.User.ToList());
        }

        public ActionResult Delete(int? id)
        {
            //IndividDBContext iDb = new IndividDBContext();
            if (!id.HasValue)
                return HttpNotFound();
            Individ user = iDb2.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Console.WriteLine(id);
            return View(user);
        }

        [HttpPost]

        public ActionResult Delete(Individ user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    iDb2.Entry(user).State = EntityState.Deleted;
                    iDb2.SaveChanges();
                    //return redirecttoaction("admin");
                }
                catch (DbUpdateConcurrencyException)
                {
                    iDb2.SaveChanges();

                }
                catch (Exception e)
                {
                    throw;
                }

                //using (IndividDBContext iDb = new IndividDBContext())
                //{
                //    var res = iDb.User.SingleOrDefault(p => p.IdUser == user.IdUser);

                //    if (res != null)
                //    {
                //        Console.WriteLine("Contul sters din DATA BASE...");
                //        iDb.User.Remove(res);
                //        iDb.SaveChanges();
                //    }
                //}

            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                //return HttpNotFound();
                return RedirectToAction("Admin");

            Individ user = iDb2.User.Find(id);

            if (null == user)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Individ user)
        {
            if (ModelState.IsValid)
            {
                iDb2.Entry(user).State =
                    System.Data.Entity.EntityState.Modified;
                iDb2.SaveChanges();

                return RedirectToAction("Admin");
            }

            return View(user);
        }
    }
}
