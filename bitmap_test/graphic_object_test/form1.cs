using System.Drawing;
using System.Windows.Forms;

public class Test : Form
{
	private Image offscreen;
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint (e);
		e.Graphics.DrawImage(offscreen, 0, 0);
	}

	public Test() 
	{
        offscreen = new Bitmap(250, 250);
		Image image = Image.FromFile("test_image.png");
        Image buf = Image.FromFile("player.bmp");
		Graphics g = Graphics.FromImage(offscreen);
		Pen pen = new Pen(Color.Black, 10);
        g.DrawImage(image, 0, 0);
        g.DrawImage(buf, 100, 100);
	}

	static void Main() 
	{
		Application.Run(new Test());
	}
}