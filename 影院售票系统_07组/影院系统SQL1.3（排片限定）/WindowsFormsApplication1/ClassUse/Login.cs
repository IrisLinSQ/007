using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    public class Login
    {
  	    private String ID;
	    private String time;
	    public Login() {

	    }
        public Login(String iD, String time)
        {

		    ID = iD;
		    this.time = time;
	    }
	    public String getID() {
		    return ID;
	    }
	    public void setID(String iD) {
		    ID = iD;
	    }
        public String getTime()
        {
		    return time;
	    }
        public void setTime(string time)
        {
		    this.time = time;
	    }

	    public String toString() {
		    return "Login [ID=" + ID + "]";
	    }
	

    }
}
