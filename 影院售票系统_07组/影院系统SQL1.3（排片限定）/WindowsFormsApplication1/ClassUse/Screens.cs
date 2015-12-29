using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    public class Screens
    {
        private int name;
	    private int row;
	    private int col;
	    private String image;
	    public Screens() {

	    }
	    public Screens(int name, int row, int col, String image) {

		    this.name = name;
		    this.row = row;
		    this.col = col;
		    this.image = image;
	    }
	    public int getName() {
		    return name;
	    }
	    public void setName(int name) {
		    this.name = name;
	    }
	    public int getRow() {
		    return row;
	    }
	    public void setRow(int row) {
		    this.row = row;
	    }
	    public int getCol() {
		    return col;
	    }
	    public void setCol(int col) {
		    this.col = col;
	    }
	    public String getImage() {
		    return image;
	    }
	    public void setImage(String image) {
		    this.image = image;
	    }

	    public String toString() {
		    return "Screens [name=" + name + ", row=" + row + ", col=" + col
				    + ", image=" + image + "]";
	    }

	
    }
}
