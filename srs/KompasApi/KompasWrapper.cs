using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Services;

namespace KompasApi
{
	/// <summary>
	/// Класс построения забора в Компас 3D.
	/// </summary>
    public class KompasWrapper : IApiService
    {
	    /// <inheritdoc/>
		public double Unit => 1;

	    /// <inheritdoc/>
		public void CreateDocument()
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc/>
		public Point CreatePoint(double x, double y)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc/>
		public ISketch CreateNewSketch(int n, double offset)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc/>
		public void Extrude(ISketch sketch, double distance)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc/>
		public override string ToString()
	    {
		    return "Kompas 3D";
	    }
    }
}
