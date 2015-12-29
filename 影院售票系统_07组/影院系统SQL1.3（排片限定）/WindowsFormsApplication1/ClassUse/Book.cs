using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    public class Book
    {
       	private String moiveName;
	    private String seat;
	    private String consumerType;
	    private int screenName;
	    private float discount;
	    private float afterDiscount;
	    private DateTime time;
	    private DateTime saleTime;
	    public Book() {

	    }
	    public Book(String moiveName, String seat, String consumerType,
			    int screenName, float discount, float afterDiscount, DateTime time,
			    DateTime saleTime) {

		    this.moiveName = moiveName;
		    this.seat = seat;
		    this.consumerType = consumerType;
		    this.screenName = screenName;
		    this.discount = discount;
		    this.afterDiscount = afterDiscount;
		    this.time = time;
		    this.saleTime = saleTime;
	    }
	    public String getMoiveName() {
		    return moiveName;
	    }
	    public void setMoiveName(String moiveName) {
		    this.moiveName = moiveName;
	    }
	    public String getSeat() {
		    return seat;
	    }
	    public void setSeat(String seat) {
		    this.seat = seat;
	    }
	    public String getConsumerType() {
		    return consumerType;
	    }
	    public void setConsumerType(String consumerType) {
		    this.consumerType = consumerType;
	    }
	    public int getScreenName() {
		    return screenName;
	    }
	    public void setScreenName(int screenName) {
		    this.screenName = screenName;
	    }
	    public float getDiscount() {
		    return discount;
	    }
	    public void setDiscount(float discount) {
		    this.discount = discount;
	    }
	    public float getAfterDiscount() {
		    return afterDiscount;
	    }
	    public void setAfterDiscount(float afterDiscount) {
		    this.afterDiscount = afterDiscount;
	    }
	    public DateTime getTime() {
		    return time;
	    }
	    public void setTime(DateTime time) {
		    this.time = time;
	    }
	    public DateTime getSaleTime() {
		    return saleTime;
	    }
	    public void setSaleTime(DateTime saleTime) {
		    this.saleTime = saleTime;
	    }

	    public String toString() {
		    return "Book [moiveName=" + moiveName + ", seat=" + seat
				    + ", consumerType=" + consumerType + ", screenName="
				    + screenName + ", discount=" + discount + ", afterDiscount="
				    + afterDiscount + "]";
	    }
	
	
	
    }
}
