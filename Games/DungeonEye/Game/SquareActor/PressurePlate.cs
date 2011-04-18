﻿#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.ComponentModel;

namespace DungeonEye
{
	/// <summary>
	/// Floor switch
	/// </summary>
	public class PressurePlate : SquareActor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square handle</param>
		public PressurePlate(Square square) : base(square)
		{
			AcceptItems = true;
			CanPassThrough = true;
			IsBlocking = false;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="field"></param>
		/// <param name="position"></param>
		/// <param name="direction"></param>
		public override void Draw(SpriteBatch batch, ViewField field, ViewFieldPosition position, CardinalPoint direction)
		{
			if (Decoration == null)
				return;

			if (IsHidden)
				return;

			TileDrawing td = DisplayCoordinates.GetFloorPlate(position);
		//TODO
		//	if (td != null)
		//		batch.DrawTile(TileSet, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Pressure plate (");


			if (IsHidden)
				sb.Append("Hidden. ");

			sb.Append("Affect ");
			if (AffectTeam)
				sb.Append("Team ");
			if (AffectMonsters)
				sb.Append("monsters ");
			if (AffectItems)
				sb.Append("items ");

			sb.Append(")");
			return sb.ToString();
		}


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != Tag)
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					case "ishidden":
					{
						IsHidden = bool.Parse(node.InnerText);
					}
					break;


					case "affectitems":
					{
						AffectItems = bool.Parse(node.InnerText);
					}
					break;

					case "affectmonsters":
					{
						AffectMonsters= bool.Parse(node.InnerText);
					}
					break;

					case "affectteam":
					{
						AffectTeam = bool.Parse(node.InnerText);
					}
					break;
					default:
					{
						base.Load(node);
					}
					break;

				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement(Tag);

			base.Save(writer);

			writer.WriteElementString("ishidden", IsHidden.ToString());
			writer.WriteElementString("affectteam", AffectTeam.ToString());
			writer.WriteElementString("affectmonsters", AffectMonsters.ToString());
			writer.WriteElementString("affectitems", AffectItems.ToString());


			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Script

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool OnTeamEnter()
		{
			if (AffectTeam)
				Activate();

			return AffectTeam;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		/// <returns></returns>
		public override bool OnMonsterEnter(Monster monster)
		{
			if (AffectMonsters)
				Activate();

			return AffectMonsters;
		}


		/// <summary>
		/// Team is leaving the pressure plate
		/// </summary>
		public override bool OnTeamLeave()
		{
			if (AffectTeam)
				Deactivate();

			return AffectTeam;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		public override bool OnMonsterLeave(Monster monster)
		{
			if (AffectMonsters)
				Deactivate();

			return AffectMonsters;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public override bool OnItemCollected(Item item)
		{
			if (AffectItems)
				Deactivate();

			return AffectItems;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public override bool OnItemDropped(Item item)
		{
			if (AffectItems)
				Activate();

			return AffectItems;
		}


		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string Tag = "pressureplate";


		/// <summary>
		/// Decoration handle
		/// </summary>
		DecorationSet Decoration
		{
			get
			{
				if (Square == null)
					return null;

				return Square.Maze.Decoration;
			}
		}


		/// <summary>
		/// Is the floor plate visible
		/// </summary>
		public bool IsHidden
		{
			get;
			set;
		}


		/// <summary>
		/// Does affect monsters
		/// </summary>
		public bool AffectMonsters
		{
			get;
			set;
		}


		/// <summary>
		/// Does affect team
		/// </summary>
		public bool AffectTeam
		{
			get;
			set;
		}


		/// <summary>
		/// Does affect items
		/// </summary>
		public bool AffectItems
		{
			get;
			set;
		}

		#endregion

	}



	/// <summary>
	/// PressurePlate activation conditions
	/// </summary>
	public enum PressurcePlateCondition
	{

		/// <summary>
		/// Everything triggers the switch.
		/// </summary>
		[Description("Always")]
		Always,

		/// <summary>
		/// On team stepping on or off the switch will activate it
		/// </summary>
		[Description("On Team")]
		OnTeam,

		/// <summary>
		/// On team stepping on the switch will activate it
		/// </summary>
		[Description("On Team enter")]
		OnTeamEnter,

		/// <summary>
		/// On team stepping off the switch will activate it
		/// </summary>
		[Description("On Team leave")]
		OnTeamLeave,


		/// <summary>
		/// On monsters stepping on or off the switch will activate it
		/// </summary>
		[Description("On monster")]
		OnMonster,

		/// <summary>
		/// On monsters stepping on the switch will activate it
		/// </summary>
		[Description("On monster enter")]
		OnMonsterEnter,

		/// <summary>
		/// On monsters stepping off the switch will activate it
		/// </summary>
		[Description("On monster leave")]
		OnMonsterLeave,


		/// <summary>
		/// On item adding or removing, the switch will activate it
		/// </summary>
		[Description("On item")]
		OnItem,

		/// <summary>
		/// On item adding, the switch will activate it
		/// </summary>
		[Description("On item add")]
		OnItemAdd,

		/// <summary>
		/// On item removing, the switch will activate it
		/// </summary>
		[Description("On item remove")]
		OnItemRemove,



		/// <summary>
		/// On team or monsters stepping on or off the switch will activate it
		/// </summary>
		[Description("On entity")]
		OnEntity,

		/// <summary>
		/// On team or monsters stepping on the switch will activate it
		/// </summary>
		[Description("On entity enter")]
		OnEntityEnter,

		/// <summary>
		/// On team or monsters stepping off the switch will activate it
		/// </summary>
		[Description("On entity leave")]
		OnEntityLeave,



		/// <summary>
		/// On team, monsters or items stepping on the switch will activate it
		/// </summary>
		[Description("On anything enter")]
		OnAnythingEnter,

		/// <summary>
		/// On team, monsters or items stepping off the switch will activate it
		/// </summary>
		[Description("On anything leave")]
		OnAnythingLeave,


	}


}