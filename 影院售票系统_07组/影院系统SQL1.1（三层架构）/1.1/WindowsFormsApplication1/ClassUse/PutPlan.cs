﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    public class PutPlan
    {
        private String moiveName;
	    private int screenName;
	    private DateTime time;
	    public PutPlan() {

	    }
	    public PutPlan(String moiveName, int screenName, DateTime time) {

		    this.moiveName = moiveName;
		    this.screenName = screenName;
		    this.time = time;
	    }
	    public String getMoiveName() {
		    return moiveName;
	    }
	    public void setMoiveName(String moiveName) {
		    this.moiveName = moiveName;
	    }
	    public int getScreenName() {
		    return screenName;
	    }
	    public void setScreenName(int screenName) {
		    this.screenName = screenName;
	    }
	    public DateTime getTime() {
		    return time;
	    }
	    public void setTime(DateTime time) {
		    this.time = time;
	    }

	    public String toString() {
		    return "PutPlan [moiveName=" + moiveName + ", screenName=" + screenName
				    + "]";
	    }

	
	
    }
}
