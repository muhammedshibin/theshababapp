using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enumerations
{
    public class MailTemplates
    {
        public const string ForgotPassword = "<div>"
            +"<h2>Forgot Password</h2>"
            +"<p>Please Click the link below to reset your password</p>"
            +"{0}"
            +"</div>";
        public const string ConfirmEmail = "<div>" +
            "<h2>Confirm your Email</h2>" +
            "<hr/>" +
            "<p>Please confirm your email by clicking the below link</p>";

    }
}
