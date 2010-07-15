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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;

namespace DungeonEye
{
	public class AutoMap : GameScreen
	{

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch">SpriteBatch to use</param>
        public AutoMap(SpriteBatch batch)
        {
            Batch = batch;
        }

		/// <summary>
		/// 
		/// </summary>
		public override void LoadContent()
		{
			Tileset = ResourceManager.CreateAsset<TileSet>("AutoMap");
			Tileset.Scale = new Vector2(2.0f, 2.0f);

			Font = ResourceManager.CreateSharedAsset<BitmapFont>("intro");

			Batch = new SpriteBatch();

		}


		/// <summary>
		/// Unload content
		/// </summary>
		public override void UnloadContent()
		{
			Trace.WriteDebugLine("[AutoMap] : UnloadContent()");

			Font = null;

			if (Tileset != null)
				Tileset.Dispose();
			Tileset = null;

            Batch = null;
		}


		#region Update & draw


		/// <summary>
		/// Update logic
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isCovered"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			if (Keyboard.IsNewKeyPress(Keys.Escape) || Keyboard.IsNewKeyPress(Keys.Tab))
				ExitScreen();
		}


		/// <summary>
		/// 
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Batch.Begin();

			// Background
			Batch.DrawTile(Tileset, 1, Point.Empty, Color.White);
			//Tileset.Draw(1, Point.Empty);

			// Some WIP
			//Font.DrawText(new Point(100, 100), Color.White, "TODO...");
			Batch.DrawString(Font, new Vector2(100, 100), Color.White, "TODO...");	

			// Draw the cursor or the item in the hand
			//Tileset.Draw(0, Mouse.Location);
			Batch.DrawTile(Tileset, 0, Mouse.Location, Color.White);

			Batch.End();
		}

		#endregion



		#region Properties

		/// <summary>
		/// Tileset
		/// </summary>
		TileSet Tileset;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch Batch;

		#endregion

	}
}
