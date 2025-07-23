
Light("sun", 55471164)
{
	Rotation(0.682990, 0.682990, -0.183100, -0.183100);
	Position(0.000000, 0.000000, 0.000000);
	Type(1);
	Color(0.274510, 0.305882, 0.313726);
	CastShadow();
	Static();
	PS2BlendMode(0);
	TileUV(1.000000,1.000000);
	OffsetUV(0.000000,0.000000);
}

GlobalLights()
{
	Light1("Sun");
	Light2("");
	Top(100, 100, 100);
	Bottom(100, 100, 100);
}
