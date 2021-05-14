using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	public abstract class BaseEntry
	{
		internal abstract void Clear(Control control);

		internal abstract void DisplayEntity(Control control, object entity);

		internal abstract void FillEntity(Control control, object entity);
	}
}
