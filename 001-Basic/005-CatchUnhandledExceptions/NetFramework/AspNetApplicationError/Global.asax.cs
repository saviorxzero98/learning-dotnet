using System;

namespace AspNetApplicationError
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            throw new Exception("Error");
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
    }
}