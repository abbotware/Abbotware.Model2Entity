$Projects        = @{"Abbotware.Model2Entity.Nugets" = @{ 
						Name           = "Abbotware.Model2Entity.UnitTests";
						Type           = "build"; 
						Action         = "build"
						Source         = "";
						Runtime        = "";
						Framework      = "";
						Configuration  = "Release"
						}

					"Abbotware.Model2Entity.UnitTests" = @{ 
						Name           = "Abbotware.Model2Entity.UnitTests";
						Type           = "unittest"; 
						Action         = "publish";
						Source         = "Test\Abbotware.Model2Entity.UnitTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						} 
					}