using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    public class Moive
    {
       private String Name;
	    private String type;
	    private String director;
	    private String actor;
	    private int length;
	    private float fare;
	    private String image;
	    private String story;
	    private DateTime putDate;
	    public Moive() {

	    }
	    public Moive(String name, String type, String director, String actor,
			    int length, float fare, String image, String story, DateTime putDate) {

		    Name = name;
		    this.type = type;
		    this.director = director;
		    this.actor = actor;
		    this.length = length;
		    this.fare = fare;
		    this.image = image;
		    this.story = story;
		    this.putDate = putDate;
	    }
	    public String getName() {
		    return Name;
	    }
	    public void setName(String name) {
		    Name = name;
	    }
	    public String getType() {
		    return type;
	    }
	    public void setType(String type) {
		    this.type = type;
	    }
	    public String getDirector() {
		    return director;
	    }
	    public void setDirector(String director) {
		    this.director = director;
	    }
	    public String getActor() {
		    return actor;
	    }
	    public void setActor(String actor) {
		    this.actor = actor;
	    }
	    public int getLength() {
		    return length;
	    }
	    public void setLength(int length) {
		    this.length = length;
	    }
	    public float getFare() {
		    return fare;
	    }
	    public void setFare(float fare) {
		    this.fare = fare;
	    }
	    public String getImage() {
		    return image;
	    }
	    public void setImage(String image) {
		    this.image = image;
	    }
	    public String getStory() {
		    return story;
	    }
	    public void setStory(String story) {
		    this.story = story;
	    }
	    public DateTime getPutDate() {
		    return putDate;
	    }
	    public void setPutDate(DateTime putDate) {
		    this.putDate = putDate;
	    }

	    public String toString() {
		    return "Moive [Name=" + Name + ", type=" + type + ", director="
				    + director + ", actor=" + actor + ", length=" + length
				    + ", fare=" + fare + ", image=" + image + ", story=" + story
				    + "]";
	    }
	
        
    }
}
