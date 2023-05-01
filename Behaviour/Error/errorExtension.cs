using System;
using OnBusyness.Model;

namespace OnBusyness.Behaviour.Error {
    public class ErrorExtension {
        public void errorLogger(string errorMsg, string innerMessage, string Stacktrace, string Source) {
            // using (var db = new ecommerceContext()) {
            //     DateTime currentDT = DateTime.Now;
            //     TblError error = new TblError ();
            //     error.ErrorMessage = errorMsg;
            //     error.StackTrace = Stacktrace;
            //     error.DateTime = currentDT;
            //     db.TblErrors.Add (error);
            //     db.SaveChanges();
            // }
        }
    }
}