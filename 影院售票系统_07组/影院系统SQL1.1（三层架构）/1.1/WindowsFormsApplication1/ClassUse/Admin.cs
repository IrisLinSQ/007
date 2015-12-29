using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    public class Admin
    {
        private String ID;
	    private String password;
	    public Admin() {

	    }
	    public Admin(String iD, String password) {

		    ID = iD;
		    this.password = password;
	    }
	    public String getID() {
		    return ID;
	    }
	    public void setID(String iD) {
		    ID = iD;
	    }
	    public String getPassword() {
		    return password;
	    }
	    public void setPassword(String password) {
		    this.password = password;
	    }

	    public String toString() {
		    return "Admin [ID=" + ID + ", password=" + password + "]";
	    }
    }
}
