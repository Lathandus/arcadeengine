﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.EventScript;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Script action control base
	/// </summary>
	public partial class ScriptActionControlBase : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ScriptActionControlBase()
		{
			InitializeComponent();
		}




		#region Properties


		/// <summary>
		/// Action to execute
		/// </summary>
		public IScriptAction Action
		{
			get;
			protected set;
		}



		#endregion
	}
}
