﻿using AM.ApplicationCore.Domain;

Plane plane1 = new Plane();
plane1.PlaneId = 1;
plane1.PlaneType = PlaneType.Boing;
plane1.Capacity = 180;
plane1.ManufactureDate = new DateTime(2018, 6, 1);

Plane plane2 = new Plane(PlaneType.Airbus, 220, new DateTime(2020, 3, 15))
{
	PlaneId = 2
};

Plane plane3 = new Plane
{
	PlaneId = 3,
	PlaneType = PlaneType.Boing,
	Capacity = 160,
	ManufactureDate = new DateTime(2016, 11, 20)
};

Console.WriteLine(plane1);
Console.WriteLine(plane2);
Console.WriteLine(plane3);
