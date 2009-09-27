﻿#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcEngine.Games.DungeonEye.Interfaces
{

	/// <summary>
	/// Interface for item
	/// </summary>
	public interface IItem
	{

		/// <summary>
		/// When the team collect an item
		/// </summary>
		/// <param name="item">Item</param>
		/// <param name="team">Team</param>
		void OnCollect(Item item, Team team);


		/// <summary>
		/// When an item is dropped
		/// </summary>
		/// <param name="item">Item</param>
		/// <param name="team">Team</param>
		/// <param name="block">Block where the item is</param>
		void OnDrop(Item item, Team team, MazeBlock block);

		/// <summary>
		/// When an item is used
		/// </summary>
		/// <param name="item"></param>
		/// <param name="team"></param>
		void OnUse(Item item, Team team);
	}
}